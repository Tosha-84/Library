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
    public partial class LogForm : Form
    {
        private Boolean check = false;
        private SqlConnection sqlConnection = null;
        public LogForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Id from Users WHERE Id = N'" + textBox1.Text + "' AND Password = N'" + textBox2.Text + "'", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count != 0)
            {
                check = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void LogForm_Activated(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void LogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (check)
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Role from Users WHERE Id = N'" + textBox1.Text + "'", sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                this.Hide();
                if (dataSet.Tables[0].Rows[0].ItemArray[0].ToString() == "Б")
                {
                    LibrarianMainForm librarianMainForm = new LibrarianMainForm();
                    librarianMainForm.ShowDialog();
                }    
                else if (dataSet.Tables[0].Rows[0].ItemArray[0].ToString() == "К")
                {
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter("SELECT FullName, PhoneNumber from Users WHERE Id = N'" + textBox1.Text + "'", sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);

                    BibiliologistStartForm bibiliologistStartForm = new BibiliologistStartForm();
                    bibiliologistStartForm.Data(textBox1.Text, dataSet1.Tables[0].Rows[0].ItemArray[0].ToString(), dataSet1.Tables[0].Rows[0].ItemArray[1].ToString(), textBox2.Text);
                    bibiliologistStartForm.ShowDialog();
                }
                else
                {
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter("SELECT FullName, PhoneNumber from Users WHERE Id = N'" + textBox1.Text + "'", sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    FormularForm formularForm = new FormularForm();
                    formularForm.Data(textBox1.Text, dataSet1.Tables[0].Rows[0].ItemArray[0].ToString(), dataSet1.Tables[0].Rows[0].ItemArray[1].ToString(), textBox2.Text);
                    formularForm.ShowDialog();
                }
            }
        }

        private void LogForm_Deactivate(object sender, EventArgs e)
        {

        }

        private void LogForm_Leave(object sender, EventArgs e)
        {

        }
    }
}
