using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownTheRabbitHole.DomainStuff
{
    abstract class AggregateRoot
    {
        public string Id { get; }
        private List<object> _unpersistedEvents = new List<object>();
        public IEnumerable<object> GetUnpersistedEvents(){ return _unpersistedEvents; }
        
        protected void Emit(object domainEvent)
        {
            _unpersistedEvents.Add(domainEvent);
        }

        internal void ApplyEvents(IEnumerable<object> events)
        {

        }
    }
}
