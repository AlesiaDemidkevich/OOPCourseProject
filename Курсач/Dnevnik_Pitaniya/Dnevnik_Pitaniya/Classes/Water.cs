using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik_Pitaniya
{
    public class User_Water
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public decimal Water { get; set; }
        public int WID { get; set; }

        public User_Water(int id, string date, decimal water, int wid )
        {
            ID = id;
            Date = date;
            Water = water;
            WID = wid;
        }
    }
}
