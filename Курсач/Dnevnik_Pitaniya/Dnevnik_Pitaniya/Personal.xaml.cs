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
using System.ComponentModel;

namespace Dnevnik_Pitaniya
{
    /// <summary>
    /// Логика взаимодействия для Personal.xaml
    /// </summary>
    public partial class Personal : Window
    {
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Dnevnik;Integrated Security=True";

        public static BindingList<Products> productlist = new BindingList<Products>();
        public static BindingList<My_Products> userproductlist = new BindingList<My_Products>();
        public static BindingList<Products> searchinglist = new BindingList<Products>();
        public static BindingList<My_Products> mysearchinglist = new BindingList<My_Products>();
        public static BindingList<ProductName> productnamelist = new BindingList<ProductName>();
        public static BindingList<My_Priem> priemlist = new BindingList<My_Priem>();
        public static BindingList<My_Priem> stpriemlist = new BindingList<My_Priem>();
        public static BindingList<My_Priem> sortlist = new BindingList<My_Priem>();
        public static BindingList<User_Water> waterlist = new BindingList<User_Water>();
        public static BindingList<User_Water> mywaterlist = new BindingList<User_Water>();
        private User_Water water_obj;
        private Products product_obj;
        private ProductName productname_obj;
        private My_Products my_product_obj;
        private My_Priem my_priem_obj;

        string us_product;    //для добавления продуктов пользователя
        int us_weight;
        decimal us_protein;
        decimal us_fat;
        decimal us_carbohydrate;
        decimal us_kkal;

        string user_name;
        double user_height;
        double user_weight;
        int user_age;
        string user_gender;
        double user_index;
        double user_kall;
        double user_water;

        int user_id;

        public Personal()
        {
            InitializeComponent();
        }

        public Personal(int id)
        {
            InitializeComponent();
            user_id = id;

            Date.Text = Convert.ToString(DateTime.Today);
            Start.Text = Convert.ToString(DateTime.Today);
            End.Text = Convert.ToString(DateTime.Today);

            GetUsers();
            GetProducts();
            ProductTable.ItemsSource = productlist;
            GetUserProducts();
            UserProductTable.ItemsSource = userproductlist;
            GetProductsName();
            AllProduct.ItemsSource = productnamelist;
            GetPriem();
            Priem.ItemsSource = stpriemlist;
            GetMyPriem();
            PriemP.ItemsSource = priemlist;
            GetWater();
            GetResult();

            Name.ToolTip = "Имя пользователя должно начинаться с заглавной буквы.\nИмя не должно содержать цифр.\nДлина имени должна составлять 2-20 символов.";
            Age.ToolTip = "Возраст должен быть в диапазоне 10-99 лет";
            Height.ToolTip = "Рост должен быть в диапазоне 70-250 см.";
            Weight.ToolTip = "Вес должен быть в диапазоне 20-150 кг.";
            Search.ToolTip = "Введите название продукта для поиска";
            Us_product.ToolTip = "Введите название продукта.\nДлина названия должна быть в диапазоне 2-30 символов.\nВ названии могут содеражться символы '%' и '-' ";
            Us_weight.ToolTip = "Введите вес продукта в граммах \n(диапазон: 10-1000г)";
            Us_protein.ToolTip = "Введите содержание белков \n(диапазон:0-300)";
            Us_fat.ToolTip = "Введите содержание жиров \n(диапазон:0-300)";
            Us_carbohydrate.ToolTip = "Введите содержание углеводов \n(диапазон:0-300)";
            Us_kkal.ToolTip = "Введите калорийность продукта \n(диапазон:0-1000)";
            AllWeight.ToolTip = "Введите вес продукта в граммах \n(диапазон: 10-1000г)";
            AllProduct.ToolTip = "Введите название продукта";
        }

        // методы с процедурами
        public void GetUsers()
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
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);
                        string gender = reader.GetString(3);
                        decimal height = Math.Round(reader.GetDecimal(5),0);
                        decimal weight = Math.Round(reader.GetDecimal(6),0);
                        decimal massa = Math.Round(reader.GetDecimal(7),1);
                        decimal norma = Math.Round(reader.GetDecimal(8),0);
                        decimal water = Math.Round(reader.GetDecimal(9),0);

                        if (id == user_id)
                        {
                            Name.Text = name;
                            Age.Text = Convert.ToString(age);
                            Height.Text = Convert.ToString(height);
                            Weight.Text = Convert.ToString(weight);
                            Massa.Text = Convert.ToString(massa);
                            Kkal.Text = Convert.ToString(norma);
                            Water.Text = Convert.ToString(water);

                            if (gender == "Женский")
                            {
                                F.IsSelected = true;
                            }
                            else if (gender == "Мужской")
                            {
                                M.IsSelected = true;
                            }

                            if (massa <= 18)
                            {
                                Class_of_index.Text = "(Деффицит)";
                            }
                            else if (massa > 18 && massa <= 25)
                            {
                                Class_of_index.Text = "(Норма)";
                            }
                            else if (massa > 25 && massa <= 35)
                            {
                                Class_of_index.Text = "(Избыток)";
                            }
                            else if (massa > 35)
                            {
                                Class_of_index.Text = "(Ожирение)";
                            }

                            break;
                        }
                    }
                }
                reader.Close();
            }

        }

        public static void ChangeUser(int user_id, string user_name, int user_age, double user_height, double user_weight, double user_kkal, double user_index, double user_water, string user_gender)
        {
            string sqlExpression = "sp_ChangeUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = user_id
                };
                command.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = user_name
                };
                command.Parameters.Add(nameParam);

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
                    Value = user_kkal
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

                var result = command.ExecuteScalar();
            }
        }

        public void GetProducts()
        {
            string sqlExpression = "sp_GetProducts";

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
                        string product = reader.GetString(0);
                        int weight = reader.GetInt32(1);
                        decimal protein = reader.GetDecimal(2);
                        decimal fat = reader.GetDecimal(3);
                        decimal carbohydrate = reader.GetDecimal(4);
                        decimal kkal = reader.GetDecimal(5);

                        product_obj = new Products(product, weight, protein, fat, carbohydrate, kkal);
                        productlist.Add(product_obj);

                    }
                }
                reader.Close();
            }
        }
        public void GetUserProducts()
        {
            string sqlExpression = "sp_GetUserProducts";

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
                        string product = reader.GetString(0);
                        int weight = reader.GetInt32(1);
                        decimal protein = Math.Round(reader.GetDecimal(2),1);
                        decimal fat = Math.Round(reader.GetDecimal(3),1);
                        decimal carbohydrate = Math.Round(reader.GetDecimal(4),1);
                        decimal kkal = Math.Round(reader.GetDecimal(5),1);
                        int id = reader.GetInt32(6);

                        if (id == user_id)
                        {
                            my_product_obj = new My_Products(product, weight, protein, fat, carbohydrate, kkal);
                            userproductlist.Add(my_product_obj);
                        }

                    }
                }
                reader.Close();
            }

        }

        public void GetProductsName()
        {
            string sqlExpression = "sp_AllProductName";

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

                        string product = reader.GetString(0);
                        int id = reader.GetInt32(6);

                        if ( id == user_id || id == 0)
                        {
                            productname_obj = new ProductName(product);
                            productnamelist.Add(productname_obj);
                        }

                    }
                }
                reader.Close();
            }
        }
        
        public static void AddUserProduct(string us_product, int us_weight, decimal us_protein, decimal us_fat, decimal us_carbohydrate, decimal us_kkal, int user_id)
        {
            string sqlExpression = "sp_InsertUserProduct";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter productParam = new SqlParameter
                {
                    ParameterName = "@product",
                    Value = us_product
                };
                command.Parameters.Add(productParam);

                SqlParameter weightParam = new SqlParameter
                {
                    ParameterName = "@weight",
                    Value = us_weight
                };
                command.Parameters.Add(weightParam);

                SqlParameter proteinParam = new SqlParameter
                {
                    ParameterName = "@protein",
                    Value = us_protein
                };
                command.Parameters.Add(proteinParam);

                SqlParameter fatParam = new SqlParameter
                {
                    ParameterName = "@fat",
                    Value = us_fat
                };
                command.Parameters.Add(fatParam);

                SqlParameter carbohydrateParam = new SqlParameter
                {
                    ParameterName = "@carbohydrate",
                    Value = us_carbohydrate
                };
                command.Parameters.Add(carbohydrateParam);

                SqlParameter kkalParam = new SqlParameter
                {
                    ParameterName = "@kkal",
                    Value = us_kkal
                };
                command.Parameters.Add(kkalParam);

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@user_id",
                    Value = user_id
                };
                command.Parameters.Add(idParam);

                var result = command.ExecuteScalar();

            }
        }

        public static void DeleteUserProduct(string product, int weight, decimal protein, decimal fat, decimal carbohydrate, decimal kkal, int id)
        {
            string sqlExpression = "sp_DeleteUserProduct";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter productParam = new SqlParameter
                {
                    ParameterName = "@product",
                    Value = product
                };
                command.Parameters.Add(productParam);

                SqlParameter weightParam = new SqlParameter
                {
                    ParameterName = "@weight",
                    Value = weight
                };
                command.Parameters.Add(weightParam);

                SqlParameter proteinParam = new SqlParameter
                {
                    ParameterName = "@protein",
                    Value = protein
                };
                command.Parameters.Add(proteinParam);

                SqlParameter fatParam = new SqlParameter
                {
                    ParameterName = "@fat",
                    Value = fat
                };
                command.Parameters.Add(fatParam);

                SqlParameter carbohydrateParam = new SqlParameter
                {
                    ParameterName = "@carbohydrate",
                    Value = carbohydrate
                };
                command.Parameters.Add(carbohydrateParam);

                SqlParameter kkalParam = new SqlParameter
                {
                    ParameterName = "@kkal",
                    Value = kkal
                };
                command.Parameters.Add(kkalParam);

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idParam);

                var result = command.ExecuteScalar();
            }
        }

        public static void AddPriem(int id, string product, int weight, DateTime datetime)
        {
            string sqlExpression = "sp_Add_Priem";

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

                SqlParameter productParam = new SqlParameter
                {
                    ParameterName = "@product",
                    Value = product
                };
                command.Parameters.Add(productParam);

                SqlParameter weightParam = new SqlParameter
                {
                    ParameterName = "@weight",
                    Value = weight
                };
                command.Parameters.Add(weightParam);


                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = datetime
                };
                command.Parameters.Add(dateParam);

                var result = command.ExecuteScalar();

            }
        }

        public void GetPriem()
        {
            string sqlExpression = "sp_GetPriem";

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
                        string product = reader.GetString(2);
                        int weight = reader.GetInt32(3);
                        decimal protein = reader.GetDecimal(4);
                        protein = protein * weight / 100;
                        decimal fat = reader.GetDecimal(5);
                        fat = fat * weight / 100;
                        decimal carbohydrate = reader.GetDecimal(6);
                        carbohydrate = carbohydrate * weight / 100;
                        decimal kkal = reader.GetDecimal(7);
                        kkal = kkal * weight / 100;
                        int p_id = reader.GetInt32(8);

                        if (id == user_id)
                        {
                            my_priem_obj = new My_Priem(date, product, weight, protein, fat, carbohydrate, kkal, p_id);
                            stpriemlist.Add(my_priem_obj);
                        }

                    }
                }
                reader.Close();
            }
        }

        public void DeletePriem(string product, int weight, string date, int id, int p_id)
        {
            string sqlExpression = "sp_DeletePriem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter productParam = new SqlParameter
                {
                    ParameterName = "@product",
                    Value = product
                };
                command.Parameters.Add(productParam);

                SqlParameter weightParam = new SqlParameter
                {
                    ParameterName = "@weight",
                    Value = weight
                };
                command.Parameters.Add(weightParam);

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = date
                };
                command.Parameters.Add(dateParam);

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(idParam);

                SqlParameter pidParam = new SqlParameter
                {
                    ParameterName = "@p_id",
                    Value = p_id
                };
                command.Parameters.Add(pidParam);

                var result = command.ExecuteScalar();
            }
        }

        private void Delete_Priem(object sender, RoutedEventArgs e)
        {
            if (PriemP.SelectedIndex == -1)
            {
                MessageBox.Show("Веберите прием пищи для удаления");
            }
            else
            {
                string product = priemlist[PriemP.SelectedIndex].Product;
                int weight = priemlist[PriemP.SelectedIndex].Weight;
                string date = priemlist[PriemP.SelectedIndex].Date;
                int p_id = priemlist[PriemP.SelectedIndex].PID;

                DeletePriem(product, weight, date, user_id, p_id);
                stpriemlist.Clear();
                GetPriem();
                Priem.ItemsSource = stpriemlist;
                GetMyPriem();
                PriemP.ItemsSource = priemlist;

                GetResult();
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
                            water_obj = new User_Water(id, date, water, wid);
                            waterlist.Add(water_obj);

                        }

                    }

                }
                reader.Close();
            }

            mywaterlist.Clear();
            foreach (var l in waterlist)
            {
                if (Convert.ToDateTime(l.Date) >= Start.SelectedDate && Convert.ToDateTime(l.Date) <= End.SelectedDate)
                {
                    mywaterlist.Add(l);
                }
            }

        }

        public void GetResult()
        {
            if (Start.SelectedDate <= End.SelectedDate)
            {
                sortlist.Clear();
                foreach (var l in stpriemlist)
                {
                    if (Convert.ToDateTime(l.Date) >= Start.SelectedDate && Convert.ToDateTime(l.Date) <= End.SelectedDate)
                    {
                        sortlist.Add(l);
                    }
                }
                Priem.ItemsSource = sortlist;

                GetWater();

                T_Protein.Text = Convert.ToString(sortlist.Sum(x => x.Protein));
                T_Fat.Text = Convert.ToString(sortlist.Sum(x => x.Fat));
                T_Carbohydrate.Text = Convert.ToString(sortlist.Sum(x => x.Carbohydrate));
                T_Kkal.Text = Convert.ToString(sortlist.Sum(x => x.Kkal));
                T_Water.Text = Convert.ToString(mywaterlist.Sum(x => x.Water));

            }
            else
            {
                MessageBox.Show("Неправильный период!");
                Start.Text = Convert.ToString(DateTime.Today);
                End.Text = Convert.ToString(DateTime.Today);

            }
        }

        private void Show_Statistic(object sender, RoutedEventArgs e)
        {
            if (Start.SelectedDate == null || End.SelectedDate == null)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                GetResult();
            }
        }

        public bool CheckProduct()
        {
            string sqlExpression = "sp_GetUserProducts";

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
                        string product = reader.GetString(0);
                        int id = reader.GetInt32(6);
                        if (product == Us_product.Text  && id == user_id)
                        {
                            return false;
                        }
                    }
                }
                reader.Close();
            }

            return true;
        }

        public bool CheckAllProduct()
        {
            string sqlExpression = "sp_AllProductName";

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
                        string product = reader.GetString(0);
                        int id = reader.GetInt32(6);
                        if ((product == AllProduct.Text && id == user_id) || (product == AllProduct.Text && id == 0))
                        {
                            return false;
                        }
                    }
                }
                reader.Close();
            }

            return true;
        }
       
        private void Window_Activated(object sender, EventArgs e)
        {
            GetWater();
            if (Start.SelectedDate <= End.SelectedDate)
            {
                T_Water.Text = Convert.ToString(mywaterlist.Sum(x => x.Water));
            }
            else
            {
                Start.Text = Convert.ToString(DateTime.Today);
                End.Text = Convert.ToString(DateTime.Today);
                GetResult();
            }
        }

        //кнопки

        private void Exit(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            userproductlist.Clear();
            productnamelist.Clear();
            stpriemlist.Clear();
            sortlist.Clear();
            priemlist.Clear();
            waterlist.Clear();
            mywaterlist.Clear();
            searchinglist.Clear();
            mysearchinglist.Clear();
            productlist.Clear();
            autorization.Show();
            this.Close();

        }

        private void Add_Product(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Us_product.Text == "" || Us_weight.Text == "" || Us_protein.Text == "" || Us_fat.Text == "" || Us_carbohydrate.Text == "" || Us_kkal.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                if (!CheckProduct())
                {
                    MessageBox.Show("Такой продукт уже сохранен!");

                    Us_product.Text = "";
                    Us_weight.Text = "";
                    Us_protein.Text = "";
                    Us_fat.Text = "";
                    Us_carbohydrate.Text = "";
                    Us_kkal.Text = "";
                }
                else
                {
                    us_product = Us_product.Text;
                    us_weight = Convert.ToInt32(Us_weight.Text);
                    us_protein = Convert.ToDecimal(Us_protein.Text);
                    us_protein = Math.Round(us_protein, 1);
                    us_fat = Convert.ToDecimal(Us_fat.Text);
                    us_fat = Math.Round(us_fat, 1);
                    us_carbohydrate = Convert.ToDecimal(Us_carbohydrate.Text);
                    us_carbohydrate = Math.Round(us_carbohydrate, 1);
                    us_kkal = Convert.ToDecimal(Us_kkal.Text);
                    us_kkal = Math.Round(us_kkal, 1);

                    AddUserProduct(us_product, us_weight, us_protein, us_fat, us_carbohydrate, us_kkal, user_id);
                    userproductlist.Add(new My_Products(us_product, us_weight, us_protein, us_fat, us_carbohydrate, us_kkal));
                    UserProductTable.ItemsSource = userproductlist;

                    productnamelist.Clear();
                    GetProductsName();
                    AllProduct.ItemsSource = productnamelist;

                    Us_product.Text = "";
                    Us_weight.Text = "";
                    Us_protein.Text = "";
                    Us_fat.Text = "";
                    Us_carbohydrate.Text = "";
                    Us_kkal.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Заполните все поля!");
                Us_product.Text = "";
                Us_weight.Text = "";
                Us_protein.Text = "";
                Us_fat.Text = "";
                Us_carbohydrate.Text = "";
                Us_kkal.Text = "";
            }
        }

        private void Change_User(object sender, RoutedEventArgs e)
        {

            if (Name.Text == "" || Age.Text == "" || Gender.Text == "" || Height.Text == "" || Weight.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                Massa.Text = "";
                Kkal.Text = "";
                Water.Text = "";
                Class_of_index.Text = "";
            }
            else
            {
                user_name = Name.Text;
                user_age = Convert.ToInt32(Age.Text);
                user_height = Convert.ToDouble(Height.Text);
                user_weight = Convert.ToDouble(Weight.Text);
                user_gender = Gender.Text;

                user_index = user_weight * 10000 / (user_height * user_height);
                user_index = Math.Round(user_index, 1);
                Massa.Text = Convert.ToString(user_index);

                if (user_index <= 18)
                {
                    Class_of_index.Text = "(Деффицит)";
                }
                else if (user_index > 18 && user_index <= 25)
                {
                    Class_of_index.Text = "(Норма)";
                }
                else if (user_index > 25 && user_index <= 35)
                {
                    Class_of_index.Text = "(Избыток)";
                }
                else if (user_index > 35)
                {
                    Class_of_index.Text = "(Ожирение)";
                }
                               
                if (user_gender == "Женский")
                {
                    user_kall = (10 * user_weight) + (6.25 * user_height) - (5 * user_age) - 161;
                    user_kall = Math.Round(user_kall, 0);
                    Kkal.Text = Convert.ToString(user_kall);

                    user_water = 31 * user_weight;
                    user_water = Math.Round(user_water, 0);
                    Water.Text = Convert.ToString(user_water);

                }
                else
                if (user_gender == "Мужской")
                {
                    user_kall = (10 * user_weight) + (6.25 * user_height) - (5 * user_age) + 5;
                    user_kall = Math.Round(user_kall, 0);
                    Kkal.Text = Convert.ToString(user_kall);

                    user_water = 35 * user_weight;
                    user_water = Math.Round(user_water, 0);
                    Water.Text = Convert.ToString(user_water);

                }

                ChangeUser(user_id, user_name, user_age, user_height, user_weight, user_kall, user_index, user_water, user_gender);

            }
        }

        private void Search_Product(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Search.Text == "")
                {
                    productlist.Clear();
                    userproductlist.Clear();
                    GetProducts();
                    ProductTable.ItemsSource = productlist;
                    GetUserProducts();
                    UserProductTable.ItemsSource = userproductlist;
                    MessageBox.Show("Введите название продукта!");
                }
                else
                {
                    searchinglist.Clear();
                    mysearchinglist.Clear();
                    Search.Text = Search.Text.Trim();
                    if (Search.Text != "" && !Search.Text.ToLower().Contains("w") && !Search.Text.ToLower().Contains("^") && !Search.Text.ToLower().Contains("s") && !Search.Text.ToLower().Contains("d") && !Search.Text.Contains("["))
                    {
                        string s = Search.Text;
                        Regex regex = new Regex(@"^" + s + @"(\w)*", RegexOptions.IgnoreCase);
                        MatchCollection matches;
                        foreach (var p in productlist)
                        {
                            matches = regex.Matches(p.Product);

                            foreach (Match match in matches)
                            {
                                searchinglist.Add(p);
                                ProductTable.ItemsSource = searchinglist;

                            }
                        }
                        foreach (var p in userproductlist)
                        {
                            matches = regex.Matches(p.Product);

                            foreach (Match match in matches)
                            {
                                mysearchinglist.Add(p);
                                UserProductTable.ItemsSource = mysearchinglist;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ничего не найдено!");
                    }
                    Search.BorderBrush = Brushes.Silver;

                }
            }
            catch(Exception)
            {
                MessageBox.Show("Ничего не найдено!");
                Search.BorderBrush = Brushes.Silver;
                Search.Text = "";
            }
        }

        private void Delete_Product(object sender, RoutedEventArgs e)
        {
            if (UserProductTable.SelectedIndex ==-1)
            {
                MessageBox.Show("Веберите продукт для удаления");
            }
            else
            {
                string product = userproductlist[UserProductTable.SelectedIndex].Product;
                int weight = userproductlist[UserProductTable.SelectedIndex].Weight;
                decimal protein = userproductlist[UserProductTable.SelectedIndex].Protein;
                decimal fat = userproductlist[UserProductTable.SelectedIndex].Fat;
                decimal carbohydrate = userproductlist[UserProductTable.SelectedIndex].Carbohydrate;
                decimal kkal = userproductlist[UserProductTable.SelectedIndex].Kkal;

                DeleteUserProduct(product, weight, protein, fat, carbohydrate, kkal, user_id);
                userproductlist.Remove(userproductlist[UserProductTable.SelectedIndex]);
                UserProductTable.ItemsSource = userproductlist;

                productnamelist.Clear();
                GetProductsName();
                AllProduct.ItemsSource = productnamelist;

            }
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)Date.SelectedDate;
            Date.SelectedDate = date.AddDays(1);

            stpriemlist.Clear();
            GetPriem();
            Priem.ItemsSource = stpriemlist;
            GetMyPriem();
            PriemP.ItemsSource = priemlist;
   
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)Date.SelectedDate;
            Date.SelectedDate = date.AddDays(-1);

            stpriemlist.Clear();
            GetPriem();
            Priem.ItemsSource = stpriemlist;
            GetMyPriem();
            PriemP.ItemsSource = priemlist;
         
        }

        private void Add_Priem(object sender, RoutedEventArgs e)
        {
            if (AllProduct.Text == "" || AllWeight.Text == "" )
            {
                MessageBox.Show("Введите информацию о приеме пищи!");
            }
            else
            if (CheckAllProduct())
            {
                MessageBox.Show("Такого продукта не существует!\nДобавьте продукт в список своих продуктов.");

                AllProduct.Text = "";
                AllWeight.Text = "";
              
            }
            else
            { 
                string product = AllProduct.Text;
                int weight = Convert.ToInt32(AllWeight.Text);
                DateTime datetime = (DateTime)Date.SelectedDate;
                string date = datetime.ToShortDateString();
                
                AddPriem(user_id, product, weight, datetime);
               
                stpriemlist.Clear();
                GetPriem();               
                Priem.ItemsSource = stpriemlist;

                GetMyPriem();
                PriemP.ItemsSource = priemlist;
                                
                GetResult();

                AllProduct.Text = "";
                AllWeight.Text = "";

            }

        }

        public void GetMyPriem()
        {
            priemlist.Clear();
            foreach (var l in stpriemlist)
            {
                if (Convert.ToDateTime(l.Date) == Date.SelectedDate)
                {
                    priemlist.Add(l);
                }
            }
            
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            Search.Text = "";
            productlist.Clear();
            userproductlist.Clear();
            GetProducts();
            ProductTable.ItemsSource = productlist;
            GetUserProducts();
            UserProductTable.ItemsSource = userproductlist;
        }

        private void Add_Water(object sender, RoutedEventArgs e)
        {
            Water water = new Water(user_id);
            water.Show();
        }

        //события 

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {

            if (Name.Text == "")
            {
                Name.BorderBrush = Brushes.Red;
            }
            else
            if (!Regex.Match(Name.Text, "^[A-ZА-ЯЁ][a-zа-яё]{1,20}?$").Success)
            {
                Name.BorderBrush = Brushes.Red;
                Name.Text = "";              
            }
            else
            {
                Name.BorderBrush = Brushes.Silver;
            }
        }

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.BorderBrush = Brushes.Silver;
        }

        private void Age_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Age.Text == "")
            {
                Age.BorderBrush = Brushes.Red;

            }
            else
            if (!Regex.Match(Age.Text, @"^[1-9]{1}\d{1}$").Success)
            {
                Age.BorderBrush = Brushes.Red;
                MessageBox.Show("Возраст должен быть в диапазоне 10-99 лет");
                Age.Text = "";
            }
        }

        private void Age_GotFocus(object sender, RoutedEventArgs e)
        {
            Age.BorderBrush = Brushes.Silver;
        }

        private void Height_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Height.Text == "")
            {
                Height.BorderBrush = Brushes.Red;

            }
            else
            if (!Regex.Match(Height.Text, @"^[1-9]{1}\d{0,2}[,]?[0-9]+$").Success)
            {
                Height.BorderBrush = Brushes.Red;
               
                Height.Text = "";
            }
            else
            if (Convert.ToDouble(Height.Text) < 70 || Convert.ToDouble(Height.Text) > 250)
            {
                Height.BorderBrush = Brushes.Red;
                MessageBox.Show("Рост должен быть в диапазоне 70-250 см.");
                Height.Text = "";
            }
        }

        private void Height_GotFocus(object sender, RoutedEventArgs e)
        {
            Height.BorderBrush = Brushes.Silver;
        }

        private void Weight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Weight.Text == "")
            {
                Weight.BorderBrush = Brushes.Red;

            }
            if (!Regex.Match(Weight.Text, @"^[1-9]{1}\d{0,2}[,]?[0-9]+$").Success)
            {
                Weight.BorderBrush = Brushes.Red;
                
                Weight.Text = "";
            }
            else
            if (Convert.ToDouble(Weight.Text) < 20 || Convert.ToDouble(Weight.Text) > 150)
            {
                Weight.BorderBrush = Brushes.Red;
                MessageBox.Show("Вес должен быть в диапазоне 20-150 кг.");
                Weight.Text = "";
            }
        }

        private void Weight_GotFocus(object sender, RoutedEventArgs e)
        {
            Weight.BorderBrush = Brushes.Silver;
        }

        private void Us_product_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Us_product.Text == "")
            {
                Us_product.BorderBrush = Brushes.Red;

            }
            if (!Regex.Match(Us_product.Text, @"^[a-zA-Zа-яА-Я0-9\s%-]*$").Success)
            {
                Us_product.BorderBrush = Brushes.Red;
                Us_product.Text = "";
            }
            if (Us_product.Text.Length < 2 || Us_product.Text.Length > 30)
            {
                Us_product.BorderBrush = Brushes.Red;
                Us_product.Text = "";
            }
        }

        private void Us_product_GotFocus(object sender, RoutedEventArgs e)
        {
            Us_product.BorderBrush = Brushes.Silver;
        }

        private void Us_weight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Us_weight.Text == "")
                {
                    Us_weight.BorderBrush = Brushes.Red;

                }
                else
                if (!Regex.Match(Us_weight.Text, @"^[1-9]{1}\d{1,3}$").Success)
                {
                    Us_weight.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите вес продукта в граммах \n(диапазон: 10-1000г)");
                    Us_weight.Text = "";
                }
                else
                if (Convert.ToDouble(Us_weight.Text) < 10 || Convert.ToDouble(Us_weight.Text) > 1000)
                {
                    Us_weight.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите вес продукта в граммах \n(диапазон: 10-1000г)");
                    Us_weight.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите вес продукта в граммах \n(диапазон: 10-1000г)");
                Us_weight.Text = "";
            }
        }

        private void Us_weight_GotFocus(object sender, RoutedEventArgs e)
        {
            Us_weight.BorderBrush = Brushes.Silver;
        }

        private void Us_protein_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Us_protein.Text == "")
                {
                    Us_protein.BorderBrush = Brushes.Red;

                }
                else
                if (!Regex.Match(Us_protein.Text, @"^[0-9]*[,]?[0-9]+$").Success)
                {
                    Us_protein.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите содержание белков \n(диапазон: 0 - 300)");
                    Us_protein.Text = "";
                }
                else
                if (Convert.ToDouble(Us_protein.Text) < 0 || Convert.ToDouble(Us_protein.Text) > 300)
                {
                    Us_protein.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите содержание белков \n(диапазон: 0 - 300)");
                    Us_protein.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите содержание белков \n(диапазон: 0 - 300)");
                Us_protein.Text = "";
            }
        }

        private void Us_protein_GotFocus(object sender, RoutedEventArgs e)
        {
            Us_protein.BorderBrush = Brushes.Silver;
        }

        private void Us_fat_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Us_fat.Text == "")
                {
                    Us_fat.BorderBrush = Brushes.Red;

                }
                else
                if (!Regex.Match(Us_fat.Text, @"^[0-9]*[,]?[0-9]+$").Success)
                {
                    Us_fat.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите содержание жиров \n(диапазон:0-300)");
                    Us_fat.Text = "";
                }
                else
                if (Convert.ToDouble(Us_fat.Text) < 0 || Convert.ToDouble(Us_fat.Text) > 300)
                {
                    Us_fat.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите содержание жиров \n(диапазон:0-300)");
                    Us_fat.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите содержание жиров \n(диапазон:0-300)");
                Us_fat.Text = "";
            }
        }

        private void Us_fat_GotFocus(object sender, RoutedEventArgs e)
        {
            Us_fat.BorderBrush = Brushes.Silver;
        }

        private void Us_carbohydrate_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Us_carbohydrate.Text == "")
                {
                    Us_carbohydrate.BorderBrush = Brushes.Red;

                }
                else
                if (!Regex.Match(Us_carbohydrate.Text, @"^[0-9]*[,]?[0-9]+$").Success)
                {
                    Us_carbohydrate.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите содержание углеводов \n(диапазон:0-300)");
                    Us_carbohydrate.Text = "";
                }
                else
                if (Convert.ToDouble(Us_carbohydrate.Text) < 0 || Convert.ToDouble(Us_carbohydrate.Text) > 300)
                {
                    Us_carbohydrate.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите содержание углеводов \n(диапазон:0-300)");
                    Us_carbohydrate.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите содержание углеводов \n(диапазон:0-300)");
                Us_carbohydrate.Text = "";
            }
        }

        private void Us_carbohydrate_GotFocus(object sender, RoutedEventArgs e)
        {
            Us_carbohydrate.BorderBrush = Brushes.Silver;
        }

        private void Us_kkal_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Us_kkal.Text == "")
                {
                    Us_kkal.BorderBrush = Brushes.Red;

                }
                else
                if (!Regex.Match(Us_kkal.Text, @"^[0-9]*[,]?[0-9]+$").Success)
                {
                    Us_kkal.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите калорийность продукта \n(диапазон:0-1000)");
                    Us_kkal.Text = "";
                }
                else
                if (Convert.ToDouble(Us_kkal.Text) < 0 || Convert.ToDouble(Us_kkal.Text) > 1000)
                {
                    Us_kkal.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите калорийность продукта \n(диапазон:0-1000)");
                    Us_kkal.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите калорийность продукта \n(диапазон:0-1000)");
                Us_kkal.Text = "";
            }
        }

        private void Us_kkal_GotFocus(object sender, RoutedEventArgs e)
        {
            Us_kkal.BorderBrush = Brushes.Silver;
        }
              
        private void AllProduct_KeyDown(object sender, KeyEventArgs e)
        {
            AllProduct.IsDropDownOpen = true;
        }
                
        private void AllWeight_GotFocus(object sender, RoutedEventArgs e)
        {
            AllWeight.BorderBrush = Brushes.Silver;
        }

        private void AllWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllWeight.Text == "")
                {
                    AllWeight.BorderBrush = Brushes.Red;

                }
                if (!Regex.Match(AllWeight.Text, @"^[1-9]{1}\d{1,3}$").Success)
                {
                    AllWeight.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите вес продукта в граммах \n(диапазон: 10-1000г)");
                    AllWeight.Text = "";
                }
                else
                if (Convert.ToDouble(AllWeight.Text) < 10 || Convert.ToDouble(AllWeight.Text) > 1000)
                {
                    AllWeight.BorderBrush = Brushes.Red;
                    MessageBox.Show("Введите вес продукта в граммах \n(диапазон: 10-1000г)");
                    AllWeight.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введите вес продукта в граммах \n(диапазон: 10-1000г)");
                AllWeight.Text = "";
            }
        }

    }
}
