using ENCAPv3.UI;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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







            #region DB

            // Check for LocalDB 2019 installation
            //if (!IsLocalDB2019Installed())
            //{
            //    Logger.Fatal("SQL Server LocalDB 2019 is not installed. Please install LocalDB 2019 and restart the application.");
            //    MessageBox.Show("SQL Server LocalDB 2019 is not installed. Please install it and restart the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            EnsureDatabaseExists();
            #endregion


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



        #region DbVersion

        /// <summary>
        /// Checks if SQL Server LocalDB 2019 is installed.
        /// </summary>
        /// <returns>True if LocalDB 2019 is installed; otherwise, false.</returns>
        private static bool IsLocalDB2019Installed()
        {
            try
            {
                // Check for LocalDB 2019 installation
                string localDBVersion = GetLocalDBVersion();
                return localDBVersion.StartsWith("2019", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Logger.Error("Error checking LocalDB 2019 installation: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Executes the SqlLocalDB utility to get the LocalDB version.
        /// </summary>
        /// <returns>The LocalDB version string.</returns>
        private static string GetLocalDBVersion()
        {
            string version = string.Empty;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "sqllocaldb.exe",
                    Arguments = "info",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        version = reader.ReadToEnd();
                    }
                }

                if (string.IsNullOrWhiteSpace(version))
                {
                    throw new Exception("Unable to retrieve LocalDB version.");
                }

                return version;
            }
            catch (Exception ex)
            {
                Logger.Error("Error retrieving LocalDB version: " + ex.Message);
                throw;
            }
        }


        #endregion

        #region Database if not Exists

        /// <summary>
        /// Ensures the database exists; if not, creates it along with its structure.
        /// </summary>
        private static void EnsureDatabaseExists()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ENCAPdb"].ConnectionString;

            // Extract the file path from the connection string
            var builder = new SqlConnectionStringBuilder(connectionString);
            string dbFilePath = builder.AttachDBFilename;

            // Check if the database file exists
            if (!File.Exists(dbFilePath))
            {
                CreateDatabase(connectionString, dbFilePath);
            }
        }

        /// <summary>
        /// Creates the database and its structure.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="dbFilePath">The file path of the database.</param>
        private static void CreateDatabase(string connectionString, string dbFilePath)
        {
            try
            {
                string dbName = Path.GetFileNameWithoutExtension(dbFilePath);

                using (SqlConnection conn = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true"))
                {
                    conn.Open();

                    // Create the database
                    using (SqlCommand cmd = new SqlCommand($"CREATE DATABASE {dbName} ON (NAME = '{dbName}', FILENAME = '{dbFilePath}')", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                // Connect to the newly created database and create tables, stored procedures, and types
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Create table
                    using (SqlCommand cmd = new SqlCommand(GetTableCreationScript(), conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Create stored procedures
                    foreach (var script in GetStoredProcedureScripts())
                    {
                        using (SqlCommand cmd = new SqlCommand(script, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Create user-defined types
                    foreach (var script in GetUserDefinedTypeScripts())
                    {
                        using (SqlCommand cmd = new SqlCommand(script, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                Logger.Info("Database and its structure created successfully.");
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to create the database and its structure: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Returns the SQL script to create the table.
        /// </summary>
        private static string GetTableCreationScript()
        {
            return @"
            CREATE TABLE [dbo].[tblStorePoints](
                [Id] [int] IDENTITY(1,1) NOT NULL,
                [Parameter] [varchar](50) NULL,
	            [Battery1] [varchar](50) NULL,
	            [Battery2] [varchar](50) NULL,
	            [Battery3] [varchar](50) NULL,
	            [Battery4] [varchar](50) NULL,
	            [Battery5] [varchar](50) NULL,
	            [Battery6] [varchar](50) NULL,
	            [Battery7] [varchar](50) NULL,
	            [Battery8] [varchar](50) NULL,
	            [Battery9] [varchar](50) NULL,
	            [Battery10] [varchar](50) NULL,
	            [Battery11] [varchar](50) NULL,
	            [Battery12] [varchar](50) NULL,
	            [Battery13] [varchar](50) NULL,
	            [Battery14] [varchar](50) NULL,
	            [Battery15] [varchar](50) NULL,
	            [Battery16] [varchar](50) NULL,
	            [Battery17] [varchar](50) NULL,
	            [Battery18] [varchar](50) NULL,
	            [Battery19] [varchar](50) NULL,
	            [Battery20] [varchar](50) NULL,
                [TimeStamp] [datetime] NULL,
                CONSTRAINT [PK_tblStorePoints] PRIMARY KEY CLUSTERED 
                (
                    [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY];
            
            ALTER TABLE [dbo].[tblStorePoints] ADD  CONSTRAINT [DF_tblStorePoints_EntryTimeStamp]  DEFAULT (getdate()) FOR [TimeStamp];
            ";
        }

        /// <summary>
        /// Returns the SQL scripts to create stored procedures.
        /// </summary>
        private static string[] GetStoredProcedureScripts()
        {
            return new string[]
            {
                @"
                CREATE PROCEDURE [dbo].[sp_BulkInsertBatteryData]
                (
                    @BatteryData BatteryDataType READONLY
                )
                AS
                BEGIN
                    INSERT INTO tblStorePoints (TimeStamp, Parameter, Battery1, Battery2, Battery3, Battery4, Battery5)
                    SELECT TimeStamp, Parameter, Battery1, Battery2, Battery3, Battery4, Battery5
                    FROM @BatteryData;
                END;
                ",

                @"
            ALTER PROCEDURE [dbo].[usp_SelectStorePoints1]
                @StartDate DATETIME,  
                @EndDate DATETIME   
            AS  
            BEGIN  
                SET NOCOUNT ON;  

                -- Query for Voltage (V)
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4, Battery5, 
                    Battery6, Battery7, Battery8, Battery9, Battery10,
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20
                FROM tblStorePoints  
                WHERE Parameter = 'Voltage (V)' 
                    AND [TimeStamp] BETWEEN @StartDate AND @EndDate
                ORDER BY [TimeStamp] ASC;

                -- Query for Current (Amps)
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4, Battery5, 
                    Battery6, Battery7, Battery8, Battery9, Battery10,
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20
                FROM tblStorePoints   
                WHERE Parameter = 'Current (Amps)'
                    AND [TimeStamp] BETWEEN @StartDate AND @EndDate;

                -- Query for Power (kW)
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4, Battery5, 
                    Battery6, Battery7, Battery8, Battery9, Battery10,
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20
                FROM tblStorePoints  
                WHERE Parameter = 'Power (kW)'
                    AND [TimeStamp] BETWEEN @StartDate AND @EndDate;

                -- Query for SOC
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4, Battery5, 
                    Battery6, Battery7, Battery8, Battery9, Battery10,
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20
                FROM tblStorePoints  
                WHERE Parameter = 'SOC'
                    AND [TimeStamp] BETWEEN @StartDate AND @EndDate;

                -- Query for Total Remaining Capacity(Ah)
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4, Battery5, 
                    Battery6, Battery7, Battery8, Battery9, Battery10,
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20
                FROM tblStorePoints  
                WHERE Parameter = 'Total Remaining Capacity(Ah)'
                    AND [TimeStamp] BETWEEN @StartDate AND @EndDate;

                -- Query for Temperature (C)
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4, Battery5, 
                    Battery6, Battery7, Battery8, Battery9, Battery10,
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20
                FROM tblStorePoints    
                WHERE Parameter = 'Temperature (C)'
                    AND [TimeStamp] BETWEEN @StartDate AND @EndDate;
            END
            ",

            @"
            CREATE PROCEDURE [dbo].[sp_BulkInsertBatteryData]
            (
                @BatteryData BatteryDataType READONLY
            )
            AS
            BEGIN
                INSERT INTO tblStorePoints (TimeStamp, Parameter, Battery1, Battery2, Battery3, Battery4, Battery5)
                SELECT TimeStamp, Parameter, Battery1, Battery2, Battery3, Battery4, Battery5
                FROM @BatteryData;
            END;
            ",
            @"
            ALTER PROCEDURE [dbo].[usp_GetRdlcReportData]
                @StartDate DATETIME, 
                @EndDate DATETIME,     
                @SelectAll BIT,      
                @IncludeSOC BIT, 
                @IncludeVoltage BIT, 
                @IncludeCurrent BIT,     
                @IncludePower BIT,
                @IncludeTotalRemainingCapacity BIT 
            AS 
            BEGIN     
                SET NOCOUNT ON;      

                SELECT          
                    Parameter, [TimeStamp] as 'DateCheck',
                    Battery1, Battery2, Battery3, Battery4,          
                    Battery5, Battery6, Battery7, Battery8, Battery9, Battery10,  
                    Battery11, Battery12, Battery13, Battery14, Battery15, 
                    Battery16, Battery17, Battery18, Battery19, Battery20    
                FROM tblStorePoints     
                WHERE [TimeStamp] BETWEEN @StartDate AND @EndDate 
                AND (
                    @SelectAll = 1 
                    OR (@IncludeTotalRemainingCapacity = 1 AND Parameter = 'Total Remaining Capacity(Ah)')    
                    OR (@IncludeSOC = 1 AND Parameter = 'SOC') 
                    OR (@IncludePower = 1 AND Parameter = 'Power (kW)') 
                    OR (@IncludeVoltage = 1 AND Parameter = 'Voltage (V)') 
                    OR (@IncludeCurrent = 1 AND Parameter = 'Current (Amps)') 
                )
                ORDER BY [TimeStamp] ASC;
            END
            "




            };
        }

        /// <summary>
        /// Returns the SQL scripts to create user-defined types.
        /// </summary>
        private static string[] GetUserDefinedTypeScripts()
        {
            return new string[]
            {
                @"
                CREATE TYPE [dbo].[BatteryDataType] AS TABLE
                (
                    [TimeStamp] [datetime] NULL,
                      NULL,
                    [Battery1] [decimal](18, 2) NULL,
                    [Battery2] [decimal](18, 2) NULL,
                    [Battery3] [decimal](18, 2) NULL,
                    [Battery4] [decimal](18, 2) NULL,
                    [Battery5] [decimal](18, 2) NULL
                );
                "
            };
        }

        #endregion





    }
}
