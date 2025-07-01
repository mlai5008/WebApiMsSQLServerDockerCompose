using Microsoft.EntityFrameworkCore;
using WebApiApp.Data;
using WebApiApp.Models;

namespace WebApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<PatientDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("WebApiAppContext") ?? throw new InvalidOperationException("Connection string 'WebApiAppContext' not found."),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // for (localdb)\\MSSQLLocalDB
            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<PatientDbContext>();                
            //    context.Database.Migrate();
            //    if (!context.Patient.Any())
            //    {
            //        context.Patient.AddRange(
            //            new Patient() { Active = true, BirthDate = DateTime.Now.Date.AddDays(-30), Family = "Taylor", Gender = Gender.Male, Use = "Official" },
            //            new Patient() { Active = true, BirthDate = DateTime.Now.Date.AddDays(-25), Family = "Schneider", Gender = Gender.Male, Use = "Official" },
            //            new Patient() { Active = true, BirthDate = DateTime.Now.Date.AddDays(-17), Family = "Kleermaker", Gender = Gender.Male, Use = "Official" }
            //        );
            //        context.SaveChanges();
            //    }
            //}

            app.MapControllers();

            app.Run();
        }
    }
}
