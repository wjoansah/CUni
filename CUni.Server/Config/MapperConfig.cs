using AutoMapper;
using CUni.Shared;

namespace CUni.Server.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {

            CreateMap<Student, CreateStudentData>().ReverseMap();
            CreateMap<Student, UpdateStudentData>().ReverseMap();
            CreateMap<Student, StudentResponse>().ReverseMap();

            CreateMap<Enrollment, EnrollmentResponse>().ReverseMap();
            CreateMap<Enrollment, CreateEnrollmentRequest>().ReverseMap();

            CreateMap<Course, CourseResponse>().ReverseMap();
        }
    }
}
