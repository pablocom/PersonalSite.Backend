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
            //var num = new Random().Next(0, 10);
            //if (num == 0)
            //{
            //    throw new Exception("Failed to send message to message bus.");
            //}
        }
    }
}
