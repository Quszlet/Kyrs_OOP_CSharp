using System.ComponentModel;
using System.Text.RegularExpressions;
using Kyrs_OOP_CSharp.repository;

namespace Kyrs_OOP_CSharp
{
    public partial class BooksDBForm : Form
    {
        private BookRepository BookRepository;
        public string FilePathDB;
        private int idChange = 1;
        private int indRow = 0;

        public BooksDBForm(string filePath)
        {
            InitializeComponent();
            FilePathDB = filePath;
            this.BookRepository = new BookRepository(filePath);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void BooksDBForm_Load(object sender, EventArgs e)
        {
            BookRepository.GetAllBooks(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bookName, authorName, authorSurname, theme;
            Book book;
            switch (comboBox2.SelectedIndex) {
                case 0:
                    bookName = textBox2.Text;
                    authorName = textBox3.Text;
                    authorSurname = textBox4.Text;
                    theme = textBox5.Text;
                    book = new Book(bookName, authorName, authorSurname, theme);
                    BookRepository.SaveBook(book);
                    BookRepository.GetAllBooks(dataGridView1);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Selected = true;
                    MessageBox.Show("Новая книга добавлена", "Учет книг", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    bookName = textBox2.Text;
                    authorName = textBox3.Text;
                    authorSurname = textBox4.Text;
                    theme = textBox5.Text;
                    book = new Book(idChange, bookName, authorName, authorSurname, theme);
                    BookRepository.UpdateBook(book);
                    BookRepository.GetAllBooks(dataGridView1);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[indRow].Selected = true;
                    MessageBox.Show("Книга изменена", "Учет книг", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    bookName = textBox2.Text;
                    authorName = textBox3.Text;
                    authorSurname = textBox4.Text;
                    theme = textBox5.Text;
                    book = new Book(idChange, bookName, authorName, authorSurname, theme);
                    BookRepository.FindBooks(dataGridView1, bookName, authorName, authorSurname, theme);
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    button1.Text = "Добавить";
                    break;
                case 1:
                    button1.Text = "Изменить";
                    break;
                case 2:
                    button1.Text = "Найти";
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BookRepository.DeleteBook(idChange);
            BookRepository.GetAllBooks(dataGridView1);
            MessageBox.Show("Книга удалена", "Учет книг", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                label9.Text = "Фильрация включена";
                label9.ForeColor = Color.Green;
                string parameter = "";
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        parameter = "book_name";
                        break;
                    case 1:
                        parameter = "author_name";
                        break;
                    case 2:
                        parameter = "author_surname";
                        break;
                    case 3:
                        parameter = "theme";
                        break;
                }
                BookRepository.FiltrationBooks(dataGridView1, parameter, textBox1.Text);
            }
            else
            {
                label9.Text = "Фильрация отключена";
                label9.ForeColor = Color.Red;
                BookRepository.GetAllBooks(dataGridView1);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dataGridView = (DataGridView)sender;
                DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
                textBox2.Text = dataGridViewRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridViewRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridViewRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridViewRow.Cells[4].Value.ToString();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                indRow = selectedRow.Index;
                idChange = Convert.ToInt32(dataGridView1.Rows[indRow].Cells[0].Value);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseDBForm chooseDBForm = new ChooseDBForm(FilePathDB);
            chooseDBForm.Show();
            this.Close();
        }

       
        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Regex regexString = new(@"^[\p{IsCyrillic}0-9]+[\-\s]{0,1}[\p{IsCyrillic}0-9]*$", RegexOptions.IgnorePatternWhitespace);

            if (!regexString.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                MessageBox.Show("Ввести необходимо буквы русского алфавита(допускаются цифры)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.BackColor = Color.Red;
            }
            else
            {
                textBox.BackColor = Color.Green;
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Regex regexString = new(@"^\p{IsCyrillic}+$", RegexOptions.IgnorePatternWhitespace);

            if (!regexString.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                MessageBox.Show("Ввести необходимо буквы русского алфавита", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.BackColor = Color.Red;
            }
            else
            {
                textBox.BackColor = Color.Green;
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Regex regexString = new(@"^\p{IsCyrillic}+$", RegexOptions.IgnorePatternWhitespace);

            if (!regexString.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                MessageBox.Show("Ввести необходимо буквы русского алфавита", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.BackColor = Color.Red;
            }
            else
            {
                textBox.BackColor = Color.Green;
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            Regex regexString = new(@"^\p{IsCyrillic}+$", RegexOptions.IgnorePatternWhitespace);

            if (!regexString.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                MessageBox.Show("Ввести необходимо буквы русского алфавита", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.BackColor = Color.Red;
            }
            else
            {
                textBox.BackColor = Color.Green;
            }
        }
    }
}
