using System;
using System.IO;
using System.Linq;

namespace Pension2025.Actions
{
    public static class ParseDetails
    {
        public static void Run()
        {
            var cnt = 0;
            var textFileName = Path.Combine(Settings.DataFolder, "Details.txt");

            var folder = Path.Combine(Settings.DataFolder, "Details");
            var files = Directory.GetFiles(folder, "*.html").OrderBy(File.GetLastWriteTime).ToArray();
            // var data = new Dictionary<string, (string, string, string, string, string, string, string, string)>();
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var ss1 = content.Split("fixed-width");
                if (ss1.Length != 2)
                    throw new Exception($"Check 'fixed-width' in file {Path.GetFileName(file)}");
                var i1 = ss1[1].IndexOf(">", StringComparison.InvariantCulture);
                var i2 = ss1[1].IndexOf("</div>", StringComparison.InvariantCulture);
                var s1 = ss1[1].Substring(i1 + 1, i2 - i1 - 1);
                
                var plainText = ReplaceRepeats(Helpers.HtmlUtilities.HTMLToText(s1), new []{" ", "\t", "\r\n", "\n "}).Trim();
                plainText = plainText.Replace("\n\t \n\t", "\n\t");

                var plainText2 = ReplaceRepeats(Helpers.HtmlUtilities.ConvertToPlainText(s1), new[] { " ", "\t", "\r\n", "\n " }).Trim();
            }

            static string ReplaceRepeats(string sourceText, string[] texts)
            {
                foreach (var text in texts)
                {
                    var whatRepeat = text + text;
                    while (sourceText.IndexOf(whatRepeat, StringComparison.InvariantCulture) != -1)
                        sourceText = sourceText.Replace(whatRepeat, text);
                }

                return sourceText;
            }
        }

    }
}
