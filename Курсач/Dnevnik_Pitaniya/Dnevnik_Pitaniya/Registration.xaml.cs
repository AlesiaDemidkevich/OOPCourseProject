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
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Dnevnik_Pitaniya
{    
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Dnevnik;Integrated Security=True";
        string name;
        string password1;
        string password;
      

        public Registration()
        {
            InitializeComponent();
            name = "";
            password = "";
            password1 = "";
            user_name.ToolTip = "Имя пользователя должно начинаться с заглавной буквы.\nИмя не должно содержать цифр.\nДлина имени должна составлять 2-20 символов.";
            user_password.ToolTip = "Пароль должен содержать не мене 6 и не более 25 символов";
        }

        public static string GetHash(string text)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

        private void Create_User(object sender, RoutedEventArgs e)
        {
            if (user_name.Text != "" || user_password.Text != "")
            {
                if (!GetUsers())
                {
                    MessageBox.Show("Пользователь с таким именем уже существует");
                }
                else
                if(!Regex.Match(user_name.Text, "^[A-ZА-ЯЁ][a-zа-яё]{1,19}$").Success)
                {
                    user_name.BorderBrush = Brushes.Red;
                    user_name.Text = "";
                    PopupText.Content= "";
                }
                else
                if (user_password.Text.Length < 6 || user_password.Text.Length > 25)
                {
                    user_password.BorderBrush = Brushes.Red;
                    user_password.Text = "";
                    PopupText1.Content = "";
                }
                else
                {
                    name = user_name.Text;
                    password1 = user_password.Text;
                    password = GetHash(password1);
                    User_Information inf = new User_Information(name,password);
                    inf.Show();
                    this.Close();
                                
                }              
               
            }
            else
            {
                PopupText.Content = "Заполните поле";
                PopupText1.Content = "Заполните поле";
                user_name.BorderBrush = Brushes.Red;
                user_password.BorderBrush = Brushes.Red;
            }
        
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
                        string name = reader.GetString(1);
                        if (name == user_name.Text)
                        {
                            return false;
                        }
                    }
                }
                reader.Close();
            }

            return true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.Show();
            this.Close();
        }

        private void User_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (user_name.Text == "")
            {
                user_name.BorderBrush = Brushes.Red;
                PopupText.Content = "Заполните поле";
            }
            else
            if (!Regex.Match(user_name.Text, "^[A-ZА-ЯЁ][a-zа-яё]{1,19}$").Success)
            {
                user_name.BorderBrush = Brushes.Red;
                PopupText.Content = "";
                MessageBox.Show("Имя пользователя должно начинаться с заглавной буквы.\nИмя не должно содержать цифр.\nДлина имени должна составлять 2-20 символов.");
            }
            else
            {
                user_name.BorderBrush = Brushes.Silver;
                PopupText.Content = "";
            }
        }

        private void User_password_LostFocus(object sender, RoutedEventArgs e)
        {

            if (user_password.Text == "")
            {
                user_password.BorderBrush = Brushes.Red;
                PopupText1.Content = "Заполните поле";
            }
            else
            if (user_password.Text.Length < 6 || user_password.Text.Length > 25)
            {
                user_password.BorderBrush = Brushes.Red;
                PopupText1.Content = "";
                MessageBox.Show("Пароль должен содержать не мене 6 и не более 25 символов.");
            }
            else
            {
                user_password.BorderBrush = Brushes.Silver;
                PopupText1.Content = "";
            }

        }

        private void User_name_GotFocus(object sender, RoutedEventArgs e)
        {
            user_name.BorderBrush = Brushes.Silver;
            PopupText.Content = "";
        }

        private void User_password_GotFocus(object sender, RoutedEventArgs e)
        {
            user_password.BorderBrush = Brushes.Silver;
            PopupText1.Content = "";
        }

    }
}
