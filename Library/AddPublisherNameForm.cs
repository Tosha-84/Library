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
    public partial class AddPublisherNameForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddPublisherNameForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddPublisherNameForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void buttonAddPublisher_Click(object sender, EventArgs e)
        {
            if ((textBoxPublisherName.Text != "") && (textBoxPublishingPlace.Text != "") && (textBoxPublishingYear.Text != ""))
            {
                Boolean check = true;
                try
                {
                    Convert.ToInt32(textBoxPublishingYear.Text);
                }
                catch
                {
                    check = false;
                    MessageBox.Show("Год должен быть выражен числом!");
                }
                if (check)
                {
                    String fullname = "";
                    fullname += textBoxPublisherName.Text + ", " + textBoxPublishingPlace.Text + ", " + textBoxPublishingYear.Text;
                    SqlCommand command = new SqlCommand($"INSERT INTO [Publishers] (PublisherName, PublisherShortName, PublishingPlace, PublishingYear)" +
                    "VALUES (@PublisherName, @PublisherShortName, @PublishingPlace, @PublishingYear)", sqlConnection);
                    command.Parameters.AddWithValue("PublisherName", fullname);
                    command.Parameters.AddWithValue("PublisherShortName", textBoxPublisherName.Text);
                    command.Parameters.AddWithValue("PublishingPlace", textBoxPublishingPlace.Text);
                    command.Parameters.AddWithValue("PublishingYear", textBoxPublishingYear.Text);
                    command.ExecuteNonQuery().ToString();
                    MessageBox.Show("Издательство добавлено в базу данных");
                    this.Close();
                }    
            }
            else
            {
                MessageBox.Show("Все поля долджны быть заполнены!");
            }
        }
    }
}
