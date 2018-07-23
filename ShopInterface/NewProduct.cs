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
    public partial class NewProduct : Form
    {
        public NewProduct()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addToListBtn_Click(object sender, EventArgs e)
        {
            // Assign values to variables, then transfer create product and transfer it to the list on main page
            // TODO - Add try-block and fix validation
            int barcodeInput = int.Parse(barcodeBox.Text);
            string nameInput = nameBox.Text;
            decimal priceInput = decimal.Parse(priceBox.Text);

            Form1.productListing.Add(new Product(barcodeInput, nameInput, priceInput));


            // Clear the textbox fields
            ClearFields();
        }

        private void toDatabaseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int barcodeInput = int.Parse(barcodeBox.Text);
                string nameInput = nameBox.Text;
                string priceInput = priceBox.Text;

                // Even though this is money, the value is passed along as string. First replace the comma with period, then pass it along.
                // Insert command treats it as a numeric value because it doesn't have extra quotes around it to turn it into string.
                string modifiedPrice = priceInput.Replace(',', '.');
                
                // NULL value on ProductID makes the database to increment ID with one since it is set as Auto Increment
                string toDatabaseQuery = $"INSERT INTO Products (ProductID, Barcode, ProductName, UnitPrice) VALUES (NULL," +
                                barcodeInput + "," + "\"" +
                                nameInput + "\"" + "," +
                                modifiedPrice + ")";

                DatabaseConnection.SaveToDatabase(toDatabaseQuery);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ClearFields()
        {
            barcodeBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            priceBox.Text = string.Empty;
        }
    }
}
