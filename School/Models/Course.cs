
using School.CustomeValidations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace School.Models
{
    public class Course
    {
          
        public int CourseId { get; set; }
        
        [Required(ErrorMessage = "Course Fees is Mandatory")]
        [Display(Name = "Course Fees")]
        public decimal? CourseFees { get; set; }

        
        [Required(ErrorMessage = "Stream is Mandatory")]
        [Display(Name = "Stream")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Stream Accepts Only Alphabets... ")]

        public string? Stream { get; set; }

        [Display(Name = "Course Start Date")]
        [DataType(DataType.Date)]
        [DateMustBeAfter]
        public DateTime StartDate { get; set; } = DateTime.Today;
       
        [Display(Name = "Course End Date")]
        [DataType(DataType.Date)]
        [CouresEndDate]
        public DateTime EndDate { get; set; } = DateTime.Today;
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? UpdateBy { get; set; } 
        public bool? Active { get; set; }

        [Required(ErrorMessage = "TitleName  is Mandatory")]
        [RegularExpression("[A-Za-z ]{3,50}", ErrorMessage = "TitleName between 3 to 50 Alphabets Only..")]
        [Display(Name = "TitleName")]
        public string? TitleName { get; set; }
        [Required(ErrorMessage = "CouresType  is Mandatory")]
        public string? CouresType { get; set; }
    }

}
