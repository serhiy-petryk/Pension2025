using System.IO;
using System.Windows.Forms;

namespace Pension2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnParseList01_Click(object sender, System.EventArgs e)
        {
            var folder = Path.Combine(Settings.DataFolder, @"List01");
            var files = Directory.GetFiles(folder, "*.html");
            foreach (var file in files)
            {
                var content = File.ReadAllText(file);
            }
        }
    }
}