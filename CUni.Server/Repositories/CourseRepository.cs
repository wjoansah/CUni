using CUni.Server.Data;
using CUni.Shared;

namespace CUni.Server.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
    }

    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(CUniContext context) : base(context)
        {
        }
    }
}
