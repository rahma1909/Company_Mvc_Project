using Company_BLL.Interfaces;
using Company_BLL.Repositories;
using Company_DAL.Data.Contexts;
using Company_DAL.Data.Models;
using Company_PL.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Company_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();//register  built in services mvc
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
);
            });
            builder.Services.AddAutoMapper(m=>m.AddProfile(new EmployeeProfile()));
           
            var app = builder.Build();

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


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
