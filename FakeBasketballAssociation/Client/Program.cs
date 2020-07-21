using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FakeBasketballAssociation.Client.Repository;

namespace FakeBasketballAssociation.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("FakeBasketballAssociation.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Supply HttpClient instances that include access tokens when making requests to the server project
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("FakeBasketballAssociation.ServerAPI"));
            services.AddApiAuthorization();

            // new
            services.AddOptions();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
        }
    }
}
