using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Pension2025.Helpers;
using Pension2025.Models;

namespace Pension2025.Actions
{
    public static class DsaList
    {
        private static readonly string DsaLinkListFileName = Path.Combine(Settings.ListDsaFolder, "Links.txt");

        public static void PrintDsaLinkList()
        {
            var links = new Dictionary<int, string>();
            var checkedHeader = false;
            foreach (var line in File.ReadAllLines(DsaLinkListFileName))
            {
                if (!checkedHeader)
                {
                    if (line != "Id\tLink")
                        throw new Exception($"Check header of {DsaLinkListFileName} file");
                    checkedHeader = true;
                    continue;
                }

                var ss = line.Split('\t');
                links.Add(int.Parse(ss[0]), ss[1]);
            }

            var apiListFileName = Settings.ListFileName_Api;
            var missingItems = new List<ListItem>();
            checkedHeader = false;
            foreach (var line in File.ReadAllLines(apiListFileName))
            {
                if (!checkedHeader)
                {
                    if (line != ListItem.ListFileHeader)
                        throw new Exception($"Check header of {apiListFileName} file");
                    checkedHeader = true;
                    continue;
                }

                var ss = line.Split('\t');
                var item = new ListItem
                {
                    Url = ss[0],
                    ResultKind = ss[1],
                    No = ss[2],
                    Type = ss[3],
                    Court = ss[4],
                    Judge = ss[5],
                    Date = DateTime.ParseExact(ss[6], "yyyy-MM-dd", Settings.UaCulture)
                };
                if (item.Type != "Адміністративне") continue;

                if (links.TryGetValue(item.CauseId, out var link))
                    Debug.Print(link);
                else
                {
                    missingItems.Add(item);
                }
            }
        }

        public static void CreateDsaLinkList()
        {
            const string csvFileHeader =
                "doc_id;court_code;judgment_code;justice_kind;category_code;cause_num;adjudication_date;receipt_date;judge;doc_url;status;date_publ";

            var zipFileNames = Directory.GetFiles(Settings.ListDsaFolder, "*.zip");
            var links = new Dictionary<int, string>();
            foreach (var zipFileName in zipFileNames)
                using (var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Read))
                {
                    var entry = zip.Entries.First(a =>
                        string.Equals(a.Name, "documents.csv", StringComparison.InvariantCultureIgnoreCase));

                    var columns = new List<string>();
                    var checkHeader = false;
                    using (var reader = new MultilineMyCsvEscapedFileReader(entry.Open()))
                    {
                        reader.Delimiter = '\t';
                        while (reader.ReadRow(columns))
                        {
                            if (!checkHeader)
                            {
                                var header = string.Join(";", columns);
                                if (!string.Equals(header, csvFileHeader, StringComparison.InvariantCulture))
                                    throw new Exception($"Check file header of {entry.Name} in {Path.GetFileName(zipFileName)}");
                                checkHeader = true;
                                continue;
                            }

                            if (!string.IsNullOrEmpty(columns[9]) && columns[3] == "4")
                            {
                                if (columns[9].StartsWith("http://od.reyestr.court.gov.ua/files/",
                                        StringComparison.InvariantCulture))
                                    links.Add(int.Parse(columns[0]), columns[9].Substring(37)); //  447-42 MB, 654-43
                                else
                                    throw new Exception($"Invalid link: {columns[9]}");
                            }
                        }
                    }
                }

            File.WriteAllText(DsaLinkListFileName,
                "Id\tLink\r\n" + string.Join("\r\n", links.Select(a => a.Key.ToString() + "\t" + a.Value)));
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            var m = GC.GetTotalMemory(true);

        }
    }
}
