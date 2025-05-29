using System.IO;
using System.Linq;

namespace Pension2025.Actions
{
    public static class RemoveUselessTagsOfDetails
    {
        public static void Run()
        {
            var folder = Path.Combine(Settings.DataFolder, "DetailsApi");
            var newFolder = Path.Combine(Settings.DataFolder, "DetailsApi01");
            if (!Directory.Exists(newFolder))
                Directory.CreateDirectory(newFolder);

            var files = Directory.GetFiles(folder, "*.html").OrderBy(File.GetLastWriteTime).ToArray();
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
                var newContent = Helpers.HtmlUtilities.RemoveUselessTags(content);
                var newFileName = Path.Combine(newFolder, Path.GetFileName(file));
                File.WriteAllText(newFileName, newContent);
            }


        }
    }
}
