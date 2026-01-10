using RPApplication.WebGUI.Components;
using RPApplication.WebGUI.ServiceContracts;
using RPApplication.WebGUI.Services;

namespace RPApplication.WebGUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient("MyAPI", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5071/api/v1/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTypedClient<ICustomerService, CustomerService>()
              .AddTypedClient<ICustomerValueService, CustomerValueService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
