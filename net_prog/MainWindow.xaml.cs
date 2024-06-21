using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace CurrencyClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetRateButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string currencyFrom = CurrencyFrom.Text;
            string currencyTo = CurrencyTo.Text;

            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 9999))
                using (NetworkStream stream = client.GetStream())
                {
                   
                    byte[] data = Encoding.UTF8.GetBytes(username);
                    stream.Write(data, 0, data.Length);
                    stream.Read(new byte[1024], 0, 1024); 

                    
                    data = Encoding.UTF8.GetBytes(password);
                    stream.Write(data, 0, data.Length);
                    stream.Read(new byte[1024], 0, 1024); 

                    
                    string message = $"{currencyFrom} {currencyTo}";
                    data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string response = Encoding.UTF8.GetString(responseData, 0, bytes);

                    ResultTextBlock.Text = response;
                }
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = $"Помилка: {ex.Message}";
            }
        }
    }
}