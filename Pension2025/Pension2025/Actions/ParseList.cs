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

            var folder = Path.Combine(Settings.DataFolder, "List01");
            var files = Directory.GetFiles(folder, "*.html");
            var data = new Dictionary<string, ListItem>();
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
                        data.Add(item.Id, item);
                }
            }

            var printData = new List<string>();
            printData.Add(ListItem.ListFileHeader);
            printData.AddRange(data.Values.Select(a => a.ToListString()));
            File.WriteAllLines(textFileName, printData);
        }
    }
}
