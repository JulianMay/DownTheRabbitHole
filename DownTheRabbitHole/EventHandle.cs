using System;

namespace DownTheRabbitHole
{
    class EventHandle
    {
        public readonly Type EventType;
        public readonly Action<object> Handle;

        private EventHandle(Type eventType, Action<object> handle)
        {
            EventType = eventType ?? throw new ArgumentNullException(nameof(EventType));
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
        }

        public static EventHandle For<TEvent>(Action<TEvent> handle)
        {
            return new EventHandle(typeof(TEvent), (ev) => handle((TEvent)ev));
        }
    }
}
