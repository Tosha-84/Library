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
    public partial class AddIssuanceForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddIssuanceForm()
        {
            InitializeComponent();
        }

        public void DataForNewIssuance(int id)
        {
            textBoxId.Text = id.ToString();
        }

        private void CheckAvailability()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT BookInventoryNumber FROM Books WHERE PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "' AND " + "BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "' AND " + "BookName = N'" + comboBoxBookName.Text.ToString() + "'" + "EXCEPT SELECT BookInventoryNumber FROM Issuances WHERE IssuanceDateEnd IS NULL", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            /*for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
            }*/
            try
            {
                //Console.WriteLine(dataSet.Tables[0].Rows[0].ItemArray[0].ToString());
                comboBoxBookInventoryNumber.Text = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch
            {
                //Console.WriteLine("Тут ничего нет");
                comboBoxBookInventoryNumber.Text = "Нет в наличии";
            }
            /*if (dataSet.Tables[0].Rows[0].ItemArray[0].ToString() == "")
            {
                Console.WriteLine("Тут ничего нет");
            }
            else
            {
                Console.WriteLine(dataSet.Tables[0].Rows[0].ItemArray[0].ToString());
            }*/
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddIssuanceForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();

            /*
            comboBoxDateYear.Text = "2021";
            comboBoxDateMonth.Enabled = true;
            comboBoxDateMonth.Items.Add("Январь");
            comboBoxDateMonth.Items.Add("Февраль");
            comboBoxDateMonth.Items.Add("Март");
            comboBoxDateMonth.Items.Add("Апрель");
            comboBoxDateMonth.Items.Add("Май");
            comboBoxDateMonth.Items.Add("Июнь");
            comboBoxDateMonth.Items.Add("Июль");
            comboBoxDateMonth.Items.Add("Август");
            comboBoxDateMonth.Items.Add("Сентябрь");
            comboBoxDateMonth.Items.Add("Октябрь");
            comboBoxDateMonth.Items.Add("Ноябрь");
            comboBoxDateMonth.Items.Add("Декабрь");
            for (int i = 2020; i < 2100; i++)
            {
                comboBoxDateYear.Items.Add(i.ToString());
            }
            */

            String sqlcomand = "SELECT BookInventoryNumber FROM Books";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine(dataSet.Tables[0].Rows[i].ItemArray[0]);
            }

            sqlcomand = "SELECT BookInventoryNumber, IssuanceDateStart, IssuanceDateEnd FROM Issuances";
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet1 = new DataSet();
            dataAdapter1.Fill(dataSet1);
            int countDataEnd = 0;
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                Console.WriteLine(dataSet1.Tables[0].Rows[i].ItemArray[0].ToString());
                Console.WriteLine(dataSet1.Tables[0].Rows[i].ItemArray[1].ToString());
                Console.WriteLine(dataSet1.Tables[0].Rows[i].ItemArray[2].ToString());


                if(dataSet1.Tables[0].Rows[i].ItemArray[2].ToString() != "")
                {
                    countDataEnd += 1;
                }

            }
            Console.WriteLine(countDataEnd);

            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("SELECT DISTINCT PublisherName FROM Books", sqlConnection);
            DataSet dataSet3 = new DataSet();
            dataAdapter3.Fill(dataSet3);
            for (int i = 0; i < dataSet3.Tables[0].Rows.Count; i++)
            {
                comboBoxPublisherName.Items.Add(dataSet3.Tables[0].Rows[i].ItemArray[0]);
            }

            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT BookAuthor FROM Books", sqlConnection);
            DataSet dataSet4 = new DataSet();
            dataAdapter4.Fill(dataSet4);
            for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
            {
                comboBoxBookAuthor.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
            }

            SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookName FROM Books", sqlConnection);
            DataSet dataSet5 = new DataSet();
            dataAdapter5.Fill(dataSet5);
            for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
            {
                comboBoxBookName.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
            }

            SqlDataAdapter dataAdapter6 = new SqlDataAdapter("SELECT BookInventoryNumber FROM Books EXCEPT SELECT BookInventoryNumber FROM Issuances WHERE IssuanceDateEnd IS NULL", sqlConnection);
            DataSet dataSet6 = new DataSet();
            dataAdapter6.Fill(dataSet6);
            for (int i = 0; i < dataSet6.Tables[0].Rows.Count; i++)
            {
                comboBoxBookInventoryNumber.Items.Add(dataSet6.Tables[0].Rows[i].ItemArray[0]);
            }
        }

        private void AddIssuanceForm_Activated(object sender, EventArgs e)
        {

        }

        private void comboBoxDateYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            comboBoxDateDay.Items.Clear();
            comboBoxDateMonth.Text = "";
            comboBoxDateMonth.Enabled = true;
            comboBoxDateDay.Text = "";
            comboBoxDateDay.Enabled = false;
            */
        }

        private void comboBoxDateMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            bool leap = false;
            comboBoxDateMonth.Enabled = true;
            for (int i = 2020; i < 2100; i += 4)
            {
                if (Convert.ToInt32(comboBoxDateYear.Text) == i)
                {
                    leap = true;
                }
            }
            comboBoxDateDay.Items.Clear();
            if (leap)
            {
                if((comboBoxDateMonth.Text == "Январь") || (comboBoxDateMonth.Text == "Март") || (comboBoxDateMonth.Text == "Май") || (comboBoxDateMonth.Text == "Июль") || (comboBoxDateMonth.Text == "Август") || (comboBoxDateMonth.Text == "Октябрь") || (comboBoxDateMonth.Text == "Декабрь"))
                {
                    for (int i = 1; i < 32; i++)
                    {
                        comboBoxDateDay.Items.Add(i.ToString());
                    }
                }
                else
                {
                    if ((comboBoxDateMonth.Text == "Апрель") || (comboBoxDateMonth.Text == "Июнь") || (comboBoxDateMonth.Text == "Сентябрь") || (comboBoxDateMonth.Text == "Ноябрь"))
                    {
                        for (int i = 1; i < 31; i++)
                        {
                            comboBoxDateDay.Items.Add(i.ToString());
                        }
                    }
                    else
                    {
                        for (int i = 1; i < 30; i++)
                        {
                            comboBoxDateDay.Items.Add(i.ToString());
                        }
                    }
                }
            }
            else
            {
                if ((comboBoxDateMonth.Text == "Январь") || (comboBoxDateMonth.Text == "Март") || (comboBoxDateMonth.Text == "Май") || (comboBoxDateMonth.Text == "Июль") || (comboBoxDateMonth.Text == "Август") || (comboBoxDateMonth.Text == "Октябрь") || (comboBoxDateMonth.Text == "Декабрь"))
                {
                    for (int i = 1; i < 32; i++)
                    {
                        comboBoxDateDay.Items.Add(i.ToString());
                    }
                }
                else
                {
                    if ((comboBoxDateMonth.Text == "Апрель") || (comboBoxDateMonth.Text == "Июнь") || (comboBoxDateMonth.Text == "Сентябрь") || (comboBoxDateMonth.Text == "Ноябрь"))
                    {
                        for (int i = 1; i < 31; i++)
                        {
                            comboBoxDateDay.Items.Add(i.ToString());
                        }
                    }
                    else
                    {
                        for (int i = 1; i < 29; i++)
                        {
                            comboBoxDateDay.Items.Add(i.ToString());
                        }
                    }
                }
            }
            comboBoxDateDay.Text = "";
            comboBoxDateDay.Enabled = true;
            */
        }

        private void comboBoxPublisherName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxBookInventoryNumber.Enabled = false;
            if ((comboBoxBookAuthor.Enabled == true) && (comboBoxBookName.Enabled == true))
            {
                comboBoxBookAuthor.Items.Clear();
                SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT BookAuthor FROM Books WHERE PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "'", sqlConnection);
                DataSet dataSet4 = new DataSet();
                dataAdapter4.Fill(dataSet4);
                for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
                {
                    comboBoxBookAuthor.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
                }
                comboBoxBookName.Items.Clear();
                SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookName FROM Books WHERE PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "'", sqlConnection);
                DataSet dataSet5 = new DataSet();
                dataAdapter5.Fill(dataSet5);
                for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
                {
                    comboBoxBookName.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
                }
                comboBoxPublisherName.Enabled = false;
            }
            else
            {
                if (comboBoxBookAuthor.Enabled == true)
                {
                    comboBoxBookAuthor.Items.Clear();
                    SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT BookAuthor FROM Books WHERE PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "' AND " + "BookName = N'" + comboBoxBookName.Text.ToString() + "'", sqlConnection);
                    DataSet dataSet4 = new DataSet();
                    dataAdapter4.Fill(dataSet4);
                    for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
                    {
                        comboBoxBookAuthor.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
                    }
                    comboBoxPublisherName.Enabled = false;
                }
                else
                {
                    if (comboBoxBookName.Enabled == true)
                    {
                        comboBoxBookName.Items.Clear();
                        SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookName FROM Books WHERE PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "' AND " + "BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "'", sqlConnection);
                        DataSet dataSet5 = new DataSet();
                        dataAdapter5.Fill(dataSet5);
                        for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
                        {
                            comboBoxBookName.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
                        }
                        comboBoxPublisherName.Enabled = false;
                    }
                    else
                    {
                        comboBoxPublisherName.Enabled = false;
                        CheckAvailability();
                    }
                }
            }
        }

        private void comboBoxBookAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxBookInventoryNumber.Enabled = false;
            if ((comboBoxPublisherName.Enabled == true) && (comboBoxBookName.Enabled == true))
            {
                comboBoxPublisherName.Items.Clear();
                SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT PublisherName FROM Books WHERE BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "'", sqlConnection);
                DataSet dataSet4 = new DataSet();
                dataAdapter4.Fill(dataSet4);
                for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
                {
                    comboBoxPublisherName.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
                }
                comboBoxBookName.Items.Clear();
                SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookName FROM Books WHERE BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "'", sqlConnection);
                DataSet dataSet5 = new DataSet();
                dataAdapter5.Fill(dataSet5);
                for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
                {
                    comboBoxBookName.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
                }
                comboBoxBookAuthor.Enabled = false;
            }
            else
            {
                if (comboBoxPublisherName.Enabled == true)
                {
                    comboBoxPublisherName.Items.Clear();
                    SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT PublisherName FROM Books WHERE BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "' AND " + "BookName = N'" + comboBoxBookName.Text.ToString() + "'", sqlConnection);
                    DataSet dataSet4 = new DataSet();
                    dataAdapter4.Fill(dataSet4);
                    for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
                    {
                        comboBoxPublisherName.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
                    }
                    comboBoxBookAuthor.Enabled = false;
                }
                else
                {
                    if (comboBoxBookName.Enabled == true)
                    {
                        comboBoxBookName.Items.Clear();
                        SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookName FROM Books WHERE BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "' AND " + "PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "'", sqlConnection);
                        DataSet dataSet5 = new DataSet();
                        dataAdapter5.Fill(dataSet5);
                        for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
                        {
                            comboBoxBookName.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
                        }
                        comboBoxBookAuthor.Enabled = false;
                    }
                    else
                    {
                        comboBoxBookAuthor.Enabled = false;
                        CheckAvailability();
                    }
                }
            }
        }

        private void comboBoxBookName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxBookInventoryNumber.Enabled = false;
            if ((comboBoxPublisherName.Enabled == true) && (comboBoxBookAuthor.Enabled == true))
            {
                comboBoxPublisherName.Items.Clear();
                SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT PublisherName FROM Books WHERE BookName = N'" + comboBoxBookName.Text.ToString() + "'", sqlConnection);
                DataSet dataSet4 = new DataSet();
                dataAdapter4.Fill(dataSet4);
                for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
                {
                    comboBoxPublisherName.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
                }
                comboBoxBookAuthor.Items.Clear();
                SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookAuthor FROM Books WHERE BookName = N'" + comboBoxBookName.Text.ToString() + "'", sqlConnection);
                DataSet dataSet5 = new DataSet();
                dataAdapter5.Fill(dataSet5);
                for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
                {
                    comboBoxBookAuthor.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
                }
                comboBoxBookName.Enabled = false;
            }
            else
            {
                if (comboBoxPublisherName.Enabled == true)
                {
                    comboBoxPublisherName.Items.Clear();
                    SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT PublisherName FROM Books WHERE BookName = N'" + comboBoxBookName.Text.ToString() + "' AND " + "BookAuthor = N'" + comboBoxBookAuthor.Text.ToString() + "'", sqlConnection);
                    DataSet dataSet4 = new DataSet();
                    dataAdapter4.Fill(dataSet4);
                    for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
                    {
                        comboBoxPublisherName.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
                    }
                    comboBoxBookName.Enabled=false;
                }
                else
                {
                    if (comboBoxBookAuthor.Enabled == true)
                    {
                        comboBoxBookAuthor.Items.Clear();
                        SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookAuthor FROM Books WHERE BookName = N'" + comboBoxBookName.Text.ToString() + "' AND " + "PublisherName = N'" + comboBoxPublisherName.Text.ToString() + "'", sqlConnection);
                        DataSet dataSet5 = new DataSet();
                        dataAdapter5.Fill(dataSet5);
                        for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
                        {
                            comboBoxBookAuthor.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
                        }
                        comboBoxBookName.Enabled = false;
                    }
                    else
                    {
                        comboBoxBookName.Enabled = false;
                        CheckAvailability();
                    }
                }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            comboBoxBookInventoryNumber.Enabled = true;
            comboBoxBookInventoryNumber.Text = "";
            comboBoxBookInventoryNumber.Items.Clear();
            comboBoxBookName.Text = "";
            comboBoxBookName.Items.Clear();
            comboBoxBookName.Enabled = true;
            comboBoxBookAuthor.Text = "";
            comboBoxBookAuthor.Items.Clear();
            comboBoxBookAuthor.Enabled = true;
            comboBoxPublisherName.Text = "";
            comboBoxPublisherName.Items.Clear();
            comboBoxPublisherName.Enabled = true;

            SqlDataAdapter dataAdapter3 = new SqlDataAdapter("SELECT DISTINCT PublisherName FROM Books", sqlConnection);
            DataSet dataSet3 = new DataSet();
            dataAdapter3.Fill(dataSet3);
            for (int i = 0; i < dataSet3.Tables[0].Rows.Count; i++)
            {
                comboBoxPublisherName.Items.Add(dataSet3.Tables[0].Rows[i].ItemArray[0]);
            }

            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT DISTINCT BookAuthor FROM Books", sqlConnection);
            DataSet dataSet4 = new DataSet();
            dataAdapter4.Fill(dataSet4);
            for (int i = 0; i < dataSet4.Tables[0].Rows.Count; i++)
            {
                comboBoxBookAuthor.Items.Add(dataSet4.Tables[0].Rows[i].ItemArray[0]);
            }

            SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT DISTINCT BookName FROM Books", sqlConnection);
            DataSet dataSet5 = new DataSet();
            dataAdapter5.Fill(dataSet5);
            for (int i = 0; i < dataSet5.Tables[0].Rows.Count; i++)
            {
                comboBoxBookName.Items.Add(dataSet5.Tables[0].Rows[i].ItemArray[0]);
            }

            SqlDataAdapter dataAdapter6 = new SqlDataAdapter("SELECT BookInventoryNumber FROM Books EXCEPT SELECT BookInventoryNumber FROM Issuances WHERE IssuanceDateEnd IS NULL", sqlConnection);
            DataSet dataSet6 = new DataSet();
            dataAdapter6.Fill(dataSet6);
            for (int i = 0; i < dataSet6.Tables[0].Rows.Count; i++)
            {
                comboBoxBookInventoryNumber.Items.Add(dataSet6.Tables[0].Rows[i].ItemArray[0]);
            }
        }

        private void buttonAddIssuance_Click(object sender, EventArgs e)
        {
            if ((textBoxId.Text != "") && (comboBoxBookInventoryNumber.Text != "") && (comboBoxBookName.Text != "") && (comboBoxBookAuthor.Text != "") && (comboBoxPublisherName.Text != "") && (dateTimePicker1.Text != ""))
            {
                if (comboBoxBookInventoryNumber.Text != "Нет в наличии")
                {
                    Console.WriteLine(dateTimePicker1.Text);
                    Console.WriteLine(dateTimePicker1.Value.ToShortDateString());
                    Console.WriteLine(dateTimePicker1.Value.GetType());

                    
                    SqlCommand command = new SqlCommand($"INSERT INTO [Issuances] (Id, BookInventoryNumber, IssuanceDateStart)" +
                "VALUES (@Id, @BookInventoryNumber, @IssuanceDateStart)", sqlConnection);
                    command.Parameters.AddWithValue("Id", textBoxId.Text);
                    command.Parameters.AddWithValue("BookInventoryNumber", comboBoxBookInventoryNumber.Text);
                    command.Parameters.AddWithValue("IssuanceDateStart", dateTimePicker1.Value);
                    command.ExecuteNonQuery().ToString();

                    MessageBox.Show("Читателю необходимо выдать книигу с инвентарным номером " + comboBoxBookInventoryNumber.Text);
                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show("Такой книги нет в наличии");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
        }

        private void comboBoxBookInventoryNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxBookName.Enabled = false;
            comboBoxBookAuthor.Enabled = false;
            comboBoxPublisherName.Enabled = false;

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT BookName, BookAuthor, PublisherName FROM Books WHERE BookInventoryNumber = " + comboBoxBookInventoryNumber.Text, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            comboBoxBookName.Text = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
            comboBoxBookAuthor.Text = dataSet.Tables[0].Rows[0].ItemArray[1].ToString();
            comboBoxPublisherName.Text = dataSet.Tables[0].Rows[0].ItemArray[2].ToString();
            /*
            SqlDataAdapter dataAdapter6 = new SqlDataAdapter("SELECT BookName FROM Books WHERE BookInventoryNumber = " + comboBoxBookInventoryNumber.Text, sqlConnection);
            DataSet dataSet6 = new DataSet();
            dataAdapter6.Fill(dataSet6);
            comboBoxBookName.Text = dataSet6.Tables[0].Rows[0].ItemArray[0].ToString();

            SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT BookAuthor FROM Books WHERE BookInventoryNumber = " + comboBoxBookInventoryNumber.Text, sqlConnection);
            DataSet dataSet5 = new DataSet();
            dataAdapter5.Fill(dataSet5);
            comboBoxBookAuthor.Text = dataSet5.Tables[0].Rows[0].ItemArray[0].ToString();

            SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT PublisherName FROM Books WHERE BookInventoryNumber = " + comboBoxBookInventoryNumber.Text, sqlConnection);
            DataSet dataSet4 = new DataSet();
            dataAdapter4.Fill(dataSet4);
            comboBoxPublisherName.Text = dataSet4.Tables[0].Rows[0].ItemArray[0].ToString();
            */
        }

        private void comboBoxDateDay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
