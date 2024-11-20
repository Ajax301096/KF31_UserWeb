using KF31_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KF31_WebApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<KF31_LliM5_DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KF31DBConnectionString")));
            services.AddSession();
            services.AddAuthentication("Cookie").AddCookie("Cookie",
                       options =>
                       {
                           options.LoginPath = "/Login/Login";
                       });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //例外が発生したときの処理
                app.UseExceptionHandler("/Error/Error");
                //app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
