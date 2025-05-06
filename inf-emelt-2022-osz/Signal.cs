internal class Signal
{
    public TimeOnly Time { get; }
    public int X { get; }
    public int Y { get; }

    public Signal(TimeOnly time, int x, int y)
    {
        Time = time;
        X = x;
        Y = y;
    }

    public Signal(int hour, int minute, int second, int x, int y)
    {
        Time = new TimeOnly(hour, minute, second);
        X = x;
        Y = y;
    }
}