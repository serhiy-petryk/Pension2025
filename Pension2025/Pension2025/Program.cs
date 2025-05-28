using System;
using System.IO;
using System.Text;
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

            /*string rtfFilePath = @"E:\Users\System\Downloads\dc6f0322469b1d622c532904416caa09.rtf"; // Path to your RTF file
            string txtFilePath = rtfFilePath.Replace(".rtf", ".1251.2.txt"); // Path to save plain text

            if (File.Exists(rtfFilePath))
            {
                // Create a hidden RichTextBox
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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