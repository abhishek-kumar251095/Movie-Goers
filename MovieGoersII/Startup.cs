using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieGoersII.Handlers;
using MovieGoersII.Handlers.MoviesHandler;
using MovieGoersII.Handlers.UserCollectionHandler;
using MovieGoersIIDAL;
using MovieGoersIIDAL.Services;
using MovieGoersIIDAL.Services.Repositories;

namespace MovieGoersII
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

            services.AddDbContext<ApplicationDBContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));
            });

            services.AddScoped<UserRepository>();
            services.AddScoped<UserCollectionRepository>();
            services.AddScoped<MoviesRepository>();

            services.AddHttpClient("moviegoersWebApi", options =>
            {
                options.BaseAddress = new Uri("https://localhost:44357/");
            });

            services.AddHttpClient("tmdbApi", options =>
            {
                options.BaseAddress = new Uri(" https://api.themoviedb.org/3/");
            });

            services.AddScoped<ITmdbHandler, TmdbHandler>();
            services.AddScoped<IUserCollectionHandler, UserCollectionHandler>();
            services.AddScoped<IMoviesHandler, MoviesHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Search}/{action=SearchMovies}/{id?}");
            });
        }
    }
}
