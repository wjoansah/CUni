using CUni.Server.Data;

namespace CUni.Server.Repositories
{
    public interface IRepositoryWrapper
    {
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly CUniContext _context;
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;

        //public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_context);

        public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_context);

        public ICourseRepository CourseRepository => _courseRepository ??= new CourseRepository(_context);

        public RepositoryWrapper(CUniContext context) => _context = context;
    }
}
