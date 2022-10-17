using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
    public partial class AddSpecialtyForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddSpecialtyForm()
        {
            InitializeComponent();
        }

        private void AddSpecialtyForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void buttonAddSpecialty_Click(object sender, EventArgs e)
        {
            if ((textBoxSpecialtyCode.Text != "") && (textBoxSpecialtyName.Text != ""))
            {
                if (textBoxSpecialtyCode.Text.Length == 8)
                {
                    Boolean check = true;
                    for (int i = 0; i < 8; i++)
                    {
                        if ((i == 2) || (i == 5))
                        {
                            if (textBoxSpecialtyCode.Text[i] != '.')
                            {
                                check = false;
                                MessageBox.Show("Код должен иметь форму: 'XX.XX.XX', где X - любая цифра");
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if ((textBoxSpecialtyCode.Text[i] == '0') || (textBoxSpecialtyCode.Text[i] == '1') || (textBoxSpecialtyCode.Text[i] == '2') || (textBoxSpecialtyCode.Text[i] == '3') || (textBoxSpecialtyCode.Text[i] == '4') || (textBoxSpecialtyCode.Text[i] == '5') || (textBoxSpecialtyCode.Text[i] == '6') || (textBoxSpecialtyCode.Text[i] == '7') || (textBoxSpecialtyCode.Text[i] == '8') || (textBoxSpecialtyCode.Text[i] == '9'))
                            {
                                continue;
                            }
                            else
                            {
                                check = false;
                                MessageBox.Show("Код должен иметь форму: 'XX.XX.XX', где X - любая цифра");
                                break;
                            }
                        }
                    }
                    if (check)
                    {
                        SqlCommand command = new SqlCommand($"INSERT INTO [Specialty] (SpecialtyCode, SpecialtyName)" +
                            "VALUES (@SpecialtyCode, @SpecialtyName)", sqlConnection);
                        command.Parameters.AddWithValue("SpecialtyCode", textBoxSpecialtyCode.Text);
                        command.Parameters.AddWithValue("SpecialtyName", textBoxSpecialtyName.Text);
                        try
                        {
                            command.ExecuteNonQuery().ToString();
                            MessageBox.Show("Специальность добавлена в базу данных");
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Такая специальность уже существует!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Код должен иметь форму: 'XX.XX.XX', где X - любая цифра");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }
    }
}
