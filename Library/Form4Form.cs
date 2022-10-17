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

namespace Library
{
    public partial class Form4Form : Form
    {
        private SqlConnection sqlConnection = null;
        public Form4Form()
        {
            InitializeComponent();
        }

        private void Form4Form_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            sqlConnection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter($"EXEC CreateForm4", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView.DataSource = dataSet.Tables[0];
            Console.WriteLine(dataGridView.Columns[2]);
            dataGridView.Sort(dataGridView.Columns[2], 0);
            dataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            String bookName = dataGridView.Rows[0].Cells[2].Value.ToString();
            
            for (int i = 1; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Cells[2].Value.ToString() == bookName)
                {
                    dataGridView.Rows[i].Cells[1].Value = "";
                    // dataGridView.Rows[i].Cells[2].Value = "";
                    dataGridView.Rows[i].Cells[3].Value = "";
                    dataGridView.Rows[i].Cells[4].Value = "";
                }
                else
                {
                    bookName = dataGridView.Rows[i].Cells[2].Value.ToString();
                }
            }
            


        }

        private void buttonPrintForm4_Click(object sender, EventArgs e)
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                books.Add(new Book()
                {
                    disciplineName = dataGridView[0, i].Value.ToString(),
                    bookAuthor = dataGridView[1, i].Value.ToString(),
                    bookName = dataGridView[2, i].Value.ToString(),
                    publisherName = dataGridView[3, i].Value.ToString(),
                    publishingPlace = dataGridView[4, i].Value.ToString(),
                    publishingYear = dataGridView[5, i].Value.ToString(),
                    quantity = dataGridView[6, i].Value.ToString(),
                });
            }
            GenerateForm(books);
        }

        private static void GenerateForm(List <Book> books)
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
            gfx.DrawString("Форма 4", new XFont("Arial", 40, XFontStyle.Bold), XBrushes.Black, new XPoint(200, 70));
            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(100, 100), new XPoint(500, 100));

            gfx.DrawString("Дисциплины", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(24, 320));

            gfx.DrawString("Авторы", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(150, 320));


            gfx.DrawString("Название книги", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(220, 320));

            gfx.DrawString("Название", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(345, 300));
            gfx.DrawString("издательства", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(340, 320));

            gfx.DrawString("Место", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(435, 300));
            gfx.DrawString("издания", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(430, 320));

            gfx.DrawString("Год", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(510, 300));
            gfx.DrawString("издания", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(500, 320));

            gfx.DrawString("К", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 230));
            gfx.DrawString("о", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 240));
            gfx.DrawString("л", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 250));
            gfx.DrawString("и", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 260));
            gfx.DrawString("ч", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 270));
            gfx.DrawString("е", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 280));
            gfx.DrawString("с", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 290));
            gfx.DrawString("т", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 300));
            gfx.DrawString("в", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 310));
            gfx.DrawString("о", new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(560, 320));



            gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, 330), new XPoint(580, 330));

            int currentYposition_values = 343;
            int currentYposition_line = 350;

            if (books.Count <= 20)
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].disciplineName.Length > 21)
                    {
                        gfx.DrawString(books[i].disciplineName.Substring(0, 21), new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    }
                    else
                    {
                        gfx.DrawString(books[i].disciplineName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    }
                    gfx.DrawString(books[i].bookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(150, currentYposition_values));
                    gfx.DrawString(books[i].bookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    gfx.DrawString(books[i].publisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(340, currentYposition_values));
                    gfx.DrawString(books[i].publishingPlace, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(420, currentYposition_values));
                    gfx.DrawString(books[i].publishingYear, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(508, currentYposition_values));
                    gfx.DrawString(books[i].quantity, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(558, currentYposition_values));
                    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));

                    currentYposition_values += 20;
                    currentYposition_line += 20;
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {

                    gfx.DrawString(books[i].disciplineName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    gfx.DrawString(books[i].bookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(150, currentYposition_values));
                    gfx.DrawString(books[i].bookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    gfx.DrawString(books[i].publisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(340, currentYposition_values));
                    gfx.DrawString(books[i].publishingPlace, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(420, currentYposition_values));
                    gfx.DrawString(books[i].publishingYear, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(508, currentYposition_values));
                    gfx.DrawString(books[i].quantity, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(558, currentYposition_values));
                    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));


                    currentYposition_values += 20;
                    currentYposition_line += 20;
                    books.Remove(books[i]);
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
                for (int i = 0; i < books.Count; i++)
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

                    gfx.DrawString(books[i].disciplineName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(20, currentYposition_values));
                    gfx.DrawString(books[i].bookAuthor, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(150, currentYposition_values));
                    gfx.DrawString(books[i].bookName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(220, currentYposition_values));
                    gfx.DrawString(books[i].publisherName, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(340, currentYposition_values));
                    gfx.DrawString(books[i].publishingPlace, new XFont("Arial", 9, XFontStyle.Bold), XBrushes.Black, new XPoint(420, currentYposition_values));
                    gfx.DrawString(books[i].publishingYear, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(508, currentYposition_values));
                    gfx.DrawString(books[i].quantity, new XFont("Arial", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(558, currentYposition_values));
                    gfx.DrawLine(new XPen(XColor.FromArgb(50, 30, 200)), new XPoint(20, currentYposition_line), new XPoint(580, currentYposition_line));

                    currentYposition_values += 20;
                    currentYposition_line += 20;
                }

            }

            document.Save(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Форма4.pdf");

            Process.Start(@"D:\ИрНИТУ\5-й семестр\Базы данных\Курсовая работа\Library\Форма4.pdf");
        }
    }
    class Book
    {
        public string disciplineName { get; set; }
        public string bookAuthor { get; set; }
        public string bookName { get; set; }
        public string publisherName { get; set; }
        public string publishingPlace { get; set; }
        public string publishingYear { get; set; }
        public string quantity { get; set; }

    }
}
