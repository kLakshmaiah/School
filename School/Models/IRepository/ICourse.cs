namespace School.Models.IRepository
{
    public interface ICourse
    {
         Task<IEnumerable<Course>> GetAllCourses(string token);
         Task<Course> GetCourseById(int courseId, string token);
        Task<Course> AddCourse(Course course, string token);
        Task<bool> UpdateCourse(Course course, string token);

        Task<bool> DeleteCourse(int courseId, string token);
    }
}
