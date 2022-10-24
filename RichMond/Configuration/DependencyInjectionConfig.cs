using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Richmond.Domain.Commands;
using Richmond.Domain.Data;
using Richmond.Domain.Repositories;
using System.Reflection;

namespace RichMond.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<RichMondContext>();
            services.AddMediatR(typeof(AccountCommand).GetTypeInfo().Assembly);
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}