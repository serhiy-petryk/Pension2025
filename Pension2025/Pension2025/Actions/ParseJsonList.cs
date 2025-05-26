using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Pension2025.Models;

namespace Pension2025.Actions
{
    public static class ParseJsonList
    {
        public class DataModel
        {
            public string status { get; set; }
            public Data data;
        }

        public class Data
        {
            public int count { get; set; }
            public Item[] items { get; set; }
        }

        public class Item
        {
            public long doc_id { get; set; }
            public int court_code { get; set; }
            public string court_name { get; set; } //+
            public byte justice_code { get; set; }
            public string justice_name { get; set; } //+ ResultKind
            public byte judgment_code { get; set; }
            public string judgment_name { get; set; } //+ Type
            public long? category_code { get; set; }
            public string category_name { get; set; }
            public string cause_number { get; set; } //+
            public string adjudication_date { get; set; } // дата рішення
            public string receipt_date { get; set; }
            public string date_publ { get; set; }
            public string judge { get; set; } //+
            public string link { get; set; } //+
            public DateTime Date => DateTimeOffset.Parse(adjudication_date, CultureInfo.InvariantCulture).Date; // дата рішення

            public ListItem ToListItem()
            {
                var item = new ListItem
                {
                    Url = link, ResultKind = justice_name, No = cause_number, Type = judgment_name, Court = court_name,
                    Judge = judge, Date = Date
                };
                return item;
            }
        }

        public static void PrintUrlList()
        {
            var folder = @"E:\Temp\Pension2025\ListApi";
            var files = Directory.GetFiles(folder, "*.json");
            foreach (var file in files)
            {
                var bytes = File.ReadAllBytes(file);
                var data = SpanJson.JsonSerializer.Generic.Utf8.Deserialize<DataModel>(bytes);
                foreach (var item in data.data.items)
                    Debug.Print(item.link);
            }
        }

        public static void SaveToListFile()
        {
            var cnt = 0;
            var folder = Path.Combine(Settings.DataFolder, "ListApi");
            var files = Directory.GetFiles(folder, "*.json");
            var data = new Dictionary<string, ListItem>();
            var resultKinds = new Dictionary<string, int>();
            var types = new Dictionary<string, int>();
            var courts = new Dictionary<string, int>();
            var byDate = new Dictionary<DateTime, int>();
            foreach (var file in files)
            {
                var bytes = File.ReadAllBytes(file);
                var items = SpanJson.JsonSerializer.Generic.Utf8.Deserialize<DataModel>(bytes);
                foreach (var item in items.data.items)
                {
                    var listItem = item.ToListItem();
                    if (data.ContainsKey(listItem.Id))
                        cnt++;
                    else
                    {
                        data.Add(listItem.Id, listItem);

                        resultKinds.TryAdd(listItem.ResultKind, 0);
                        resultKinds[listItem.ResultKind]++;

                        types.TryAdd(listItem.Type, 0);
                        types[listItem.Type]++;

                        courts.TryAdd(listItem.Court, 0);
                        courts[listItem.Court]++;

                        byDate.TryAdd(listItem.Date, 0);
                        byDate[listItem.Date]++;
                    }
                }
            }

            Debug.Print($"Duplicate item count: {cnt}");

            var printData = new List<string>();
            printData.Add(ListItem.ListFileHeader);
            printData.AddRange(data.Values.Select(a => a.ToListString()));
            File.WriteAllLines(Settings.ListFileName_Json, printData);

            var resultKindsFileName = Path.Combine(Settings.DataFolder, "ResultKinds.json.txt");
            var typesFileName = Path.Combine(Settings.DataFolder, "Types.json.txt");
            var courtsFileName = Path.Combine(Settings.DataFolder, "Courts.json.txt");
            var byDateFileName = Path.Combine(Settings.DataFolder, "ByDate.json.txt");

            printData.Clear();
            printData.Add("ResultKind\tCount");
            printData.AddRange(resultKinds.OrderBy(a => a.Key).Select(a => $"{a.Key}\t{a.Value}"));
            File.WriteAllLines(resultKindsFileName, printData);

            printData.Clear();
            printData.Add("Type\tCount");
            printData.AddRange(types.OrderBy(a => a.Key).Select(a => $"{a.Key}\t{a.Value}"));
            File.WriteAllLines(typesFileName, printData);

            printData.Clear();
            printData.Add("Court\tCount");
            printData.AddRange(courts.OrderBy(a => a.Key).Select(a => $"{a.Key}\t{a.Value}"));
            File.WriteAllLines(courtsFileName, printData);

            printData.Clear();
            printData.Add("Date\tCount");
            printData.AddRange(byDate.OrderBy(a => a.Key).Select(a => $"{a.Key:yyyy-MM-dd}\t{a.Value}"));
            File.WriteAllLines(byDateFileName, printData);
        }

    }
}
