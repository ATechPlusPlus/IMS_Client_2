using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.CompilerServices;
[assembly: SuppressIldasm]
namespace IMS_Client_2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Application is already running", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //string myServiceName = "MSSQL$SQLEXPRESS"; //service name of SQL Server Express
                //string myServiceName = "MSSQLSERVER"; //service name of SQL Server Express

                string myServiceName = "MSSQL$SQL2014"; //service name of SQL Server Express ashfaque
                string status; //service status (For example, Running or Stopped)

                //display service status: For example, Running, Stopped, or Paused
                ServiceController mySC = new ServiceController(myServiceName);
                try
                {
                    status = mySC.Status.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                    return;
                }
                //display service status: For example, Running, Stopped, or Paused
                //MessageBox.Show("Service status : " + status);

                //if service is Stopped or StopPending, you can run it with the following code.
                if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
                {
                    try
                    {
                        MessageBox.Show("Starting the service...");
                        mySC.Start();
                        mySC.WaitForStatus(ServiceControllerStatus.Running);
                        MessageBox.Show("The service is now " + mySC.Status.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in starting the service: " + ex.Message);
                    }
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new SplashWindow());
                Application.Run(new frmHome());
            }
        }
    }
}
