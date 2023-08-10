using AutoMapper;
using CUni.Server.Repositories;
using CUni.Shared;
using Microsoft.EntityFrameworkCore;

namespace CUni.Server.Services
{
    public interface IStudentService
    {
        IEnumerable<StudentResponse> GetStudents(string sortOrder, string searchString);
        StudentResponse? GetStudent(int id);
        Task<StudentResponse> CreateStudent(CreateStudentData studentRequest);
        Task<StudentResponse> UpdateStudent(int id, UpdateStudentData studentUpdates);
        Task<bool> DeleteStudent(int id);

        Task<List<EnrollmentDateGroup>> GetEnrollmentDateStats();
    }

    public class StudentsService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        public StudentsService(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _mapper = mapper;
            _studentRepository = wrapper.StudentRepository;
        }

        public async Task<StudentResponse> CreateStudent(CreateStudentData studentRequest)
        {
            var student = _mapper.Map<Student>(studentRequest);
            _studentRepository.Create(student);
            await _studentRepository.Save();
            return _mapper.Map<StudentResponse>(student);
        }

        public IEnumerable<StudentResponse> GetStudents(string sortOrder = "", string searchString = "")
        {
            //var students = _studentRepository.GetAll();
            //return _mapper.Map<IEnumerable<StudentResponse>>(students);

            string nameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            string dateSort = sortOrder == "Date" ? "date_desc" : "Date";
            string currentFilter = searchString;

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

        public StudentResponse? GetStudent(int id)
        {
            var student = _studentRepository.FindByCondition((x) => x.Id == id).Include(s => s.Enrollments).FirstOrDefault();
            if (student == null) { return null; }
            return _mapper.Map<StudentResponse>(student);
        }


        public async Task<StudentResponse> UpdateStudent(int id, UpdateStudentData studentUpdates)
        {

            var student = _studentRepository.FindByCondition((x) => x.Id == id).FirstOrDefault();
            if (student == null) { return null; }

            _mapper.Map(studentUpdates, student);

            _studentRepository.Update(student);
            await _studentRepository.Save();

            var patched = _studentRepository.FindByCondition((x) => x.Id == student.Id).FirstOrDefault();

            return _mapper.Map<StudentResponse>(patched);
        }

        public async Task<List<EnrollmentDateGroup>> GetEnrollmentDateStats()
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

        public async Task<bool> DeleteStudent(int id)
        {

            var student = _studentRepository.FindByCondition((s) => s.Id == id).FirstOrDefault();
            if (student == null) return false;

            _studentRepository.Delete(student);
            await _studentRepository.Save();

            return true;
        }
    }
}
