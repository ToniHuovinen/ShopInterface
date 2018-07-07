using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopInterface
{
    public class Product
    {
        // Fields
        private int barcode;
        private string productName;
        private decimal unitPrice;

        // Properties
        public int Barcode
        {
            set
            {
                barcode = value;
            }
            get
            {
                return barcode;
            }
        }

        public string ProductName
        {
            set { productName = value; }
            get { return productName; }
        }

        public decimal UnitPrice
        {
            set { unitPrice = value; }
            get { return unitPrice; }
        }

        // Constructors
        public Product()
        {
            barcode = 00000;
            productName = "-----";
            unitPrice = 0;
        }

        public Product(int barcode, string productName, decimal unitPrice)
        {
            this.barcode = barcode;
            this.productName = productName;
            this.unitPrice = unitPrice;
        }
    }
}
