using DemoSecu_WPF.Tools;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoSecu_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _connectionString = @"Data Source=DESKTOP-RGPQP6I\TFTIC2019;Initial Catalog=DemoSecu;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = null;
            using (SqlConnection c = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = "UserLogin";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("email", email.Text);
                    cmd.Parameters.AddWithValue("password", password.Text);
                    c.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentUser = new User
                            {
                                Id = (int)reader["Id"],
                                NickName = reader["NickName"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                    c.Close();
                }
            }

            if (currentUser is null) etat.Content = "Erreur de connexion";
            else {
                SessionHelper.CurrentUser = currentUser;
                etat.Content = "Bonjour " + SessionHelper.CurrentUser.NickName;
                Application.Current.Properties["CurrentUser"] = currentUser;
                Window1 w = new Window1();
                w.Show();
                this.Close();
            } 
        }
    }
}
