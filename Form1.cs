using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string selectedImagePath = "";

        private void btnAddEmployeeScreen_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private bool IsDuplicateID(string ID)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if(item.Text == ID)
                    return true;
            }
            return false;
        }

        private void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddEmployeeID.Text) || string.IsNullOrWhiteSpace(tbFullName.Text) || string.IsNullOrWhiteSpace(tbDepartment.Text)
                || string.IsNullOrWhiteSpace(tbJob.Text) || string.IsNullOrWhiteSpace(tbSalary.Text) || string.IsNullOrWhiteSpace(mtbPhoneNumber.Text))
                return;

            string EmployeeID = tbAddEmployeeID.Text.Trim();

            if(IsDuplicateID(EmployeeID))
            {
                MessageBox.Show("Employee with this ID already exists!", "Duplicate ID", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            ListViewItem Item = new ListViewItem(EmployeeID);

            if (rbMale.Checked)
                Item.ImageIndex = 0;
            else
                Item.ImageIndex = 1;

            Item.SubItems.Add(tbFullName.Text.Trim());
            if (Item.ImageIndex == 0)
                Item.SubItems.Add("Male");
            else
                Item.SubItems.Add("Female");

            Item.SubItems.Add(tbJob.Text.Trim());
            Item.SubItems.Add(tbDepartment.Text.Trim());
            Item.SubItems.Add(tbSalary.Text.Trim());
            Item.SubItems.Add(dtHireDate.Value.ToShortDateString());
            Item.SubItems.Add(mtbPhoneNumber.Text.Trim());
            Item.Tag = selectedImagePath;

            listView1.Items.Add(Item);


            MessageBox.Show("Employee was added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            tbAddEmployeeID.Clear();
            tbFullName.Clear();
            tbDepartment.Clear();
            tbJob.Clear();
            tbSalary.Clear();
            mtbPhoneNumber.Clear();
            dtHireDate.Value = DateTime.Now;

            tbAddEmployeeID.Focus();

            picEmployeePhoto.Image = null;
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            string IDToRemove = tbFindEmployeeID.Text.Trim();

            if (string.IsNullOrWhiteSpace(IDToRemove))
            {
                MessageBox.Show("Please enter an employee ID.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbFindEmployeeID.Focus();
                return;
            }

            ListViewItem item = listView1.FindItemWithText(IDToRemove, false, 0, false);

            if (item != null)
            {
                listView1.Items.Remove(item);
                MessageBox.Show("Employee removed successfully.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tbFindEmployeeID.Clear();
                tbFindEmployeeID.Focus();
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void tbFindEmployeeID_TextChanged(object sender, EventArgs e)
        {

            if (listView1.Items.Count == 0 || string.IsNullOrWhiteSpace(tbFindEmployeeID.Text))
            {
                ClearLabels();
                return;
            }

            ListViewItem Item = listView1.FindItemWithText(tbFindEmployeeID.Text, false, 0, false);

            
            if (Item != null)
            {
                lblFullName.Text = Item.SubItems[1].Text;
                lblGender.Text = Item.SubItems[2].Text;
                lblJob.Text = Item.SubItems[3].Text;
                lblDepartment.Text = Item.SubItems[4].Text;
                lblSalary.Text = Item.SubItems[5].Text;
                lblHireDate.Text = Item.SubItems[6].Text;
                lblPhoneNumber.Text = Item.SubItems[7].Text;

            }
            else
            {
                ClearLabels();
            }
        }

        private void ClearLabels()
        {
            lblFullName.Text = "";
            lblGender.Text = "";
            lblJob.Text = "";
            lblDepartment.Text = "";
            lblSalary.Text = "";
            lblHireDate.Text = "";
            lblPhoneNumber.Text = "";
        }

        private void rbDetails_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void rbSmallIcon_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void rbList_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void rbLargeIcon_CheckedChanged(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                ListViewItem Item = new ListViewItem(i.ToString());

                if (i % 2 == 0)
                    Item.ImageIndex = 0;
                else
                    Item.ImageIndex = 1;


                Item.SubItems.Add("Person " + i);

                if (Item.ImageIndex == 0)
                {
                    Item.SubItems.Add(rbMale.Text);
                    Item.Tag = Item.ImageIndex;
                }
                else
                {
                    Item.SubItems.Add(rbFemale.Text);
                    Item.Tag = Item.ImageIndex;

                }

                Item.SubItems.Add("Department " + i);
                Item.SubItems.Add("Job " + i);
                Item.SubItems.Add("Salary " + i);
                Item.SubItems.Add("Hire Date " + i);
                Item.SubItems.Add("Phone Number " + i);
                listView1.Items.Add(Item);
            }
        }


        private void btnEmployeeInfo_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select Employee Photo";
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                picEmployeePhoto.Image = Image.FromFile(openFile.FileName);

                // ✅ Store the path for later use when adding the employee
                selectedImagePath = openFile.FileName;
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem SelectedItem = listView1.SelectedItems[0];

                string ImagePath = SelectedItem.Tag.ToString();

                if(ImagePath == "0")
                {
                    pbEmployeeInfo.Image = imageList2.Images[0];
                }
                else if(ImagePath == "1") 
                {
                    pbEmployeeInfo.Image = imageList2.Images[1];
                }

                else if (!string.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
                {
                    pbEmployeeInfo.Image = Image.FromFile(ImagePath);
                }
                else
                {
                    pbEmployeeInfo.Image = null;
                }
            }
        }

        
    }
}
