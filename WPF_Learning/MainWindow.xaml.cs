using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Learning.Services;

namespace WPF_Learning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnTestLogic_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Visibility == Visibility.Visible)
            {
                var authService = new AuthService();
                string user = txtUsername.Text;
                string pass = txtPassword.Password;

                bool isSuccess = await authService.LoginAsync(user, pass);

                if (isSuccess)
                {
                    MessageBox.Show($"Success! ID: {UserSession.BusinessEntityID}");

                    txtPassword.Visibility = Visibility.Collapsed;
                    txtUsername.Visibility = Visibility.Collapsed;
                    btnTestLogic.HorizontalAlignment = HorizontalAlignment.Left;
                    btnTestLogic.VerticalAlignment = VerticalAlignment.Top;
                    btnTestLogic.Margin = new Thickness(0);

                    LoadOrderHistory();
                }
                else
                {
                    MessageBox.Show("Login failed. Check API and Credentials.");
                }
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtUsername.Visibility = Visibility.Visible;

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException("will be done after Identity testing");
        }

        private async void LoadOrderHistory()
        {
            var orderService = new OrderService();
            var orders = await orderService.GetUserOrdersAsync();

            try
            {
                if (orders != null)
                {
                    ProductGrid.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show($"Detailed Error: {ex.Message} \n\n Inner: {ex.InnerException?.Message}");
            }
        }
    }
}