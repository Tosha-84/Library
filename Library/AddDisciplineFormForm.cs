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
    public partial class AddDisciplineFormForm : Form
    {
        private SqlConnection sqlConnection = null;
        public AddDisciplineFormForm()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddDisciplineFormForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void buttonAddDiscipline_Click(object sender, EventArgs e)
        {
            if ((textBoxDisciplineName.Text != "") && (comboBoxDisciplineCycleName.Text != ""))
            {
                String sqlcomand = "SELECT DisciplineCycleName FROM DisciplineCycles WHERE Decryption = N'" + comboBoxDisciplineCycleName.Text + "'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                SqlCommand command = new SqlCommand($"INSERT INTO [Disciplines] (DisciplineName, DisciplineCycleName)" +
                    "VALUES (@DisciplineName, @DisciplineCycleName)", sqlConnection);
                command.Parameters.AddWithValue("DisciplineName", textBoxDisciplineName.Text);
                command.Parameters.AddWithValue("DisciplineCycleName", dataSet.Tables[0].Rows[0].ItemArray[0].ToString());
                try
                {
                    command.ExecuteNonQuery().ToString();
                    MessageBox.Show("Дисциплина добавлена в базу данных");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Такая дисциплина уже существует!");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }

        private void AddDisciplineFormForm_Activated(object sender, EventArgs e)
        {
            comboBoxDisciplineCycleName.Items.Clear();
            String sqlcomand = "SELECT Decryption FROM DisciplineCycles";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcomand, sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                comboBoxDisciplineCycleName.Items.Add(dataSet.Tables[0].Rows[i].ItemArray[0].ToString());
                Console.WriteLine(dataSet.Tables[0].Rows[i].ItemArray[0]);
            }
        }
    }
}
