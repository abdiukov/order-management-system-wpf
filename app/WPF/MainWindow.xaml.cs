using Auth0.OidcClient;
using IdentityModel.OidcClient;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using View;

namespace LoginAuth
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Login();
        }

        private async void Login()
        {
            string domain = ConfigurationManager.AppSettings["Auth0:Domain"];
            string clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];

            Auth0Client client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = domain,
                ClientId = clientId
            });

            var extraParameters = new Dictionary<string, string>
            {
                { "connection", "Username-Password-Authentication" }
            };
            ProcessOutputFromLogin(await client.LoginAsync(extraParameters: extraParameters));
            this.Close();
        }

        private void ProcessOutputFromLogin(LoginResult loginResult)
        {
            //If it's not an error, start the application
            if (!loginResult.IsError)
            {
                MainView pageobj = new MainView();
                pageobj.Show();
            }
        }

    }
}