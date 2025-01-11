namespace lab10.utils;

public static class DateTimeExtensions
{
    public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
    {
        return date >= start && date <= end;
    }
}