using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using CsvHelper.Configuration;
using Facebook;

namespace AqlaEvents
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var cb = new ContainerBuilder();
            cb.RegisterModule<DiMainModule>();
            var ev2 = cb.Build().Resolve<CityEventFromFacebookImporter>().Import("301552323647200");

            using (var f = new StreamWriter("test.csv", false, new UTF8Encoding(true)))
            {
                f.BaseStream.SetLength(0);
                var st = new CsvStorage();
                st.WriteAll(f, new[] { ev2, ev2 });
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}