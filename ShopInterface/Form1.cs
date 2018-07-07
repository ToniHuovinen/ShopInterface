using System;
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

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            productList.Items.Clear();

            for (int i = 0; i < productListing.Count; i++)
            {
                productList.Items.Add(productListing[i].Barcode + " " + productListing[i].ProductName + " " + productListing[i].UnitPrice);
            }
        }

        private void productList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int amount = 1;
            decimal priceToForm = 0;

            // Determine selected index of selected product, then add that product to
            // shoppingcartlist.
            selectedProduct = productList.SelectedIndex;
            shoppingCartListing.Add(productListing[selectedProduct]);

            // Create a row, and populate that row with the information of the product
            // TODO - Come up with a logic that allows to check if the product is already in the list. If it is, raise amount by one.
            DataGridViewRow row = (DataGridViewRow)productsGrid.Rows[0].Clone();
            row.Cells[0].Value = shoppingCartListing[selectedProduct].Barcode;
            row.Cells[1].Value = shoppingCartListing[selectedProduct].ProductName;
            row.Cells[2].Value = amount;
            row.Cells[3].Value = shoppingCartListing[selectedProduct].UnitPrice;
            row.Cells[4].Value = amount * shoppingCartListing[selectedProduct].UnitPrice;

            // Add new row to datagrid
            productsGrid.Rows.Add(row);

            // Calculates total price
            for (int i = 0; i < productsGrid.Rows.Count-1; i++)
            {
                priceToForm += (decimal)productsGrid.Rows[i].Cells[4].Value;
                totalPriceLbl.Text = priceToForm.ToString();
            }
            priceToForm = 0;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
