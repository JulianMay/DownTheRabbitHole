using System;
using System.Collections.Generic;
using System.Linq;

namespace DownTheRabbitHole.DomainStuff
{
    abstract class AggregateRoot
    {
        //Aggregate root's has a unique ID
        public string Id { get; }
        protected AggregateRoot(string id)
        {
            Id = id;
        }

        //events are emitted
        private List<object> _emittedEvents = new List<object>();
        public IEnumerable<object> GetEmittedEvents(){ return _emittedEvents; }
        protected void Emit(object domainEvent)
        {
            ApplyEvent(domainEvent);
            _emittedEvents.Add(domainEvent);
        }

        //... and events are applied
        private Dictionary<Type, Action<object>> _eventAppliers;
        protected void RegisterEventAppliers(params EventHandle[] handles)
        {
            _eventAppliers = handles.ToDictionary(x => x.EventType, x => x.Handle);
        }
        internal void ApplyEvents(IEnumerable<object> events)
        {
            foreach(var @event in events)
            {
                ApplyEvent(@event);
            }
        }

        private void ApplyEvent(object @event)
        {
            Action<object> apply;
            if (_eventAppliers.TryGetValue(@event.GetType(), out apply))
                apply(@event);
        }
    }

    
}
