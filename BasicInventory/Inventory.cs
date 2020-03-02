using BasicInventory.InventoryClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
