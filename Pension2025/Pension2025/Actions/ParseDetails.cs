using System;
using System.IO;
using System.Linq;

namespace Pension2025.Actions
{
    public static class ParseDetails
    {
        public static void Run()
        {
            var keyVstanovyv = new string[]
            {
                "встановив:", "В С Т А Н О В И В:", "В С Т А Н О В И В :", "УСТАНОВИВ:", "В С Т А Н О В И Л А:",
                "ВСТАНОВИЛА :", "ВСТАНОВИЛА:", "У С Т А Н О В И В:", "В С Т А Н О В И Л А :", "у с т а н о в и в :",
                "в с т а н о в и в", "В С Т А Н О В И Л А", "ВСТАНОВИВ", "УСТАНОВИЛА:", "в с та н о в и в:",
                "ВСТАНОВИВ:", "У С Т А Н О В И Л А:", "У С Т А Н О В И ЛА:", "у с т а н о в и л а :",
                "ЗМІСТ ПОЗОВНИХ ВИМОГ", "ПОЗОВНІ ВИМОГИ ТА", "І. СТИСЛИЙ ВИКЛАД ПОЗИЦІЙ СТОРІН", "ІСТОРІЯ СПРАВИ",
                "I. ЗМІСТ ПОЗОВНИХ ВИМОГ", "прозаява про встановлення судового контролю,", "І. ЗМІСТ ПОЗОВНИХ ВИМОГ",
                "РУХ СПРАВИ", "І. ПРОЦЕДУРА/ПРОЦЕСУАЛЬНІ ДІЇ", "ОПИСОВА ЧАСТИНА", "Суть спору: ", "І. Суть спору:",
                "І. Суть спору", "Стислий виклад позицій сторін. ", "Суть спору, позиція сторін. ",
                "08.10.2024 вх.№47408/24 позивач у позовній заяві просить:", "Стислий виклад доводів сторін. ",
                "проСт. 382 судовий контроль,", "ОСОБА_1 звернувся до суду з позовом:"
            };
            var keyVstanovyv2 = new string[]
            {
                ",В С Т А Н О В И В:", ", У С Т А Н О В И В:", ",В С Т А Н О В И В :", ", -В С Т А Н О В И В:",
                ",встановив:", ", встановив:", ",ВСТАНОВИВ:", " діївстановив:", " В С Т А Н О В И В:",
                " дії, постановив ухвалу.", ",ВСТАНОВИВ", ", -встановив:", " зазначенаВСТАНОВИВ:", ", ВСТАНОВИВ:",
                " діїВСТАНОВИВ:"
            };

            var keyVyrishyv = new string[]
            {
                "УХВАЛИВ:", "у х в а л и в:", "ухвалив :", "у х в ал и в:", "У Х В АЛ ИВ:","У Х В А Л И В :","у х в а л и л а:","У Х В А Л И В;",
                "вирішив:", "В И Р І Ш И В:", "В И Р І Ш И В :", "В И Р ІШ И В :","ВИРІШИВ :","В И Р І Ш И ЛА:",
                "ПОСТАНОВИВ:", "ПОСТАНОВИВ :", "ПОСТАНОВИВ", "П О С Т А Н О В И В:", "П О С Т А Н О В И В :", "ПОСТАНОВИЛА:",
                "П О С Т А Н О В И Л А:", "П О С Т А Н О В И Л А :", "П О С Т АН О В И В:", "П О С Т А Н О В И Л А",
                "ВСТАНОВИВ", "ВИРІШИВ", "УХВАЛИВ", "У Х В А Л ИВ:", "У Х В А Л И В", "ВИІРШИВ:"
            };
            var keyVyrishyv2 = new[] { ", - в и р і ш и в:" };

            var cnt = 0;
            var cnt1 = 0;
            var cnt2 = 0;
            var folder = Path.Combine(Settings.DataFolder, "Details");
            var files = Directory.GetFiles(folder, "*.html").OrderBy(File.GetLastWriteTime).ToArray();
            foreach (var file in files)
            {
                cnt++;
                // if (cnt < 9700) continue;

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

                if (plainText.StartsWith("ОКРЕМА ДУМКА", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                if (plainText.IndexOf("1,197", StringComparison.InvariantCulture) != -1 ||
                    plainText.IndexOf("1.197", StringComparison.InvariantCulture) != -1)
                    cnt1++;
                if (plainText.IndexOf("1,197", StringComparison.InvariantCulture) != -1 ||
                    plainText.IndexOf("1.197", StringComparison.InvariantCulture) != -1)
                    cnt2++;

                i1 = -1;
                foreach (var key in keyVstanovyv)
                {
                    if (cnt == 730 && key == "ВСТАНОВИВ")
                    {

                    }
                    i1 = plainText.IndexOf("\t" + key, StringComparison.CurrentCultureIgnoreCase);
                    if (i1 != -1)
                        break;
                    i1 = plainText.IndexOf("\t " + key, StringComparison.CurrentCultureIgnoreCase);
                    if (i1 != -1)
                        break;
                }

                if (i1 == -1)
                {
                    foreach (var key in keyVstanovyv2)
                    {
                        i1 = plainText.IndexOf(key, StringComparison.CurrentCulture);
                        if (i1 != -1)
                            break;
                    }
                }

                if (i1 == -1)
                {
                    continue;
                }


                i2 = -1;
                foreach (var key in keyVyrishyv)
                {
                    i2 = plainText.IndexOf("\t" + key, i1+10, StringComparison.CurrentCultureIgnoreCase);
                    if (i2 != -1)
                        break;
                    i2 = plainText.IndexOf("\t " + key, i1+10, StringComparison.CurrentCultureIgnoreCase);
                    if (i2 != -1)
                        break;
                }

                if (i2 == -1)
                {
                    foreach (var key in keyVyrishyv2)
                    {
                        i2 = plainText.IndexOf(key, i1+10, StringComparison.CurrentCulture);
                        if (i2 != -1)
                            break;
                    }
                }

                if (i2 == -1)
                {

                }
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
