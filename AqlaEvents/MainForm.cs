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
using IContainer = Autofac.IContainer;

namespace AqlaEvents
{
    public partial class MainForm : Form
    {
        const string EventsCSV = "events.csv";
        readonly List<CityEvent> _events = new List<CityEvent>();

        readonly CityEventFromFacebookImporter _fbImporter;

        public MainForm()
        {
            InitializeComponent();
            InitializeChromium("https://www.facebook.com");
            
            var cb = new ContainerBuilder();
            cb.RegisterModule<DiMainModule>();
            IContainer container = cb.Build();
            _fbImporter = container.Resolve<CityEventFromFacebookImporter>();
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
            return TryAddFacebookEventLink(request.Url);
        }

        bool TryAddFacebookEventLink(string uriText)
        {
            if (uriText.StartsWith("https://www.facebook.com/events/", StringComparison.OrdinalIgnoreCase))
            {
                string idString = uriText.Substring("https://www.facebook.com/events/".Length);
                string id = new string(idString.TakeWhile(char.IsDigit).ToArray());
                if (id.Length > 0)
                {
                    CityEvent ev = _fbImporter.Import(id);
                    if (_events.Any(x => x.Uri == ev.Uri))
                    {
                        Console.Beep();
                        Console.Beep();
                        return true;
                    }
                    ev.Description = ev.Description.Replace("\r", "").Replace(@"\", @"\\").Replace("\n", @"\n");
                    _events.Add(ev);
                    Save();
                    Console.Beep();
                    return true;
                }
            }
            return false;
        }

        void Save()
        {
            using (var f = new StreamWriter(EventsCSV, false, new UTF8Encoding(true)))
            {
                f.BaseStream.SetLength(0);
                var st = new CsvStorage();
                st.WriteAll(f, _events);
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