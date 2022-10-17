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
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Library
{
    public partial class FormularForm : Form
    {
        private SqlConnection sqlConnection = null;
        public FormularForm()
        {
            InitializeComponent();
        }
        public void Data(String id, String name, String number, String password)
        {
            label3.Text = id;
            label5.Text = name;
            label7.Text = number;
            label9.Text = password;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormularForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();
        }

        private void FormularForm_Activated(object sender, EventArgs e)
        {
            String comand_for_sql = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber, Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
            comand_for_sql = comand_for_sql + label3.Text;
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

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            String comand_for_sql = "SELECT Issuances.IssuanceNumber, Issuances.BookInventoryNumber, Books.BookName, Books.BookAuthor, Books.PublisherName, Issuances.IssuanceDateStart, Issuances.IssuanceDateEnd FROM Issuances INNER JOIN Books ON Issuances.BookInventoryNumber = Books.BookInventoryNumber WHERE Id = ";
            comand_for_sql = comand_for_sql + label3.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string id = label3.Text;
            string name = label5.Text;
            string phone = label7.Text;
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

    }

}
