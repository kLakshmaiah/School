using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.Models.IRepository;
using SchoolApi.Model;

namespace School.Controllers
{

    public class SchoolController : Controller
    {

        ICourse CourseDeatils { get; set; }
        private readonly ICourseGrid CourseGrid;
        CourseViewModel obj = new CourseViewModel();
        RecordsSearch search { get; set; } = new RecordsSearch();
        ITokenServices _TokenServices { get; set; }

        Token _Token { get; set; }=new Token();
        public SchoolController(ICourse courseDeatils, ICourseGrid courseGrid,ITokenServices tokenServices)
        {
            CourseDeatils = courseDeatils;
            CourseGrid = courseGrid;
            _TokenServices = tokenServices;
           
        }

       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
         var details = new Login { Username = "Laxman", Password = "Laxman@123" };
         Token tokens=  await _TokenServices.GetTokens(details);
         HttpContext.Session.SetString("AccessToken", tokens.AccessToken);
         HttpContext.Session.SetString("RefreshToken", tokens.RefreshToken);
         return View();
        }

        public IActionResult AssignmentsPerCourse()
        {
            return View();
        }


        public IActionResult AssignmentsPerStudentPerCourse() { return View(); }

        public IActionResult StudentsPerCourse() { return View(); }

        public IActionResult TrainersPerCourse() { return View(); }



        #region READING ALL THE COURSE DATA
        [NonAction]
        async Task<IEnumerable<CourseGridReturn>> GetCourses(RecordsSearch search )
        {
            if (search == null)
            {

                return await CourseGrid.GetCourseGridDetaisl(new RecordsSearch { PageSize = 10,PageNumber=1 }) ;
            }
            return await CourseGrid.GetCourseGridDetaisl(search);
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> Course(string? Name, int id)
        {

            obj.CourseGridData = await GetCourses(null);
            var totalpages = (int)Math.Ceiling((decimal)obj.CourseGridData.Count() / (decimal)2);
            obj.Pagging.PageSize = 2;
            obj.Pagging.PageNumber = 1;
            var actionName = "Course";
            if (!string.IsNullOrEmpty(Name) && Name == "page")
            {
                //search.PageNumber = id;//Record Search Values
                //obj.Pagging.PageSize = 2;
                obj.Pagging.PageNumber = id;
                var newtotalpages = (int)Math.Ceiling((decimal)obj.CourseGridData.Count() / (decimal)(obj.Pagging.PageSize));
                obj.CourseGridData = obj.CourseGridData.Skip((int)((obj.Pagging.PageNumber - 1) * (obj.Pagging.PageSize))).Take((int)((obj.Pagging.PageNumber) * (obj.Pagging.PageSize))).ToList();
                obj.Pagging = new Pagging(newtotalpages, actionName, id);
                return View(obj);
            }
            obj.CourseGridData = obj.CourseGridData.Skip((int)((obj.Pagging.PageNumber - 1) * (obj.Pagging.PageSize))).Take((int)((obj.Pagging.PageNumber) * (obj.Pagging.PageSize))).ToList();

            //Getting Session Values
            _Token.AccessToken = HttpContext.Session.GetString("AccessToken");
            _Token.RefreshToken = HttpContext.Session.GetString("RefreshToken");
            if (!string.IsNullOrEmpty(Name) && Name == "view")//for view
            {
                obj.ViewName = Name;
                obj.AddCourse = await CourseDeatils.GetCourseById(id, _Token.AccessToken);
                obj.Pagging = new Pagging(totalpages, actionName,1);
                return View(obj);
            }
            else if (!string.IsNullOrEmpty(Name) && Name == "edit")//for edit
            {
                obj.ViewName = Name;
                obj.AddCourse = await CourseDeatils.GetCourseById(id, _Token.AccessToken);
                obj.Pagging = new Pagging(totalpages, actionName,1);
                return View(obj);
            }
            else if (!string.IsNullOrEmpty(Name) && Name == "delete")//for delete
            {
                obj.ViewName = Name;
                bool status = await CourseDeatils.DeleteCourse(id, _Token.AccessToken);
                if (status)
                {
                    obj.CourseGridData = await GetCourses(search);
                    obj.Pagging = new Pagging(totalpages, actionName,1);
                    return View(obj);
                }
                obj.Pagging = new Pagging(totalpages, actionName,1);
                return View(obj);
            }
            else
            {
                obj.Pagging = new Pagging(totalpages, actionName,1);
                return View(obj);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewModel course)
        {

            if (ModelState.IsValid)
            {
                course.AddCourse.Active = true;
                course.AddCourse.CreatedBy = 1;
                course.AddCourse.CreatedOn = DateTime.Now;
                if (course.AddCourse.CourseId > 0)
                {
                    course.AddCourse.UpdateBy = 1;
                    course.AddCourse.UpdateOn = DateTime.Now;
                    bool status = await CourseDeatils.UpdateCourse(course.AddCourse, _Token.AccessToken);
                    if (status)
                    {
                        return RedirectToAction("Course");
                    }
                    else { return RedirectToAction("Course", course); }
                }
                else
                {
                    Course response = await CourseDeatils.AddCourse(course.AddCourse, _Token.AccessToken);
                    if (response != null)
                    {
                        course.AddCourse.Active = true;
                        course.AddCourse.CreatedBy = 1;
                        course.AddCourse.CreatedOn = DateTime.Now;
                        return RedirectToAction("Course");
                    }
                }

            }
            course.CourseGridData = await GetCourses(null);
            return View("Course", obj);
        }



        //[HttpGet]
        //public ViewResult PageNation(string? Name,int id)
        //{

        //    pagging.TotalPages = 5;
        //    pagging.PageSize = 10;
        //    if (id == 0)
        //    {

        //    }
        //    else
        //    {
        //         TempData["id"]=id;
        //    }
        //    return View("_PageNation", pagging);
        //}


    }
}
