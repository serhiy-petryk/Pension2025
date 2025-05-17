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

        private async void btnParseList01_Click(object sender, System.EventArgs e)
        {
            btnParseList01.Enabled = false;
            await Task.Factory.StartNew(Actions.ParseList.Run);
            btnParseList01.Enabled = true;
        }

    }
}