using System;
using System.Windows.Forms;

namespace Pension2025
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string rtfFilePath = @"E:\Quote\WebData\Minute\Polygon2003\Data\MP2003_20250524\MP2003_A_20250512.json"; // Path to your RTF file
            string txtFilePath = rtfFilePath.Replace(".json", ".3.json"); // Path to save plain text
            var s = File.ReadAllText(rtfFilePath);
            File.WriteAllText(txtFilePath, s, Encoding.GetEncoding("windows-1251"));*/

            /*if (File.Exists(rtfFilePath))
            {
                // Create a hidden RichTextBox
                using (RichTextBox rtb = new RichTextBox())
                {
                    rtb.LoadFile(rtfFilePath, RichTextBoxStreamType.RichText);
//                    File.WriteAllText(txtFilePath, rtb.Text, Encoding.GetEncoding("windows-1251"));
                    File.WriteAllText(txtFilePath, rtb.Text.Replace("\n", Environment.NewLine), Encoding.GetEncoding(1251));
//                    File.WriteAllText(filePath, content, Encoding.UTF8);
                    Console.WriteLine($"Conversion complete. Plain text saved to: {txtFilePath}");
                }
            }*/

            Application.Run(new Form1());
        }
    }
}