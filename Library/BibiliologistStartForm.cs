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
    public partial class BibiliologistStartForm : Form
    {
        private String id;
        private String name;
        private String number;
        private String password;
        public BibiliologistStartForm()
        {
            InitializeComponent();
        }
        public void Data(String id, String name, String number, String password)
        {
            this.id = id;
            this.name = name;
            this.number = number;
            this.password = password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4Form form4Form = new Form4Form();
            form4Form.ShowDialog();
        }

        private void buttonMainWindow_Click(object sender, EventArgs e)
        {
            BibliologistMainForm bibliologistMainForm = new BibliologistMainForm();
            bibliologistMainForm.Data(this.id, this.name, this.number, this.password);
            bibliologistMainForm.ShowDialog();
        }

        private void buttonReaderWindow_Click(object sender, EventArgs e)
        {
            FormularForm formularForm = new FormularForm();
            formularForm.Data(id, name, number, password);
            formularForm.ShowDialog();
        }

        private void BibiliologistStartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
