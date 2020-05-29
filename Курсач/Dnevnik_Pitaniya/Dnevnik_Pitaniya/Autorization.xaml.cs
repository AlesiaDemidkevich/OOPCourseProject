using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Dnevnik_Pitaniya
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Dnevnik;Integrated Security=True";
        public int user_id { get; set; }

        public Autorization()
        {
            InitializeComponent();

            user_name1.ToolTip = "Имя пользователя должно начинаться с заглавной буквы.\nИмя не должно содержать цифр.\nДлина имени должна составлять 2-20 символов.";
            user_password1.ToolTip = "Пароль должен содержать не мене 6 и не более 25 символов";
            checkbox1.ToolTip = "Показать пароль";
        }
                            
        public bool GetUsers()
        {
            string sqlExpression = "sp_GetUsers";

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
                        user_id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string password = reader.GetString(4);
                        string in_password = Registration.GetHash(user_password1.Password);
                        if (name == user_name1.Text && password == in_password)
                        {                            
                            return false;
                        }

                    }
                }
                reader.Close();
            }

            return true;
        }

        private void Autorization_User(object sender, RoutedEventArgs e)
        {
            if (user_name1.Text != "" || user_password1.Password != "")
            {
                if (!GetUsers())
                {
                    Personal personal = new Personal(user_id);
                    personal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователя не существует");
                    user_name1.Text = "";
                    user_password1.Password = "";
                }

            }
            else
            {
                PopupText.Content = "Заполните поле";
                PopupText1.Content = "Заполните поле";
                user_password1.BorderBrush = Brushes.Red;
                user_name1.BorderBrush = Brushes.Red;
            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void Checkbox1_Click(object sender, RoutedEventArgs e)
        {
            var checkBox1 = sender as CheckBox;
            if (checkBox1.IsChecked.Value)
            {
                password_text1.Text = user_password1.Password; 
                password_text1.Visibility = Visibility.Visible; 
                user_password1.Visibility = Visibility.Hidden; 
            }
            else
            {
                user_password1.Password = password_text1.Text;
                password_text1.Visibility = Visibility.Hidden; 
                user_password1.Visibility = Visibility.Visible; 
            }
        }

        private void User_name1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (user_name1.Text == "")
            {
                user_name1.BorderBrush = Brushes.Red;
                PopupText.Content = "Заполните поле";
            }
            else
            {
                user_name1.BorderBrush = Brushes.Silver;
                PopupText.Content = "";
            }
        }

        private void User_password1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (user_password1.Password == "")
            {
                user_password1.BorderBrush = Brushes.Red;
                PopupText1.Content = "Заполните поле";
            }
            else
            {
                user_password1.BorderBrush = Brushes.Silver;
                PopupText1.Content = "";
            }
        }

        private void User_password1_GotFocus(object sender, RoutedEventArgs e)
        {
            user_password1.BorderBrush = Brushes.Silver;
            PopupText1.Content = "";
        }

        private void User_name1_GotFocus(object sender, RoutedEventArgs e)
        {
            user_name1.BorderBrush = Brushes.Silver;
            PopupText.Content = "";
        }
    }
}
