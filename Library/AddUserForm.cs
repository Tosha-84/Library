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
    public partial class AddUserForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
            comboBoxRole.Items.Add("Читатель");
            comboBoxRole.Items.Add("Книговед");
            comboBoxRole.Items.Add("Библиотекарь");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddUser_Click(object sender, EventArgs e)
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
                    if(comboBoxRole.Text == "Читатель")
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
                        SqlCommand command = new SqlCommand($"INSERT INTO [Users] (Password, Role, FullName, PhoneNumber)" +
                                    "VALUES (@Password, @Role, @FullName, @PhoneNumber)", sqlConnection);
                        command.Parameters.AddWithValue("Password", textBoxPassword.Text);
                        command.Parameters.AddWithValue("Role", role);
                        command.Parameters.AddWithValue("FullName", textBoxFullName.Text);
                        command.Parameters.AddWithValue("PhoneNumber", textBoxPhoneNumber.Text);
                        command.ExecuteNonQuery().ToString();

                        String sqlcomand = "SELECT Id FROM USERS WHERE PhoneNumber = " + "'" + textBoxPhoneNumber.Text + "' AND FullName = " + "'" + textBoxFullName.Text + "'";
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        //Console.WriteLine(dataSet.Tables[0].Rows[0].ItemArray[0]);
                        String id = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();

                        MessageBox.Show("Пользователь добавлен в базу данных\nЕго Id: " + id);
                    }
                    catch
                    {
                        String sqlcomand = "SELECT Id FROM USERS WHERE PhoneNumber = " + "'" + textBoxPhoneNumber.Text + "'";
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        //Console.WriteLine(dataSet.Tables[0].Rows[0].ItemArray[0]);
                        String id = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
                        MessageBox.Show("Пользователь с таким же именем и таким номером телефона уже существует\nЕго Id: " + id);
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
