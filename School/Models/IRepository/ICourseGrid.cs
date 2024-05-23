using SchoolApi.Model;

namespace School.Models.IRepository
{
    public interface ICourseGrid
    {
        Task<IEnumerable<CourseGridReturn>> GetCourseGridDetaisl(RecordsSearch search);
    }
}