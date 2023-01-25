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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private user2Entities database;
        public Product product { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Manufacturers { get; set; }
        public List<string> Providers { get; set; }
        public Add(user2Entities database)
        {
            InitializeComponent();
            product= new Product();
            this.database = database;
            Categories=database.Product.Select(c=>c.ProductCategory).Distinct().ToList();
            Manufacturers=database.Product.Select(m=>m.ProductManufacturer).Distinct().ToList();
            Providers = database.Product.Select(p => p.ProductProvider).Distinct().ToList();
            DataContext = this;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            database.Product.Add(product);
            database.SaveChanges();
            MessageBox.Show("Товар добавлен!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
