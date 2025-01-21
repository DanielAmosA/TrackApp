public interface IEventAggregator
{
    void Subscribe<TEvent>(Action<TEvent> eventHandler);
    void Publish<TEvent>(TEvent eventToPublish);
}

public class EventAggregator : IEventAggregator
{
    private readonly List<Delegate> _subscribers = new List<Delegate>();

    public void Subscribe<TEvent>(Action<TEvent> eventHandler)
    {
        _subscribers.Add(eventHandler);
    }

    public void Publish<TEvent>(TEvent eventToPublish)
    {
        foreach (var subscriber in _subscribers.OfType<Action<TEvent>>())
        {
            subscriber(eventToPublish);
        }
    }
}

public class DataUpdatedEvent
{
    public string Message { get; set; }
}

public class PublisherViewModel
{
    private readonly IEventAggregator _eventAggregator;

    public PublisherViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public void PublishUpdate()
    {
        var eventMessage = new DataUpdatedEvent { Message = "Data has been updated!" };
        _eventAggregator.Publish(eventMessage);
    }
}

public class SubscriberViewModel
{
    public SubscriberViewModel(IEventAggregator eventAggregator)
    {
        eventAggregator.Subscribe<DataUpdatedEvent>(OnDataUpdated);
    }

    private void OnDataUpdated(DataUpdatedEvent eventData)
    {
        Console.WriteLine($"Received in Subscriber: {eventData.Message}");
    }
}

var eventAggregator = new EventAggregator();
var publisher = new PublisherViewModel(eventAggregator);
var subscriber = new SubscriberViewModel(eventAggregator);

// פרסום האירוע
publisher.PublishUpdate();

