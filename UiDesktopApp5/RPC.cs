using DiscordRPC;
using Launcher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class RPC
    {
        public static DiscordRpcClient client;
        public static Timestamps rpctimestamp { get; set; }
        private static RichPresence presence;



        private void LoadUsernameFromMongoDB()
        {
            try
            {
                string storedEmail = UserData.ReadValue("Auth", "Email");
                var user = GetJsonBody("http://ip:port/username", $"{{\"email\":\"{storedEmail}\"}}");

                if (user != "")
                {
                    user = user.Length > 25 ? user.Substring(0, 25) : user;
                }
                else
                {
                    user = "Username not found for this email.";
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static void InitializeRPC()
        {
            string storedEmail = UserData.ReadValue("Auth", "Email");
            var user = GetJsonBody("http://ip:port/username", $"{{\"email\":\"{storedEmail}\"}}");

            string GetJsonBody(string url, string body)
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

            client = new DiscordRpcClient("1252424129173393479");
            client.Initialize();
            Button[] buttons = { new Button() { Label = "Join Discord!", Url = "your discord or whatever" } };
            presence = new RichPresence()
            {
                Details = $"Logged in as {user}",
                State = "Project Skibidi",
                Timestamps = rpctimestamp,
                Buttons = buttons,

                Assets = new Assets()
                {
                    LargeImageKey = "", // image
                    LargeImageText = "Project Skibidi",
                    SmallImageKey = "",
                    SmallImageText = "",

                }
            };
            SetState("Project Skibidi");
        }


        public static void SetState(string state, bool watching = false)
        {
            if (watching)
                state = "" + state;

            presence.State = state;
            client.SetPresence(presence);
        }

        public string GetJsonBody(string url, string body)
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
    }
}