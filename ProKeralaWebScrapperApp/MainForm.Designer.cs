namespace ProKeralaWebScrapperApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.locationComboBox = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timeSpanComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.logBox = new System.Windows.Forms.ListBox();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export file path";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 31);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(324, 23);
            this.txtPath.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(342, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Export file name with extension";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(11, 77);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(406, 23);
            this.txtFileName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Start Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "End Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Location";
            // 
            // locationComboBox
            // 
            this.locationComboBox.FormattingEnabled = true;
            this.locationComboBox.Location = new System.Drawing.Point(11, 165);
            this.locationComboBox.Name = "locationComboBox";
            this.locationComboBox.Size = new System.Drawing.Size(406, 23);
            this.locationComboBox.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(11, 194);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(406, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 223);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(404, 23);
            this.progressBar.TabIndex = 12;
            // 
            // timeSpanComboBox
            // 
            this.timeSpanComboBox.FormattingEnabled = true;
            this.timeSpanComboBox.Location = new System.Drawing.Point(291, 466);
            this.timeSpanComboBox.Name = "timeSpanComboBox";
            this.timeSpanComboBox.Size = new System.Drawing.Size(121, 23);
            this.timeSpanComboBox.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 448);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Wait time on each call";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.logBox.ForeColor = System.Drawing.Color.White;
            this.logBox.FormattingEnabled = true;
            this.logBox.HorizontalScrollbar = true;
            this.logBox.ItemHeight = 15;
            this.logBox.Location = new System.Drawing.Point(11, 239);
            this.logBox.Margin = new System.Windows.Forms.Padding(10);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(406, 199);
            this.logBox.TabIndex = 16;
            this.logBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.logBox_DrawItem);
            this.logBox.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.logBox_MeasureItem);
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(11, 121);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(200, 23);
            this.startDate.TabIndex = 6;
            this.startDate.ValueChanged += new System.EventHandler(this.startDate_ValueChanged);
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(217, 121);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(200, 23);
            this.endDate.TabIndex = 8;
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 497);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.timeSpanComboBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.locationComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "ProKerala Web Scrapper App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog;
        private Label label1;
        private TextBox txtPath;
        private Button button1;
        private Label label2;
        private TextBox txtFileName;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox locationComboBox;
        private Button btnExport;
        private ProgressBar progressBar;
        private ComboBox timeSpanComboBox;
        private Label label6;
        private System.Windows.Forms.Timer timer1;
        private ListBox logBox;
        private DateTimePicker startDate;
        private DateTimePicker endDate;
    }
}