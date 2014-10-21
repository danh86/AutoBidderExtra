using System;
using System.Collections.Generic;
using System.Linq;
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

using System.Threading.Tasks;

using FUTAutoBuyer;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Data;

namespace FUTAutoBuyer.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FUTClient client;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (await LoginAsync())
            {
                //logged in hopefully
                tbLoginInfo.Text = "Login Successful!";
            }
            else
            {
                tbLoginInfo.Text = "Error Logging in.";
            }
        }

        private async Task<Boolean> LoginAsync()
        {
            client = new FUTClient();
            var loginDetails = new LoginDetails("EMAIL", "PASSWORD", "SECRET Q", PlayerPlatform.XboxOne);

            try
            {
                var loginResponse = await client.LoginAsync(loginDetails);
                if (loginResponse != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                //do something
                return false;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var pd = new PlayerData();
            //pd.UpdatePlayerData(client, lbUpdateInfo);            
        }
    }
}
