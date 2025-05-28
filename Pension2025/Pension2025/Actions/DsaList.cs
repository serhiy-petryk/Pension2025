using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Pension2025.Helpers;

namespace Pension2025.Actions
{
    public static class DsaList
    {
        public static void CreateDsaLinkList()
        {
            const string csvFileHeader =
                "doc_id;court_code;judgment_code;justice_kind;category_code;cause_num;adjudication_date;receipt_date;judge;doc_url;status;date_publ";

            var zipFileNames = Directory.GetFiles(@"E:\Temp\Pension2025\ListDsa", "*.zip");
            var resultFileName = @"E:\Temp\Pension2025\ListDsa\Links.txt";
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

            File.WriteAllText(resultFileName,
                "Id\tLink\r\n" + string.Join("\r\n", links.Select(a => a.Key.ToString() + "\t" + a.Value)));
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            var m = GC.GetTotalMemory(true);

        }
    }
}
