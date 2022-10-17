using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class LibrarianMainForm : Form
    {
        public LibrarianMainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListOfUsersAndIssuanesForm listOfUsersAndIssuanesForm = new ListOfUsersAndIssuanesForm();
            listOfUsersAndIssuanesForm.ShowDialog();
        }

        private void buttonForm4_Click(object sender, EventArgs e)
        {
            Form4Form form4Form = new Form4Form();
            form4Form.ShowDialog();
        }
    }
}
