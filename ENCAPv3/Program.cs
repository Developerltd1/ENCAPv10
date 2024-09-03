using ENCAPv3.UI;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENCAPv3
{
    static class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Ensure the log directory exists
            string logDirectory = @"C:\Logs\MyApp";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            // Configure log4net
            XmlConfigurator.Configure();


            // Set up log4net configuration programmatically
            ConfigureLogging();

            Logger.Info("Application started.");
            // Global error handling
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(GlobalExceptionHandler);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);


            Logger.Info("Application started.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {

                //Application.Run(new DashBoard1());
                //Application.Run(new MainForm(null,null));
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Logger.Fatal("Application encountered a fatal error and needs to close. " + ex.Message);
                throw;  // Re-throw to let the application crash if it's a critical error
            }
            finally
            {
                Logger.Info("Application closed.");
                LogManager.Shutdown();  // Ensure to flush and close down internal threads and timers
            }

        }

        private static void ConfigureLogging()
        {
            // Define the layout
            PatternLayout layout = new PatternLayout
            {
                ConversionPattern = "%date %-5level %logger - %message%newline"
            };
            layout.ActivateOptions();

            // Define the file appender
            RollingFileAppender fileAppender = new RollingFileAppender
            {
                File = @"C:\Logs\MyApp\logfile.txt",
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxSizeRollBackups = 100, // Number of backup files to keep
                MaximumFileSize = "100MB", // Max file size before rolling over
                StaticLogFileName = true,
                Layout = layout
            };
            fileAppender.ActivateOptions();

            // Configure log4net with the new settings
            BasicConfigurator.Configure(fileAppender);
        }

        static void GlobalExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.Error(e.Exception+ "Unhandled thread exception");
            MessageBox.Show("An GlobalExceptionHandler unexpected error occurred. The application will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal( "Unhandled domain exception: "+ (Exception)e.ExceptionObject);
            MessageBox.Show("A UnhandledExceptionHandler critical error occurred. The application will now close.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
    }
}
