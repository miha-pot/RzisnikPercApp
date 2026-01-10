using Microsoft.EntityFrameworkCore;
using RPApplication.Entities.Models;
using System.Text.Json;

namespace RPApplication.Entities.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerValue> CustomerValues { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasKey(x => x.ExternalCode);

            modelBuilder.Entity<CustomerValue>(entity =>
            {
                entity.HasKey(x => new { x.Reg1Value, x.RegDate });
                entity.HasOne<Customer>()
                      .WithMany(x => x.Values)
                      .HasForeignKey(x => x.CustomerCode);
            });

            string filePath = Path.Combine(AppContext.BaseDirectory,
                                           "Data",
                                           "CustomerData.json");
            string? jsonData = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(jsonData,
                                                                                   options);

            List<CustomerValue> allValues = [];

            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    if (customer != null)
                    {
                        foreach (var customerValue in customer.Values)
                        {
                            customerValue.CustomerCode = customer.ExternalCode;
                            allValues.Add(customerValue);
                        }
                    }
                }

                var customersToSeed = customers.Select(x => new Customer()
                {
                    ExternalCode = x.ExternalCode,
                    Name = x.Name,
                    MpCode = x.MpCode,
                    Street = x.Street,
                    SerialNo = x.SerialNo,
                    Values = []
                }).ToList();

                modelBuilder.Entity<Customer>().HasData(customersToSeed);

                modelBuilder.Entity<CustomerValue>().HasData(allValues);
            }
        }
    }
}
