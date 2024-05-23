using School.Models.IRepository;
using School.Models.IResponse;
using School.Models.Response;

namespace School
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Adding the Sessions
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            //Adding the ConfigurationManager
            ConfigurationManager configurationManager = builder.Configuration;
            var ConnectionString= configurationManager["ApiUrl"];
            builder.Services.AddHttpClient<ICourse, CourseServices>(client=>client.BaseAddress=new Uri(ConnectionString));
            builder.Services.AddHttpClient<ICourseGrid, CourseGridServices>(client => client.BaseAddress = new Uri(ConnectionString));
            builder.Services.AddHttpClient<ITokenServices, TokenServices>(client=>client.BaseAddress=new Uri(ConnectionString));
            var app = builder.Build();
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=School}/{action=Index}/{Name?}/{id?}");
            //app.MapControllers();

            app.Run();
        }
    }
}