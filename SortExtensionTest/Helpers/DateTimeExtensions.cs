namespace SortExtensionTest.Helpers
{
    internal static class DateTimeExtensions
    {
        internal static string ToUserFriendlyDate(this DateTime dateTime)
        {
            return $"{dateTime.Year} {dateTime.GetFriendlyMonth()} {dateTime.GetOrdinalDay()}";
        }

        private static string GetFriendlyMonth(this DateTime dateTime)
        {
            switch (dateTime.Month)
            {
                case 1:
                    return "Jan.";
                case 2:
                    return "Feb.";
                case 3:
                    return "Mar.";
                case 4:
                    return "Apr.";
                case 5:
                    return "May.";
                case 6:
                    return "Jun.";
                case 7:
                    return "Jul.";
                case 8:
                    return "Aug.";
                case 9:
                    return "Sep.";
                case 10:
                    return "Oct.";
                case 11:
                    return "Nov.";
                case 12:
                    return "Dec.";
            }

            return string.Empty;
        }

        private static string GetOrdinalDay(this DateTime dateTime)
        {
            var val = dateTime.Day % 10;

            switch (val)
            {
                case 1:
                    return $"{dateTime.Day}st";
                case 2:
                    return $"{dateTime.Day}nd";
                case 3:
                    return $"{dateTime.Day}rd";
                default:
                    return $"{dateTime.Day}th";
            }
        }
    }
}
