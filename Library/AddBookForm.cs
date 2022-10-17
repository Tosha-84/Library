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
    public partial class AddBookForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddBookForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAddBook_Click(object sender, EventArgs e)
        {
            if ((textBoxBookName.Text != "") && (textBoxBookAuthor.Text != "") && (comboBoxPublisherName.Text != "") && (textBoxNumberOfPages.Text != "") && (textBoxPrice.Text != "") && (textBoxQuantity.Text != "") && (checkedListBoxDisciplines.CheckedItems.Count != 0))
            {
                Boolean check = true;
                string error = "";

                for (int i = 0; i < textBoxNumberOfPages.Text.Length; i++)
                {
                    if ((textBoxNumberOfPages.Text[i] == '0') || (textBoxNumberOfPages.Text[i] == '1') || (textBoxNumberOfPages.Text[i] == '2') || (textBoxNumberOfPages.Text[i] == '3') || (textBoxNumberOfPages.Text[i] == '4') || (textBoxNumberOfPages.Text[i] == '5') || (textBoxNumberOfPages.Text[i] == '6') || (textBoxNumberOfPages.Text[i] == '7') || (textBoxNumberOfPages.Text[i] == '8') || (textBoxNumberOfPages.Text[i] == '9'))
                    {
                        continue;
                    }
                    else
                    {
                        error = error + "Количество страниц должно быть указано числом\n";
                        check = false;
                        break;
                    }
                }
                for (int i = 0; i < textBoxPrice.Text.Length; i++)
                {
                    if ((textBoxPrice.Text[i] == '0') || (textBoxPrice.Text[i] == '1') || (textBoxPrice.Text[i] == '2') || (textBoxPrice.Text[i] == '3') || (textBoxPrice.Text[i] == '4') || (textBoxPrice.Text[i] == '5') || (textBoxPrice.Text[i] == '6') || (textBoxPrice.Text[i] == '7') || (textBoxPrice.Text[i] == '8') || (textBoxPrice.Text[i] == '9'))
                    {
                        continue;
                    }
                    else
                    {
                        error = error + "Цена должна быть указана числом\n";
                        check = false;
                        break;
                    }
                }
                for (int i = 0; i < textBoxQuantity.Text.Length; i++)
                {
                    if ((textBoxQuantity.Text[i] == '0') || (textBoxQuantity.Text[i] == '1') || (textBoxQuantity.Text[i] == '2') || (textBoxQuantity.Text[i] == '3') || (textBoxQuantity.Text[i] == '4') || (textBoxQuantity.Text[i] == '5') || (textBoxQuantity.Text[i] == '6') || (textBoxQuantity.Text[i] == '7') || (textBoxQuantity.Text[i] == '8') || (textBoxQuantity.Text[i] == '9'))
                    {
                        continue;
                    }
                    else
                    {
                        error = error + "Количество книг должно быть указано числом\n";
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    SqlCommand command = new SqlCommand($"INSERT INTO [Books] (PublisherName, BookName, BookAuthor, NumberOfPages, Price, Quantity)" +
            "VALUES (@PublisherName, @BookName, @BookAuthor, @NumberOfPages, @Price, @Quantity)", sqlConnection);
                    command.Parameters.AddWithValue("PublisherName", comboBoxPublisherName.Text);
                    command.Parameters.AddWithValue("BookName", textBoxBookName.Text);
                    command.Parameters.AddWithValue("BookAuthor", textBoxBookAuthor.Text);
                    command.Parameters.AddWithValue("NumberOfPages", textBoxNumberOfPages.Text);
                    command.Parameters.AddWithValue("Price", textBoxPrice.Text);
                    command.Parameters.AddWithValue("Quantity", textBoxQuantity.Text);
                    for (int i = 0; i < Convert.ToInt32(textBoxQuantity.Text); i++)
                    {
                        command.ExecuteNonQuery().ToString();
                    }


                    SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT BookInventoryNumber FROM Books WHERE PublisherName = N'" + comboBoxPublisherName.Text + "' AND " + 
                        "BookName = N'" + textBoxBookName.Text + "' AND " + 
                        "BookAuthor = N'" + textBoxBookAuthor.Text + "' AND " + 
                        "NumberOfPages = N'" + textBoxNumberOfPages.Text + "' AND " +
                        "Price = N'" + textBoxPrice.Text + "' AND " +
                        "Quantity = N'" + textBoxQuantity.Text + "'", sqlConnection);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    Console.WriteLine("Строк в получившемся Датасете: " + dataSet.Tables[0].Rows.Count);
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < checkedListBoxDisciplines.CheckedItems.Count; j++)
                        {
                            Console.WriteLine("Инвентарный номер: " + dataSet.Tables[0].Rows[i].ItemArray[0] + " Дисциплина: " + checkedListBoxDisciplines.CheckedItems[j]);
                            SqlCommand command1 = new SqlCommand($"INSERT INTO [BookDiscipline] (BookInventoryNumber, DisciplineName)" +
                                "VALUES (@BookInventoryNumber, @DisciplineName)", sqlConnection);
                            command1.Parameters.AddWithValue("BookInventoryNumber", dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
                            command1.Parameters.AddWithValue("DisciplineName", checkedListBoxDisciplines.CheckedItems[j].ToString());
                            command1.ExecuteNonQuery().ToString();
                        }
                    }
                    MessageBox.Show("Книга добавлена в базу данных");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(error);
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены, и должна быть выбрана хотя бы одна дисциплина!");
            }
        }

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void comboBoxPublisherName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPublisherName.Text == "Добавить издание")
            {
                AddPublisherNameForm addPublisherNameForm = new AddPublisherNameForm();
                addPublisherNameForm.ShowDialog();
            }
        }

        private void AddBookForm_Activated(object sender, EventArgs e)
        {
            checkedListBoxDisciplines.Items.Clear();
            comboBoxPublisherName.Items.Clear();
            comboBoxPublisherName.SelectedIndex = -1;

            String sqlcomand = "SELECT PublisherName FROM Publishers";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                comboBoxPublisherName.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
                Console.WriteLine(dataSet.Tables[0].Rows[i].ItemArray[0]);
            }
            comboBoxPublisherName.Items.Add("Добавить издание");

            String sqlcomand1 = "SELECT DisciplineName FROM Disciplines";
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(sqlcomand1, sqlConnection);
            DataSet dataSet1 = new DataSet();
            dataAdapter1.Fill(dataSet1);
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                checkedListBoxDisciplines.Items.Add(dataSet1.Tables[0].Rows[i].ItemArray[0].ToString());
            }

        }

        private void AddBookForm_Enter(object sender, EventArgs e)
        {
            // comboBoxPublisherName.SelectedIndex = -1;
        }
    }
}
