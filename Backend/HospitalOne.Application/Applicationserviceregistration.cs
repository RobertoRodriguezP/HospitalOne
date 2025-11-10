using FluentValidation;
using HospitalOne.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HospitalOne.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutoMapper (requiere: AutoMapper.Extensions.Microsoft.DependencyInjection)
            // Descomenta cuando instales el paquete: dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Registrar MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Registrar FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Registrar Behaviours (Pipeline)
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            return services;
        }
    }
}