using System.Linq;
using System.Reflection;
using Facebook;

namespace AqlaEvents
{
    public class FacebookEventRetriever
    {
        readonly FacebookClient _client;

        public static string FieldsValue = string.Join(",", typeof(FacebookEvent).GetTypeInfo().DeclaredProperties.Select(x => x.Name).ToArray());

        public FacebookEventRetriever(FacebookClient client)
        {
            _client = client;
        }

        public FacebookEvent GetEvent(string id)
        {
            return _client.Get<FacebookEvent>(id + "?fields=" + FieldsValue);
        }
    }
}