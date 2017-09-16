using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper.Configuration;

namespace AqlaEvents
{
    public class CsvStorage
    {
        readonly CsvConfiguration _conf = new CsvConfiguration();
        
        public CsvStorage()
        {
            _conf.AutoMap<CityEvent>();
            _conf.IsHeaderCaseSensitive = false;
            _conf.Delimiter = ";";
            _conf.HasHeaderRecord = true;
        }

        public IList<CityEvent> ReadAll(StreamReader stream)
        {
            var list = new List<CityEvent>();

            var rr = new CsvHelper.CsvReader(stream, _conf);
            if (!rr.ReadHeader()) throw new InvalidOperationException();
            while (rr.Read())
                list.Add(rr.GetRecord<CityEvent>());
            return list;
        }

        public void WriteAll(StreamWriter stream, IEnumerable<CityEvent> events)
        {
            using (var ww = new CsvHelper.CsvWriter(stream, _conf))
            {
                ww.WriteHeader<CityEvent>();
                foreach (var el in events)
                    ww.WriteRecord(el);
            }
        }
    }
}