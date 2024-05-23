using Newtonsoft.Json;
using School.Models.IRepository;
using SchoolApi.Model;
using System.Text;

namespace School.Models.Response
{
    public class CourseGridServices : ICourseGrid
    {
        private readonly HttpClient _HttpClient;

        public CourseGridServices(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
         async Task<IEnumerable<CourseGridReturn>> ICourseGrid.GetCourseGridDetaisl(RecordsSearch search)
        {
           
            var gridsearchjson = new StringContent(JsonConvert.SerializeObject(search), Encoding.UTF8, "application/json");
            var response =await _HttpClient.PostAsync("/api/CourseGrid",gridsearchjson);
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<CourseGridReturn>>(await response.Content.ReadAsStringAsync());
            }else
            {
                return null;
            }
        }
    }
}
