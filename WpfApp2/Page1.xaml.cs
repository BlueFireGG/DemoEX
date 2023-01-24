using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public static user2Entities connection = new user2Entities();


        public static ObservableCollection<Product> products { get; set; }



        private string code = "";
        Random random = new Random((int)DateTime.Now.Ticks);
        public Page1()
        {
            InitializeComponent();
            CreateCaptha();
        }

        private void CreateCaptha()
        {
            string chars = "qwerty123#";
            code = "";
            for (int i = 0; i < 5; i++)
            {
                int a = random.Next(0, chars.Length - 1);
                code += chars.Substring(a, 1);
                LetterCreate(code[i], i);
            }
            LineCreator();
        }

        private void LetterCreate(char a, int b)
        {
            Label label = new Label();
            label.Content = a;
            label.FontSize = 24;
            label.FontWeight = FontWeights.Bold;
            label.RenderTransformOrigin = new Point(0.5, 0.5);
            label.RenderTransform = new RotateTransform(random.Next(-20, 31));
            canvas.Children.Add(label);
            Canvas.SetLeft(label, 50 + (b * 20));
        }

        private void LineCreator()
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            line.StrokeThickness = 4;
            line.X1 = 5;
            line.Y1 = random.Next(0, 41);
            line.X2 = 197;
            line.Y2 = random.Next(0, 41);
            canvas.Children.Add(line);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string number_ad = login.Text;
            string password_ad = pass.Text;
            string capp_ad = cap.Text;

            //if (cap.Text != code)
            //{
            //    MessageBox.Show("Неверный код проверки!");
            //    canvas.Children.Clear();

            //    CreateCaptha();
            //    return;
            //}
            if ((number_ad.Length == 0) && (password_ad.Length == 0) && (capp_ad.Length == 0))
            {
                MessageBox.Show("Все поля пустые!");
                return;
            }
            if (number_ad.Length == 0)
            {
                MessageBox.Show("Поле логин пустое!");
                return;
            }
            if ((number_ad.Length == 0) && (password_ad.Length == 0))
            {
                MessageBox.Show("Поле пароль пустое!");
                return;
            }
            var employ = connection.User.Where(em => em.UserLogin == number_ad && em.UserPassword == password_ad).FirstOrDefault();
            if (employ != null)
            {
                switch (employ.UserRole)
                {
                    case 1:
                        MessageBox.Show("Добро пожаловать, Администратор!");
                        NavigationService.Navigate(Class1.GetPage2());
                        break;
                    case 2:
                        MessageBox.Show("Добро пожаловать, Менеджер!");
                        break;
                    case 3:
                        MessageBox.Show("Добро пожаловать, Клиент");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Введены неправильные данные");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Class1.Page3);
        }
    }
}
