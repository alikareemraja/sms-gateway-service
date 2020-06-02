using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace SmsGateway.HttpHeader
{
    /// <summary>
    /// Class ApiKeyHandler.
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public class ApiAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var requestApiKey = "";
            var apiKeyHeaders = context.Request.Headers.FirstOrDefault(x => x.Key == "API-KEY");
            if (apiKeyHeaders.Value != null)
            {
                requestApiKey = apiKeyHeaders.Value.FirstOrDefault();
            }

#if DEBUG

            // Assume swagger request to be valid...
            requestApiKey = System.Configuration.ConfigurationManager.AppSettings["API-KEY"];;
#endif

            var correctKey = System.Configuration.ConfigurationManager.AppSettings["API-KEY"];

            if (!string.IsNullOrWhiteSpace(requestApiKey) && correctKey == requestApiKey)
            {

            }
            else
                context.ErrorResult = new UnauthorizedResult(
                    new AuthenticationHeaderValue[0],
                    context.Request); // mark unauthorized
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}