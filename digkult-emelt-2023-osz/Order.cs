internal class Order(int day, City city, int amount)
{

    public int Day { get; } = day;
    public City City { get; } = city;
    public int Amount { get; } = amount;

    public override string ToString()
    {
        return $"Order {{Day={Day}, City={City}, Amount={Amount}}}";
    }

    public string Export()
    {
        return $"{Day}\t{City}\t{Amount}";
    }
}