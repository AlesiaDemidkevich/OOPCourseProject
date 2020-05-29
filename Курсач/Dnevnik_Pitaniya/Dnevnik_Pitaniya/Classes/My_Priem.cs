using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik_Pitaniya
{
    public class My_Priem
    {
        public string Product { get; set; }
        public int Weight { get; set; }
        public string Date { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Kkal { get; set; }
        public int PID { get; set; }

        public My_Priem(string date, string product, int weight, decimal protein, decimal fat, decimal carbohydrate, decimal kkal, int p_id )
        {
            Product = product;
            Weight = weight;
            Date = date;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
            Kkal = kkal;
            PID = p_id;
        }
    }
}
