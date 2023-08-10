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
    public class CoursesController : ControllerBase
    {

        private readonly ServiceWrapper _services;

        public CoursesController(ServiceWrapper wrapper)
        {
            _services = wrapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseResponse>> GetCourses()
        {
            return _services.Course.GetCourses();
        }

        [HttpGet, Route("{id}")]
        public ActionResult<CourseResponse> GetCourse(int id)
        {
            var course = _services.Course.GetCourse(id);
            if (course == null) { return NotFound(); }
            return course;
        }
    }
}
