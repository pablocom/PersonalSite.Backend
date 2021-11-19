using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PersonalSite.Domain.Events;

public static class DomainEvents
{
    private static IDictionary<Type, List<Delegate>> _dynamicHandlers;
    private static List<Type> _staticHandlers;

    public static void Init()
    {
        _dynamicHandlers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(IDomainEvent).IsAssignableFrom(x) && !x.IsInterface)
            .ToArray()
            .ToDictionary(x => x, x => new List<Delegate>());

        _staticHandlers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
            .ToList();
    }

    public static void Register<T>(Action<T> eventHandler)
    {
        _dynamicHandlers[typeof(T)].Add(eventHandler);
    }

    public static void Raise<T>(T domainEvent)
        where T : IDomainEvent
    {
        foreach (var handler in _dynamicHandlers[domainEvent.GetType()])
        {
            var action = (Action<T>)handler;
            action(domainEvent);
        }

        foreach (var handlerType in _staticHandlers)
        {
            if (typeof(IDomainEventHandler<T>).IsAssignableFrom(handlerType))
            {
                var handler = (IDomainEventHandler<T>)Activator.CreateInstance(handlerType);
                handler.Handle(domainEvent);
            }
        }
    }
}
