using AutoMapper;
using CUni.Server.Repositories;
using CUni.Server.Services;
using CUni.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CUni.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ServiceWrapper _services;

        public StudentsController(ServiceWrapper serviceWrapper)
        {
            _services = serviceWrapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentResponse>> GetAllStudents([FromQuery] string? sortOrder,
            [FromQuery] string? searchString)
        {
            return _services.Student.GetStudents(sortOrder, searchString);
        }

        [HttpGet, Route("stats")]
        public async Task<List<EnrollmentDateGroup>> GetEnrollmentStats()
        {
            return await _services.Student.GetEnrollmentDateStats();
        }

        [HttpGet, Route("{id}")]
        public ActionResult<StudentResponse> GetStudentById(int id)
        {
            var student = _services.Student.GetStudent(id);
            if (student == null) { return NotFound(); };
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateStudentData studentRequest)
        {
            var student = await _services.Student.CreateStudent(studentRequest);
            return CreatedAtAction(nameof(GetStudentById), new { student.Id }, student);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Put(int id, UpdateStudentData studentUpdates)
        {
            var student = await _services.Student.UpdateStudent(id, studentUpdates);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _services.Student.DeleteStudent(id);
            return NoContent();
        }
    }
}
