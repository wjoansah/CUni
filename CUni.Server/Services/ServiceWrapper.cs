using AutoMapper;
using CUni.Server.Repositories;

namespace CUni.Server.Services
{
    public interface IServiceWrapper
    {
        IStudentService Student { get; }
    }
    public class ServiceWrapper : IServiceWrapper
    {

        private readonly RepositoryWrapper _wrapper;
        private readonly IMapper _mapper;

        private IStudentService student;
        private ICoursesService course;

        public ServiceWrapper(RepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _wrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public IStudentService Student => student ??= new StudentsService(_wrapper, _mapper) ;
        public ICoursesService Course => course ??= new CoursesService(_wrapper, _mapper);
    }
}
