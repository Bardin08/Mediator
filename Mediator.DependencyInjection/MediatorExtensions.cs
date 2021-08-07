using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mediator.DependencyInjection
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped, params Assembly[] assemblies)
        {
            var handlerInfos = new ConcurrentDictionary<Type, Type>();

            foreach (var assembly in assemblies)
            {
                var requests = GetClassesImplementingInterface(assembly, typeof(IRequest<>));
                var handlers = GetClassesImplementingInterface(assembly, typeof(IHandler<,>));

                requests.ForEach(request => 
                    handlerInfos[request] = handlers.SingleOrDefault(handler =>
                        request == handler.GetInterface("IHandler`2")!.GetGenericArguments()[0]));
                    
                var handlerServiceDescriptors = handlers.Select(x => new ServiceDescriptor(x, x, lifetime));
                services.TryAdd(handlerServiceDescriptors);
            }
                
            services.AddSingleton<IMediator>(x => new Mediator(x.GetRequiredService, handlerInfos));

            return services;
        }

        private static List<Type> GetClassesImplementingInterface(Assembly assembly, Type interfaceType)
        {
            return assembly.ExportedTypes.Where(type =>
            {
                var genericInterfacesTypes = type.GetInterfaces().Where(x => x.IsGenericType);
                var implementRequestType = genericInterfacesTypes
                    .Any(x => x.GetGenericTypeDefinition() == interfaceType);

                return !type.IsInterface && !type.IsAbstract && implementRequestType;
            }).ToList();
        }
    }
}