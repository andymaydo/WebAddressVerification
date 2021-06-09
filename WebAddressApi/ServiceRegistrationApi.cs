using Aplication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressApi
{
    public static class ServiceRegistrationApi
    {
        public static void AddWebApiServices(this IServiceCollection services)
        {
            services.AddHttpClient<IWebAddressVerification, WebAddressApiService>();
            
        }
    }
}
