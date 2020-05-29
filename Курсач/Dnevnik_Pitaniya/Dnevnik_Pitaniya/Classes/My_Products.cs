using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik_Pitaniya
{
    public  class My_Products
    {

        public string Product { get; set; }
        public int Weight { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Kkal { get; set; }

        public My_Products(string product, int weight, decimal protein, decimal fat, decimal carbohydrate, decimal kkal)
        {
            Product = product;
            Weight = weight;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
            Kkal = kkal;

        }
    }
}
