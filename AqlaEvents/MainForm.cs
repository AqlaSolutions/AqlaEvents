using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using Facebook;
using IContainer = Autofac.IContainer;

namespace AqlaEvents
{
    public partial class MainForm : Form
    {
        const string EventsCSV = "events.csv";
        readonly List<CityEvent> _events = new List<CityEvent>();

        readonly CityEventFromFacebookImporter _importer;
        readonly FacebookEventFormat _eventFormat;
        readonly FacebookEventRetriever _retriever;
        
        public MainForm()
        {
            InitializeComponent();
            
            var cb = new ContainerBuilder();
            cb.RegisterModule<DiMainModule>();
            IContainer container = cb.Build();
            _importer = container.Resolve<CityEventFromFacebookImporter>();
            _retriever = container.Resolve<FacebookEventRetriever>();
            _fb = container.Resolve<FacebookClient>();
            _fbLoginUri = _fb.GetLoginUrl(new
            {
                client_id = Settings.Default.AppId,
                redirect_uri = "https://www.facebook.com/connect/login_success.html",
                response_type = "token",
                scope = "rsvp_event,user_events,publish_actions"
            });
            _eventFormat = container.Resolve<FacebookEventFormat>();
            InitializeChromium(_fbLoginUri.AbsoluteUri);
            try
            {
                using (var f = new StreamReader(EventsCSV))
                {
                    _events.AddRange(container.Resolve<CsvStorage>().ReadAll(f));
                }
            }
            catch (FileNotFoundException)
            {
            }
            QueryBox.Items.AddRange(Settings.Default.SearchQueries.Cast<object>().ToArray());
        }

        string _prevClipboardValue = null;
        FacebookClient _fb;
        readonly Uri _fbLoginUri;

        private void ClipboardTimer_Tick(object sender, EventArgs e)
        {
            string v = Clipboard.GetText(TextDataFormat.Text);
            if (v != _prevClipboardValue)
            {
                _prevClipboardValue = v;
                TryAddFacebookEventLink(v);
            }
        }

        public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        {
            FacebookOAuthResult res;
            if (_fb.TryParseOAuthCallbackUrl(new Uri(request.Url), out res))
            {
                if (!res.IsSuccess)
                    _browser.Load(_fbLoginUri.AbsoluteUri);
                else
                {
                    _fb = new FacebookClient(res.AccessToken);
                    _browser.Load("https://www.facebook.com/events/discovery");
                    foreach (var e in _fb.Get<FacebookEventsList>("me/events?include_canceled=false&fields=" + FacebookEventRetriever.FieldsValue).data)
                    {
                        string uri = _eventFormat.MakeEventUri(e.id);
                        if (e.start_time > DateTime.Now && _events.All(x => x.Uri != uri))
                            _events.Add(_importer.Import(e));
                    }
                    Save();
                    return true;

                }

                return false;
            }
            return TryAddFacebookEventLink(request.Url);
        }

        bool TryAddFacebookEventLink(string uriText, bool isBatch = false)
        {
            if (uriText.StartsWith("https://www.facebook.com/events/", StringComparison.OrdinalIgnoreCase))
            {
                string idString = uriText.Substring("https://www.facebook.com/events/".Length);
                string id = new string(idString.TakeWhile(char.IsDigit).ToArray());
                if (id.Length > 0)
                {
                    var fbEvent = _retriever.GetEvent(id);
                    CityEvent ev = _importer.Import(fbEvent);
                    if (!isBatch && _events.Any(x => x.Uri == ev.Uri))
                    {
                        Console.Beep();
                        Console.Beep();
                        return true;
                    }
                    ev.Description = ev.Description.Replace("\r", "").Replace(@"\", @"\\").Replace("\n", @"\n");
                    _events.Add(ev);
                    dynamic r = _fb.Post(id + "/maybe", new { });
                    if (r.success != true)
                        MessageBox.Show("Can't set rsvp: " + r, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    if (!isBatch)
                    {
                        Save();
                        Console.Beep();
                    }
                    return true;
                }
            }
            return false;
        }

        void Save()
        {
            using (var f = new StreamWriter(EventsCSV + ".new", false, new UTF8Encoding(true)))
            {
                f.BaseStream.SetLength(0);
                var st = new CsvStorage();
                st.WriteAll(f, _events.OrderBy(x => x.Start));
            }
            try
            {
                File.Delete(EventsCSV);
                File.Move(EventsCSV + ".new", EventsCSV);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), e.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void toolStripContainer_ContentPanel_Load(object sender, EventArgs e)
        {
        }

        private void urlTextBox_Click(object sender, EventArgs e)
        {

        }

        private void QueryBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            LoadSelectedSearchQuery(false);
        }

        bool _ignoreDropdownChange;

        private void NextQueryButton_Click(object sender, EventArgs e)
        {
            LoadSelectedSearchQuery(true);
        }

        void LoadSelectedSearchQuery(bool next)
        {
            _ignoreDropdownChange = true;
            try
            {
                if (string.IsNullOrEmpty(QueryBox.Text))
                {
                    if (QueryBox.Items.Count == 0 || !next) return;
                    QueryBox.SelectedIndex = 0;
                }
                string v = QueryBox.Text;
                int? ind = QueryBox.Items.OfType<string>().Where(x => x == v).Select((x, i) => (int?)i).FirstOrDefault();
                if (ind != null)
                {
                    if (next)
                    {
                        if (QueryBox.SelectedIndex != ind)
                            QueryBox.SelectedIndex = ind.Value;
                        if (QueryBox.Items.Count - 1 == QueryBox.SelectedIndex)
                            QueryBox.SelectedIndex = 0;
                        else
                            QueryBox.SelectedIndex++;
                    }
                    v = QueryBox.Text;
                }
                else
                {
                    QueryBox.Items.Add(v);
                    QueryBox.SelectedIndex = QueryBox.Items.Count - 1;
                    Settings.Default.SearchQueries = QueryBox.Items.OfType<string>().ToArray();
                    Settings.Default.Save();
                }

                LoadSearchQuery(v);
            }
            finally
            {
                _ignoreDropdownChange = false;
            }
        }

        void LoadSearchQuery(string v)
        {
            bool filterDate = false;
            if (v.EndsWith("-"))
            {
                filterDate = true;
                v = v.Substring(0, v.Length - 1);
            }
            string url = "https://www.facebook.com/search/str/" + Uri.EscapeUriString(v) +
                         "/keywords_events?filters_rp_events_location=%7B%22name%22%3A%22filter_events_location%22%2C%22args%22%3A%22111227078906045%22%7D";
            if (filterDate)
            {
                string from = DateTime.UtcNow.ToString("yyyy-MM-dd");
                string to = (DateTime.UtcNow + TimeSpan.FromDays(14)).ToString("yyyy-MM-dd");
                url += "&filters_rp_events_date={\"name\"%3A\"filter_events_date\"%2C\"args\"%3A\"" + from + "~" + to + "\"}";
            }
            _browser.Load(url);
        }

        private void DeleteQuery_Click(object sender, EventArgs e)
        {
            int ind = QueryBox.SelectedIndex;
            if (ind == -1)
            {
                QueryBox.Text = "";
                return;
            }
            QueryBox.Items.RemoveAt(ind);
            if (QueryBox.Items.Count > 0)
                QueryBox.SelectedIndex = Math.Min(ind, QueryBox.Items.Count - 1);
            else
                QueryBox.Text = "";
        }

        private void QueryBox_Click(object sender, EventArgs e)
        {

        }

        private void QueryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_ignoreDropdownChange && QueryBox.SelectedIndex != -1)
                LoadSearchQuery(QueryBox.Text);
        }

        private void ThisWeekButton_Click(object sender, EventArgs e)
        {
            _browser.Load(GetDiscoverUri("this_week"));
        }

        private void NextWeekButton_Click(object sender, EventArgs e)
        {
            _browser.Load(GetDiscoverUri("next_week"));
        }

        string GetDiscoverUri(string time)
        {
            return $"https://www.facebook.com/events/discovery/?suggestion_token=%7B%22city%22%3A%22111227078906045%22%2C%22event_categories%22%3A[%221284277608291920%22%2C%221116111648515721%22%2C%22660032617536373%22%2C%22258647957895086%22%2C%221138994019544264%22%2C%221219165261515884%22%2C%221254988834549294%22%2C%22432347013823672%22%2C%221915104302042536%22%2C%22359809011100389%22%2C%22607999416057365%22%2C%221712245629067288%22]%2C%22timezone%22%3A%22Europe%2FKiev%22%2C%22time%22%3A%22{time}%22%7D";
        }

        private void FeedButton_Click(object sender, EventArgs e)
        {
            _browser.Load("https://www.facebook.com/");
        }

        private void PagesFeedButton_Click(object sender, EventArgs e)
        {
            _browser.Load("https://www.facebook.com/pages/feed");
        }
    }
}