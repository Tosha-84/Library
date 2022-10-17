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
    public partial class EndIssuanceForm : Form
    {
        private SqlConnection sqlConnection = null;
        public EndIssuanceForm()
        {
            InitializeComponent();
        }

        public void DataForIssuance(String id, String issuanceNumber, String bookInventoryNumber, String bookName, String bookAuthor, String publisherName, String dateStart)
        {
            textBoxId.Text = id;
            textBoxIssuanceNumber.Text = issuanceNumber;
            textBoxBookInventoryNumber.Text = bookInventoryNumber;
            textBoxBookName.Text = bookName;
            textBoxBookAuthor.Text = bookAuthor;
            textBoxPublisherName.Text = publisherName;
            dateTimePicker.Value = DateTime.Parse(dateStart);
        }

        private void EndIssuanceForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();


            //dateTimePicker.Value = DateTime.Parse("20.01.2021");
        }

        private void comboBoxEndDateYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEndDateMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePickerEnd.Text != "")
            {
                DateTime dateEnd = dateTimePickerEnd.Value;
                Console.WriteLine(dateEnd);
                Console.WriteLine(dateEnd.GetType());
                Console.WriteLine(dateEnd.ToString());
                Console.WriteLine($"u: {dateEnd:u}");
                Console.WriteLine($"d: {dateEnd.ToString("d")}");
                Console.WriteLine(dateEnd.ToString("yyyy.MM.dd"));

                if (dateEnd >= dateTimePicker.Value)
                {
                    SqlCommand command = new SqlCommand($"UPDATE Issuances SET IssuanceDateEnd = '" + dateEnd.ToString("yyyy.MM.dd") + "' WHERE IssuanceNumber = " + textBoxIssuanceNumber.Text, sqlConnection);
                    command.ExecuteNonQuery().ToString();

                    MessageBox.Show("Выдача закрыта");
                    this.Close();
                }
                else MessageBox.Show("Дата закрытиия должна быть позже даты открытия или равна ей");
            }
            else
            {
                MessageBox.Show("Необходимо выбрать дату закрытия");
            }
        }
    }
}
