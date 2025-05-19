using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Pension2025.Helpers
{
    public class HtmlUtilities
    {
        #region ======  From https://learn.microsoft.com/en-us/answers/questions/594274/convert-html-text-to-plain-text-in-c  =======
        public static string HTMLToText(string HTMLCode)
        {
            // Remove new lines since they are not visible in HTML  
            // HTMLCode = HTMLCode.Replace("\n", " ");
            // Remove tab spaces  
            // HTMLCode = HTMLCode.Replace("\t", " ");
            // Remove multiple white spaces from HTML  
            // HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");
            // Remove HEAD tag  
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Remove any JavaScript  
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
                , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Replace special characters like &, <, >, " etc.  
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
            // Note: There are many more special characters, these are just  
            // most common. You can add new characters in this arrays if needed  
            string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
                "&gt;", "&reg;", "&copy;", "&bull;", "&trade;","&#39;"};
            string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                sbHTML.Replace(OldWords[i], NewWords[i]);
            }
            // Check if there are line breaks (<br>) or paragraph (<p>)  
            sbHTML.Replace("<br>", "\n<br>");
            sbHTML.Replace("<br ", "\n<br ");
            sbHTML.Replace("<p>", "\n<p>");
            sbHTML.Replace("<p ", "\n<p ");
            // Finally, remove all HTML tags and return plain text  
            return System.Text.RegularExpressions.Regex.Replace(
                sbHTML.ToString(), "<[^>]*>", "");
        }
        #endregion

        #region =====  From https://github.com/ceee/ReadSharp/blob/master/ReadSharp/HtmlUtilities.cs  =======
        /// <summary>
        /// Converts HTML to plain text / strips tags.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string ConvertToPlainText(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }


        /// <summary>
        /// Count the words.
        /// The content has to be converted to plain text before (using ConvertToPlainText).
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns></returns>
        public static int CountWords(string plainText)
        {
            return !String.IsNullOrEmpty(plainText) ? plainText.Split(' ', '\n').Length : 0;
        }


        public static string Cut(string text, int length)
        {
            if (!String.IsNullOrEmpty(text) && text.Length > length)
            {
                text = text.Substring(0, length - 4) + " ...";
            }
            return text;
        }


        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }


        private static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    // get text
                    html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat paragraphs as crlf
                            outText.Write("\r\n");
                            break;
                        case "br":
                            outText.Write("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }
        #endregion

        public static string RemoveUselessTags(string htmlContent)
        {
            var len = htmlContent.Length;
            var i1 = htmlContent.IndexOf("<!-- BEGIN WAYBACK TOOLBAR INSERT -->", StringComparison.InvariantCulture);
            var i2 = htmlContent.LastIndexOf("<!-- END WAYBACK TOOLBAR INSERT -->", StringComparison.InvariantCulture);
            if (i1 > -1 && i2 > i1) // file from web.archive.org
            {
                htmlContent = htmlContent.Substring(0, i1) + htmlContent.Substring(i2 + 35);
            }

            i1 = htmlContent.IndexOf("<body", StringComparison.InvariantCulture);
            i2 = htmlContent.LastIndexOf("</body>", StringComparison.InvariantCulture);
            if (i2 > i1 && i1 > -1)
            {
                htmlContent = htmlContent.Substring(i1, i2 - i1 + 7);
            }

            i1 = htmlContent.IndexOf("</script>", StringComparison.InvariantCulture);
            while (i1 != -1)
            {
                i2 = htmlContent.LastIndexOf("<script", i1, StringComparison.InvariantCulture);
                if (i2 == -1)
                    break;
                htmlContent = htmlContent.Substring(0, i2) + htmlContent.Substring(i1 + 9);
                i1 = htmlContent.IndexOf("</script>", StringComparison.InvariantCulture);
            }

            i1 = htmlContent.IndexOf("</style>", StringComparison.InvariantCulture);
            while (i1 != -1)
            {
                i2 = htmlContent.LastIndexOf("<style", i1, StringComparison.InvariantCulture);
                if (i2 == -1)
                    break;
                htmlContent = htmlContent.Substring(0, i2) + htmlContent.Substring(i1 + 8);
                i1 = htmlContent.IndexOf("</style>", StringComparison.InvariantCulture);
            }

            i1 = htmlContent.IndexOf("</svg>", StringComparison.InvariantCulture);
            while (i1 != -1)
            {
                i2 = htmlContent.LastIndexOf("<svg", i1, StringComparison.InvariantCulture);
                if (i2 == -1)
                    break;
                htmlContent = htmlContent.Substring(0, i2) + htmlContent.Substring(i1 + 6);
                i1 = htmlContent.IndexOf("</svg>", StringComparison.InvariantCulture);
            }

            /* not need: file size is less only ~5%
            i1 = htmlContent.IndexOf("-->", StringComparison.InvariantCulture);
            while (i1 != -1)
            {
                i2 = htmlContent.LastIndexOf("<!--", i1, StringComparison.InvariantCulture);
                if (i2 == -1)
                    break;
                htmlContent = htmlContent.Substring(0, i2) + htmlContent.Substring(i1 + 3);
                i1 = htmlContent.IndexOf("-->", StringComparison.InvariantCulture);
            }

            i1 = htmlContent.IndexOf(" style=\"", StringComparison.InvariantCulture);
            while (i1 != -1)
            {
                i2 = htmlContent.IndexOf("\"", i1+8, StringComparison.InvariantCulture);
                if (i2 == -1)
                    break;
                htmlContent = htmlContent.Substring(0, i1) + htmlContent.Substring(i2 + 1);
                i1 = htmlContent.IndexOf(" style=\"", StringComparison.InvariantCulture);
            }*/

            var len2 = htmlContent.Length;
            return htmlContent;
        }

    }
}