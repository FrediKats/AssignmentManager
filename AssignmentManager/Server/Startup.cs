using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AssignmentManager.Server.Data;

using AssignmentManager.Server.Middleware;
using AssignmentManager.Server.Models;
using AssignmentManager.Server.Persistence.Contexts;
using AssignmentManager.Server.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.OpenApi.Models;

namespace AssignmentManager.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("in-memory-db"));
          
            /*services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));*/

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            /*services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, AppDbContext>();*/

            
            services.AddScoped<IProfileService, ProfileService>();
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, AppDbContext>(options => {
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
                })
                .AddProfileService<ProfileService>();

// Need to do this as it maps "role" to ClaimTypes.Role and causes issues
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
            
            services.AddAuthentication()
                .AddIdentityServerJwt();

            
            services.AddScoped<ISpecialityService, SpecialityService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IInstructorSubjectService, InstructorSubjectService>();
            services.AddMemoryCache();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AssigmentManager API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            //context.Database.Migrate();
            
            /*app.UseMiddleware<ResponseFormatterMiddleware>();*/
            app.UseMiddleware<CustomErrorHandlerMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            
            context.Database.EnsureCreated();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            DataSeeder.SeedUsers(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}