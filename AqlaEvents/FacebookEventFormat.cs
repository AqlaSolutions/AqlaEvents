using System;

namespace AqlaEvents
{
    public class FacebookEventFormat
    {
        public CityEvent ParseBaseEventInfo(FacebookEvent fbEvent)
        {
            string location = "";
            if (fbEvent.place != null)
            {
                if (fbEvent.place.location != null)
                {
                    location = fbEvent.place.location.street ?? "";
                    if (!location.Contains(fbEvent.place.location.city))
                    {
                        if (location.Length > 0)
                            location += ", ";
                        location += fbEvent.place.location.city;
                    }
                }
                if (fbEvent.place.name.Length > 0)
                {
                    if (!location.Contains(fbEvent.place.name))
                    {
                        if (location.Length > 0)
                            location += $" ({fbEvent.place.name})";
                        else
                            location = fbEvent.place.name;
                    }
                }
            }

            return new CityEvent()
            {
                Description = fbEvent.description,
                Start = DateTime.SpecifyKind(fbEvent.start_time, DateTimeKind.Utc).ToLocalTime(),
                End = DateTime.SpecifyKind(fbEvent.end_time, DateTimeKind.Utc).ToLocalTime(),
                DurationHours = (int)(fbEvent.end_time - fbEvent.start_time).TotalHours,
                Location = location,
                Name = fbEvent.name,
                Uri = MakeEventUri(fbEvent.id),
                HostedBy = fbEvent.owner != null ? (MakeEventOwnerUri(fbEvent.owner.id) + " - " + fbEvent.owner.name) : ""
            };
        }

        public string MakeEventUri(string id)
        {
            return "https://www.facebook.com/events/" + id;
        }

        public string MakeEventOwnerUri(string id)
        {
            return "https://www.facebook.com/" + id;
        }
    }
}