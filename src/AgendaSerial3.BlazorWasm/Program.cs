using AgendaSerial3.BlazorWasm.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AgendaSerial3.BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // HTTP Client
            string baseApiUrl = builder.HostEnvironment.IsDevelopment()
                ? "https://localhost:7000/"
                : "https://agenda-serial-3-app.azurewebsites.net/";

            // Local Storage
            builder.Services.AddBlazoredLocalStorage();

            // Services
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<ApiService>();

            await builder.Build().RunAsync();
        }
    }
}
