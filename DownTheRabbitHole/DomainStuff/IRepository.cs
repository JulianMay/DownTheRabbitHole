namespace DownTheRabbitHole.DomainStuff
{
    internal interface IRepository
    {
        TAggregate Load<TAggregate>(string aggregateId) where TAggregate : AggregateRoot;        
    }
}
