using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicInventory.InventoryClasses
{
    class ItemClass
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public float UnitPrice { get; set; }
        public string Supplier { get; set; }
        public string Date { get; set; }
        public string Quantity { get; set; }

        //Database Connection config
        static string myconn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Retrieving from Database
        public DataTable Select()
        {
            //Database Connection
            SqlConnection conn = new SqlConnection(myconn);
            DataTable dt = new DataTable();
            try
            {
                //SQL Query
                string sql = "SELECT * FROM tbl_items";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        //Inserting to Database
        public bool Insert(ItemClass i)
        {
            bool isSuccessful = false;

            //Databse Connection
            SqlConnection conn = new SqlConnection(myconn);
            try
            {
                //SQL Query
                string sql = "INSERT INTO tbl_items (ItemId, ItemName, UnitPrice, Quantity, Supplier, Date) VALUES(@ItemId, @ItemName, @UnitPrice, @Quantity, @Supplier, @Date)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ItemId", i.ItemId);
                cmd.Parameters.AddWithValue("@ItemName", i.ItemName);
                cmd.Parameters.AddWithValue("@UnitPrice", i.UnitPrice);
                cmd.Parameters.AddWithValue("@Quantity", i.Quantity);
                cmd.Parameters.AddWithValue("@Supplier", i.Supplier);
                cmd.Parameters.AddWithValue("@Date", i.Date);

                conn.Open();
                int querySuccessful = cmd.ExecuteNonQuery();
                //If successful > 0 
                if(querySuccessful > 0)
                {
                    isSuccessful = true;
                }
            }
            catch(Exception e)
            {
                
            }
            finally
            {
                conn.Close();
            }

            return isSuccessful;
        }

        //Updating Database
        public bool Update(ItemClass i)
        {
            bool isSuccessful = false;

            //Databse Connection
            SqlConnection conn = new SqlConnection(myconn);
            try
            {
                //SQL Query
                string sql = "UPDATE tbl_items SET ItemId = @ItemId, ItemName = @ItemName, UnitPrice = @UnitPrice, Quantity = @Quantity, Supplier = @Supplier, Date =  @Date";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ItemId", i.ItemId);
                cmd.Parameters.AddWithValue("@ItemName", i.ItemName);
                cmd.Parameters.AddWithValue("@UnitPrice", i.UnitPrice);
                cmd.Parameters.AddWithValue("@Quantity", i.Quantity);
                cmd.Parameters.AddWithValue("@Supplier", i.Supplier);
                cmd.Parameters.AddWithValue("@Date", i.Date);

                conn.Open();
                int querySuccessful = cmd.ExecuteNonQuery();
                //If successful > 0 
                if (querySuccessful > 0)
                {
                    isSuccessful = true;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccessful;
        }

        //Deleting from Database
        public bool Delete(ItemClass i)
        {
            bool isSuccessful = false;

            SqlConnection conn = new SqlConnection(myconn);

            try
            {
                //SQL Query
                string sql = "DELETE FROM tbl_items WHERE ItemId = @ItemId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ItemId", i.ItemId);

                conn.Open();
                int querySuccessful = cmd.ExecuteNonQuery();
                //If successful > 0 
                if (querySuccessful > 0)
                {
                    isSuccessful = true;
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccessful;
        }
    }
}
