namespace KspHelper.Date
{
    public static class DateUtils
    {
        /// <summary>
        /// Return time in game format. Like digit.
        /// </summary>
        /// <param name="year">count of years</param>
        /// <param name="day">count of days</param>
        /// <param name="hour">count of hours</param>
        /// <param name="minute">count of minites</param>
        /// <param name="sec">count of sec</param>
        /// <returns></returns>
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
