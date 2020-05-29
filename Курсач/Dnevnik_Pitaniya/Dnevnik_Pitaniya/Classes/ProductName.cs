using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik_Pitaniya
{
    public class ProductName
    {
        public string Product { get; set; }

        public ProductName(string product)
        {
            Product = product;
           
        }
        public override string ToString()
        {
            return Product;
        }

    }
}
