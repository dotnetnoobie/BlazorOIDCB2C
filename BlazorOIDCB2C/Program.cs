using BlazorOIDCB2C.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorOIDCB2C
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("AzureB2C", options.ProviderOptions);
                //options.ProviderOptions.ResponseType = "code";
                //options.ProviderOptions.DefaultScopes.Add("openid"); 
            });

            builder.Services.AddScoped<OidcService>();

            await builder.Build().RunAsync();
        }
    }
}





// "MetadataUrl": "https://blazoroidcb2c.b2clogin.com/blazoroidcb2c.onmicrosoft.com/v2.0/.well-known/openid-configuration?p=B2C_1_Signin",

//options.ProviderOptions.MetadataUrl = "https://blazoroidcb2c.b2clogin.com/blazoroidcb2c.onmicrosoft.com/B2C_1_SignupAndSignin/v2.0/.well-known/openid-configuration";
//options.ProviderOptions.Authority = "https://blazoroidcb2c.b2clogin.com";
//options.ProviderOptions.ClientId = "2c4115b1-bc20-4945-abbc-213b3c22aa21";
