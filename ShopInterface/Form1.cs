﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                isNewProduct = IsProductNew();
            }

            if (isNewProduct)
            {
                
                // Uses the count of products in shoppingcart as a location value below. Adds new product on that location.
                int productPlace = shoppingCartListing.Count;

                AddNewProduct(productPlace, amount);
                
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

                DatabaseConnection.GetFromDatabase(queryString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        // METHODS


        private void AddNewProduct(int place, int amount)
        {
            // If product is new, then add it into the cart, create all the information for it.
            shoppingCartListing.Add(productListing[selectedProduct]);

            DataGridViewRow row = (DataGridViewRow)productsGrid.Rows[0].Clone();

            // Here is a bug. selectedProduct must be switched to something else
            row.Cells[0].Value = shoppingCartListing[place].Barcode;
            row.Cells[1].Value = shoppingCartListing[place].ProductName;
            row.Cells[2].Value = amount;
            row.Cells[3].Value = shoppingCartListing[place].UnitPrice;
            row.Cells[4].Value = amount * shoppingCartListing[place].UnitPrice;



            // Add new row to datagrid
            productsGrid.Rows.Add(row);
        }


        private bool IsProductNew()
        {
            bool isNewProduct = false;

            for (int i = 0; i < shoppingCartListing.Count; i++)
            {
                //If product exists, check its current amount and add one to it.Then calculate the total price
                if (shoppingCartListing[i].Barcode == productListing[selectedProduct].Barcode)
                {
                    isNewProduct = false;

                    int itemAmount = (int)productsGrid.Rows[i].Cells[2].Value;
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

            return isNewProduct;
        }
    }
}
