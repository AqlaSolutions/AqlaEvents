using System;

namespace AqlaEvents
{
    public class FacebookEvent
    {
        public string description { get; set; }
        public DateTime end_time { get; set; }
        public DateTime start_time { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public FacebookEventOwner owner { get; set; }
        public FacebookPlace place { get; set; }
    }
}