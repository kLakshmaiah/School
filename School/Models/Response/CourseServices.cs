using Newtonsoft.Json;
using School.Models.IRepository;
using System.Text;

namespace School.Models.IResponse
{
    public class CourseServices : ICourse
    {
        private readonly HttpClient _HttpClient;

        public CourseServices(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
        public async Task<Course> AddCourse(Course course, string token)
        {
            _HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var courseJson = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
            var response = await _HttpClient.PostAsync($"api/Course", courseJson);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<bool> DeleteCourse(int courseId, string token)
        {
            _HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _HttpClient.DeleteAsync($"api/Course/{courseId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<Course>> GetAllCourses(string token)
        {
            _HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _HttpClient.GetAsync($"api/Course");
            if (response.IsSuccessStatusCode)
            {
                // return await JsonSerializer.DeserializeAsync<IEnumerable<Course>>(await response.Content.ReadAsStreamAsync());
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Course>>(json);

            }
            return null;
        }

        public async Task<Course> GetCourseById(int courseId, string token)
        {
            _HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _HttpClient.GetAsync("api/Course/" + courseId);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        public async Task<bool> UpdateCourse(Course course, string token)
        {
            _HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var coursejson = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
            var response = await _HttpClient.PutAsync($"api/Course/", coursejson);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            else
                return false;
        }
    }
}
