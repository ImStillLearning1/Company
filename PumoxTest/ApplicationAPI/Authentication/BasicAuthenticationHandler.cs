using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ApplicationAPI.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No header found");

            var headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(headerValue.Parameter);

            string credentials = Encoding.UTF8.GetString(bytes);

            if(!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(":");

                string username = array[0];
                string password = array[1];

                if(username == "admin" && password == "admin123")
                {
                    var claim = new[] { new Claim(ClaimTypes.Name, username) };
                    var identity = new ClaimsIdentity(claim, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("The user is not authorized");
                }
            }

            return AuthenticateResult.Fail("No header found");

        }
    }
}
