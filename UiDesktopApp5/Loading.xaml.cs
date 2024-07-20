using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        public Loading()
        {
            InitializeComponent();
            StartLoadingProcess();
        }

        private async void StartLoadingProcess()
        {
            await Task.Delay(1000);
            await Task.Delay(1000);
            await Task.Delay(1000);
            await Task.Delay(1000);

            var login = new Login();
            login.Show();
            this.Close();
            /*/    string storedEmail = INIProcess.ReadValue("Auth", "Email");
                string storedPassword = INIProcess.ReadValue("Auth", "Password");

                bool isCredentialsValid = ValidateCredentialsAsync(storedEmail, storedPassword);

                if (!isCredentialsValid)
                {
                    var login = new Login();
                    login.Show();
                    this.Close();
                }
                else
                {
                    var content = new Content();
                    content.Show();
                    this.Close();
                } /*/
        }
    }
}

     /*/   public string GetJsonBody(string url, string body)
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
        }
    }
}
     /*/  