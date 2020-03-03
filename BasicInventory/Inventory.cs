using BasicInventory.InventoryClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicInventory
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        ItemClass i = new ItemClass();

        private void Inventory_Load(object sender, EventArgs e)
        {
            //Load Data Table
            DataTable dt = i.Select();
            itemsList.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get form fields
            try
            {
                i.ItemId = Int32.Parse(textBoxItemId.Text);
                i.ItemName = textBoxItemName.Text;
                i.UnitPrice = float.Parse(textBoxUnitPrice.Text);
                i.Quantity = textBoxQuantity.Text;
                i.Supplier = textBoxSupplier.Text;
                i.Date = textBoxDate.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid Format(s) of data");
            }
            

            //Insert data into Database
            bool isSuccessful = i.Insert(i);
            if(isSuccessful == true)
            {
                MessageBox.Show("Data Inserted Successfully");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to insert data");
            }

            //Load data table
            DataTable dt = i.Select();
            itemsList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Clear input fields
        public void Clear()
        {
            textBoxItemId.Text = "";
            textBoxItemId.ReadOnly = false;
            textBoxItemName.Text = "";
            textBoxQuantity.Text = "";
            textBoxUnitPrice.Text = "";
            textBoxSupplier.Text = "";
            textBoxDate.Text = "";

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                i.ItemId = Int32.Parse(textBoxItemId.Text);
                i.ItemName = textBoxItemName.Text;
                i.UnitPrice = float.Parse(textBoxUnitPrice.Text);
                i.Quantity = textBoxQuantity.Text;
                i.Supplier = textBoxSupplier.Text;
                i.Date = textBoxDate.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Format(s) of data");
            }

            bool isSuccessful = i.Update(i);
            if(isSuccessful == true)
            {
                MessageBox.Show("Data Updated Successfully");
                Clear();
                DataTable dt = i.Select();
                itemsList.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to update data");
            }
        }

        private void itemsList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get row data from data grid to input fields
            int rowIndex = e.RowIndex;
            textBoxItemId.Text = itemsList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxItemId.ReadOnly = true;
            textBoxItemName.Text = itemsList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxUnitPrice.Text = itemsList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxQuantity.Text = itemsList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxSupplier.Text = itemsList.Rows[rowIndex].Cells[4].Value.ToString();
            textBoxDate.Text = itemsList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                i.ItemId = Int32.Parse(textBoxItemId.Text);
                bool isSuccessful = i.Delete(i);
                if (isSuccessful)
                {
                    MessageBox.Show("Item Deleted Successfully");
                    Clear();
                    DataTable dt = i.Select();
                    itemsList.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Error Deleting Item");
                }
            }
            catch
            {
                MessageBox.Show("Please select an item to delete");
            }
        }

        static string myconn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_items WHERE ItemId LIKE '%" + keyword + "%' OR ItemName LIKE '%" + keyword + "%' OR Supplier LIKE '%" + keyword + "%' OR Date LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            itemsList.DataSource = dt;
        }
    }
}
