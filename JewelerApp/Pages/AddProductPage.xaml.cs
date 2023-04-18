using JewelerApp.Databases;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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

namespace JewelerApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private OpenFileDialog _openFileDialog = new OpenFileDialog()
        {
            Multiselect = false,
            Filter = "Images (*.JPG; *.PNG)| *.JPG;*.PNG"
        };
        private Product _currentProduct = new Product();
        public AddProductPage(Product selectedProduct)
        {
            InitializeComponent();

            var categoryList = JewelerDatabase.GetContext().Category.ToList();
            categoryList.Insert(0, new Category
            {
                CategoryName = "Выберите категорию"
            });
            CategoryComboBox.ItemsSource = categoryList.ToList();
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedIndex = 0;

            var manufacturerList = JewelerDatabase.GetContext().Manufacturer.ToList();
            manufacturerList.Insert(0, new Manufacturer
            {
                ManufacturerName = "Выберите производителя"
            });
            ManufacturerComboBox.ItemsSource = manufacturerList.ToList();
            ManufacturerComboBox.DisplayMemberPath = "ManufacturerName";
            ManufacturerComboBox.SelectedIndex = 0;
            if (selectedProduct != null)
            {
                _currentProduct = selectedProduct;
                AddProductButton.Content = "Изменить товар";
                ArticleTextBox.IsEnabled = false;
            }
           
            DataContext = _currentProduct;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            //_currentProduct.ProductStatus = Convert.ToBoolean(StatusComboBox.SelectedIndex);
            JewelerDatabase.GetContext().Product.AddOrUpdate(_currentProduct);
            try
            {
                JewelerDatabase.GetContext().SaveChanges();
            }
            catch (Exception exeption)
            {

                MessageBox.Show(exeption.Message);
            }
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if(_openFileDialog.ShowDialog() == true)
            {
                var file = _openFileDialog.FileName;
                _currentProduct.ProductPhoto = File.ReadAllBytes(file);
            }
        }
    }
}
