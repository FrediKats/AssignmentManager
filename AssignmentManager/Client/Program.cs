

using System;
using System.Collections.Generic;
using System.Net.Http;

using System.Threading.Tasks;
using AssignmentManager.Client.Pages;
using AssignmentManager.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace AssignmentManager.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("AssignmentManager.ServerAPI",
                    client =>
                    {
                        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                    })
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            
                
            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("AssignmentManager.ServerAPI"));
            builder.Services.AddMudServices();
            builder.Services.AddApiAuthorization();
            await builder.Build().RunAsync();
        }
    }
}