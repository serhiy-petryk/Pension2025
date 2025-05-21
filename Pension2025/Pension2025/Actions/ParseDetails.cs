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
                "ОСОБА_1 звернувся до суду з позовом:", "ОБСТАВИНИ СПРАВИ:", "ВИКЛАД ОБСТАВИН:",
                "В И К Л А Д О Б С Т А В И Н:", "Позовні вимоги:"
            };
            var keyVstanovyv2 = new string[]
            {
                ",В С Т А Н О В И В:", ", У С Т А Н О В И В:", ",В С Т А Н О В И В :", ", -В С Т А Н О В И В:",
                ",встановив:", ", встановив:", ",ВСТАНОВИВ:", " діївстановив:", " В С Т А Н О В И В:",
                " дії, постановив ухвалу.", ",ВСТАНОВИВ", ", -встановив:", " зазначенаВСТАНОВИВ:", ", ВСТАНОВИВ:",
                " діїВСТАНОВИВ:", " певних дій.", " певні дії,", " певні дії.", " вчинити дії.", "НОМЕР_3 ) про",
                " певні дії, ", " вчинити дії,", "40109058) про", " певні дії"
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
            var folder = Path.Combine(Settings.DataFolder, "Details");
            var files = Directory.GetFiles(folder, "*.html").OrderBy(File.GetLastWriteTime).ToArray();
            foreach (var file in files)
            {
                cnt++;
                // if (cnt < 8300) continue;
                // if (!file.Contains("127340774-9027de709174fa075e5f0aafe3b722f6")) continue;

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
                    plainText.IndexOf("1.197", StringComparison.InvariantCulture) != -1) item.Tag = "К";
                if (plainText.IndexOf("адвокат", StringComparison.InvariantCultureIgnoreCase) != -1) item.Tag += "А";
                if (plainText.IndexOf("представни", StringComparison.CurrentCultureIgnoreCase) != -1) item.Tag += "П";
                if (plainText.IndexOf("військ", StringComparison.CurrentCultureIgnoreCase) != -1) item.Tag += "В";
                // no items: if (plainText.IndexOf("анастас", StringComparison.CurrentCultureIgnoreCase) != -1) item.Tag += "Н";
                if (!item.IsValid) item.Tag += "Х";

                if (!item.IsValid)
                    continue;

                // Calculate index of Vstanovyv
                var tempList = new List<(string, int)>();
                foreach (var key in keyVstanovyv)
                {
                    i1 = plainText.IndexOf("\t" + key + "\n", StringComparison.CurrentCulture);
                    if (i1 != -1)
                    {
                        tempList.Add((key, i1));
                        d1[key]++;
                    }
                }
                foreach (var key in keyVstanovyv2)
                {
                    if (key == " певних дій.")
                    {

                    }
                    i1 = plainText.IndexOf(key + "\n", StringComparison.CurrentCulture);
                    if (i1 != -1)
                    {
                        tempList.Add((key, i1));
                        d2[key]++;
                    }
                }
                foreach (var key in keyVstanovyv3)
                {
                    i1 = plainText.IndexOf("\t" + key, StringComparison.CurrentCulture);
                    if (i1 != -1)
                    {
                        tempList.Add((key, i1));
                        d3[key]++;
                    }
                }

                if (tempList.Count == 0)
                    throw new Exception("Check 'Vstanovyv'");
                var vstanovyvItem = tempList.OrderBy(a => a.Item2).First();

                // Calculate index of Vyrishyv
                tempList.Clear();
                foreach (var key in keyVyrishyv)
                {
                    i2 = plainText.IndexOf("\t" + key + "\n", i1+10, StringComparison.CurrentCulture);
                    if (i2 != -1)
                    {
                        tempList.Add((key, i2));
                        d4[key]++;
                    }
                }
                foreach (var key in keyVyrishyv2)
                {
                    i2 = plainText.IndexOf(key + "\n", i1 + 10, StringComparison.CurrentCulture);
                    if (i2 != -1)
                    {
                        tempList.Add((key, i2));
                        d5[key]++;
                    }
                }

                if (tempList.Count == 0)
                    throw new Exception("Check 'Vyrishyv'");
                var vyrishyvItem = tempList.OrderByDescending(a => a.Item2).First();

                var sStart = plainText.Substring(0, vstanovyvItem.Item2);

                var tempStarts = new List<string>(sStart.Replace("до вчинен", "").Replace("до пенсії", "")
                    .Replace(" до неї", "").Replace(" до розгляду", "").Replace(" до перерахунку", "")
                    .Replace(", до треть", "").Replace(" до вчинит", "").Replace(" до процеду", "")
                    .Replace(" до суду за", "").Replace(" до суду із", "").Replace(" до судовог", "")
                    .Replace("прийняття до провадження", "").Replace(" до пункту", "").Replace("2016 до", "")
                    .Replace("2018 до", "").Replace(" до постанови", "").Replace("додані до нього", "")
                    .Replace("до суду в ", "").Replace("до їх вчинення", "")
                    .Split(
                        new string[]
                        {
                            " до ", "\tдо ", ")до ", "(до ", " до: ", "\tдо:", "доГоловн", "доВійс", "довійс", "до6 Державн",
                            "до3 Державн", "доІНФОР", "до5 державн", "\tвідповідач: ", " ОСОБА_1 Головн", "доДержав", "доВідділ",
                            "доУправлі", "доАварійн", "доНаціонал", "до11 державн", "доАдміністр", "доРегіонал", "дофінансового",
                            "\tвідповідач-1:", "доФінансов"
                        }, StringSplitOptions.None));

                if (tempStarts.Count == 3 && item.Id is "126813663-a4fde1835c4c5927d6b8cc706295d051" or "127306466-c4a61f4ae2d81adf53427388823f2305"
                        or "127261999-3effb13192bfea318362274962156191" or "127229351-ba4243fa8ca98b53151f6378c34d9e4e"
                        or "125140084-adccdd9564e7f8f6ec8ef8043af9d9a0" or "125075258-bbb4f4c7cd22b14d1354c609aae10371"
                        or "125237476-c4873428719dba5a2588cd9db7f734a7" or "124878193-1dc7992ec9fd8effc5221940b251755c")
                {
                    tempStarts.RemoveAt(2);
                }

                if (tempStarts.Count == 3 && item.Id is "127058897-321b015fd711e59e23869ab46a833695" or "126883284-c9ca97fc9186a8541d2c783a9d80277e")
                    tempStarts.RemoveAt(0);

                if (tempStarts.Count == 1 && item.Id is "126989827-7d11fcea00c641b3fefcd4a422a63638"
                        or "126989822-18cf6d35d8e40c74834fed27347af6b8" or "126989820-dc6467e172139f5f2e43a84e1db0aa98"
                        or "126699012-ab7685344079b01b0f29456789f739a0")
                    tempStarts = new List<string>(sStart.Split("про визнання протиправною"));
                if (tempStarts.Count == 1 && item.Id== "127336986-112af62f28e591a42ace16d1e8f066d9")
                    tempStarts = new List<string>(sStart.Split(", Головн"));

                var t1 = cnt;
                if (tempStarts.Count != 2)
                {
                    if (tempStarts.Count == 1 && item.Id == "127265684-9e149bc6d3f2b37e2943d6aa5140a767")
                    {
                        item.From = "ОСОБА";
                        item.To = "ПФУ";
                        // ОСОБА_1 -> Головного управління Пенсійного фонду України в Полтавській області
                    }
                    else if (tempStarts.Count == 1 && item.Id == "126164307-755a296773dc4e592893af3e709d7983")
                    {
                        item.From = "ОСОБА";
                        item.To = "ПФУ";
                        // ОСОБА_1 -> Головного управління Пенсійного фонду України в Одеській області
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125650514-89e14e53e91384c693c3c55f4457b90b")
                    {
                        item.From = "ОСОБА";
                        item.To = "ЗСУ";
                        // ОСОБА_1 -> Військової частини НОМЕР_1
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125530211-5fb734fc202756c2085b9a374489f4dd")
                    {
                        item.From = "ОСОБА";
                        item.To = "МВС";
                        // ОСОБА_1 -> Головного управління Національної поліції у Львівській області
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125533771-0745746c7ce666a85d02dde464d262eb")
                    {
                        item.From = "ОСОБА";
                        item.To = "ДСНС";
                        // ОСОБА_1 -> Державного пожежно-рятувального загону Головного управління Державної служби України з надзвичайних ситуацій у Дніпропетровській області
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125507443-fa8df4626221de22bec5c9b1d3aa6a40")
                    {
                        item.From = "ОСОБА";
                        item.To = "ІНФОРМАЦІЯ";
                        // ОСОБА_1 -> ІНФОРМАЦІЯ_1 (військова частина НОМЕР_2 )
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125476346-3127b8125a9da89fcdcb3f3ff292cd21")
                    {
                        item.From = "ОСОБА";
                        item.To = "ПФУ";
                        // ОСОБА_1 -> Головного управління Пенсійного фонду України в Одеській області
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125507962-f26c9a212692be5341863d6fc370e43d")
                    {
                        item.From = "ОСОБА";
                        item.To = "ПФУ";
                        // ОСОБА_1 -> Головного управління Пенсійного фонду України в м. Києві
                    }
                    else if (tempStarts.Count == 1 && item.Id == "127339593-1da822bee6696da10e04c549fe6826a0")
                    {
                        item.From = "ОСОБА";
                        item.To = "ПФУ";
                        // ОСОБА_1 -> Головного управління Пенсійного фонду України в Черкаській області
                    }
                    else if (tempStarts.Count == 1 && item.Id == "125008358-d9cfad96766968e07a292712f3432679")
                    {
                        item.From = "ОСОБА";
                        item.To = "ІНФОРМАЦІЯ";
                        // ОСОБА_1 -> ІНФОРМАЦІЯ_1 (військова частина НОМЕР_1 )
                    }
                    else if (tempStarts.Count == 1 && item.Id is "124978362-b72f7cfb8d48f84d06191d2c12af6171"
                             or "127164466-56e499a40a0465220e39b6cdf4aaf9c4")
                    {
                        item.From = "ОСОБА";
                        item.To = "ПФУ";
                        // ОСОБА_1 -> Головного управління Пенсійного фонду України у Львівській області
                    }
                    else if (item.Id == "127165271-9f92cb05b61cbcceefae353ea31afa1d")
                    {
                        item.From = "ОСОБА";
                        item.To = "ІНФОРМАЦІЯ";
                        // ОСОБА_1 -> Інформація не підлягає розголошенню
                    }
                    else
                    {
                    }

                }
                else
                {
                    var t2 = cnt;
                    var sStart1 = sStart;

                    // =================
                    // To define the 'To'
                    // =================
                    var toList = new Dictionary<string, string>
                    {
                        { "правління Пенсійного фонд", "ПФУ" }, { "правління Пенсійного Фонд", "ПФУ" }, {"правління пенсійного фонд","ПФУ"}, {" ПФУ ", "ПФУ"},
                        { "МАЦІЯ_", "ІНФОРМАЦІЯ" }, {"НОМЕР_", "НОМЕР"},
                        { "ькова частин", "ЗСУ" }, { "ькової частин", "ЗСУ" }, {"Державної установ", "Держстанова"},
                        {"аціонального університет", "Нацуніверситет"}, {"лужби безпеки Украї","СБУ"}, {"ержавної служб", "Держслужби"},
                        {"прикордонної служб", "ДПСУ"}, {"іністерства оборони", "МО"}, {"примусового виконання рішень", "Виконавча"},
                        {"Збройних Сил", "ЗСУ"}, {"Збройних сил", "ЗСУ"}, {"аціональної поліці", "МВС"}, {"іністерства юстиці", "Мінюст"},
                        {"ійськового інститут", "ЗСУ"}, {"ійськової академі", "ЗСУ"}, {"аціональної академії сухопутн", "ЗСУ"}, {"аціональної Академії Сухопу", "ЗСУ"},
                        {"ьково-медичног","ЗСУ"}, {"ністерства внутрішніх спр", "МВС"}, {" ДСНС ", "ДСНС"}, {"ержавної охоро", "Держохорона"},
                        {" МВС ", "МВС"}, {"адзвичайних ситуаці", "ДСНС"}, {"вартирно-експлуатаційн", "КЕВ"},
                        {" ДПС ", "ДПС"}, {"лужби зовнішньої розвід","СЗР"}, {"ерспецзв`язк", "СпецЗв'язок"}, {"акетних військ", "ЗСУ"},
                        {"аціональної гварді", "Нацгвардія"}, {"ійськового ліце", "ЗСУ"}, {"ійськово-морського ліц", "ЗСУ"},
                        {"ержавного університет", "Держуніверситет"}, {"ивільного захист", "ДСНС"}, {"нформаційного агентств", "Інформагентство"},
                        {"иправна колоні", "Колонія"}, {"ніверситету оборо", "ЗСУ"}, {" ДФС ", "ДФС"}, {"портивного клуб", "Спортклуб"},
                        {"ьково-медичної академі", "ЗСУ"}, {"кадемія сухопутн", "ЗСУ"}, {"римінально-виконавчої служ", "Виконавча"},
                        {"лідчий ізолятор", "Колонія"}, {"епартаменту внутрішньої безпеки", "ВнутрБезпека"}, {"Кабінету Міністрів Україн", "Кабмін"},
                        {"енітенціарної академ", "Колонія"}, {" ГСЦ МВ", "МВС"}, {"ніверситету Повітряних Сил", "ЗСУ"}, {" повітряних сил", "ЗСУ"},
                        {"ніверситета оборон", "ЗСУ"}
                    };
                    var tempList1 = new List<(string, int)>();
                    foreach (var kvp in toList)
                    {
                        i1 = tempStarts[1].IndexOf(kvp.Key, StringComparison.InvariantCulture);
                        if (i1 != -1)
                            tempList1.Add((kvp.Key, i1));
                    }

                    if (tempList1.Count >0)
                    {
                        item.To = toList[tempList1.OrderBy(a => a.Item2).First().Item1];
                    }
                    else
                    {

                    }

                    // =================
                    // To define the 'To'
                    // =================
                    var fromList = new Dictionary<string, string>
                    {
                        { "ОСОБА_1", "ОСОБА" }, { "Головного управління Пенсійного фонду", "ПФУ" },
                        { "за позовом Державної установи", "Держустанова" }, { "Головного управління ПФУ", "ПФУ" },
                        { "Приватного акціонерного товариств", "Приватне підприємство" }
                    };
                    tempList1.Clear();
                    foreach (var kvp in fromList)
                    {
                        i1 = tempStarts[0].LastIndexOf(kvp.Key, StringComparison.InvariantCulture);
                        if (i1!=-1)
                            tempList1.Add((kvp.Key, i1));
                    }

                    if (sStart.IndexOf(" ОСОБА_1 Головн", StringComparison.InvariantCulture) != -1)
                    {
                        if (tempList1.Count == 0) item.From = "ОСОБА";
                        else
                        {

                        }
                    }
                    else if (tempList1.Count >0)
                    {
                        var x1 = item;
                        item.From = fromList[tempList1.OrderByDescending(a => a.Item2).First().Item1];
                    }
                    else
                    {

                    }
                }

                if (item.From == item.To)
                {

                }

                // ====================
                // To define the result
                // ====================
                var sEnd = plainText.Substring(vyrishyvItem.Item2 + 1);
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
                else if (keys.Count == 2 && item.Id == "127308065-eae29d34b12880eaaf455b180df36537")
                    keys.RemoveAt(1);

                if (keys.Count == 1)
                    item.Result = keys[0];
                else
                {
                }
            }

            var printData = new List<string>();
            printData.Add(ListItem.ExtendedListFileHeader);
            printData.AddRange(listData.Select(a => a.Value.ToExtendedListString()));
            File.WriteAllLines(Settings.ExtendedListFileName, printData);

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
