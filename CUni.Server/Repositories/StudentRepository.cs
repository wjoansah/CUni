using CUni.Server.Data;
using CUni.Shared;
using Microsoft.EntityFrameworkCore;

namespace CUni.Server.Repositories
{
    public class StudentRepository : RepositoryBase<Student>
    {
        public StudentRepository(CUniContext context) : base(context)
        {
        }
    }
}
