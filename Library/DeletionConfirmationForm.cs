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
    public partial class DeletionConfirmationForm : Form
    {
        private SqlConnection sqlConnection = null;
        private String issuanceNumber;
        public DeletionConfirmationForm()
        {
            InitializeComponent();
        }

        public void DataForForm(String sentence)
        {
            label1.Text = sentence;
        }

        public void DataForIssuanceNumber(String issuanceNumber1)
        {
            issuanceNumber = issuanceNumber1;
        }

        private void DeletionConfirmationForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            String comand_for_sql = "DELETE FROM Issuances WHERE IssuanceNumber = ";
            comand_for_sql = comand_for_sql + issuanceNumber;
            SqlCommand sqlCommand = new SqlCommand(comand_for_sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Выдача удалена");
            this.Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
