using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Dnevnik_Pitaniya
{
    /// <summary>
    /// Логика взаимодействия для User_Information.xaml
    /// </summary>
    public partial class User_Information : Window
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Dnevnik;Integrated Security=True";

        double user_height;
        double user_weight;
        int user_age;
        string user_gender;
        double user_index;
        double user_kall;
        double user_water;


        string user_name;
        string user_password;
        int user_id;

        public User_Information()
        {
            InitializeComponent();
        }

        public User_Information(string name, string password)
        {
            InitializeComponent();
            age.ToolTip = "Возраст должен быть в диапазоне 10-99 лет";
            height.ToolTip = "Рост должен быть в диапазоне 70-250 см.";
            weight.ToolTip = "Вес должен быть в диапазоне 20-150 кг.";
            user_name = name;
            user_password = password;

        }

        public void Back(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            this.Close();
        }

        private void Create_User(object sender, RoutedEventArgs e)
        {
            if (age.Text == "" || height.Text == "" || weight.Text == "" || gender.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                string sqlExpression = "sp_InsertUser";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                   
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter ageParam = new SqlParameter
                    {
                        ParameterName = "@age",
                        Value = user_age
                    };
                    command.Parameters.Add(ageParam);
                  
                    SqlParameter heightParam = new SqlParameter
                    {
                        ParameterName = "@height",
                        Value = user_height
                    };
                    command.Parameters.Add(heightParam);

                    SqlParameter weightParam = new SqlParameter
                    {
                        ParameterName = "@weight",
                        Value = user_weight
                    };
                    command.Parameters.Add(weightParam);

                    SqlParameter normaParam = new SqlParameter
                    {
                        ParameterName = "@norma",
                        Value = user_kall
                    };
                    command.Parameters.Add(normaParam);

                    SqlParameter massaParam = new SqlParameter
                    {
                        ParameterName = "@massa",
                        Value = user_index
                    };
                    command.Parameters.Add(massaParam);

                    SqlParameter waterParam = new SqlParameter
                    {
                        ParameterName = "@water",
                        Value = user_water
                    };
                    command.Parameters.Add(waterParam);

                    SqlParameter genderParam = new SqlParameter
                    {
                        ParameterName = "@gender",
                        Value = user_gender
                    };
                    command.Parameters.Add(genderParam);

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@name",
                        Value = user_name
                    };

                    command.Parameters.Add(nameParam);

                    SqlParameter passwordParam = new SqlParameter
                    {
                        ParameterName = "@password",
                        Value = user_password
                    };
                    command.Parameters.Add(passwordParam);

                    var result = command.ExecuteScalar();
                    user_id = Convert.ToInt32(result);
            
                }
                
                Personal personal = new Personal(user_id);
                personal.Show();
               
            }
            this.Close();
        }

 
        private void Age_LostFocus(object sender, RoutedEventArgs e)
        {
            if (age.Text == "")
            {
                age.BorderBrush = Brushes.Red;

            }
            else
            if (!Regex.Match(age.Text, @"^[1-9]{1}\d{0,2}[,]?[0-9]+$").Success)
            {
                age.BorderBrush = Brushes.Red;
                MessageBox.Show("Возраст должен быть в диапазоне 10-99 лет");
                age.Text = "";
            }
                      
        }

        private void Age_GotFocus(object sender, RoutedEventArgs e)
        {
            age.BorderBrush = Brushes.Silver;

        }

        private void Height_LostFocus(object sender, RoutedEventArgs e)
        {
            if (height.Text == "")
            {
                height.BorderBrush = Brushes.Red;

            }
            else
            if (!Regex.Match(height.Text, @"^[1-9]{1}\d{0,2}$").Success)
            {
                height.BorderBrush = Brushes.Red;              
                height.Text = "";
            }
            else
            if (Convert.ToDouble(height.Text) < 70 || Convert.ToDouble(height.Text) > 250)
            {
                height.BorderBrush = Brushes.Red;
                MessageBox.Show("Рост должен быть в диапазоне 70-250 см.");
                height.Text = "";
            }
        }

        private void Height_GotFocus(object sender, RoutedEventArgs e)
        {
            height.BorderBrush = Brushes.Silver;

        }

        private void Weight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (weight.Text == "")
            {
                weight.BorderBrush = Brushes.Red;

            }
            if (!Regex.Match(weight.Text, @"^[1-9]{1}\d{0,2}[,]?[0-9]+$").Success)
            {
                weight.BorderBrush = Brushes.Red;
               
                weight.Text = "";
            }
            else
            if (Convert.ToDouble(weight.Text) < 20 || Convert.ToDouble(weight.Text) > 150)
            {
                weight.BorderBrush = Brushes.Red;
                MessageBox.Show("Вес должен быть в диапазоне 20-150 кг.");
                weight.Text = "";
            }
                    
        }

        private void Weight_GotFocus(object sender, RoutedEventArgs e)
        {
            weight.BorderBrush = Brushes.Silver;

        }

        private void Gender_LostFocus(object sender, RoutedEventArgs e)
        {
            if (gender.Text == "")
            {
                gender.BorderBrush = Brushes.Red;
            }
           
        }
                         
        private void Gender_GotFocus(object sender, RoutedEventArgs e)
        {
           gender.BorderBrush = Brushes.Silver;
        }


        private void Сalculate(object sender, RoutedEventArgs e)
        {
            if (age.Text == "" || height.Text == "" || weight.Text == "" || gender.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                index.Text = "";
                kall.Text = "";
                water.Text = "";
            }
            else
            {                
                user_age = Convert.ToInt32(age.Text);
                user_height = Convert.ToDouble(height.Text);
                user_weight = Convert.ToDouble(weight.Text);
                user_gender = gender.Text;
                
                user_index = user_weight * 10000 / (user_height * user_height);
                user_index = Math.Round(user_index, 2);
                index.Text = Convert.ToString(user_index);
         

                if (user_gender == "Женский")
                {
                    user_kall = (10 * user_weight) + (6.25 * user_height) - (5 * user_age) - 161;
                    user_kall = Math.Round(user_kall, 2);
                    kall.Text = Convert.ToString(user_kall);                    

                    user_water = 31 * user_weight;
                    user_water = Math.Round(user_water, 2);
                    water.Text = Convert.ToString(user_water);
                
                }
                else
                if (user_gender == "Мужской")
                {
                    user_kall = (10 * user_weight) + (6.25 * user_height) - (5 * user_age) + 5;
                    user_kall = Math.Round(user_kall, 2);
                    kall.Text = Convert.ToString(user_kall);                  

                    user_water = 35 * user_weight;
                    user_water = Math.Round(user_water, 2);
                    water.Text = Convert.ToString(user_water);
                   
                }

            }
        }
    
    }
}
