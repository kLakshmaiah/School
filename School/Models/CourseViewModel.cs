
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using SchoolApi.Model;

namespace School.Models
{
    public class CourseViewModel
    {
        //[ValidateComplexType]
        public Course AddCourse { get; set; }=new Course();
        public IEnumerable<Course> ListCourses { get; set; }=new List<Course>();
        public IEnumerable<CourseGridReturn> CourseGridData { get; set; } =new List<CourseGridReturn>();
        public Pagging Pagging { get; set; }=new Pagging();
        public string? ViewName { get;set; }
    }
}
