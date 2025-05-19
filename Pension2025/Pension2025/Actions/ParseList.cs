using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pension2025.Models;

namespace Pension2025.Actions
{
    public static class ParseList
    {
        public static void Run()
        {
            var cnt = 0;
            var textFileName = Path.Combine(Settings.DataFolder, "List.txt");
            var resultKindsFileName = Path.Combine(Settings.DataFolder, "ResultKinds.txt");
            var typesFileName = Path.Combine(Settings.DataFolder, "Types.txt");
            var courtsFileName = Path.Combine(Settings.DataFolder, "Courts.txt");
            var byDateFileName = Path.Combine(Settings.DataFolder, "ByDate.txt");

            var folder = Path.Combine(Settings.DataFolder, "List01");
            var files = Directory.GetFiles(folder, "*.html");
            var data = new Dictionary<string, ListItem>();
            var resultKinds = new Dictionary<string, int>();
            var types = new Dictionary<string, int>();
            var courts = new Dictionary<string, int>();
            var byDate = new Dictionary<DateTime, int>();
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var ss1 = content.Split(new[] { "card-body" }, StringSplitOptions.None);
                for (var k = 1; k < ss1.Length; k++)
                {
                    var item = ListItem.ParseFromHtml(ss1[k], file);
                    if (data.ContainsKey(item.Id))
                        cnt++;
                    else
                    {
                        data.Add(item.Id, item);

                        resultKinds.TryAdd(item.ResultKind, 0);
                        resultKinds[item.ResultKind]++;

                        types.TryAdd(item.Type, 0);
                        types[item.Type]++;

                        courts.TryAdd(item.Court, 0);
                        courts[item.Court]++;

                        byDate.TryAdd(item.Date, 0);
                        byDate[item.Date]++;
                    }
                }
            }

            var printData = new List<string>();
            printData.Add(ListItem.ListFileHeader);
            printData.AddRange(data.Values.Select(a => a.ToListString()));
            File.WriteAllLines(textFileName, printData);

            printData.Clear();
            printData.Add("ResultKind\tCount");
            printData.AddRange(resultKinds.OrderBy(a=>a.Key).Select(a=>$"{a.Key}\t{a.Value}"));
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
