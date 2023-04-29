namespace Kyrs_OOP_CSharp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LibraryForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = "https://github.com/Quszlet";
            System.Diagnostics.Process.Start(psi);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StartWork_Click(object sender, EventArgs e)
        {

        }
    }
}