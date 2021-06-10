using Aplication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    public static class ServiceRegistration
    {
        public static void AddDalService(this IServiceCollection service)
        {
            service.AddTransient<IWebAddressVerification, WebAddressVerificationService>();
            service.AddTransient<IIdentity, IdentityService>();

        }
    }
}
