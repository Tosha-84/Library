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
    public partial class EditUserForm : Form
    {
        private SqlConnection sqlConnection = null;
        public EditUserForm()
        {
            InitializeComponent();
        }
        public void DataForEdit(String id, String password, String role, String fullName, String phoneNumber)
        {
            textBoxId.Text = id;
            textBoxPassword.Text = password;
            comboBoxRole.Text = role;
            textBoxFullName.Text = fullName;
            textBoxPhoneNumber.Text = phoneNumber;
        }
        private void EditUserForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
            comboBoxRole.Items.Add("Читатель");
            comboBoxRole.Items.Add("Книговед");
            comboBoxRole.Items.Add("Библиотекарь");
        }

        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            if ((textBoxPassword.Text != "") && (comboBoxRole.Text != "") && (textBoxFullName.Text != "") && (textBoxPhoneNumber.Text != ""))
            {
                Boolean check = true;
                String error = "";
                for (int i = 0; i < textBoxPhoneNumber.Text.Length; i++)
                {
                    if ((textBoxPhoneNumber.Text[i] == '0') || (textBoxPhoneNumber.Text[i] == '1') || (textBoxPhoneNumber.Text[i] == '2') || (textBoxPhoneNumber.Text[i] == '3') || (textBoxPhoneNumber.Text[i] == '4') || (textBoxPhoneNumber.Text[i] == '5') || (textBoxPhoneNumber.Text[i] == '6') || (textBoxPhoneNumber.Text[i] == '7') || (textBoxPhoneNumber.Text[i] == '8') || (textBoxPhoneNumber.Text[i] == '9'))
                    {
                        continue;
                    }
                    else
                    {
                        error = error + "Телефонный номер должен состоять из цифр\n";
                        check = false;
                        break;
                    }
                }
                if ((comboBoxRole.Text != "Читатель") && (comboBoxRole.Text != "Книговед") && (comboBoxRole.Text != "Библиотекарь"))
                {
                    error = error + "Выберите одну из допустимых ролей\n";
                    check = false;
                }
                if (check)
                {
                    String role = "";
                    if (comboBoxRole.Text == "Читатель")
                    {
                        role = "Ч";
                    }
                    else
                    {
                        if (comboBoxRole.Text == "Книговед")
                        {
                            role = "К";
                        }
                        else
                        {
                            role = "Б";
                        }
                    }

                    try
                    {
                        String comand_for_sql = "UPDATE Users SET Password = ";
                        comand_for_sql = comand_for_sql + "N" + "'" + textBoxPassword.Text + "', " + "Role = ";
                        comand_for_sql = comand_for_sql + "N" + "'" + role + "', " + "FullName = ";
                        comand_for_sql = comand_for_sql + "N" + "'" + textBoxFullName.Text + "', " + "PhoneNumber = ";
                        comand_for_sql = comand_for_sql + "N" + "'" + textBoxPhoneNumber.Text + "'" + " WHERE Id = ";
                        comand_for_sql = comand_for_sql + textBoxId.Text;
                        Console.WriteLine(comand_for_sql);
                        SqlCommand sqlCommand = new SqlCommand(comand_for_sql, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Редактировано");
                    }
                    catch
                    {
                        MessageBox.Show("Такое редактирование невозможно, так как возникнут пользователи с совпадающими ролями и номерами телефонов");
                    }

                }
                else
                {
                    MessageBox.Show(error);
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
            this.Close();
        }
    }
}
