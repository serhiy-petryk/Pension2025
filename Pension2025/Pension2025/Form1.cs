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
            btnParseList.Enabled = false;
            await Task.Factory.StartNew(Actions.ParseList.Run);
            btnParseList.Enabled = true;
        }

        private async void btnParseDetails_Click(object sender, System.EventArgs e)
        {
            btnParseList.Enabled = false;
            await Task.Factory.StartNew(Actions.ParseDetails.Run);
            btnParseList.Enabled = true;
        }
    }
}