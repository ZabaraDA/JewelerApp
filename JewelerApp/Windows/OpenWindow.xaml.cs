using JewelerApp.Databases;
using JewelerApp.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JewelerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class OpenWindow : Window
    {
        public OpenWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = JewelerDatabase.GetContext().User.FirstOrDefault(x => x.UserLogin.Equals(LoginTextBox.Text) && x.UserPassword.Equals(PasswordBox.Password));

            if(selectedUser != null)
            {
                MenuWindow menuWindow = new MenuWindow(selectedUser);
                menuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введён неверный логин или пароль");
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow(null);
            menuWindow.Show();
            this.Close();
        }
    }
}
