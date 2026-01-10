using Microsoft.EntityFrameworkCore;
using RPApplication.Entities.DatabaseContext;
using RPApplication.Repositories;
using RPApplication.RepositoryContracts;
using RPApplication.ServiceContracts;
using RPApplication.Services;
using RPApplication.WebAPI.Endpoints.v1;

namespace RPApplication.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerValueRepository, CustomerValueRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICustomerValueService, CustomerValueService>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("MyInMemoryDb");
            });

            builder.Services.AddEndpointsApiExplorer();

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin();
                    policyBuilder.AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Endpoints
            app.MapCustomerEndpoints();
            app.MapCustomerValueEndpoints();

            app.UseCors();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}
