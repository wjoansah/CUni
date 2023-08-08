using AutoMapper;
using CUni.Server.Repositories;
using CUni.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CUni.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {

        private readonly CourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(CourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseResponse>> GetAllCourses()
        {
            var courses = _courseRepository.GetAll();
            return _mapper.Map<IEnumerable<CourseResponse>>(courses);
        }
    }
}
