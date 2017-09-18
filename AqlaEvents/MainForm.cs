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

        readonly CityEventFromFacebookImporter _fbImporter;
        readonly FacebookEventFormat _eventFormat;

        public MainForm()
        {
            InitializeComponent();
            
            var cb = new ContainerBuilder();
            cb.RegisterModule<DiMainModule>();
            IContainer container = cb.Build();
            _fbImporter = container.Resolve<CityEventFromFacebookImporter>();
            _fb = container.Resolve<FacebookClient>();
            _fbLoginUri = _fb.GetLoginUrl(new
            {
                client_id = Settings.Default.AppId,
                redirect_uri = "https://www.facebook.com/connect/login_success.html",
                response_type = "token",
                scope = "rsvp_event,user_events"
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
                    foreach (var e in _fb.Get<FacebookEventsList>("me/events?include_canceled=false").data)
                    {
                        string uri = _eventFormat.MakeEventUri(e.id);
                        if (e.start_time > DateTime.Now && _events.All(x => x.Uri != uri))
                            _events.Add(_fbImporter.Import(e));
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
                    CityEvent ev = _fbImporter.Import(id);
                    if (!isBatch && _events.Any(x => x.Uri == ev.Uri))
                    {
                        Console.Beep();
                        Console.Beep();
                        return true;
                    }
                    ev.Description = ev.Description.Replace("\r", "").Replace(@"\", @"\\").Replace("\n", @"\n");
                    _events.Add(ev);
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
    }
}