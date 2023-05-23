using Microsoft.VisualBasic.ApplicationServices;

namespace Kyrs_OOP_CSharp
{
    public partial class MainForm : Form
    {
        private Repository repository;
        public MainForm()
        {
            InitializeComponent();
            repository = new Repository("C:\\Users\\izgar\\source\\repos\\Kyrs_OOP_CSharp\\Kyrs_OOP_CSharp\\database\\libraryDB");
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
            Hide();
            ChooseDBForm libraryFomr = new ChooseDBForm(repository);
            libraryFomr.Show();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = "https://github.com/Quszlet/Kyrs_OOP_CSharp";
            System.Diagnostics.Process.Start(psi);
        }
    }
}