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

namespace Launcher
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        /*/
         *         public string GetJsonBody(string url, string body)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Content = content;
                var field = typeof(HttpMethod).GetField("_content", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (field != null && field.GetValue(request.Method) is HttpMethod method)
                {
                    method.GetType().GetProperty("ContentBodyNotAllowed", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(method, false);
                }

                HttpResponseMessage response = httpClient.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }
        private bool ValidateCredentialsAsync(string email, string password)
        {
            try
            {
                return GetJsonBody("http://ip:port/login", $"{{\"email\":\"{email}\", \"password\":\"{password}\"}}") == "true";
            }
            catch (Exception ex)
            {
                return false;
            }
        } /*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Content content = new Content();
            content.Show();
            this.Close();

            // add ur own code or use this

            /*/
        string email = Email.Text;
            string password = Password.Password;

            bool isValid = ValidateCredentialsAsync(email, password);

            if (isValid)
            {
                UserData.WriteToConfig("Auth", "Email", email);
                UserData.WriteToConfig("Auth", "Password", password);
                var content = new Content();
                content.Show();
                this.Close();
            }
            else
            {
                UserData.WriteToConfig("Auth", "Email", email);
                UserData.WriteToConfig("Auth", "Password", password);
                MessageBox.Show("ERROR: Email/Password is incorrect!");
            } /*/
        }
    }
}
