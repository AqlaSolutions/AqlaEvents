using System;

namespace AqlaEvents
{
    public class FacebookEventParser
    {
        public CityEvent ParseBaseEventInfo(FacebookEvent fbEvent)
        {
            return new CityEvent()
            {
                Description = fbEvent.description,
                Start = DateTime.SpecifyKind(fbEvent.start_time, DateTimeKind.Utc).ToLocalTime(),
                End = DateTime.SpecifyKind(fbEvent.end_time, DateTimeKind.Utc).ToLocalTime(),
                DurationHours = (int)(fbEvent.end_time - fbEvent.start_time).TotalHours,
                Location = (fbEvent.place.location != null ? fbEvent.place.location.street + ", " + fbEvent.place.location.city + ", " : "") + fbEvent.place.name,
                Name = fbEvent.name,
                Uri = "https://www.facebook.com/events/" + fbEvent.id
            };
        }
    }
}