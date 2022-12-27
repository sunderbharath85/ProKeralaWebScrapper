using OfficeOpenXml;
using System.Globalization;
using System.Reflection;

namespace CalendarWebScrapperApp
{
    public partial class MainForm : Form
    {
        private Logger logger;

        public void initializeLocationsDropdown()
        {
            var locations = DataAccess.GetLocations();
            locations.Add(new Location { LocationCode = "", LocationName = "New" });

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = locations;
            locationComboBox.DataSource = bindingSource;
            locationComboBox.DisplayMember = "LocationName";
            locationComboBox.SelectedIndex = 0;
        }

        public MainForm()
        {
            DataAccess.InitializeDatabase();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            logBox.DrawMode = DrawMode.OwnerDrawVariable;
            logger = new Logger(logBox);
            logger.log("Welcome to ProKerala web scrapper application, you can select all the values above and click on export");

            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddDays(365);

            this.initializeLocationsDropdown();

            BindingSource timeSpanceBindingSource = new BindingSource();
            timeSpanceBindingSource.DataSource = new TimeSpan().GetTimeSpans();
            timeSpanComboBox.DataSource = timeSpanceBindingSource;
            timeSpanComboBox.DisplayMember = "displayMS";
            timeSpanComboBox.SelectedIndex = 0;

            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            txtLocationCode.Enabled = false;
            updateFileName();

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
         private async void btnExport_Click(object sender, EventArgs e)
        {
            var fileName = txtFileName.Text + ".xlsx";
            if (endDate.Value < startDate.Value)
            {
                MessageBox.Show($"End date ({endDate.Value.ToString("D")}) is less than start date ({startDate.Value.ToString("D")})", "Date validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                endDate.Value = startDate.Value.AddDays(365);
            }
            else
            {
                enbaleComponents(false);
                await ExportHelper.ExportPanchangData(
                    startDate.Value,
                    endDate.Value,
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

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            endDate.Value = startDate.Value.AddDays(365);
            if (locationComboBox.SelectedItem != null)
            {
                updateFileName();
            }
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            
            if (locationComboBox.SelectedItem != null)
            {
                updateFileName();
            }
        }

        private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locationComboBox.SelectedItem != null)
            {
                updateFileName();
                txtLocationCode.Text = ((Location)locationComboBox.SelectedItem).LocationCode;
                if (((Location)locationComboBox.SelectedItem).LocationName == "New")
                {
                    var form = new LocationForm(this);
                    form.Show();
                }
            }
        }

        #region form_helper
        private void enbaleComponents(bool isEnable)
        {
            btnExport.Enabled = isEnable;
            txtFileName.Enabled = isEnable;
            button1.Enabled = isEnable;
            startDate.Enabled = isEnable;
            endDate.Enabled = isEnable;
            locationComboBox.Enabled = isEnable;
            timeSpanComboBox.Enabled = isEnable;
        }

        private void updateFileName()
        {
            txtFileName.Text = ((Location)locationComboBox.SelectedItem).LocationName + "-" + startDate.Value.ToString("dd-MMMM-yyyy") + "-" + endDate.Value.ToString("dd-MMMM-yyyy");
        }

        #endregion

    }
}