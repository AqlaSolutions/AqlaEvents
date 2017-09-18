using System.Linq;

namespace AqlaEvents
{
    public class CityEventFromFacebookImporter
    {
        readonly MinMaxPriceExtractor _minMaxPriceExtractor;
        readonly FacebookEventRetriever _facebookEventRetriever;
        readonly FacebookEventFormat _facebookEventFormat;
        readonly EventDescriptionParser _descriptionParser;

        public CityEventFromFacebookImporter(EventDescriptionParser descriptionParser, FacebookEventFormat facebookEventFormat, FacebookEventRetriever facebookEventRetriever, MinMaxPriceExtractor minMaxPriceExtractor)
        {
            _descriptionParser = descriptionParser;
            _facebookEventFormat = facebookEventFormat;
            _facebookEventRetriever = facebookEventRetriever;
            _minMaxPriceExtractor = minMaxPriceExtractor;
        }

        public CityEvent Import(string id)
        {
            var fbEvent = _facebookEventRetriever.GetEvent(id);
            return Import(fbEvent);
        }

        public CityEvent Import(FacebookEvent fbEvent)
        {
            var cityEvent = _facebookEventFormat.ParseBaseEventInfo(fbEvent);

            cityEvent.Phones = string.Join(", ", _descriptionParser.ParsePhones(fbEvent.description));
            int? priceMin;
            int? priceMax;
            string currency;
            _minMaxPriceExtractor.GetMinMaxPrice(_descriptionParser.ParseMoney(fbEvent.description).ToArray(), out priceMin, out priceMax, out currency);
            cityEvent.MaxPrice = priceMax;
            cityEvent.MinPrice = priceMin;
            cityEvent.Currency = currency;

            return cityEvent;
        }
    }
}