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

namespace Dishes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        if(string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password)) { 
                MessageBox.Show("Введите логин и пароль");
                return;
            }
        //запрос к базе данных
        using (var db = new TableWareEntities())
            {
                //ссылка на таблицу базы данных
                var user = db.User
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == TextBoxLogin.Text && u.Password == PasswordBox.Password);
                //проверка 
                if (user == null)
                {
                    MessageBox.Show("Пользоветль с таким именем не найден!");
                    return;
                }        
                MessageBox.Show("Пользователь успешно найден!");

                switch (user.RoleID)
                {
                    case 1:
                        Window AdminWindow = new AdminWindow();
                        AdminWindow.Show();
                        break;
                    case 2:
                        Window UserWindown = new UserWindow();
                        UserWindown.Show();
                        break;
                }
                Close(); //для закрытия окна при вводе данных           
            }
        }
    }
}
