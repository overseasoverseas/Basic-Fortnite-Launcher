using DiscordRPC;
using System.Windows;
using System.IO;
using WindowsAPICodePack.Dialogs;
using Launcher.Utils;
using System.Diagnostics;
using System.Net;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.System;
using System.Windows.Controls;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for Content.xaml
    /// </summary>
    public partial class Content : Window
    {
        private DiscordRpcClient _client;

        bool _isLaunchingTheGame = false;
        string _username = string.Empty;
        private string downloadFolder;

        System.Threading.Thread launcherThread;
        bool running = false;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public class Vars
        {
            public static string Email = "NONE";
            public static string Password = "NONE";
            public static string Path1 = "NONE";



        }
        public Content()
        {
            InitializeComponent();
            string storedPath1 = UserData.ReadValue("Auth", "Path1");
            string storedPath3 = UserData.ReadValue("Auth", "Path3");

          //  RPC.InitializeRPC();
         //   RPC.rpctimestamp = new Timestamps();
            if (storedPath1 != "NONE") PathBox.Text = storedPath1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = "Select A Fortnite Build";
            commonOpenFileDialog.Multiselect = false;
            CommonFileDialogResult commonFileDialogResult = commonOpenFileDialog.ShowDialog();


            bool flag = commonFileDialogResult == CommonFileDialogResult.Ok;
            if (flag)
            {
                if (File.Exists(System.IO.Path.Combine(commonOpenFileDialog.FileName, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe")))
                {
                    this.PathBox.Text = commonOpenFileDialog.FileName;
                }
                else
                {
                    System.Windows.MessageBox.Show("Please make sure that your the folder contains FortniteGame and Engine In");

                }
            }
        }

        public async void Launch()
        {
            try
            {
                if (running) return;
                running = true;
                if (Vars.Path1 == "NONE")
                {
                    Vars.Path1 = UserData.ReadValue("Auth", "Path1");
                }
                string GamePath = Vars.Path1;
                if (GamePath != "NONE")
                {

                    if (File.Exists(System.IO.Path.Combine(GamePath, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe")))
                    {
                        if (Vars.Email == "NONE")
                        {
                            Vars.Email = UserData.ReadValue("Auth", "Email");
                        }
                        if (Vars.Password == "NONE")
                        {
                            Vars.Password = UserData.ReadValue("Auth", "Password");
                        }
                        if (Vars.Email == "NONE" || Vars.Password == "NONE")
                        {
                            System.Windows.MessageBox.Show("Your login was incorrect, please logout!");
                        }
                        foreach (var proc in Process.GetProcessesByName("FortniteClient-Win64-Shipping"))
                        {
                            try
                            {
                                if (proc.MainModule.FileName.StartsWith(GamePath))
                                {
                                    proc.Kill();
                                    proc.WaitForExit();
                                }
                            }
                            catch
                            {

                            }
                        }
                        WebClient Client = new WebClient();
                        Client.DownloadFile($"redirect url thing!", Path.Combine(GamePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                        Game.Start(GamePath, "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -fromfl=eac -fltoken=919348d6add4c4c7c7507e61 -skippatchcheck", Vars.Email, Vars.Password);
                        ProcessAC.Start(GamePath, "FortniteClient-Win64-Shipping_BE.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "r");
                        ProcessAC.Start(GamePath, "FortniteLauncher.exe", $"-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck", "dsf");
                        try
                        {
                            ProcessAC._LauncherProcess.Close();
                            ProcessAC._EACProcess.Close();
                        }
                        catch (Exception ex)
                        {
                        }

                        IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                        ShowWindow(h, 6);
                        try
                        {
                            Game._FortniteProcess.WaitForExit();
                        }
                        catch (Exception) { }
                        try
                        {
                            try
                            {
                                Kill();
                            }
                            catch
                            {

                            }

                        }
                        catch (Exception)
                        {
                            System.Windows.MessageBox.Show("An error occurred while closing Fake ProcessAC!");

                        }



                        running = false;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Selected path is not a valid fortnite installation!");

                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please add your game path in settings!");

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());

            }
        }


        public static void Kill()
        {
            try
            {
                if (Game._FortniteProcess != null && !Game._FortniteProcess.HasExited) Process.GetProcessById(Game._FortniteProcess.Id).Kill();
                if (ProcessAC._LauncherProcess != null && !ProcessAC._LauncherProcess.HasExited) ProcessAC._LauncherProcess.Kill();
                if (ProcessAC._EACProcess != null && !ProcessAC._EACProcess.HasExited) ProcessAC._EACProcess.Kill();
            }
            catch (Exception)
            {

            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
     //       RPC.SetState(" Launching Chapter 90 Season 69...", true);
            launcherThread = new System.Threading.Thread(Launch);
            launcherThread.Start();
        }

        private void PathBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            UserData.WriteToConfig("Auth", "Path1", PathBox.Text);
        }
    }
}
