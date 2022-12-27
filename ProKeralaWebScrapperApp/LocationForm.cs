using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalendarWebScrapperApp
{
    public partial class LocationForm : Form
    {
        List<Location> locations = new List<Location>();
        int selectedLocationIndex = -1;
        MainForm mainForm;
        public LocationForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void loadData()
        {
            locations = DataAccess.GetLocations();
            dgLocations.DataSource = locations;
            dgLocations.Refresh();
        }

        private void LocationForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedLocationIndex == -1) {
                DataAccess.AddLocation(new Location { LocationCode = txtLocationCode.Text, LocationName= txtLocationName.Text });
            } else {
                Location location = locations[selectedLocationIndex];
                location.LocationName = txtLocationName.Text;
                location.LocationCode = txtLocationCode.Text;
                DataAccess.UpdateLocation(location);
            }

            DialogResult dr = MessageBox.Show("Saved data successfully");
            if (dr == DialogResult.OK)
            {
                mainForm.initializeLocationsDropdown();
            }
            loadData();
        }

        private void onClearData()
        {
            txtLocationCode.Text = "";
            txtLocationName.Text = "";
            selectedLocationIndex = -1;
            foreach (DataGridViewRow selectedRow in dgLocations.SelectedRows)
            {
                selectedRow.Selected = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            onClearData();
        }

        private int getSelectedIndex()
        {
            if (dgLocations.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect 
                && dgLocations.SelectedRows.Count > 0)
            {
                selectedLocationIndex = dgLocations.SelectedRows[0].Index;
            }
            return selectedLocationIndex;
        }

        private void dgLocations_SelectionChanged(object sender, EventArgs e)
        {
            var index = getSelectedIndex();
            if (index != -1)
            {
                var location = locations[index];
                txtLocationCode.Text = location.LocationCode;
                txtLocationName.Text = location.LocationName;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedLocationIndex != -1 && 
                MessageBox.Show("Are you sure want to delete?", "Delete Location", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var location = locations[selectedLocationIndex];
                DataAccess.DeleteLocation(location.Id);
                onClearData();
                loadData();
                mainForm.initializeLocationsDropdown();
            }
        }
    }
}
