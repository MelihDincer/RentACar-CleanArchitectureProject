using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules)); //BaseBusinessRules türünde olan her şeyi IoC'ye ekle.

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //Mediator'a: Git bütün assembly'i tara. Orada commandleri, queryleri bul. Onların handlerlarını bul. Onları birbirleriyle eşleştir ve listene koy. Sana ben bir send yaptığımda git onun handlerını çalıştır.
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>)); //Eğer bir request çalıştıracaksan bu middleware den geçir.
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
