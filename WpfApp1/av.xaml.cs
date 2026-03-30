using System.Windows;
using WpfApp1;

namespace Bulochnaya
{
    public partial class LoginWindow : Window
    {
        private readonly BulochnayaRepository _repo;

        public LoginWindow()
        {
            InitializeComponent();
            string conn = "Server=.\\SQLEXPRESS;Database=AuthDemo;Integrated Security=true;";
            _repo = new BulochnayaRepository(conn);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = string.Empty;
            string user = UsernameTextBox.Text.Trim();
            string pass = PasswordBox.Password;

            if (string.IsNullOrEmpty(user)) { ErrorTextBlock.Text = "Введите имя пользователя."; UsernameTextBox.Focus(); return; }
            if (string.IsNullOrEmpty(pass)) { ErrorTextBlock.Text = "Введите пароль."; PasswordBox.Focus(); return; }

            bool ok = _repo.ValidateUser(user, pass);
            if (ok)
            {
                MessageBox.Show($"Добро пожаловать, {user}!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                var main = new av(user);
                main.Show();
                this.Close();
            }
            else
            {
                ErrorTextBlock.Text = "Неверное имя пользователя или пароль.";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var reg = new RegisterWindow(_repo);
            reg.Owner = this;
            reg.ShowDialog();
        }
    }
}
