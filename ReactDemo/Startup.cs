using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using DBRepository.Interfaces;
using DBRepository.Factories;
using DBRepository.Repositories;

namespace ReactDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ���� ����� ���������� ������ ����������. ����������� ���� ����� ��� ���������� ����� � ���������.
        public void ConfigureServices(IServiceCollection services)
        {
            //��� ������ � �� � Repository
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>(); // 1
            services.AddScoped<IBlogRepository>(provider => new BlogRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>())); // 2


            services.AddControllersWithViews(); // ����������� �����������

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact(); // ����������� ���������� �����

            // Make sure a JS engine is registered, or you will get an error!
            //���������, ��� JS-������ ���������������, ����� �� �������� ������!
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
              .AddV8();
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

           // app.UseMvc();
            app.UseHttpsRedirection();
            app.UseStaticFiles(); //����������� ����������� ������ 

            app.UseRouting();

            app.UseAuthorization();

           

        // ���������������� ReactJS.NET. ������ ����, ����� ��� ����������� �����.
        app.UseReact(config =>
            {
                // If you want to use server-side rendering of React components,
                // add all the necessary JavaScript files here. This includes
                // your components as well as all of their dependencies.
                // See http://reactjs.net/ for more information. Example:
                //config
                //  .AddScript("~/js/First.jsx")
                //  .AddScript("~/js/Second.jsx");

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //  .SetLoadBabel(false)
                //  .AddScriptWithoutTransform("~/js/bundle.server.js");
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
