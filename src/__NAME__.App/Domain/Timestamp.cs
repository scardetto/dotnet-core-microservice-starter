using System;

namespace __NAME__.App.Domain
{
    public static class TimestampExtensions
    {
        public static DateTime ToTheSecond(this DateTime date)
        {
            return date.RoundTicks(interval: 10000000L);
        }

        public static DateTime RoundTicks(this DateTime date, long interval)
        {
            return new DateTime(date.Ticks - date.Ticks % interval, date.Kind);
        }
    }
}