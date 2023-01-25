using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public List<Sort> Sorts { get; set; }
        public ObservableCollection<string> ProductFilter { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public Page2()
        {
            InitializeComponent();
            Page1.products = new ObservableCollection<Product>(Page1.connection.Product.ToList());
            listboxproduct.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Source = Page1.products });
            Sorts = new List<Sort>
            {
                new Sort() { SortName = "Возрастание", SortClass = true },
                new Sort() { SortName = "Убывание", SortClass = false }
            };
            //Sortirovka.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = Sorts });
            Sortirovka.DisplayMemberPath = "SortName";

            DataContext = this;

            ProductFilter = new ObservableCollection<string>(Page1.connection.Product.Select(p => p.ProductManufacturer).Distinct().ToList());
            Filter.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = ProductFilter });

            DataContext = this;
        }
        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            Add q = new Add(Page1.connection);
            q.Show();
        }

        private void Sortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
            //var view = CollectionViewSource.GetDefaultView(listboxproduct.ItemsSource);
            //if(view != null)
            //{
            //    string manufacturer = Filter.SelectedItem as string;
            //    view.Filter = new Predicate<object>(product => ((Product)product).ProductManufacturer == manufacturer);
            //}
        }
        public void Sortik() 
        {
            //Sort sort1 = Sortirovka.SelectedItem as Sort;
            //if (sort1 == null)
            //    return;
            //var view = CollectionViewSource.GetDefaultView(listboxproduct.ItemsSource);

            //ListSortDirection direction;
            //if (sort1.SortClass == true)
            //    direction = ListSortDirection.Ascending;
            //else
            //    direction = ListSortDirection.Descending;

            //view.SortDescriptions.Clear();
            //view.SortDescriptions.Add(new SortDescription("ProductCost", direction));
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
            //var view = CollectionViewSource.GetDefaultView(listboxproduct.ItemsSource);
            //if(view !=null)
            //{
            //    if(Search.Text.Trim().Length>0)
            //    {
            //        string searchValue = Search.Text.Trim().ToLower();
            //        view.Filter = new Predicate<object>(product =>
            //        {
            //            Product p = product as Product;

            //            return p.ProductManufacturer.ToLower().Contains(searchValue) ||
            //                   p.ProductName.ToLower().Contains(searchValue) ||
            //                   p.ProductCategory.ToLower().Contains(searchValue) ||
            //                   p.ProductDescription.ToLower().Contains(searchValue);
            //        });
            //    }
            //}
        }
        public void UpdateFilter()
        {
            var view = CollectionViewSource.GetDefaultView(listboxproduct.ItemsSource);
            if (view != null)
            {
                view.Filter = new Predicate<object>(product =>
                {
                    Product p = product as Product;
                    bool result = true;
                    //Search
                    if (Search.Text.Trim().Length > 0)
                    {
                        string searchValue = Search.Text.Trim().ToLower();
                        result = p.ProductManufacturer.ToLower().Contains(searchValue) ||
                               p.ProductName.ToLower().Contains(searchValue) ||
                               p.ProductCategory.ToLower().Contains(searchValue) ||
                               p.ProductDescription.ToLower().Contains(searchValue);
                    }
                    //Filter
                    if (Filter.SelectedIndex > -1)
                    {
                        string manufacturer = Filter.SelectedItem as string;
                        result = result && p.ProductManufacturer == manufacturer;
                    }
                    return result;
                });
                //Sort
                view.SortDescriptions.Clear();
                if (Sortirovka.SelectedIndex>-1)
                {
                    Sort sort1 = Sortirovka.SelectedItem as Sort;
                    ListSortDirection direction;
                    if (sort1.SortClass == true)
                        direction = ListSortDirection.Ascending;
                    else
                        direction = ListSortDirection.Descending;
                    view.SortDescriptions.Add(new SortDescription("ProductCost", direction));
                }
            }
        }

        private void listboxproduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Product product=listboxproduct.SelectedItem as Product;
           Edit w= new Edit(Page1.connection,product);
           w.Show();
        }
    }
}
