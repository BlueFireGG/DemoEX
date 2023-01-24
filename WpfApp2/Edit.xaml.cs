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
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private user2Entities database;
        public Product product { get; set; }
        public List<string> Articles { get; set; }
        public Edit(user2Entities database, Product product)
        {
            InitializeComponent();
            this.database = database;
            this.product = product;
            Articles = database.Product.Select(a => a.ProductArticleNumber).Distinct().ToList();
            DataContext = this;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (dialog.ShowDialog()==true)
            {
                product.ProductPhoto = dialog.SafeFileName;
                Photo.GetBindingExpression(Image.SourceProperty)?.UpdateTarget();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            database.SaveChanges();
            MessageBox.Show("Данные изменены!");
        }
    }
}
