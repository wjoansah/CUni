using AutoMapper;
using CUni.Server.Repositories;
using CUni.Shared;
using Microsoft.EntityFrameworkCore;

namespace CUni.Server.Services
{
    public interface ICoursesService
    {
        IEnumerable<CourseResponse> GetCourses();
        CourseResponse GetCourse(int id);
    }

    public class CoursesService : ICoursesService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesService(RepositoryWrapper wrapper, IMapper mapper)
        {
            _mapper = mapper;
            _courseRepository = wrapper.CourseRepository;
        }

        public CourseResponse? GetCourse(int id)
        {
            var course = _courseRepository.FindByCondition((c) => c.Id == id)
                .Include(c => c.Instructors)
                .ThenInclude(i => i.OfficeAssignment)
                .FirstOrDefault();
            if (course == null) return null;

            return _mapper.Map<CourseResponse>(course);
        }

        public IEnumerable<CourseResponse> GetCourses()
        {
            var courses = _courseRepository.GetAll().Include((c) => c.Instructors).ThenInclude((i) => i.OfficeAssignment);
            return _mapper.Map<IEnumerable<CourseResponse>>(courses);
        }
    }
}
