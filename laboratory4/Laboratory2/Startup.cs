using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Laboratory2.Helpers;
using Laboratory2.Handlers;
using Laboratory2.Data;
using Microsoft.EntityFrameworkCore;
using Laboratory2.Models;
using Laboratory2.Services.Repositories;
using Laboratory2.Services;
using Laboratory2;
using Microsoft.IdentityModel.Tokens;

namespace Laboratory2
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<AccountContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IRepository<User>, UsersRepositoryService>();
            services.AddScoped<IRepository<Post>, PostsRepositoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthentication("JwtBearerAuthentication")
                .AddScheme<JwtBearerAuthenticationOptions, JwtBearerAuthenticationHandler>("JwtBearerAuthentication", options =>
                {
                    options.JwtKey = Configuration["AppSettings:JwtKey"];
                    options.JwtIssuer = Configuration["AppSettings:JwtIssuer"];
                });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}