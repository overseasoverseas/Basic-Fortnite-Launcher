using Nest;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Launcher.Utils
{
    internal class Game
    {
        public static Process _FortniteProcess;
        public static void Start(string PATH, string args, string Email, string Password)
        {
            if (File.Exists(Path.Combine(PATH, "MoralClient.exe")))
            {
                Game._FortniteProcess = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        Arguments = $"-AUTH_LOGIN={Email} -AUTH_PASSWORD={Password} -AUTH_TYPE=epic " + args,
                        FileName = Path.Combine(PATH, "MoralClient.exe")
                    },
                    EnableRaisingEvents = true
                };
                Game._FortniteProcess.Exited += new EventHandler(Game.OnFortniteExit);
                Game._FortniteProcess.Start();


            }

        }

        public static void OnFortniteExit(object sender, EventArgs e)
        {
            Process fortniteProcess = Game._FortniteProcess;
            if (fortniteProcess != null && fortniteProcess.HasExited)
            {
                Game._FortniteProcess = (Process)null;
            }
            if (ProcessAC._LauncherProcess != null && !ProcessAC._LauncherProcess.HasExited) ProcessAC._LauncherProcess?.Kill();
            if (ProcessAC._EACProcess != null && !ProcessAC._LauncherProcess.HasExited) ProcessAC._EACProcess?.Kill();
        }
    }
}

