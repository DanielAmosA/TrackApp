public interface IMediator
{
    void Register(string eventType, Action<object> callback);
    void Notify(string eventType, object data);
}

public class Mediator : IMediator
{
    private Dictionary<string, Action<object>> _events = new Dictionary<string, Action<object>>();

    public void Register(string eventType, Action<object> callback)
    {
        if (!_events.ContainsKey(eventType))
        {
            _events.Add(eventType, callback);
        }
    }

    public void Notify(string eventType, object data)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType](data);
        }
    }
}

public class Page1ViewModel
{
    private readonly IMediator _mediator;

    public Page1ViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void NavigateToPage2()
    {
        // מפרסם אירוע למתווך
        _mediator.Notify("Page1Navigated", "Hello from Page1");
    }
}

public class Page2ViewModel
{
    private readonly IMediator _mediator;

    public Page2ViewModel(IMediator mediator)
    {
        _mediator = mediator;
        _mediator.Register("Page1Navigated", OnPage1Navigated);
    }

    private void OnPage1Navigated(object data)
    {
        // כאן אנחנו מקבלים את המידע
        Console.WriteLine($"Received in Page2: {data}");
    }
}

var mediator = new Mediator();
var page1 = new Page1ViewModel(mediator);
var page2 = new Page2ViewModel(mediator);

// הדף הראשון מפרסם אירוע
page1.NavigateToPage2();
