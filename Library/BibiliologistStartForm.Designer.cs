namespace Library
{
    partial class BibiliologistStartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMainWindow = new System.Windows.Forms.Button();
            this.buttonForm4 = new System.Windows.Forms.Button();
            this.buttonReaderWindow = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonMainWindow, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonForm4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonReaderWindow, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(614, 569);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonMainWindow
            // 
            this.buttonMainWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMainWindow.Location = new System.Drawing.Point(3, 3);
            this.buttonMainWindow.Name = "buttonMainWindow";
            this.buttonMainWindow.Size = new System.Drawing.Size(608, 183);
            this.buttonMainWindow.TabIndex = 0;
            this.buttonMainWindow.Text = "Управление банком книг";
            this.buttonMainWindow.UseVisualStyleBackColor = true;
            this.buttonMainWindow.Click += new System.EventHandler(this.buttonMainWindow_Click);
            // 
            // buttonForm4
            // 
            this.buttonForm4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonForm4.Location = new System.Drawing.Point(3, 192);
            this.buttonForm4.Name = "buttonForm4";
            this.buttonForm4.Size = new System.Drawing.Size(608, 183);
            this.buttonForm4.TabIndex = 1;
            this.buttonForm4.Text = "Печать формы 4";
            this.buttonForm4.UseVisualStyleBackColor = true;
            this.buttonForm4.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonReaderWindow
            // 
            this.buttonReaderWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReaderWindow.Location = new System.Drawing.Point(3, 381);
            this.buttonReaderWindow.Name = "buttonReaderWindow";
            this.buttonReaderWindow.Size = new System.Drawing.Size(608, 185);
            this.buttonReaderWindow.TabIndex = 2;
            this.buttonReaderWindow.Text = "Кабинет читателя";
            this.buttonReaderWindow.UseVisualStyleBackColor = true;
            this.buttonReaderWindow.Click += new System.EventHandler(this.buttonReaderWindow_Click);
            // 
            // BibiliologistStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 569);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BibiliologistStartForm";
            this.Text = "Основное окно книговеда";
            this.Load += new System.EventHandler(this.BibiliologistStartForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonMainWindow;
        private System.Windows.Forms.Button buttonForm4;
        private System.Windows.Forms.Button buttonReaderWindow;
    }
}