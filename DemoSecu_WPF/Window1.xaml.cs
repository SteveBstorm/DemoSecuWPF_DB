using DemoSecu_WPF.Tools;
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

namespace DemoSecu_WPF
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            label.Content = "Utilisateur connecté : " + SessionHelper.CurrentUser.NickName;
            User u = (User)Application.Current.Properties["CurrentUsr"];
            label1.Content = "From App.Current : " + u.NickName;
        }
    }
}
