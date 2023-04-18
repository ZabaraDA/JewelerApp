using JewelerApp.Databases;
using JewelerApp.Pages;
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

namespace JewelerApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow(User selectedUser)
        {
            InitializeComponent();
            if (selectedUser == null)
            {
                NavigationStackPanel.Visibility = Visibility.Collapsed;
                MenuFrame.Navigate(new ProductPage(false));
            }
            else
            {
                RoleTextBlock.Text = selectedUser.Role.RoleName;
                NameTextBlock.Text = selectedUser.UserName;
                SurnameTextBlock.Text = selectedUser.UserSurname;
                MenuFrame.Navigate(new WelcomePage());
            }

        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new ProductPage(true));
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new OrderPage());
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new UserPage());
        }
    }
}
