﻿
using Bootcamp.Web.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace Bootcamp.Web.TokenServices
{
    public class ClientCredentialTokenInterceptor(TokenService tokenService):DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenAsClientCredential = await tokenService.GetTokenWithClientCredentials();

            if (!tokenAsClientCredential.isSuccess)
            {
               throw new Exception("Token Servisinde hata var");
            }


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenAsClientCredential.token);





            var response= await base.SendAsync(request, cancellationToken);

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {

                tokenService.ClearTokenCache();

                tokenAsClientCredential = await tokenService.GetTokenWithClientCredentials();


                if (!tokenAsClientCredential.isSuccess)
                {
                    throw new Exception("Token Servisinde Hata Var.");                   
                }
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenAsClientCredential.token);


                response = await base.SendAsync(request, cancellationToken);
            }





            return response;
        }













    }
}
