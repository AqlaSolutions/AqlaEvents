using Facebook;

namespace AqlaEvents
{
    public class FacebookEventRetriever
    {
        readonly FacebookClient _client;

        public FacebookEventRetriever(FacebookClient client)
        {
            _client = client;
        }

        public FacebookEvent GetEvent(string id)
        {
            return _client.Get<FacebookEvent>(id);
        }
    }
}