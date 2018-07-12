using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ShopInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int selectedProduct;
        bool isNewProduct;

        // List of products. User can transfer products from this list to shopping basket
        static public List<Product> productListing = new List<Product>();

        // List of products that are in the shopping cart.
        static public List<Product> shoppingCartListing = new List<Product>();

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addNewBtn_Click(object sender, EventArgs e)
        {
            NewProduct nProduct = new NewProduct();
            nProduct.Show();
        }


        // Refresh the product list. This content comes from either through form or from database
        private void refreshBtn_Click(object sender, EventArgs e)
        {
            productList.Items.Clear();

            for (int i = 0; i < productListing.Count; i++)
            {
                productList.Items.Add(productListing[i].Barcode + "\t" + productListing[i].UnitPrice + "\t" + productListing[i].ProductName);
            }
        }

        private void productList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int amount = 1;
            int itemAmount = 1;
            decimal priceToForm = 0;


            // Determine selected index of selected product, then add that product to
            // shoppingcartlist.
            selectedProduct = productList.SelectedIndex;

            // If there is no product in the cart, then it is new by default
            if (shoppingCartListing.Count == 0)
            {
                isNewProduct = true;
            }
            else
            {
                // Iterate through the shopping cart and check if the selected product is there already
                for (int i = 0; i < shoppingCartListing.Count; i++)
                {
                    // If product exists, check its current amount and add one to it. Then calculate the total price
                    if (shoppingCartListing[i].Barcode == productListing[selectedProduct].Barcode)
                    {
                        isNewProduct = false;

                        itemAmount = (int)productsGrid.Rows[i].Cells[2].Value;
                        productsGrid.Rows[i].Cells[2].Value = itemAmount + 1;

                        productsGrid.Rows[i].Cells[4].Value = (int)productsGrid.Rows[i].Cells[2].Value * shoppingCartListing[i].UnitPrice;

                        itemAmount = 1;

                        break;
                    }
                    else
                    {
                        isNewProduct = true;
                    }
                }
            }

            if (isNewProduct)
            {
                // If product is new, then add it into the cart, create all the information for it.
                shoppingCartListing.Add(productListing[selectedProduct]);

                DataGridViewRow row = (DataGridViewRow)productsGrid.Rows[0].Clone();

                row.Cells[0].Value = shoppingCartListing[selectedProduct].Barcode;
                row.Cells[1].Value = shoppingCartListing[selectedProduct].ProductName;
                row.Cells[2].Value = amount;
                row.Cells[3].Value = shoppingCartListing[selectedProduct].UnitPrice;
                row.Cells[4].Value = amount * shoppingCartListing[selectedProduct].UnitPrice;

                // Add new row to datagrid
                productsGrid.Rows.Add(row);
            }

            // Calculates total price
            for (int i = 0; i < shoppingCartListing.Count; i++)
            {
                priceToForm += (decimal)productsGrid.Rows[i].Cells[4].Value;
                totalPriceLbl.Text = priceToForm.ToString();
            }
            priceToForm = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Nothing here yet.
        }

        private void fromDBBtn_Click(object sender, EventArgs e)
        {
            productList.Items.Clear();

            try
            {
                string queryString = "SELECT * FROM products";

                // Create connection, command and reader. Change the connection string according to your needs, mines just default local db for debug use
                // Using phpMyAdmin (XAMPP) as database
                MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=store");
                MySqlCommand command = new MySqlCommand(queryString, connection);
                connection.Open();

                MySqlDataReader reader;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    productListing.Add(new Product(reader.GetInt32(1), reader.GetString(2), (decimal)reader.GetFloat(3)));
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
