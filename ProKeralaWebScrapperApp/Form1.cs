using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ProKeralaWebScrapperApp
{
    public partial class Form1 : Form
    {
        private Logger logger;

        public Form1()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            logBox.DrawMode = DrawMode.OwnerDrawVariable;
            logger = new Logger(logBox);
            logger.log("Welcome to ProKerala web scrapper application, you can select all the values above and click on export");

            for (int i=0;i< 15; i++)
            {
                int year = 2022 + i;
                yearComboBox.Items.Add(year.ToString());
            }
            yearComboBox.SelectedIndex = 0;

            string[] months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            foreach(string month in months)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    monthComboBox.Items.Add(month.ToString());
                }
            }

            DateTime dt = DateTime.Now;
            string currentMonth = dt.ToString("MMMM");
            monthComboBox.SelectedItem = currentMonth;

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = new Location().GetLocations();
            locationComboBox.DataSource = bindingSource;
            locationComboBox.DisplayMember = "locationName";
            locationComboBox.SelectedIndex = 0;

            BindingSource timeSpanceBindingSource = new BindingSource();
            timeSpanceBindingSource.DataSource = new TimeSpan().GetTimeSpans();
            timeSpanComboBox.DataSource = timeSpanceBindingSource;
            timeSpanComboBox.DisplayMember = "displayMS";
            timeSpanComboBox.SelectedIndex = 1;

            txtPath.Text = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Desktop";
            this.updateFileName();

        }

        private void updateFileName()
        {
            txtFileName.Text = ((Location)locationComboBox.SelectedItem).locationName + "-" + monthComboBox.SelectedItem.ToString() + "-" + yearComboBox.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void enbaleComponents (bool isEnable)
        {
            btnExport.Enabled = isEnable;
            txtFileName.Enabled = isEnable;
            button1.Enabled = isEnable;
            yearComboBox.Enabled = isEnable;
            monthComboBox.Enabled = isEnable;
            locationComboBox.Enabled = isEnable;
            timeSpanComboBox.Enabled = isEnable;
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            var fileName = txtFileName.Text + ".xlsx";
            enbaleComponents(false);
            await ExportHelper.ExportPanchangData(
                yearComboBox.SelectedItem.ToString(), 
                monthComboBox.SelectedItem.ToString(), 
                monthComboBox.SelectedIndex + 1, 
                fileName, 
                txtPath.Text,
                (Location)locationComboBox.SelectedItem,
                progressBar,
                logger,
                ((TimeSpan)timeSpanComboBox.SelectedItem).durationMS
            );

            logger.success($"{fileName} is created in {txtPath.Text}" + Environment.NewLine);
            enbaleComponents(true);
            MessageBox.Show($"{fileName} is created in {txtPath.Text}", "Export file success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void yearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locationComboBox.SelectedItem != null)
            {
                this.updateFileName();
            }
        }

        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locationComboBox.SelectedItem != null)
            {
                this.updateFileName();
            }
        }

        private void logBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var item = logBox.Items[e.Index] as ColoredItem;

            if (item != null)
            {
                e.Graphics.DrawString(
                    item.Text,
                    e.Font,
                    new SolidBrush(item.Color),
                    e.Bounds);
            }
        }

        private void logBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(((ColoredItem)logBox.Items[e.Index]).Text, logBox.Font, logBox.Width).Height;
        }
    }
}