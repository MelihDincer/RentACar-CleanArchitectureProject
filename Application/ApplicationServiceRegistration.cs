using Core.Application.Rules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules)); //BaseBusinessRules türünde olan her şeyi IoC'ye ekle.

        //Mevcut çalışan assemblydeki
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //Mediator'a: Git bütün assembly'i tara. Orada commandleri, queryleri bul. Onların handlerlarını bul. Onları birbirleriyle eşleştir ve listene koy. Sana ben bir send yaptığımda git onun handlerını çalıştır.
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }

    //Uygulama açıldığında git iş kuralı olan herşeyi bul ve IoC'ye ekle.
    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }
}
