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
    public partial class BibliologistMainForm : Form
    {
        private SqlConnection sqlConnection = null;
        public BibliologistMainForm()
        {
            InitializeComponent();
        }

        public void Data(String id, String name, String number, String password)
        {
            labelId.Text = id;
            labelName.Text = name;
            labelPhone.Text = number;
            labelPassword.Text = password;
        }
        private void BibliologistMainForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void BibliologistMainForm_Activated(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"EXEC CreateBiologistMainForm1", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            Console.WriteLine(dataSet.Tables[0].Rows[0].ItemArray[0]);

            dataSet.Tables[0].Columns.Add("Наличие");
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                dataSet.Tables[0].Rows[i]["Наличие"] = "Выдана";     
            }

            dataAdapter = new SqlDataAdapter($"EXEC CreateBiologistMainForm2", sqlConnection);
            DataSet dataSet1 = new DataSet();
            dataAdapter.Fill(dataSet1);
            dataSet1.Tables[0].Columns.Add("Наличие");
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                dataSet1.Tables[0].Rows[i]["Наличие"] = "В наличии";
            }

            dataSet.Tables[0].Merge(dataSet1.Tables[0]);

            dataGridViewBooks.DataSource = dataSet.Tables[0];
            //dataGridViewBooks.DataSource = dataSet1.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm addBookForm = new AddBookForm();
            addBookForm.ShowDialog();
        }

        private void buttonAddDiscipline_Click(object sender, EventArgs e)
        {
            AddDisciplineFormForm addDisciplineForm = new AddDisciplineFormForm();
            addDisciplineForm.ShowDialog();
        }

        private void buttonAddSepeciality_Click(object sender, EventArgs e)
        {
            AddSpecialtyForm addSpecialtyForm = new AddSpecialtyForm();
            addSpecialtyForm.ShowDialog();
        }

        private void buttonAddDisciplineSpecialty_Click(object sender, EventArgs e)
        {
            AddDisciplineSpecialtyForm addDisciplineSpecialtyForm = new AddDisciplineSpecialtyForm();
            addDisciplineSpecialtyForm.ShowDialog();
        }
    }
}
