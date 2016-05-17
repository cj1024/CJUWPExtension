using System;

namespace CJUWPExtension.Common
{

    public static class DateTimeExtensions
    {

        private static readonly DateTime UtcStandardTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double UnixTimeStamp(this DateTime time)
        {
            return (time.ToUniversalTime() - UtcStandardTime).TotalSeconds;
        }

    }

}
