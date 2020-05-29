using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dnevnik_Pitaniya
{
    /// <summary>
    /// Логика взаимодействия для Water.xaml
    /// </summary>
    public partial class Water : Window
    {
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Dnevnik;Integrated Security=True";
        public static BindingList<User_Water> waterlist = new BindingList<User_Water>();
        public static BindingList<User_Water> mywaterlist = new BindingList<User_Water>();
        private User_Water water_obj;

        int user_id;
        public Water()
        {
            InitializeComponent();
        }

        public Water(int id)
        {
            InitializeComponent();
            user_id = id;
            WaterBox.ToolTip = "Введите объем выпитой воды. \nОбъем должен быть в диапазоне 10-1000 мл";
            Date.Text = Convert.ToString(DateTime.Today);

            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)Date.SelectedDate;
            Date.SelectedDate = date.AddDays(1);

            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)Date.SelectedDate;
            Date.SelectedDate = date.AddDays(-1);

            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        private void Add_Water(object sender, RoutedEventArgs e)
        {
            if (WaterBox.Text == "")
            {
                MessageBox.Show("Введите объем выпитой воды (мл)");
            }
            else
            if (!Regex.Match(WaterBox.Text, @"^[1-9]{1}\d{1,3}$").Success)
            {
                MessageBox.Show("Объем воды должен быть в диапазоне 10-1000 мл");
                WaterBox.BorderBrush = Brushes.Red;
                WaterBox.Text = "";
            }
            else
            if (Convert.ToDouble(WaterBox.Text) < 10 || Convert.ToDouble(WaterBox.Text) > 1000)
            {
                MessageBox.Show("Объем воды должен быть в диапазоне 10-1000 мл");
                WaterBox.BorderBrush = Brushes.Red;
                WaterBox.Text = "";
            }
            else
            {
                DateTime datetime = (DateTime)Date.SelectedDate;
                string date = datetime.ToShortDateString();
                decimal water = Convert.ToDecimal(WaterBox.Text);

                AddWater(user_id, datetime, water);
                GetWater();
                GetMyWater();
                Water_Table.ItemsSource = mywaterlist;

                WaterBox.BorderBrush = Brushes.Silver;
                WaterBox.Text = "";
            }
        }


        public void AddWater(int id, DateTime date, decimal water)
        {
            string sqlExpression = "sp_AddWater";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idParam);

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = date
                };
                command.Parameters.Add(dateParam);

                SqlParameter waterParam = new SqlParameter
                {
                    ParameterName = "@water",
                    Value = water
                };
                command.Parameters.Add(waterParam);

                var result = command.ExecuteScalar();
            }
        }

        public void GetWater()
        {
            string sqlExpression = "sp_GetWater";
            waterlist.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        DateTime datetime = reader.GetDateTime(1);
                        string date = datetime.ToShortDateString();
                        decimal water = reader.GetDecimal(2);
                        int wid = reader.GetInt32(3);

                        if (id == user_id)
                        {
                            water_obj = new User_Water(id,date,water,wid);
                            waterlist.Add(water_obj);

                        }
                       
                    }
                    
                }
                reader.Close();
            }
                   

        }

        public void GetMyWater()
        {
            mywaterlist.Clear();
            foreach (var l in waterlist)
            {
                if (Convert.ToDateTime(l.Date) == Date.SelectedDate)
                {
                    mywaterlist.Add(l);
                }
            }
        }

        private void Add_300(object sender, RoutedEventArgs e)
        {
            DateTime datetime = (DateTime)Date.SelectedDate;
            string date = datetime.ToShortDateString();
            decimal water = 300;

            AddWater(user_id, datetime, water);
            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        private void Add_200(object sender, RoutedEventArgs e)
        {
            DateTime datetime = (DateTime)Date.SelectedDate;
            string date = datetime.ToShortDateString();
            decimal water = 200;

            AddWater(user_id, datetime, water);
            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        private void Add_100(object sender, RoutedEventArgs e)
        {
            DateTime datetime = (DateTime)Date.SelectedDate;
            string date = datetime.ToShortDateString();
            decimal water = 100;

            AddWater(user_id, datetime, water);
            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        private void Add_50(object sender, RoutedEventArgs e)
        {
            DateTime datetime = (DateTime)Date.SelectedDate;
            string date = datetime.ToShortDateString();
            decimal water = 50;

            AddWater(user_id, datetime, water);
            GetWater();
            GetMyWater();
            Water_Table.ItemsSource = mywaterlist;
        }

        public void DeleteWater(int id, string date, decimal water, int wid)
        {
            string sqlExpression = "sp_DeleteWater";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idParam);

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = date
                };
                command.Parameters.Add(dateParam);

                SqlParameter waterParam = new SqlParameter
                {
                    ParameterName = "@water",
                    Value = water
                };
                command.Parameters.Add(waterParam);

                SqlParameter widParam = new SqlParameter
                {
                    ParameterName = "@wid",
                    Value = wid
                };
                command.Parameters.Add(widParam);

                var result = command.ExecuteScalar();
            }
        }

        private void Delete_Water(object sender, RoutedEventArgs e)
        {
            if (Water_Table.SelectedIndex == -1)
            {
                MessageBox.Show("Веберите значение, которое хотите удалить");
            }
            else
            {
                int id = mywaterlist[Water_Table.SelectedIndex].ID;
                string date = mywaterlist[Water_Table.SelectedIndex].Date;
                decimal water = mywaterlist[Water_Table.SelectedIndex].Water;
                int wid = mywaterlist[Water_Table.SelectedIndex].WID;

                DeleteWater(id, date, water, wid);
                GetWater();
                GetMyWater();
                Water_Table.ItemsSource = mywaterlist;
                
            }
        }
              
    }
}
