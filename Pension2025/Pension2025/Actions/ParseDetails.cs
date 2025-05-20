using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Pension2025.Models;

namespace Pension2025.Actions
{
    public static class ParseDetails
    {
        public static void Run()
        {
            var results = new []
            {
                "адовольнити частково","изнати неповажними", "задовольнити.", "задовільнити.",
                "задовольнити .", "задовольнити,", "адовольнити повністю", "адовільнити повністю",
                "без задоволення", "відмовити.", "відмовити повністю", "відмовити .", " без руху",
                "задоволити частково", "задоволити повністю", " задоволити.", "Прийняти звіт ", "Прийняти поданий","Визнати звіт ",
                "адовольнити клопотання ", "адовольнити позов", "задовольнити;", "алишити без розгляду",
                "рийняти позовну заяву", "адовільнити позов частково","адовольнити в повному обсязі",
                "адовольнити у повному обсязі", "ідмовити в повному обсязі", "ідмовити у повному обсязі",
                "адовольнити адміністративний позов ОСОБА","адовольнити адміністративний позов частково",
                "Витребувати у ", "адовольнити часково", "додаткове судове рішення", "оновити провадження",
                "оновити пропущений строк", "Поновити строк",
                "родовжити ОСОБА_1 строк для ", "ідкрити провадження",
                "адовільнити позов.", "родовжити ОСОБА_1 процесуальний строк", "овернути заявнику",
                "родовжити строк ", "упинити провадження", "овернути позивач", "зупинити до",
                "изнати протиправною бездіяльність", "оз`яснити учасникам справи", "передати на розгляд",
                "без здоволення", "адовольнити апеля", "здійснювати за правилами",
                "упинити апеляційне провадження", "Визнати причини ", "рийняти до провадження", "адовільнити частково",
                "Призначити до розгляду", "Залучити до участі", "ідкрити касаційне провадження ",
                "адовольнити часово", "адовольнити частоково", "Закрити провадження", "Визнати протиправними",
                "позов задовольнити\n", "повернути особі,", "ідмовити за безпідставністю", "Повернути ОСОБА",
                "продовжити розгляд справи", "Ухвалити додаткове рішення", "прийняти додаткове рішення",
                "1. Відмовити", "\n. Відмовити", "\nзакрити підготовче провадження", "\nВідмовити"
            };
            var results2 = new[]
            {
                "адовольнити повністю", "адовольнити клопотання", "адовольнити позов", "адовольнити частково",
                "повернути позивач", "прийняти позовну заяву", "Прийняти звіт ", "залишити без задоволення",
                "відмовити повністю", "алишити без руху", "алишити без розгляду", /*"вчинити певні дії",*/
                "адовільнити позов частково", "ідмовити повністю", "адовольнити в повному обсязі",
                "адовольнити у повному обсязі", "итребувати ", "изнати неповажними", " задовольнити."
            };
            var dResults = new Dictionary<string, int>(StringComparer.CurrentCulture);
            foreach (var s in results) dResults.Add(s, 0);

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

            var listData = ListItem.GetListFromFile(Settings.ListFileName);
            var cnt = 0;
            var cnt1 = 0;
            var cnt2 = 0;
            var cntArmy = 0;
            var cntInvalid = 0;
            var folder = Path.Combine(Settings.DataFolder, "Details");
            var files = Directory.GetFiles(folder, "*.html").OrderBy(File.GetLastWriteTime).ToArray();
            foreach (var file in files)
            {
                cnt++;
                if (cnt < 3300) continue;

                var item = listData[Path.GetFileNameWithoutExtension(file)];
                var content = File.ReadAllText(file);
                var ss1 = content.Split("fixed-width");
                if (ss1.Length != 2)
                    throw new Exception($"Check 'fixed-width' in file {Path.GetFileName(file)}");
                var i1 = ss1[1].IndexOf(">", StringComparison.InvariantCulture);
                var i2 = ss1[1].IndexOf("</div>", StringComparison.InvariantCulture);
                var s1 = ss1[1].Substring(i1 + 1, i2 - i1 - 1);
                
                var plainText = ReplaceRepeats(Helpers.HtmlUtilities.HTMLToText(s1), new []{" ", "\t", "\r\n", "\n "}).Trim();
                while (plainText.IndexOf("\n\t \n\t", StringComparison.InvariantCulture) != -1)
                    plainText = plainText.Replace("\n\t \n\t", "\n\t");

                plainText = plainText.Replace("\n\t. \n\t", "\n\t").Replace("\n\t.\n\t", "\n\t")
                    .Replace("\n\t\u007f\n\t", "\n\t");

                if (plainText.IndexOf("1,197", StringComparison.InvariantCulture) != -1 ||
                    plainText.IndexOf("1.197", StringComparison.InvariantCulture) != -1) item.Tag = "K";
                if (plainText.IndexOf("адвокат", StringComparison.InvariantCultureIgnoreCase) != -1) item.Tag += "А";
                if (plainText.IndexOf("військ", StringComparison.CurrentCultureIgnoreCase) != -1) item.Tag += "В";
                if (!item.IsValid) item.Tag += "X";

                if (!item.IsValid)
                    continue;

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

                var sEnd = plainText.Substring(i2 + 1);
                var i31 = sEnd.IndexOf("\n", StringComparison.CurrentCulture);
                var i32 = sEnd.IndexOf("\n", i31 + 1, StringComparison.CurrentCulture);
                var sEnd_FirstParagraph = "\n" + sEnd.Substring(i31 + 1, i32 - i31 - 1).Trim() + "\n";
                var temp = sEnd_FirstParagraph;
                var i33 = sEnd.IndexOf("\n", i32 + 1, StringComparison.CurrentCulture);
                if (i33 > -1)
                {
                    var i34 = sEnd.IndexOf("\n", i33 + 1, StringComparison.CurrentCulture);
                    if (i34 > -1)
                    {
                        var sEnd_SecondParagraph = sEnd.Substring(i32 + 1, i33 - i32 - 1).Trim();
                        if (sEnd_SecondParagraph.Contains("не підлягає розголошенню") || sEnd_SecondParagraph.Contains("заборонена для загального доступу"))
                            temp = "\n" + sEnd.Substring(i33 + 1, i34 - i33 - 1).Trim() + "\n";
                    }
                }


                var keys = new List<string>();
                foreach (var key in dResults.Keys)
                {
                    if (temp.IndexOf(key, StringComparison.CurrentCulture) != -1) keys.Add(key);
                }

                if (keys.Count == 0 && item.Id== "127089164-7b93972c6c84d5ca415ea89356917918")
                    keys.Add("повернути позивачу");
                else if (keys.Count==0 && item.Id == "125960288-a53bb77137078f9817c2edf390a288ae")
                    keys.Add("Визнати протиправною");
                else if (keys.Count == 0 && item.Id == "125305560-86a7d06bae2e6aea4dbff594ab6961d2")
                    keys.Add("задовольнити частково");
                else if (keys.Count == 2 && item.Id == "126550493-3918a6a42c0c3b7c341f04067d00a923")
                    keys.RemoveAt(1);
                else if (keys.Count == 2 && keys[0] == "рийняти позовну заяву" && string.Equals(keys[1], "ідкрити провадження"))
                    keys.RemoveAt(1);

                var t1 = cnt;
                if (keys.Count != 1 && !string.Equals(item.Id, "127308065-eae29d34b12880eaaf455b180df36537"))
                {
                }
            }

            var printData = new List<string>();
            printData.Add(ListItem.ExtendedListFileHeader);
            printData.AddRange(listData.Select(a => a.Value.ToExtendedListString()));
            File.WriteAllLines(Settings.ExtendedListFileName, printData);

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
