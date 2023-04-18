using JewelerApp.Databases;
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

namespace JewelerApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(bool isChanged)
        {
            InitializeComponent();

            if (isChanged == false)
            {
                ChangedStackPanel.Visibility = Visibility.Collapsed;
            }
            var categoryList = JewelerDatabase.GetContext().Category.ToList();
            categoryList.Insert(0, new Category
            {
                CategoryName = "Все категории"
            });
            CategoryComboBox.ItemsSource = categoryList.ToList();
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedIndex = 0;

            var manufacturerList = JewelerDatabase.GetContext().Manufacturer.ToList();
            manufacturerList.Insert(0, new Manufacturer
            {
                ManufacturerName = "Все производители"
            });
            ManufacturerComboBox.ItemsSource = manufacturerList.ToList();
            ManufacturerComboBox.DisplayMemberPath = "ManufacturerName";
            ManufacturerComboBox.SelectedIndex = 0;

            CostSortComboBox.SelectedIndex = 0;

            FilterProduct();
        }

        private void FilterProduct()
        {
            List<Product> productList = JewelerDatabase.GetContext().Product.ToList();

            if (!string.IsNullOrEmpty(NameSearchTextBox.Text))
            {
                productList = productList.Where(x => x.ProductName.ToLower().Contains(NameSearchTextBox.Text.ToLower())).ToList();
            }
            if (CategoryComboBox.SelectedIndex > 0)
            {
                productList = productList.Where(x => x.Category.Equals(CategoryComboBox.SelectedItem as Category)).ToList();
            }
            if (ManufacturerComboBox.SelectedIndex > 0)
            {
                productList = productList.Where(x => x.Manufacturer.Equals(ManufacturerComboBox.SelectedItem as Manufacturer)).ToList();
            }
            if (CostSortComboBox.SelectedIndex > 0)
            {
                if (CostSortComboBox.SelectedIndex == 1)
                {
                    productList = productList.OrderBy(x => x.ProductCost).ToList();
                }
                else
                {
                    productList = productList.OrderByDescending(x => x.ProductCost).ToList();
                }
            }

            ProductListView.ItemsSource = productList.ToList();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterProduct();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProduct();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage(null));
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = ProductListView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                try
                {
                    JewelerDatabase.GetContext().Product.Remove(selectedProduct);
                    JewelerDatabase.GetContext().SaveChanges();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }
            }
        }

        private void ChangeProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = ProductListView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                NavigationService.Navigate(new AddProductPage(selectedProduct));
            }
        }
    }
}
