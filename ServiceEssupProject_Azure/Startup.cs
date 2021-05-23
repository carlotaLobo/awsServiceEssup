
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceEssupProject_Azure.Data;
using ServiceEssupProject_Azure.Helpers;
using ServiceEssupProject_Azure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure
{
    public class Startup 
    {
        IConfiguration Config;
        public Startup(IConfiguration config)
        {
            this.Config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(
                options =>
                {
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                }).AddCookie();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(name: "v2", new OpenApiInfo {

                        Title = "Api Essup",
                        Version = "v2",
                        Description="Api para acceder a los metodos de la app Essup"
                    });
                }
                );

            //para el controll de formularios mediante keys
            services.AddAntiforgery();
            services.AddMemoryCache();
            String cadena = this.Config.GetConnectionString("awsmysql");
            //indicamos el contexto y el acceso a la base de datos
            services.AddDbContextPool<Context>(options => options.UseMySql(cadena, ServerVersion.AutoDetect(cadena))); services.AddSingleton<IConfiguration>(this.Config);
            services.AddTransient<IRepositoryEssup, RepositoryEssup>();
            //acceso a Geojson
            //acceso a rutas del servidor
            services.AddSingleton<PathService>();
            services.AddSingleton<UploadLocalFile>();
            services.AddSingleton<EmailService>();

            //Token configuration
            services.AddTransient<HelperToken>();
            HelperToken token = new HelperToken(this.Config);
            services.AddAuthentication(token.GetAuthenticationOptions()).AddJwtBearer(token.GetJwtBearerOptions());

            //sesion
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {    
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            } );

            services.AddControllersWithViews(options => options.EnableEndpointRouting = false); 
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            //sesion
            app.UseSession();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint(url: "/swagger/v2/swagger.json", name: "api v2");
                    options.RoutePrefix = "";
                }
                );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
