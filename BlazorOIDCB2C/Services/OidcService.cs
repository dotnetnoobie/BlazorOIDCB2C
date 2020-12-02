using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace BlazorOIDCB2C.Services
{
    public class OidcService
    {
        private IConfiguration configuration;
        private IWebAssemblyHostEnvironment environment;
        private NavigationManager navigation;

        public OidcService(IConfiguration configuration, IWebAssemblyHostEnvironment environment, NavigationManager navigation)
        {
            this.configuration = configuration.GetSection("AzureB2C");
            this.environment = environment;
            this.navigation = navigation;
        }

        public void SigninAndSignup() => GreateUrl(configuration["SignInAndSignUp"], "authentication/login-callback");

        public void Signin() => GreateUrl(configuration["SignIn"], "authentication/login-callback");

        public void Signup() => GreateUrl(configuration["SignUp"], "");

        public void PasswordReset() => GreateUrl(configuration["PasswordReset"], "");

        public void ProfileEditing() => GreateUrl(configuration["ProfileEditing"], "");

        private void GreateUrl(string userFlow, string redirect)
        {
            Console.WriteLine(userFlow);

            //var clientId = configuration["ClientId"];
            //var domain = configuration["Domain"];

            //var baseUrl = $"https://{domain}.b2clogin.com/{domain}.onmicrosoft.com/oauth2/v2.0/authorize";
            //redirect = $"{environment.BaseAddress}{redirect}";

            // var url = $"{baseUrl}?client_id={clientId}&redirect_uri={redirect}&response_mode=query&response_type={configuration["response_type"]}&scope=openid&nonce={Guid.NewGuid()}&state=12345&p={userFlow}";

            var url = $"https://{configuration["DomainName"]}.b2clogin.com/{configuration["DomainName"]}.onmicrosoft.com/oauth2/v2.0/authorize";

            url += $"?client_id={configuration["ClientId"]}";
            url += $"&redirect_uri={environment.BaseAddress}{redirect}";
            url += $"&response_mode=query";
            url += $"&response_type={configuration["ResponseType"]}";
            url += $"&scope=openid";
            url += $"&nonce={Guid.NewGuid()}";
            url += $"&state=12345";
            url += $"&p={userFlow}";
            url += $"&code_challenge=YTFjNjI1OWYzMzA3MTI4ZDY2Njg5M2RkNmVjNDE5YmEyZGRhOGYyM2IzNjdmZWFhMTQ1ODg3NDcxY2Nl";
            url += $"&code_challenge_method=S256";
            url += $"&prompt=login";
            //var url = $"{baseUrl}?client_id={clientId}&redirect_uri={redirect}&response_mode=query&response_type=id_token&scope=openid&nonce={Guid.NewGuid()}&state=12345&p={userFlow}";

            navigation.NavigateTo(url);
        }
    }
}

// https://blazoroidcb2c.b2clogin.com/blazoroidcb2c.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_ProfileEditing&client_id=2c4115b1-bc20-4945-abbc-213b3c22aa21&nonce=defaultNonce&redirect_uri=https%3A%2F%2Flocalhost%3A44351%2Fauthentication%2Flogin-callback&scope=openid&response_type=code&prompt=login