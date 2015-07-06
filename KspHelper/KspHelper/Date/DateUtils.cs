namespace KspHelper.Date
{
    public static class DateUtils
    {
        public static double ConvertToGameDate(int year, int day, int hour, int minute, int sec)
        {
            var date = new[] { sec, minute, hour, day, year };
            return GameSettings.KERBIN_TIME ? ConvertToKerbinTime(date) : ConvertToEarthTime(date);
        }

        private static double ConvertToKerbinTime(int[] date)
        {
            return date[0] + date[1] * 60 + date[2] * 3600 + date[2] * 21600 + date[4] * 9201600;
        }

        private static double ConvertToEarthTime(int[] date)
        {
            return date[0] + date[1] * 60 + date[2] * 3600 + date[3] * 86400 + date[4] * 31536000;
        }
    }
}
