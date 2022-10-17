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

using System.Diagnostics;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;




using PdfSharp.Pdf;

namespace Library
{
    public partial class ListOfUsersAndIssuanesForm : Form
    {
        private SqlConnection sqlConnection = null;
        public ListOfUsersAndIssuanesForm()
        {
            InitializeComponent();
        }

        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridViewUsers.Rows.Count != 0)
            {
                String comand_for_sql = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber,Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
                comand_for_sql = comand_for_sql + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
                if (checkBox.Checked)
                {
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
                else
                {
                    comand_for_sql += " AND IssuanceDateEnd IS NULL";
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
                if (dataGridViewIssuances.Rows.Count != 0)
                {
                    if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() != "")
                    {
                        buttonEnd.Enabled = false;
                    }
                    else
                    {
                        buttonEnd.Enabled = true;
                    }
                }
                else buttonEnd.Enabled = false;
            }
        }

        private void ListOfUsersAndIssuanesForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void ListOfUsersAndIssuanesForm_Activated(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Users", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridViewUsers.DataSource = dataSet.Tables[0];

            for (int i = 0; i < dataGridViewUsers.Rows.Count; i++)
            {
                if (dataGridViewUsers[2, i].Value.ToString() == "Ч")
                {
                    dataGridViewUsers[2, i].Value = "Читатель";
                }
                else
                {
                    if (dataGridViewUsers[2, i].Value.ToString() == "К")
                    {
                        dataGridViewUsers[2, i].Value = "Книговед";
                    }
                    else
                    {
                        dataGridViewUsers[2, i].Value = "Библиотекарь";
                    }
                }
            }

            if (dataGridViewUsers.CurrentRow != null)
            {
                String comand_for_sql = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber, Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
                comand_for_sql = comand_for_sql + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
                if (checkBox.Checked)
                {
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
                else
                {
                    comand_for_sql += " AND IssuanceDateEnd IS NULL";
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
            }
            if (dataGridViewIssuances.Rows.Count != 0)
            {
                if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() != "")
                {
                    buttonEnd.Enabled = false;
                }
                else
                {
                    buttonEnd.Enabled = true;
                }
            }
            else buttonEnd.Enabled = false;
        }

        private void dataGridViewIssuanes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewIssuances_CellContentClick(sender, e);
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewUsers_CellContentClick(sender, e);
        }

        private void dataGridViewUsers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewUsers_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            String comand_for_sql = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber, Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
            comand_for_sql = comand_for_sql + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            if (checkBox.Checked)
            {
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                DataSet dataSet1 = new DataSet();
                dataAdapter1.Fill(dataSet1);
                dataGridViewIssuances.DataSource = dataSet1.Tables[0];
            }
            else
            {
                comand_for_sql += " AND IssuanceDateEnd IS NULL";
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                DataSet dataSet1 = new DataSet();
                dataAdapter1.Fill(dataSet1);
                dataGridViewIssuances.DataSource = dataSet1.Tables[0];
            }
            if (dataGridViewIssuances.Rows.Count != 0)
            {
                if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() != "")
                {
                    buttonEnd.Enabled = false;
                }
                else
                {
                    buttonEnd.Enabled = true;
                }
            }
            buttonEnd.Enabled = false;
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm();
            addUserForm.ShowDialog();
        }

        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            EditUserForm editUserForm = new EditUserForm();
            String id = dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            String password = dataGridViewUsers[1, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            String role = dataGridViewUsers[2, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            String fullname = dataGridViewUsers[3, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            String phonneNumber = dataGridViewUsers[4, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            editUserForm.DataForEdit(id, password, role, fullname, phonneNumber);
            editUserForm.ShowDialog();
        }

        private void buttonDelUser_Click(object sender, EventArgs e)
        {
            String comand_for_sql = "DELETE FROM Issuances WHERE Id = ";
            comand_for_sql = comand_for_sql + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            SqlCommand sqlCommand = new SqlCommand(comand_for_sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            try
            {
                dataGridViewIssuances.Rows.Clear();
            }
            catch
            {

            }
            comand_for_sql = "DELETE FROM Users WHERE Id = ";
            comand_for_sql = comand_for_sql + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            sqlCommand = new SqlCommand(comand_for_sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Users", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridViewUsers.DataSource = dataSet.Tables[0];

            for (int i = 0; i < dataGridViewUsers.Rows.Count; i++)
            {
                if (dataGridViewUsers[2, i].Value.ToString() == "Ч")
                {
                    dataGridViewUsers[2, i].Value = "Читатель";
                }
                else
                {
                    if (dataGridViewUsers[2, i].Value.ToString() == "К")
                    {
                        dataGridViewUsers[2, i].Value = "Книговед";
                    }
                    else
                    {
                        dataGridViewUsers[2, i].Value = "Библиотекарь";
                    }
                }
            }

            if (dataGridViewUsers.CurrentRow != null)
            {
                String comand_for_sql1 = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber, Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
                comand_for_sql1 = comand_for_sql1 + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
                if (checkBox.Checked)
                {
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql1, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
                else
                {
                    comand_for_sql1 += " AND IssuanceDateEnd IS NULL";
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql1, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
            }
            if (dataGridViewIssuances.Rows.Count != 0)
            {
                if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() != "")
                {
                    buttonEnd.Enabled = false;
                }
                else
                {
                    buttonEnd.Enabled = true;
                }
            }
            else buttonEnd.Enabled = false;
        }

        private void buttonAddIssuane_Click(object sender, EventArgs e)
        {
            AddIssuanceForm addIssuanceForm = new AddIssuanceForm();
            addIssuanceForm.DataForNewIssuance(Convert.ToInt32(dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString()));
            addIssuanceForm.ShowDialog();
        }

        private void buttonEditIssuane_Click(object sender, EventArgs e)
        {

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsers.Rows.Count != 0)
            {
                String comand_for_sql = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber,Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
                comand_for_sql = comand_for_sql + dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
                if (checkBox.Checked)
                {
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
                else
                {
                    comand_for_sql = comand_for_sql + " AND IssuanceDateEnd IS NULL";
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(comand_for_sql, sqlConnection);
                    DataSet dataSet1 = new DataSet();
                    dataAdapter1.Fill(dataSet1);
                    dataGridViewIssuances.DataSource = dataSet1.Tables[0];
                }
            }
            if (dataGridViewIssuances.Rows.Count != 0)
            {
                if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() != "")
                {
                    buttonEnd.Enabled = false;
                }
                else
                {
                    buttonEnd.Enabled = true;
                }
            }
            else buttonEnd.Enabled = false;
        }

        private void buttonPrinting_Click(object sender, EventArgs e)
        {
            // Код, закоментированный ниже - это код, сосдающий пдф документ с текстом
            /*
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString("ФОРМУЛЯР", font, XBrushes.Black, new XRect(0, 35, page.Width, page.Height), XStringFormats.TopCenter);

            
            // Save the document...
            const string filename = @"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\HelloWorld.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
            */

            // System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //dataGridViewIssuances[1, dataGridViewIssuances.CurrentRow.Index].Value.ToString();
            Console.WriteLine(dataGridViewIssuances.RowCount.ToString());


            List<User> users = new List<User>();
            var rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                users.Add(new User()
                {
                    NumberOne = "4" + rnd.Next(5, 66).ToString(),
                    NumberTwo = "5" + rnd.Next(5, 66).ToString(),
                    Random = "Теоретическая механика" + rnd.Next(5, 20000).ToString(),
                    FirstName = "Булидзе В.Е." + rnd.Next(5, 66).ToString(),
                    LastName = "Издательство" + rnd.Next(5, 66).ToString() + i.ToString(),
                    DateOfBirth = DateTime.UtcNow.Subtract(new TimeSpan(rnd.Next(10000, 20000), 0, 0, 0, 0)),
                    NewDate = DateTime.UtcNow.Subtract(new TimeSpan(rnd.Next(10000, 20000), 0, 0, 0, 0))
                });
            }
            //GeneratePDF(users);

            string id = dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            string name = dataGridViewUsers[3, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            string phone = dataGridViewUsers[4, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            List<Issuance> issuances = new List<Issuance>();



            for (int i = 0; i < dataGridViewIssuances.RowCount; i++)
            {
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter("SELECT PublisherShortName FROM Publishers WHERE PublisherName = N'" + dataGridViewIssuances[4, i].Value.ToString() + "'", sqlConnection);
                DataSet dataSet1 = new DataSet();
                dataAdapter1.Fill(dataSet1);
                String helpName = dataSet1.Tables[0].Rows[0].ItemArray[0].ToString();
                if (dataGridViewIssuances[6, i].Value.ToString() != "")
                {
                    issuances.Add(new Issuance()
                    {
                        IssuanceNumber = dataGridViewIssuances[0, i].Value.ToString(),
                        BookInventoryNumber = dataGridViewIssuances[1, i].Value.ToString(),
                        BookName = dataGridViewIssuances[2, i].Value.ToString(),
                        BookAuthor = dataGridViewIssuances[3, i].Value.ToString(),

                        // PublisherName = dataGridViewIssuances[4, i].Value.ToString(),
                        PublisherName = helpName,

                        //IssuanceDateStart = test1,
                        //IssuanceDateEnd = test2,
                        IssuanceDateStart = DateTime.Parse(dataGridViewIssuances[5, i].Value.ToString()),
                        IssuanceDateEnd = DateTime.Parse(dataGridViewIssuances[6, i].Value.ToString()),
                    });
                }
                else
                {
                    issuances.Add(new Issuance()
                    {
                        IssuanceNumber = dataGridViewIssuances[0, i].Value.ToString(),
                        BookInventoryNumber = dataGridViewIssuances[1, i].Value.ToString(),
                        BookName = dataGridViewIssuances[2, i].Value.ToString(),
                        BookAuthor = dataGridViewIssuances[3, i].Value.ToString(),
                        // PublisherName = dataGridViewIssuances[4, i].Value.ToString(),
                        PublisherName = helpName,


                        //IssuanceDateStart = test1,
                        //IssuanceDateEnd = test2,
                        IssuanceDateStart = DateTime.Parse(dataGridViewIssuances[5, i].Value.ToString()),
                        //IssuanceDateEnd = DateTime.Parse(dataGridViewIssuances[6, i].Value.ToString()),
                    });
                }
            }
            GenerateForm(id, name, phone, issuances);

            // Ниже - просто огрызки от поиска осздания PDF
            /*
            // New pdf document
            PdfDocument document = new PdfDocument();

            // New page
            PdfPage page = document.AddPage();

            // The class will provide drawing related methods for specified page
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Choose font to be used on the text
            XFont font = new XFont("Arial", 20); // Сюда третьим аргументом показывали XFontStyle

            gfx.DrawString("First line of text", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

            gfx.DrawString("Second line of text", font, XBrushes.Violet,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.BottomLeft);

            gfx.DrawString("Third line of text", font, XBrushes.Red, new XPoint(100, 300));

            document.Save(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\TestPDF.pdf");
            */
        }

        private static void GeneratePDF(List<User> users)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PDF Example";

            PdfPage page = document.AddPage();
            /*
            XGraphics gfx1 = XGraphics.FromPdfPage(page);
            // Variation 1: Draw a watermark as a text string.
            // Get an XGraphics object for drawing beneath the existing content.
            //gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend);
            // Get the size (in points) of the text.
            var size = gfx1.MeasureString("Библиотека Вилачёва", new XFont("Arial", 20, XFontStyle.Bold));
            // Define a rotation transformation at the center of the page.
            gfx1.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx1.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx1.TranslateTransform(-page.Width / 2, -page.Height / 2);
            // Create a string format.
            var format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;
            // Create a dimmed red brush.
            XBrush brush = new XSolidBrush(XColor.FromArgb(128, 255, 0, 0));
            // Draw the string.
            gfx1.DrawString("Библиотека Вилачёва", new XFont("Arial", 20, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
            gfx1.Dispose();
            */

            XGraphics gfx = XGraphics.FromPdfPage(page);

            gfx.DrawImage(XImage.FromFile(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Logo2.png"), new XPoint(250, 100));

            // Variation 1: Draw a watermark as a text string.
            // Get an XGraphics object for drawing beneath the existing content.
            //gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend);
            // Get the size (in points) of the text.
            var size = gfx.MeasureString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold));
            // Define a rotation transformation at the center of the page.
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);
            // Create a string format.
            var format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;
            // Create a dimmed red brush.
            XBrush brush = new XSolidBrush(XColor.FromArgb(100, 64, 224, 208));
            // Draw the string.
            gfx.DrawString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
            gfx.Dispose();
            gfx = XGraphics.FromPdfPage(page);




            // generate header
            gfx.DrawString("Формуляр", new XFont("Arial", 40, XFontStyle.Bold), XBrushes.Black, new XPoint(200, 70));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(100, 100), new XPoint(500, 100));

            gfx.DrawString("Личные данные", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 118));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 125), new XPoint(100, 125));
            gfx.DrawString("ID: 13", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 140));
            gfx.DrawString("ФИО: Вилачёв Антон Евгеньевич", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 160));
            gfx.DrawString("Телефонный номер: 89148891191", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 180));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 187), new XPoint(100, 187));


            //Добавил
            //gfx.DrawString("Жопа", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(23, 300));


            //gfx.DrawString("Номер", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(23, 300));
            //gfx.DrawString("выдачи", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 320));

            gfx.DrawString("Н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 210));
            gfx.DrawString("о", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 220));
            gfx.DrawString("м", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 230));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 240));
            gfx.DrawString("р", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 250));
            gfx.DrawString(" ", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 260));
            gfx.DrawString("в", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 270));
            gfx.DrawString("ы", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 280));
            gfx.DrawString("д", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 290));
            gfx.DrawString("а", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 300));
            gfx.DrawString("ч", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 310));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 320));

            /*
            gfx.DrawString("Инвентарный", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(66, 280));
            gfx.DrawString("номер", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(84, 300));
            gfx.DrawString("книги", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(86, 320));
            */
            gfx.DrawString("И", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 220));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 230));
            gfx.DrawString("в", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 240));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 250));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 260));
            gfx.DrawString("т", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 270));
            gfx.DrawString("а", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 280));
            gfx.DrawString("р", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 290));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 300));
            gfx.DrawString("ы", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 310));
            gfx.DrawString("й", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 320));

            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 220));
            gfx.DrawString("о", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 230));
            gfx.DrawString("м", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 240));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 250));
            gfx.DrawString("р", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 260));

            gfx.DrawString("к", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 280));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 290));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 300));
            gfx.DrawString("г", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 310));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 320));



            //gfx.DrawString("Название", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(170, 300));
            gfx.DrawString("Название книги", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(108, 320));

            //gfx.DrawString("Автор", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(280, 300));
            gfx.DrawString("Автор книги", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(265, 320));

            gfx.DrawString("Название", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(372, 300));
            gfx.DrawString("издательства", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(363, 320));

            gfx.DrawString("Дата", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(472, 300));
            gfx.DrawString("выдачи", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(465, 320));

            gfx.DrawString("Дата", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(536, 300));
            gfx.DrawString("возврата", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(527, 320));


            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 330), new XPoint(580, 330));

            int currentYposition_values = 343;
            int currentYposition_line = 350;

            if (users.Count <= 20)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    gfx.DrawString(users[i].NumberOne, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    gfx.DrawString(users[i].NumberTwo, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    gfx.DrawString(users[i].Random, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                    gfx.DrawString(users[i].FirstName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                    gfx.DrawString(users[i].LastName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                    gfx.DrawString(users[i].NewDate.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                    gfx.DrawString(users[i].DateOfBirth.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));

                    currentYposition_values += 20;
                    currentYposition_line += 20;
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    gfx.DrawString(users[i].NumberOne, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    gfx.DrawString(users[i].NumberTwo, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    gfx.DrawString(users[i].Random, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                    gfx.DrawString(users[i].FirstName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                    gfx.DrawString(users[i].LastName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                    gfx.DrawString(users[i].NewDate.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                    gfx.DrawString(users[i].DateOfBirth.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));


                    currentYposition_values += 20;
                    currentYposition_line += 20;
                    users.Remove(users[i]);
                }

                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                currentYposition_values = 33;
                currentYposition_line = 40;

                size = gfx.MeasureString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold));
                // Define a rotation transformation at the center of the page.
                gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
                gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);
                // Create a string format.
                format = new XStringFormat();
                format.Alignment = XStringAlignment.Near;
                format.LineAlignment = XLineAlignment.Near;
                // Create a dimmed red brush.
                brush = new XSolidBrush(XColor.FromArgb(100, 64, 224, 208));
                // Draw the string.
                gfx.DrawString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
                gfx.Dispose();
                gfx = XGraphics.FromPdfPage(page);

                /*
                var document = new iTextSharp.text.Document();
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(@"C:\Users\Иван\Desktop\pdf.pdf", FileMode.Create));

                document.OpenDocument();

                var logo = iTextSharp.text.Image.GetInstance(new FileStream(@"C:\Users\Иван\Desktop\image.png", FileMode.Open));
                logo.SetAbsolutePosition(x, y);
                writer.DirectContent.AddImage(logo);

                document.Close();
                writer.Close();
                */

                //var logo = PdfSharp.Drawing.XPoint;

                bool firstpage = true;
                for(int i = 0; i < users.Count; i++)
                {
                    if(i != 0 && i % 30 == 0)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        currentYposition_values = 33;
                        currentYposition_line = 40;

                        size = gfx.MeasureString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold));
                        // Define a rotation transformation at the center of the page.
                        gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                        gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
                        gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);
                        // Create a string format.
                        format = new XStringFormat();
                        format.Alignment = XStringAlignment.Near;
                        format.LineAlignment = XLineAlignment.Near;
                        // Create a dimmed red brush.
                        brush = new XSolidBrush(XColor.FromArgb(100, 64, 224, 208));
                        // Draw the string.
                        gfx.DrawString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
                        gfx.Dispose();
                        gfx = XGraphics.FromPdfPage(page);
                    }
                    gfx.DrawString(users[i].NumberOne, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    gfx.DrawString(users[i].NumberTwo, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                    gfx.DrawString(users[i].Random, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                    gfx.DrawString(users[i].FirstName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                    gfx.DrawString(users[i].LastName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                    gfx.DrawString(users[i].NewDate.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                    gfx.DrawString(users[i].DateOfBirth.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));

                    currentYposition_values += 20;
                    currentYposition_line += 20;
                }
                
            }

            document.Save(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Test2PDF.pdf");

            Process.Start(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Test2PDF.pdf");
        }

        private static void GenerateForm(string id, string name, string phone, List<Issuance> issuances)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PDF Example";

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            gfx.DrawImage(XImage.FromFile(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Logo2.png"), new XPoint(250, 100));

            // Ниже - это водяная марка
            // Get the size (in points) of the text.
            var size = gfx.MeasureString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold));
            // Define a rotation transformation at the center of the page.
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);
            // Create a string format.
            var format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;
            // Create a dimmed red brush.
            XBrush brush = new XSolidBrush(XColor.FromArgb(100, 64, 224, 208));
            // Draw the string.
            gfx.DrawString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
            gfx.Dispose();
            gfx = XGraphics.FromPdfPage(page);




            // generate header
            gfx.DrawString("Формуляр", new XFont("Arial", 40, XFontStyle.Bold), XBrushes.Black, new XPoint(200, 70));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(100, 100), new XPoint(500, 100));

            gfx.DrawString("Личные данные", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 118));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 125), new XPoint(100, 125));
            gfx.DrawString("ID: " + id, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 140));
            gfx.DrawString("ФИО: " + name, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 160));
            gfx.DrawString("Телефонный номер: " + phone, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(20, 180));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 187), new XPoint(100, 187));

            gfx.DrawString("Н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 210));
            gfx.DrawString("о", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 220));
            gfx.DrawString("м", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 230));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 240));
            gfx.DrawString("р", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 250));
            gfx.DrawString(" ", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 260));
            gfx.DrawString("в", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 270));
            gfx.DrawString("ы", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 280));
            gfx.DrawString("д", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 290));
            gfx.DrawString("а", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 300));
            gfx.DrawString("ч", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 310));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 320));

            gfx.DrawString("И", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 220));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 230));
            gfx.DrawString("в", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 240));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 250));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 260));
            gfx.DrawString("т", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 270));
            gfx.DrawString("а", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 280));
            gfx.DrawString("р", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 290));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 300));
            gfx.DrawString("ы", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 310));
            gfx.DrawString("й", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(46, 320));

            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 220));
            gfx.DrawString("о", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 230));
            gfx.DrawString("м", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 240));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 250));
            gfx.DrawString("р", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 260));

            gfx.DrawString("к", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 280));
            gfx.DrawString("н", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 290));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 300));
            gfx.DrawString("г", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 310));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(60, 320));

            gfx.DrawString("Название книги", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(108, 320));

            gfx.DrawString("Автор книги", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(265, 320));

            gfx.DrawString("Название", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(372, 300));
            gfx.DrawString("издательства", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(363, 320));

            gfx.DrawString("Дата", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(472, 300));
            gfx.DrawString("выдачи", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(465, 320));

            gfx.DrawString("Дата", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(536, 300));
            gfx.DrawString("возврата", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(527, 320));

            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 330), new XPoint(580, 330));

            int currentYposition_values = 343;
            int currentYposition_line = 350;

            if (issuances.Count <= 20)
            {
                for (int i = 0; i < issuances.Count; i++)
                {
                    if (issuances[i].IssuanceDateEnd.ToShortDateString().ToString() != "01.01.0001")
                    {
                        gfx.DrawString(issuances[i].IssuanceNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                        gfx.DrawString(issuances[i].BookInventoryNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                        gfx.DrawString(issuances[i].BookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                        gfx.DrawString(issuances[i].BookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                        gfx.DrawString(issuances[i].PublisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateStart.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateEnd.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                        gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));
                    }
                    else
                    {
                        gfx.DrawString(issuances[i].IssuanceNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                        gfx.DrawString(issuances[i].BookInventoryNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                        gfx.DrawString(issuances[i].BookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                        gfx.DrawString(issuances[i].BookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                        gfx.DrawString(issuances[i].PublisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateStart.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                        //gfx.DrawString(issuances[i].IssuanceDateEnd.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                        gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));
                    }

                    currentYposition_values += 20;
                    currentYposition_line += 20;
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    if (issuances[i].IssuanceDateEnd.ToShortDateString().ToString() != "01.01.0001")
                    {
                        gfx.DrawString(issuances[i].IssuanceNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                        gfx.DrawString(issuances[i].BookInventoryNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                        gfx.DrawString(issuances[i].BookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                        gfx.DrawString(issuances[i].BookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                        gfx.DrawString(issuances[i].PublisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateStart.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateEnd.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                        gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));
                    }
                    else
                    {
                        gfx.DrawString(issuances[i].IssuanceNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                        gfx.DrawString(issuances[i].BookInventoryNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                        gfx.DrawString(issuances[i].BookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                        gfx.DrawString(issuances[i].BookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                        gfx.DrawString(issuances[i].PublisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateStart.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                        //gfx.DrawString(issuances[i].IssuanceDateEnd.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                        gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));
                    }


                    currentYposition_values += 20;
                    currentYposition_line += 20;
                    issuances.Remove(issuances[i]);
                }

                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                currentYposition_values = 33;
                currentYposition_line = 40;

                size = gfx.MeasureString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold));
                // Define a rotation transformation at the center of the page.
                gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
                gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);
                // Create a string format.
                format = new XStringFormat();
                format.Alignment = XStringAlignment.Near;
                format.LineAlignment = XLineAlignment.Near;
                // Create a dimmed red brush.
                brush = new XSolidBrush(XColor.FromArgb(100, 64, 224, 208));
                // Draw the string.
                gfx.DrawString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
                gfx.Dispose();
                gfx = XGraphics.FromPdfPage(page);

                /*
                var document = new iTextSharp.text.Document();
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(@"C:\Users\Иван\Desktop\pdf.pdf", FileMode.Create));

                document.OpenDocument();

                var logo = iTextSharp.text.Image.GetInstance(new FileStream(@"C:\Users\Иван\Desktop\image.png", FileMode.Open));
                logo.SetAbsolutePosition(x, y);
                writer.DirectContent.AddImage(logo);

                document.Close();
                writer.Close();
                */

                //var logo = PdfSharp.Drawing.XPoint;

                bool firstpage = true;
                for (int i = 0; i < issuances.Count; i++)
                {
                    if (i != 0 && i % 30 == 0)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        currentYposition_values = 33;
                        currentYposition_line = 40;

                        size = gfx.MeasureString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold));
                        // Define a rotation transformation at the center of the page.
                        gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                        gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
                        gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);
                        // Create a string format.
                        format = new XStringFormat();
                        format.Alignment = XStringAlignment.Near;
                        format.LineAlignment = XLineAlignment.Near;
                        // Create a dimmed red brush.
                        brush = new XSolidBrush(XColor.FromArgb(100, 64, 224, 208));
                        // Draw the string.
                        gfx.DrawString("Библиотека Вилачёва", new XFont("Arial", 50, XFontStyle.Bold), brush, new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2), format);
                        gfx.Dispose();
                        gfx = XGraphics.FromPdfPage(page);
                    }

                    if (issuances[i].IssuanceDateEnd.ToShortDateString().ToString() != "01.01.0001")
                    {
                        gfx.DrawString(issuances[i].IssuanceNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                        gfx.DrawString(issuances[i].BookInventoryNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                        gfx.DrawString(issuances[i].BookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                        gfx.DrawString(issuances[i].BookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                        gfx.DrawString(issuances[i].PublisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateStart.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateEnd.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                        gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));
                    }
                    else
                    {
                        gfx.DrawString(issuances[i].IssuanceNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                        gfx.DrawString(issuances[i].BookInventoryNumber, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(50, currentYposition_values));
                        gfx.DrawString(issuances[i].BookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(80, currentYposition_values));
                        gfx.DrawString(issuances[i].BookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(255, currentYposition_values));
                        gfx.DrawString(issuances[i].PublisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(360, currentYposition_values));
                        gfx.DrawString(issuances[i].IssuanceDateStart.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(460, currentYposition_values));
                        //gfx.DrawString(issuances[i].IssuanceDateEnd.ToShortDateString(), new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(525, currentYposition_values));
                        gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));
                    }

                    currentYposition_values += 20;
                    currentYposition_line += 20;
                }

            }

            document.Save(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Формуляр.pdf");

            Process.Start(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Формуляр.pdf");
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            EndIssuanceForm endIssuanceForm = new EndIssuanceForm();

            String id = dataGridViewUsers[0, dataGridViewUsers.CurrentRow.Index].Value.ToString();
            String issuanceNumber = dataGridViewIssuances[0, dataGridViewIssuances.CurrentRow.Index].Value.ToString();
            String bookInventoryNumber = dataGridViewIssuances[1, dataGridViewIssuances.CurrentRow.Index].Value.ToString();
            String bookName = dataGridViewIssuances[2, dataGridViewIssuances.CurrentRow.Index].Value.ToString();
            String bookAuthor = dataGridViewIssuances[3, dataGridViewIssuances.CurrentRow.Index].Value.ToString();
            String bookPublisherName = dataGridViewIssuances[4, dataGridViewIssuances.CurrentRow.Index].Value.ToString();
            String dateStart = dataGridViewIssuances[5, dataGridViewIssuances.CurrentRow.Index ].Value.ToString();

            endIssuanceForm.DataForIssuance(id, issuanceNumber, bookInventoryNumber, bookName, bookAuthor, bookPublisherName, dateStart);
            endIssuanceForm.ShowDialog();
        }

        private void dataGridViewIssuances_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(dataGridViewIssuances[1, dataGridViewIssuances.CurrentRow.Index].Value.ToString());
            if (dataGridViewIssuances.Rows.Count != 0)
            {
                if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() != "")
                {
                    buttonEnd.Enabled = false;
                }
                else
                {
                    buttonEnd.Enabled = true;
                }
            }
            else buttonEnd.Enabled = false;
        }

        private void buttonDelIssuane_Click(object sender, EventArgs e)
        {
            if (dataGridViewIssuances.Rows.Count > 0)
            {
                DeletionConfirmationForm deletionConfirmationForm = new DeletionConfirmationForm();
                deletionConfirmationForm.DataForIssuanceNumber(dataGridViewIssuances[0, dataGridViewIssuances.CurrentRow.Index].Value.ToString());
                if (dataGridViewIssuances[6, dataGridViewIssuances.CurrentRow.Index].Value.ToString() == "")
                {
                    deletionConfirmationForm.DataForForm("Вы уверены, что хотите удалить НЕЗАКРЫТУЮ выдачу с номером " + dataGridViewIssuances[0, dataGridViewIssuances.CurrentRow.Index].Value.ToString() + "?");
                }
                else
                {
                    deletionConfirmationForm.DataForForm("Вы уверены, что хотите удалить выдачу с номером " + dataGridViewIssuances[0, dataGridViewIssuances.CurrentRow.Index].Value.ToString() + "?");
                }

                deletionConfirmationForm.ShowDialog();
            }
        }
    }
    class User
    {
        public string NumberOne { get; set; }
        public string NumberTwo { get; set; }
        public string Random { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime NewDate { get; set; }
    }

    class Issuance
    {
        public string IssuanceNumber { get; set; }
        public string BookInventoryNumber { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string PublisherName { get; set; }
        public DateTime IssuanceDateStart { get; set; }
        public DateTime IssuanceDateEnd { get; set; }
    }
}
