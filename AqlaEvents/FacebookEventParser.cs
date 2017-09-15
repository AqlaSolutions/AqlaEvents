namespace AqlaEvents
{
    public class FacebookEventParser
    {
        public CityEvent ParseBaseEventInfo(FacebookEvent fbEvent)
        {
            return new CityEvent()
            {
                Description = fbEvent.description,
                Start = fbEvent.start_time,
                End = fbEvent.end_time,
                DurationHours = (int)(fbEvent.end_time - fbEvent.start_time).TotalHours,
                Location = fbEvent.place.location.street + ", " + fbEvent.place.location.city,
                Name = fbEvent.name,
                Uri = "https://www.facebook.com/events/" + fbEvent.id
            };
        }
    }
}