using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Pension2025.Models
{
    public class ListItem
    {
        #region ======  Static  =======
        public const string ListFileHeader = "Url\tResultKind\tSubListUrl\tNo\tType\tCourt\tJudge\tDate";
        public const string ExtendedListFileHeader = "Tag\tUrl\tResultKind\tSubListUrl\tNo\tType\tCourt\tJudge\tDate";
        public static ListItem ParseFromHtml(string html, string parentFileName)
        {
            var i1 = html.IndexOf("text-body-tertiary", StringComparison.InvariantCulture);
            var i2 = html.IndexOf("</div>", i1, StringComparison.InvariantCulture);
            var s = html.Substring(0, i2);

            var ss2 = s.Split("href=\"");
            if (ss2.Length != 3)
                throw new Exception($"Check href in file {Path.GetFileName(parentFileName)}");
            var href1 = GetUrlAndText(ss2[1]);
            var href2 = GetUrlAndText(ss2[2]);

            var ss3 = s.Split("</p>");
            if (ss3.Length != 4)
                throw new Exception($"Check '<p>' in file {Path.GetFileName(parentFileName)}");
            var type = GetText(ss3[0]);
            var court = GetText(ss3[1]);
            var judge = GetText(ss3[2]);

            var s1 = s.Replace("</small>", "").Replace("</span>", "");
            var date = DateTime.ParseExact(GetText(s1), "dd.MM.yyyy", Settings.UaCulture);

            var item = new ListItem()
            {
                Url = href1.Item1, ResultKind = href1.Item2, SubListUrl = href2.Item1, No = href2.Item2, Type = type,
                Court = court, Judge = judge, Date = date
            };
            return item;

            static (string, string) GetUrlAndText(string line)
            {
                var i1 = line.IndexOf("\"", StringComparison.InvariantCulture);
                var url = line.Substring(0, i1);
                var i11 = line.IndexOf(">", i1, StringComparison.InvariantCulture);
                var i12 = line.IndexOf("<", i11, StringComparison.InvariantCulture);
                var s = line.Substring(i11 + 1, i12 - i11 - 1).Trim();
                return (url, s);
            }
            static string GetText(string line)
            {
                var i = line.LastIndexOf(">", StringComparison.InvariantCulture);
                var text = line.Substring(i + 1).Trim();
                return HttpUtility.HtmlDecode(text);
            }
        }

        public static Dictionary<string, ListItem> GetListFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            if (!string.Equals(lines[0], ListFileHeader))
                throw new Exception($"Check file header in {Path.GetFileName(filename)}");

            var data = new Dictionary<string, ListItem>();
            for (var k = 1; k < lines.Length; k++)
            {
                var ss = lines[k].Split('\t');
                var item = new ListItem
                {
                    Url = ss[0],
                    ResultKind = ss[1],
                    SubListUrl = ss[2],
                    No = ss[3],
                    Type = ss[4],
                    Court = ss[5],
                    Judge = ss[6],
                    Date = DateTime.ParseExact(ss[7], "yyyy-MM-dd", Settings.UaCulture)
                };
                data.Add(item.Id, item);
            }

            return data;
        }

        #endregion
        public string Url { get; set; }
        public string ResultKind { get; set; }
        public string SubListUrl { get; set; }
        public string No { get; set; }
        public string Type { get; set; }
        public string Court { get; set; }
        public string Judge { get; set; }
        public string Tag { get; set; }
        public DateTime Date { get; set; }
        public string Id => Path.GetFileName(Url);

        public bool IsValid => string.Equals(Type, "Адміністративне") &&
                               Court.IndexOf("район", StringComparison.CurrentCultureIgnoreCase) == -1 &&
                               ResultKind.IndexOf("Окрема", StringComparison.InvariantCultureIgnoreCase) == -1;

        public string ToListString() => $"{Url}\t{ResultKind}\t{SubListUrl}\t{No}\t{Type}\t{Court}\t{Judge}\t{Date:yyyy-MM-dd}";
        public string ToExtendedListString() => $"{Tag}\t{ToListString()}";

        public override string ToString() => $"{ResultKind}\t{Type}\t{Court}\t{Tag}";

        /*
ResultKind	Count
Окрема думка	3
Окрема ухвала	1
Постанова	2466
Рішення	6768
Ухвала	742
	
Type	Count
Адміністративне	9971
Адмінправопорушення	2
Цивільне	7
         */
    }
}
