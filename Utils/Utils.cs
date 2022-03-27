using MongoDB.Bson;
using System;

namespace IsIoTWeb.Utils
{
    public static class Utils
    {
        public static string TimestampToDatetime(double timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string DynamicObjectIdToString(int timestamp, int machine, short pid, int increment)
        {
            return new ObjectId(timestamp, machine, pid, increment).ToString();
        }
    }
}
