using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AqlaEvents
{
    [Serializable]
    public class CityEvent
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int DurationHours { get; set; }
        public string Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string Currency { get; set; }
        public string Uri { get; set; }
        public string Location { get; set; }
        public string Phones { get; set; }
        public string Description { get; set; }
    }
}
