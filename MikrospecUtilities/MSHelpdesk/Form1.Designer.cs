﻿
namespace MSHelpdesk
{
    partial class Form1
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
            this.btnEles = new System.Windows.Forms.Button();
            this.btnTeszt = new System.Windows.Forms.Button();
            this.lblSrv = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.taskpage606 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edtTETELSZAM = new System.Windows.Forms.TextBox();
            this.edtSORSZAMIG = new System.Windows.Forms.TextBox();
            this.edtSORSZAMTOL = new System.Windows.Forms.TextBox();
            this.edtAZONKH = new System.Windows.Forms.TextBox();
            this.edtFH = new System.Windows.Forms.TextBox();
            this.edtPEP = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.taskpage602 = new System.Windows.Forms.TabPage();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnToCsv = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btn602Kihagyott = new System.Windows.Forms.Button();
            this.btn602Dupla = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tabControl1.SuspendLayout();
            this.taskpage606.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.taskpage602.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEles
            // 
            this.btnEles.Location = new System.Drawing.Point(12, 21);
            this.btnEles.Name = "btnEles";
            this.btnEles.Size = new System.Drawing.Size(170, 23);
            this.btnEles.TabIndex = 0;
            this.btnEles.Text = "Éles kapcsolódás";
            this.btnEles.UseVisualStyleBackColor = true;
            this.btnEles.Click += new System.EventHandler(this.btnEles_Click);
            // 
            // btnTeszt
            // 
            this.btnTeszt.Location = new System.Drawing.Point(188, 21);
            this.btnTeszt.Name = "btnTeszt";
            this.btnTeszt.Size = new System.Drawing.Size(170, 23);
            this.btnTeszt.TabIndex = 1;
            this.btnTeszt.Text = "Teszt kapcsolódás";
            this.btnTeszt.UseVisualStyleBackColor = true;
            this.btnTeszt.Click += new System.EventHandler(this.btnTeszt_Click);
            // 
            // lblSrv
            // 
            this.lblSrv.Location = new System.Drawing.Point(13, 47);
            this.lblSrv.Name = "lblSrv";
            this.lblSrv.Size = new System.Drawing.Size(345, 17);
            this.lblSrv.TabIndex = 4;
            this.lblSrv.Text = "Kapcsolódj szerverhez!";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.taskpage606);
            this.tabControl1.Controls.Add(this.taskpage602);
            this.tabControl1.Location = new System.Drawing.Point(16, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(793, 768);
            this.tabControl1.TabIndex = 5;
            // 
            // taskpage606
            // 
            this.taskpage606.Controls.Add(this.button1);
            this.taskpage606.Controls.Add(this.label4);
            this.taskpage606.Controls.Add(this.label5);
            this.taskpage606.Controls.Add(this.label6);
            this.taskpage606.Controls.Add(this.label3);
            this.taskpage606.Controls.Add(this.label2);
            this.taskpage606.Controls.Add(this.label1);
            this.taskpage606.Controls.Add(this.edtTETELSZAM);
            this.taskpage606.Controls.Add(this.edtSORSZAMIG);
            this.taskpage606.Controls.Add(this.edtSORSZAMTOL);
            this.taskpage606.Controls.Add(this.edtAZONKH);
            this.taskpage606.Controls.Add(this.edtFH);
            this.taskpage606.Controls.Add(this.edtPEP);
            this.taskpage606.Controls.Add(this.textBox1);
            this.taskpage606.Controls.Add(this.logBox);
            this.taskpage606.Controls.Add(this.dataGridView1);
            this.taskpage606.Location = new System.Drawing.Point(4, 22);
            this.taskpage606.Name = "taskpage606";
            this.taskpage606.Padding = new System.Windows.Forms.Padding(3);
            this.taskpage606.Size = new System.Drawing.Size(785, 742);
            this.taskpage606.TabIndex = 0;
            this.taskpage606.Text = "606 kezelése";
            this.taskpage606.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(495, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Tételszám";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Sorszamig";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Sorszamtol";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "AZONKH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "FH";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "EP";
            // 
            // edtTETELSZAM
            // 
            this.edtTETELSZAM.Location = new System.Drawing.Point(333, 58);
            this.edtTETELSZAM.Name = "edtTETELSZAM";
            this.edtTETELSZAM.Size = new System.Drawing.Size(100, 20);
            this.edtTETELSZAM.TabIndex = 12;
            // 
            // edtSORSZAMIG
            // 
            this.edtSORSZAMIG.Location = new System.Drawing.Point(333, 32);
            this.edtSORSZAMIG.Name = "edtSORSZAMIG";
            this.edtSORSZAMIG.Size = new System.Drawing.Size(100, 20);
            this.edtSORSZAMIG.TabIndex = 11;
            // 
            // edtSORSZAMTOL
            // 
            this.edtSORSZAMTOL.Location = new System.Drawing.Point(333, 6);
            this.edtSORSZAMTOL.Name = "edtSORSZAMTOL";
            this.edtSORSZAMTOL.Size = new System.Drawing.Size(100, 20);
            this.edtSORSZAMTOL.TabIndex = 10;
            // 
            // edtAZONKH
            // 
            this.edtAZONKH.Location = new System.Drawing.Point(77, 58);
            this.edtAZONKH.Name = "edtAZONKH";
            this.edtAZONKH.Size = new System.Drawing.Size(100, 20);
            this.edtAZONKH.TabIndex = 9;
            // 
            // edtFH
            // 
            this.edtFH.Location = new System.Drawing.Point(77, 32);
            this.edtFH.Name = "edtFH";
            this.edtFH.Size = new System.Drawing.Size(100, 20);
            this.edtFH.TabIndex = 8;
            // 
            // edtPEP
            // 
            this.edtPEP.Location = new System.Drawing.Point(77, 6);
            this.edtPEP.Name = "edtPEP";
            this.edtPEP.Size = new System.Drawing.Size(100, 20);
            this.edtPEP.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 318);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(753, 164);
            this.textBox1.TabIndex = 6;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(6, 502);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(753, 234);
            this.logBox.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 107);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(753, 190);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.BindingContextChanged += new System.EventHandler(this.dataGridView1_BindingContextChanged);
            // 
            // taskpage602
            // 
            this.taskpage602.Controls.Add(this.lblLog);
            this.taskpage602.Controls.Add(this.btnToCsv);
            this.taskpage602.Controls.Add(this.dataGridView2);
            this.taskpage602.Controls.Add(this.btn602Kihagyott);
            this.taskpage602.Controls.Add(this.btn602Dupla);
            this.taskpage602.Controls.Add(this.dateTimePicker1);
            this.taskpage602.Location = new System.Drawing.Point(4, 22);
            this.taskpage602.Name = "taskpage602";
            this.taskpage602.Padding = new System.Windows.Forms.Padding(3);
            this.taskpage602.Size = new System.Drawing.Size(785, 742);
            this.taskpage602.TabIndex = 1;
            this.taskpage602.Text = "602 -es IF Kódlabor különbözeti kontroll lista";
            this.taskpage602.UseVisualStyleBackColor = true;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(6, 411);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(111, 13);
            this.lblLog.TabIndex = 5;
            this.lblLog.Text = "Futtasd a lekérdezést!";
            this.lblLog.Visible = false;
            // 
            // btnToCsv
            // 
            this.btnToCsv.Location = new System.Drawing.Point(6, 374);
            this.btnToCsv.Name = "btnToCsv";
            this.btnToCsv.Size = new System.Drawing.Size(384, 23);
            this.btnToCsv.TabIndex = 4;
            this.btnToCsv.Text = "Futtasd a lekérdezést!";
            this.btnToCsv.UseVisualStyleBackColor = true;
            this.btnToCsv.Visible = false;
            this.btnToCsv.Click += new System.EventHandler(this.btnToCsv_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 43);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(773, 325);
            this.dataGridView2.TabIndex = 3;
            // 
            // btn602Kihagyott
            // 
            this.btn602Kihagyott.Location = new System.Drawing.Point(623, 14);
            this.btn602Kihagyott.Name = "btn602Kihagyott";
            this.btn602Kihagyott.Size = new System.Drawing.Size(156, 23);
            this.btn602Kihagyott.TabIndex = 2;
            this.btn602Kihagyott.Text = "602 kihagyott tételek";
            this.btn602Kihagyott.UseVisualStyleBackColor = true;
            this.btn602Kihagyott.Click += new System.EventHandler(this.btn602Kihagyott_Click);
            // 
            // btn602Dupla
            // 
            this.btn602Dupla.Location = new System.Drawing.Point(461, 14);
            this.btn602Dupla.Name = "btn602Dupla";
            this.btn602Dupla.Size = new System.Drawing.Size(156, 23);
            this.btn602Dupla.TabIndex = 1;
            this.btn602Dupla.Text = "602 dupla tételek";
            this.btn602Dupla.UseVisualStyleBackColor = true;
            this.btn602Dupla.Click += new System.EventHandler(this.btn602Dupla_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy - MM (MMMM)";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(6, 17);
            this.dateTimePicker1.MinDate = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(148, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2023, 12, 25, 23, 59, 59, 0);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 872);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblSrv);
            this.Controls.Add(this.btnTeszt);
            this.Controls.Add(this.btnEles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.taskpage606.ResumeLayout(false);
            this.taskpage606.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.taskpage602.ResumeLayout(false);
            this.taskpage602.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEles;
        private System.Windows.Forms.Button btnTeszt;
        private System.Windows.Forms.Label lblSrv;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage taskpage606;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage taskpage602;
        private System.Windows.Forms.TextBox edtFH;
        private System.Windows.Forms.TextBox edtPEP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtTETELSZAM;
        private System.Windows.Forms.TextBox edtSORSZAMIG;
        private System.Windows.Forms.TextBox edtSORSZAMTOL;
        private System.Windows.Forms.TextBox edtAZONKH;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn602Kihagyott;
        private System.Windows.Forms.Button btn602Dupla;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnToCsv;
        private System.Windows.Forms.Label lblLog;
    }
}

