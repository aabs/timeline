namespace timeline;
public abstract class BaseEventManager
{
    protected TimeLineContext GetDb(string dbPath)
    {
        var result = new TimeLineContext();
        return result;
    }
}

