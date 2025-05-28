using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Pension2025.Actions
{
    public static class DsaDetails
    {
        public static void ConvertRtfFilesToTxt()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var sourceFolder = Path.Combine(Settings.DataFolder, "DetailsDsa");
            var destinationFolder = Path.Combine(Settings.DataFolder, "DetailsTxtDsa");
            var sourceFiles = Directory.GetFiles(sourceFolder, "*.rtf");
            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile =
                    Path.Combine(destinationFolder, Path.GetFileNameWithoutExtension(sourceFile) + ".txt");
                using (RichTextBox rtb = new RichTextBox())
                {
                    rtb.LoadFile(sourceFile, RichTextBoxStreamType.RichText);
                    File.WriteAllText(destinationFile, rtb.Text.Replace("\n", Environment.NewLine), Encoding.GetEncoding(1251));
                }
            }
        }
    }
}
