using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Mediator'a: Git bütün assembly'i tara. Orada commandleri, queryleri bul. Onların handlerlarını bul. Onları birbirleriyle eşleştir ve listene koy. Sana ben bir send yaptığımda git onun handlerını çalıştır.
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
