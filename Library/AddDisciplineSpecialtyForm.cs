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
    public partial class AddDisciplineSpecialtyForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddDisciplineSpecialtyForm()
        {
            InitializeComponent();
        }

        private void AddDisciplineSpecialtyForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();

            String sqlcomand = "SELECT SpecialtyName FROM Specialty";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                comboBoxSpecialtyName.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
            }
        }

        private void AddDisciplineSpecialtyForm_Activated(object sender, EventArgs e)
        {

        }

        private void comboBoxSpecialtyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sqlcomand = "SELECT SpecialtyCode FROM Specialty WHERE SpecialtyName = N'" + comboBoxSpecialtyName.Text + "'";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            textBoxSpecialtyCode.Text = dataSet.Tables[0].Rows[0].ItemArray[0].ToString();
            comboBoxDisciplineName.Enabled = true;
            comboBoxDisciplineName.Items.Clear();
            sqlcomand = "SELECT DisciplineName FROM Disciplines EXCEPT (SELECT DisciplineName FROM DisciplineSpecialty WHERE SpecialtyCode = '" + textBoxSpecialtyCode.Text + "')";
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet1 = new DataSet();
            dataAdapter1.Fill(dataSet1);
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                comboBoxDisciplineName.Items.Add(dataSet1.Tables[0].Rows[i].ItemArray[0]);
            }
        }

        private void buttonAddDisciplineSpecialty_Click(object sender, EventArgs e)
        {
            if ((comboBoxSpecialtyName.Text != "") && (comboBoxDisciplineName.Text != ""))
            {
                SqlCommand command = new SqlCommand($"INSERT INTO [DisciplineSpecialty] (DisciplineName, SpecialtyCode)" +
                    "VALUES (@DisciplineName, @SpecialtyCode)", sqlConnection);
                command.Parameters.AddWithValue("DisciplineName", comboBoxDisciplineName.Text);
                command.Parameters.AddWithValue("SpecialtyCode", textBoxSpecialtyCode.Text);
                command.ExecuteNonQuery().ToString();
                MessageBox.Show("Дисциплина добавлена в перечень дисциплин выбранной специальности");
                this.Close();
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }
    }
}
