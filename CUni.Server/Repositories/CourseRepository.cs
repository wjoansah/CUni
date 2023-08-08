using CUni.Server.Data;
using CUni.Shared;

namespace CUni.Server.Repositories
{
    public class CourseRepository : RepositoryBase<Course>
    {
        public CourseRepository(CUniContext context) : base(context)
        {
        }
    }
}
