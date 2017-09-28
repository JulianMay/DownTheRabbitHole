namespace DownTheRabbitHole
{
    interface IHandle<TCommand>
    {
        void Handle(TCommand command);
    }
}
