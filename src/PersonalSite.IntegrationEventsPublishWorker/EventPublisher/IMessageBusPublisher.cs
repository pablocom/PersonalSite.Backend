namespace PersonalSite.IntegrationEventsPublishWorker.EventPublisher
{
    public interface IMessageBusPublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : class;
    }

    public class DummyMessageBusPublisher : IMessageBusPublisher
    {
        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            if (Random.Shared.Next(0, 4) == 0)
            {
                throw new Exception("Failed to send message to message bus.");
            }
        }
    }
}
