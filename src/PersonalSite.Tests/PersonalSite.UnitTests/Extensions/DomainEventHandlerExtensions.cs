using NSubstitute;
using PersonalSite.Domain.Events;
using System;

namespace PersonalSite.UnitTests.Extensions
{
    public static class DomainEventHandlerExtensions
    {
        public static void AssertEventWasRaised<TDomainEvent>(this IHandleDomainEventsSynchronouslyInCurrentScope<TDomainEvent> handler, Action<TDomainEvent> assert) 
            where TDomainEvent : IDomainEvent
        {
            handler.Received(1).Handle(Arg.Is<TDomainEvent>(ev => AssertEvent(assert, ev)));
        }

        private static bool AssertEvent<TDomainEvent>(Action<TDomainEvent> assert, TDomainEvent actualDomainEvent) 
            where TDomainEvent : IDomainEvent
        {
            assert(actualDomainEvent);
            return true;
        }
    }
}
