using Kyrs_OOP_CSharp.repository;
using Microsoft.VisualBasic.ApplicationServices;

namespace Kyrs_OOP_CSharp
{
    public partial class MainForm : Form
    {
        private Repository Repository;
        private string FilePathDB;
        public MainForm()
        {
            InitializeComponent();
            FilePathDB = System.IO.Path.GetFullPath("database");
            Repository = new Repository(FilePathDB);
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
            ChooseDBForm libraryFomr = new ChooseDBForm(FilePathDB);
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