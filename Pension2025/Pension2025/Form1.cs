using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pension2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnParseList_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            await Task.Factory.StartNew(Actions.ParseList.Run);
            ((Control)sender).Enabled = true;
        }

        private async void btnParseDetails_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            await Task.Factory.StartNew(Actions.ParseDetails.Run);
            ((Control)sender).Enabled = true;
        }

        private async void btnRemoveUselessTagsOfDetails_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            await Task.Factory.StartNew(Actions.RemoveUselessTagsOfDetails.Run);
            ((Control)sender).Enabled = true;
        }
    }
}