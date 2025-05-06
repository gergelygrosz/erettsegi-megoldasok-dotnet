using System.Drawing;

internal class Signal
{
    public TimeOnly Time { get; }
    public Point Position { get; }

    public Signal(TimeOnly time, Point position)
    {
        Time = time;
        Position = position;
    }

    public Signal(int hour, int minute, int second, int x, int y)
    {
        Time = new TimeOnly(hour, minute, second);
        Position = new Point(x, y);
    }
}