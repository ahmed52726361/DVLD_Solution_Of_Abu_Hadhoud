using DVLD.Applications;
using DVLD.Login;
using DVLD.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // --- DATABASE DEPLOYMENT FIX ---
            // Create a dedicated folder in the user's AppData for the database
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DVLD_Database";
            if (!Directory.Exists(appDataPath)) 
            {
                Directory.CreateDirectory(appDataPath);
            }

            // Define where the files should be in AppData
            string dbDestPath = Path.Combine(appDataPath, "DVLD.mdf");
            string logDestPath = Path.Combine(appDataPath, "DVLD_log.ldf");

            // If the database isn't in AppData yet, copy it from the installation folder (Program Files)
            if (!File.Exists(dbDestPath))
            {
                string dbSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DVLD.mdf");
                string logSourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DVLD_log.ldf");
                
                if (File.Exists(dbSourcePath))
                {
                    File.Copy(dbSourcePath, dbDestPath, true);
                    // Remove read-only attribute that might be inherited from Program Files
                    File.SetAttributes(dbDestPath, FileAttributes.Normal);
                }
                
                if (File.Exists(logSourcePath))
                {
                    File.Copy(logSourcePath, logDestPath, true);
                    File.SetAttributes(logDestPath, FileAttributes.Normal);
                }
            }

            // Tell the application that |DataDirectory| in the connection string now points to the writable AppData folder!
            AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);
            // ----------------------------------------------------

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new frmMain());
          // Application.Run(new frmTest2());
          Application.Run(new frmLogin());
         


        }
    }
}
