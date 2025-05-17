using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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
            var data = new Dictionary<string, (string, string, string, string, string, string, string, string)>();
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var ss1 = content.Split(new[] { "card-body" }, StringSplitOptions.None);
                for (var k = 1; k < ss1.Length; k++)
                {
                    var i1 = ss1[k].IndexOf("text-body-tertiary", StringComparison.InvariantCulture);
                    var i2 = ss1[k].IndexOf("</div>", i1, StringComparison.InvariantCulture);
                    var s = ss1[k].Substring(0, i2);

                    var ss2 = s.Split("href=\"");
                    if (ss2.Length != 3)
                        throw new Exception($"Check href in file {Path.GetFileName(file)}");
                    var href1 = GetUrlAndText(ss2[1]);
                    var href2 = GetUrlAndText(ss2[2]);

                    var ss3 = s.Split("</p>");
                    if (ss3.Length != 4)
                        throw new Exception($"Check '<p>' in file {Path.GetFileName(file)}");
                    var kind = GetText(ss3[0]);
                    var court = GetText(ss3[1]);
                    var who = GetText(ss3[2]);

                    var s1 = s.Replace("</small>", "").Replace("</span>", "");
                    var sDate = GetText(s1);

                    if (!data.ContainsKey(href1.Item1))
                    {
                        data.Add(href1.Item1, (href1.Item1, href1.Item2, href2.Item1, href2.Item2, kind, court, who, sDate));
                    }
                    else
                    {
                        cnt++;
                    }
                }
            }

            File.WriteAllLines(textFileName, data.Values.Select(a => GetString(a)));

            static string GetString((string, string, string, string, string, string, string, string) p)
            {
                return $"{p.Item1}\t{p.Item2}\t{p.Item3}\t{p.Item4}\t{p.Item5}\t{p.Item6}\t{p.Item7}\t{p.Item8}";
            }

        }

        private static (string, string) GetUrlAndText(string line)
        {
            var i1 = line.IndexOf("\"", StringComparison.InvariantCulture);
            var url = line.Substring(0, i1);
            var i11 = line.IndexOf(">", i1, StringComparison.InvariantCulture);
            var i12 = line.IndexOf("<", i11, StringComparison.InvariantCulture);
            var s = line.Substring(i11 + 1, i12 - i11 - 1).Trim();
            if (s.Contains("&"))
            {

            }
            return (url, s);
        }
        private static string GetText(string line)
        {
            var i = line.LastIndexOf(">", StringComparison.InvariantCulture);
            var text = line.Substring(i + 1).Trim();
            return HttpUtility.HtmlDecode(text);
        }

    }
}
