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

        }

        private void ClearFields()
        {
            barcodeBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            priceBox.Text = string.Empty;
        }
    }
}
