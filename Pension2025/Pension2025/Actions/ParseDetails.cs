using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                "ВСТАНОВИВ:", "У С Т А Н О В И ЛА:", "у с т а н о в и л а :", "в с т а н о в и в:",
                "в с т а н о в и в :", "встановиВ:", "У С Т А Н О В И В :", " УСТАНОВИВ:", " ВСТАНОВИВ:", "ВСТАНОВИВ :",
                " в с т а н о в и в:", "в с т а н о в и л а:", "у с т а н о в и в:", "У С Т А Н О В И В: ",
                "установив:", "ВСТАНОВИВ: ", " встановив:", "в с т а н о в и В:", "в с т а н о в и В :",
                "В С Т А Н О В И В: ", " В С Т А Н О В И В :", "в с т а н о в и в : ", "в с т а н о в и л а :",
                " у с т а н о в и в:", " У С Т А Н О В И В :", " ВСТАНОВИВ: ", " в с т а н о в и в :",
                " В С Т А Н О В И В: ", "В С Т А Н О В И В : ", "в с т а н о в и в: ", "У С Т А Н О В И Л А: ",
                " ВСТАНОВИЛА:", " встановив: ", "ВСТАНОВИЛА: ", " У С Т А Н О В И В: ", "встановив: ",
                "В с т а н о в и в:", " У С Т А Н О В И В:", "ЗМІСТ ПОЗОВНИХ ВИМОГ ТА ЗАПЕРЕЧЕНЬ ",
                "ЗМІСТ ПОЗОВНИХ ВИМОГ", "ПОЗОВНІ ВИМОГИ ТА", "І. СТИСЛИЙ ВИКЛАД ПОЗИЦІЙ СТОРІН", "ІСТОРІЯ СПРАВИ",
                "I. ЗМІСТ ПОЗОВНИХ ВИМОГ", "прозаява про встановлення судового контролю,", "І. ЗМІСТ ПОЗОВНИХ ВИМОГ",
                "РУХ СПРАВИ", "І. СТИСЛИЙ ВИКЛАД ПОЗИЦІЙ СТОРІН ", "І. ПРОЦЕДУРА/ПРОЦЕСУАЛЬНІ ДІЇ", "ОПИСОВА ЧАСТИНА",
                "Обставини справи.", "І. Суть спору:", "І. Суть спору",
                "Стислий виклад позицій сторін. Процесуальні дії суду: ", "ЗМІСТ ПОЗОВНИХ ВИМОГ ТА ЗАПЕРЕЧЕНЬ",
                "ОПИСОВА ЧАСТИНА ", "08.10.2024 вх.№47408/24 позивач у позовній заяві просить: ",
                "Суть спору, позиція сторін. Процесуальні дії та заяви сторін.",
                "Стислий виклад доводів сторін. Процесуальні дії суду:", "проСт. 382 судовий контроль,",
                "Суть спору, позиція сторін. Процесуальні дії суду. Заяви сторін.",
                "ОСОБА_1 звернувся до суду з позовом:"
            };
            var keyVstanovyv2 = new string[]
            {
                ",В С Т А Н О В И В:", ", У С Т А Н О В И В:", ",В С Т А Н О В И В :", ", -В С Т А Н О В И В:",
                ",встановив:", ", встановив:", ",ВСТАНОВИВ:", " діївстановив:", " В С Т А Н О В И В:",
                " дії, постановив ухвалу.", ",ВСТАНОВИВ", ", -встановив:", " зазначенаВСТАНОВИВ:", ", ВСТАНОВИВ:",
                " діїВСТАНОВИВ:"
            };
            var keyVstanovyv3 = new[] { "Суть спору: " };

            var keyVyrishyv = new string[]
            {
                "УХВАЛИВ:", "у х в а л и в:", "ухвалив :", "у х в ал и в:", "У Х В АЛ ИВ:", "У Х В А Л И В :",
                "у х в а л и л а:", "вирішив:", "В И Р І Ш И В:", "В И Р І Ш И В :", "В И Р ІШ И В :", "ВИРІШИВ :",
                "В И Р І Ш И ЛА:", "ВИРІШИВ:", "ПОСТАНОВИВ:", "ПОСТАНОВИВ :", "ПОСТАНОВИВ", "П О С Т А Н О В И В:",
                "П О С Т А Н О В И В :", "ПОСТАНОВИЛА:", "П О С Т А Н О В И Л А:", "П О С Т А Н О В И Л А :",
                "П О С Т АН О В И В:", "П О С Т А Н О В И Л А", "ВСТАНОВИВ", "ВИРІШИВ", "УХВАЛИВ", "У Х В А Л ИВ:",
                "У Х В А Л И В", "ВИІРШИВ:", "в и р і ш и в:", "У Х В А Л И В:", "в и р і ш и в :", "В И Р І Ш И В: ",
                " В И Р І Ш И В:", " У Х В А Л И В:", " ПОСТАНОВИВ:", " ВИРІШИВ:", "УХВАЛИВ :", "ухвалив:",
                "п о с т а н о в и л а:", " постановив:", " У Х В А Л И В :", "У Х В А Л И В: ", "ВИРІШИВ: ",
                "УХВАЛИВ: ", "у х в а л и в :", " ухвалив:", "П О С Т А Н О В И В: ", "п о с т а н о в и в:",
                "п о с т а н о в и в :", "постановив:", "п о с т а н о в и л а : ", " п о с т а н о в и в:",
                "п о с т а н о в и л а :", "У Х В А Л И Л А:", " в и р і ш и в:", " П О С Т А Н О В И В :",
                " П О С Т А Н О В И В:", "В И Р І Ш И В : ", "ПОСТАНОВИВ: ", "У Х В А Л И В : ", "вирішив",
                "в и р і ш и в : ", " постановив: ", " ПОСТАНОВИВ :", " УХВАЛИВ:", "постановив: ", "у х в а л и в : ",
                "У Х В А Л И В; ", "УХВАЛИВУХВАЛИВ:", " В И Р І Ш И В :", " в и р і ш и в :", " ухвалив: ",
                "в и р і ш и в: "
            };
            var keyVyrishyv2 = new[] { ", - в и р і ш и в:", ", суд П О С Т А Н О В И В :" };

            var d1 = new Dictionary<string, int>(StringComparer.CurrentCulture);
            foreach (var s in keyVstanovyv) d1.Add(s, 0);
            var d2 = new Dictionary<string, int>(StringComparer.CurrentCulture);
            foreach (var s in keyVstanovyv2) d2.Add(s, 0);
            var d3 = new Dictionary<string, int>(StringComparer.CurrentCulture);
            foreach (var s in keyVstanovyv3) d3.Add(s, 0);
            var d4 = new Dictionary<string, int>(StringComparer.CurrentCulture);
            foreach (var s in keyVyrishyv) d4.Add(s, 0);
            var d5 = new Dictionary<string, int>(StringComparer.CurrentCulture);
            foreach (var s in keyVyrishyv2) d5.Add(s, 0);

            var cnt = 0;
            var cnt1 = 0;
            var cnt2 = 0;
            var folder = Path.Combine(Settings.DataFolder, "Details");
            var files = Directory.GetFiles(folder, "*.html").OrderBy(File.GetLastWriteTime).ToArray();
            foreach (var file in files)
            {
                cnt++;
                // if (cnt < 9100) continue;

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
                    i1 = plainText.IndexOf("\t" + key + "\n", StringComparison.CurrentCulture);
                    if (i1 != -1)
                    {
                        d1[key]++;
                        break;
                    }
                }

                if (i1 == -1)
                {
                    foreach (var key in keyVstanovyv2)
                    {
                        i1 = plainText.IndexOf(key + "\n", StringComparison.CurrentCulture);
                        if (i1 != -1)
                        {
                            d2[key]++;
                            break;
                        }
                    }
                }
                if (i1 == -1)
                {
                    foreach (var key in keyVstanovyv3)
                    {
                        i1 = plainText.IndexOf("\t" + key, StringComparison.CurrentCulture);
                        if (i1 != -1)
                        {
                            d3[key]++;
                            break;
                        }
                    }
                }

                if (i1 == -1)
                    throw new Exception("Check i1");


                i2 = -1;
                foreach (var key in keyVyrishyv)
                {
                    i2 = plainText.IndexOf("\t" + key + "\n", i1+10, StringComparison.CurrentCulture);
                    if (i2 != -1)
                    {
                        d4[key]++;
                        break;
                    }
                }

                if (i2 == -1)
                {
                    foreach (var key in keyVyrishyv2)
                    {
                        i2 = plainText.IndexOf(key + "\n", i1+10, StringComparison.CurrentCulture);
                        if (i2 != -1)
                        {
                            d5[key]++;
                            break;
                        }
                    }
                }

                if (i2 == -1)
                    throw new Exception("Check i2");
            }

            Debug.Print($"Cnt: {cnt}/{cnt1}");

            foreach (var kvp in d1)
                Debug.Print($"D1:\t{kvp.Key}\t{kvp.Value}");
            foreach (var kvp in d2)
                Debug.Print($"D2:\t{kvp.Key}\t{kvp.Value}");
            foreach (var kvp in d3)
                Debug.Print($"D3:\t{kvp.Key}\t{kvp.Value}");
            foreach (var kvp in d4)
                Debug.Print($"D4:\t{kvp.Key}\t{kvp.Value}");
            foreach (var kvp in d5)
                Debug.Print($"D5:\t{kvp.Key}\t{kvp.Value}");

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
