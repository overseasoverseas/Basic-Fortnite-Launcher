using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Launcher.Utils
{
    internal class ProcessAC
    {
        public static Process _LauncherProcess;
        public static Process _EACProcess;

        public static void Start(string Path2, string FileName, string args = "", string t = "r")
        {
            try
            {
                if (File.Exists(Path.Combine(Path2, "FortniteGame\\Binaries\\Win64\\", FileName)))
                {
                    ProcessStartInfo ProcessIG = new ProcessStartInfo()
                    {
                        FileName = Path.Combine(Path2, "FortniteGame\\Binaries\\Win64\\", FileName),
                        Arguments = args,
                        CreateNoWindow = true,
                    };

                    if (t == "r")
                    {
                        _EACProcess = Process.Start(ProcessIG);
                        if (_EACProcess.Id == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Error With Launching. Please Contact Support!");
                        }
                        _EACProcess.Freeze();
                    }
                    else
                    {
                        _LauncherProcess = Process.Start(ProcessIG);
                        if (_LauncherProcess.Id == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Error With Launching. Please Contact Support!");
                        }
                        _LauncherProcess.Freeze();
                    }

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error With Launching. Please Contact Support!");
            }
        }
    }
}