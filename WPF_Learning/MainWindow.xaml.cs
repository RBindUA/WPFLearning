using System.Collections.ObjectModel;
using System.Net.Http;
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
using WPF_Learning.Models;
using WPF_Learning.Services;

namespace WPF_Learning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        public ObservableCollection<ProductDTO> Catalog { get; set; } = new ();
        public ObservableCollection<ProductDTO> Cart { get; set; } = new();

        private readonly OrderService _orderService = new();
        private readonly ProductServiceClient _productService = new(new HttpClient());
        public MainWindow()
        {
            InitializeComponent();
            HistoryGrid.Visibility = Visibility.Hidden;
            CatalogGrid.ItemsSource = Catalog;
            CartGrid.ItemsSource = Cart;
        }

        private async void btnTestLogic_Click(object sender, RoutedEventArgs e)
        {
            //TO DO: mess with visibilities and text in the future
            if (txtPassword.Visibility == Visibility.Visible)
            {
                var authService = new AuthService();
                string user = txtUsername.Text;
                string pass = txtPassword.Password;

                bool isSuccess = await authService.LoginAsync(user, pass);

                if (isSuccess)
                {
                    MessageBox.Show($"Success! ID: {UserSession.BusinessEntityID}");

                    //Auth completed. Show UI
                    txtPassword.Visibility = Visibility.Collapsed;
                    txtUsername.Visibility = Visibility.Collapsed;
                    HistoryGrid.Visibility = Visibility.Visible;
                    CatalogGrid.Visibility = Visibility.Visible;
                    btnTestLogic.HorizontalAlignment = HorizontalAlignment.Left;
                    btnTestLogic.VerticalAlignment = VerticalAlignment.Top;
                    btnTestLogic.Margin = new Thickness(0);

                    LoadOrderHistory();
                    await LoadCatalog();
                }
                else
                {
                    MessageBox.Show("Login failed. Check API and Credentials.");
                }
            }
            else
            {
                //HIDE EVERYTHING during login
                txtPassword.Visibility = Visibility.Visible;
                txtUsername.Visibility = Visibility.Visible;
                HistoryGrid.Visibility = Visibility.Hidden;

            }

        }

        private async void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Starting to place order");
            if (Cart.Count == 0) return;

            try
            {
                var newOrder = new OrderDTO
                {
                    CustomerID = UserSession.BusinessEntityID,
                    TotalDue = Cart.Sum(x => x.ListPrice),
                    OrderDate = DateTime.Now,
                    //Required by db
                    DueDate = DateTime.Now.AddDays(7),
                    ShipDate = DateTime.Now.AddDays(2)
                };

                bool isSuccess = await _orderService.SubmitOrderAsync(newOrder);

                if (isSuccess)
                {
                    LoadOrderHistory();
                    Cart.Clear();
                    MessageBox.Show("Order successful.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Order Error: {ex.Message}");
            }
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (CatalogGrid.SelectedItem is ProductDTO selected)
            {
                Cart.Add(selected);
            }
        }

        private async void LoadOrderHistory()
        {
            var orderService = new OrderService();
            var orders = await orderService.GetUserOrdersAsync();

            try
            {
                if (orders != null)
                {
                    HistoryGrid.ItemsSource = orders;
                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show($"Detailed Error: {ex.Message} \n\n Inner: {ex.InnerException?.Message}");
            }
        }
        private async Task LoadCatalog()
        {
            try
            {
                var products = await _productService.GetCatalogAsync();
                if (products != null)
                {
                    Catalog.Clear();
                    foreach (var p in products) Catalog.Add(p);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load catalog: {ex.Message}");
            }
        }
    }
}