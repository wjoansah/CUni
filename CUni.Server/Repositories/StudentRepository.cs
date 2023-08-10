using CUni.Server.Data;
using CUni.Shared;

namespace CUni.Server.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
    }
      
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(CUniContext context) : base(context)
        {
        }
    }
}
