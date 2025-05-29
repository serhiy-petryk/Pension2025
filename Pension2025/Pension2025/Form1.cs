using System;
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

        private void btnParseList_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.ParseList.Run, (Control)sender);

        private void btnParseDetails_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.ParseDetails.Run, (Control)sender);

        private void btnRemoveUselessTagsOfDetails_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.RemoveUselessTagsOfDetails.Run, (Control)sender);

        private void btnHttpList_Api_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.ParseApiList.PrintUrlList, (Control)sender);

        private void btnSaveToListJson_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.ParseApiList.SaveToListFile, (Control)sender);

        private void btnCreateDsaLinkList_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.DsaList.CreateDsaLinkList, (Control)sender);

        private void btnHttpList_Dsa_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.DsaList.PrintDsaLinkList, (Control)sender);

        private void btnConvertRtfToTxt_Click(object sender, System.EventArgs e) =>
            RunAction(Actions.DsaDetails.ConvertRtfFilesToTxt, (Control)sender);

        private async void RunAction(Action action, Control control)
        {
            control.Enabled = false;
            await Task.Factory.StartNew(action);
            control.Enabled = true;
            MessageBox.Show(@"Done!");
        }
    }
}