namespace Domain.Core.EventSourcing
{
    public interface IEventSourcing
    {
        EventCollection Events
        {
            get;
        }
    }
}
