using AutoMapper;
using CUni.Server.Repositories;
using CUni.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CUni.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(StudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentResponse>> GetAllStudents([FromQuery] string? sortOrder,
            [FromQuery] string? searchString)
        {
            string nameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            string dateSort = sortOrder == "Date" ? "date_desc" : "Date";
            string currentFilter = searchString ?? "";

            var students = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(currentFilter))
            {
                students = students.Where(s => s.LastName.Contains(currentFilter)
                || s.FirstMidName.Contains(currentFilter));
            }

            students = sortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.LastName),
                "Date" => students.OrderBy(s => s.EnrollmentDate),
                "date_desc" => students.OrderByDescending(s => s.EnrollmentDate),
                _ => students.OrderBy((x) => x.LastName),
            };

            students = students.Include(s => s.Enrollments);

            return _mapper.Map<IEnumerable<StudentResponse>>(students);
        }

        [HttpGet, Route("stats")]
        public async Task<List<EnrollmentDateGroup>> GetEnrollmentStats()
        {
            IQueryable<EnrollmentDateGroup> data =
                            from student in _studentRepository.GetAll()
                            group student by student.EnrollmentDate into dateGroup
                            select new EnrollmentDateGroup()
                            {
                                EnrollmentDate = dateGroup.Key,
                                StudentCount = dateGroup.Count()
                            };

            return await data.AsNoTracking().ToListAsync();
        }

        [HttpGet, Route("{id}")]
        public ActionResult<StudentResponse> GetStudentById(int id)
        {
            var student = _studentRepository.FindByCondition((x) => x.Id == id).Include((x) => x.Enrollments).FirstOrDefault();
            if (student == null) { return NotFound(); }

            return Ok(_mapper.Map<StudentResponse>(student));
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateStudentData studentRequest)
        {
            var student = _mapper.Map<Student>(studentRequest);
            _studentRepository.Create(student);
            await _studentRepository.Save();
            var response = _mapper.Map<StudentResponse>(student);

            return CreatedAtAction(nameof(GetStudentById), new { student.Id }, response);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Put(int id, UpdateStudentData studentUpdates)
        {
            var student = _studentRepository.FindByCondition((x) => x.Id == id).FirstOrDefault();
            if (student == null) { return NotFound(); }

            _mapper.Map(studentUpdates, student);

            _studentRepository.Update(student);
            await _studentRepository.Save();

            var patched = _studentRepository.FindByCondition((x) => x.Id == student.Id).FirstOrDefault();

            return Ok(_mapper.Map<StudentResponse>(patched));
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var student = _studentRepository.FindByCondition((s) => s.Id == id).FirstOrDefault();
            if (student == null) return NotFound();

            _studentRepository.Delete(student);
            await _studentRepository.Save();

            return NoContent();
        }
    }
}
