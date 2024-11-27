using EMView.UI;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMView
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                string databaseFilePath = @"C:\DbFile\ENCAPdb.mdf";
                string directoryPath = @"C:\DbFile";

                // Check and create the database if not exists
                //if (!Directory.Exists(directoryPath))
                //{
                //    Directory.CreateDirectory(directoryPath);
                //    if (!File.Exists(databaseFilePath))
                //    {
                //        CreateDatabase();
                //    }
                //}
                //else
                //{

                //}

                //Application.Run(new DashBoard1());
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
        #region DBCreate&ExistCheck

        // Method to check if the database exists
        private static bool DatabaseExists()
        {
            // SQL query to check if the database exists
            string checkDatabaseQuery = "SELECT database_id FROM sys.databases WHERE name = 'ENCAPDB'";

            try
            {
                using (SqlConnection connection = new SqlConnection(new BusinessLogic.DbConnection().connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(checkDatabaseQuery, connection);
                    var result = command.ExecuteScalar();

                    return result != DBNull.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking database: {ex.Message}");
                return false;
            }
        }
        // Method to create the database and run the script
        private static void CreateDatabase()
        {
            string createDatabaseScript = @"
            
            CREATE DATABASE [C:\DbFile\ENCAPDB.MDF];
            GO

            CREATE TYPE [dbo].[BatteryDataType] AS TABLE(
        	[TimeStamp] [datetime] NULL,
        	[Parameter] [nvarchar](50) NULL,
        	[Battery1] [float] NULL,
        	[Battery2] [float] NULL,
        	[Battery3] [float] NULL,
        	[Battery4] [float] NULL,
        	[Battery5] [float] NULL
            )
            
            CREATE TYPE [dbo].[BatteryDataType1] AS TABLE(
            	[TimeStamp] [datetime] NULL,
            	[Parameter] [nvarchar](50) NULL,
            	[Battery1] [float] NULL,
            	[Battery2] [float] NULL,
            	[Battery3] [float] NULL,
            	[Battery4] [float] NULL,
            	[Battery5] [float] NULL
            )
            
            CREATE TABLE [dbo].[tblAlarm](
            	[Id] [int] IDENTITY(1,1) NOT NULL,
            	[BatteryId] [int] NULL,
            	[Alarm] [varchar](150) NULL,
            	[OccurrenceTime] [datetime] NULL,
            	[EntryTime] [datetime] NULL,
             CONSTRAINT [PK_tblAlarm] PRIMARY KEY CLUSTERED 
            (
            	[Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            
            CREATE TABLE [dbo].[tblCanbus](
            	[Id] [int] IDENTITY(1,1) NOT NULL,
            	[VoltCanbus] [decimal](18, 0) NULL,
            	[CurrentCanbus] [decimal](18, 0) NULL,
            	[PowerCanbus] [decimal](18, 0) NULL,
            	[AvgTempCanbus] [decimal](18, 0) NULL,
            	[SOCCanbus] [decimal](18, 0) NULL,
            	[SOHCanbus] [decimal](18, 0) NULL,
            	[EntryDate] [datetime] NULL,
             CONSTRAINT [PK_tblCanbus] PRIMARY KEY CLUSTERED 
            (
            	[Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            
            CREATE TABLE [dbo].[tblMainParameters](
            	[Id] [int] IDENTITY(1,1) NOT NULL,
            	[Voltage] [decimal](18, 2) NULL,
            	[Currents] [decimal](18, 2) NULL,
            	[Power] [decimal](18, 2) NULL,
            	[SOC] [decimal](18, 2) NULL,
            	[Temp] [decimal](18, 2) NULL,
            	[EntryDateTime] [datetime] NULL,
             CONSTRAINT [PK_tblMainParameters] PRIMARY KEY CLUSTERED 
            (
            	[Id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            
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
            ) ON [PRIMARY]
            GO

            ALTER TABLE [dbo].[tblAlarm] ADD  CONSTRAINT [DF_tblAlarm_EntryTime]  DEFAULT (getdate()) FOR [EntryTime]
            GO
            ALTER TABLE [dbo].[tblCanbus] ADD  CONSTRAINT [DF_tblCanbus_EntryDate]  DEFAULT (getdate()) FOR [EntryDate]
            GO
            ALTER TABLE [dbo].[tblMainParameters] ADD  CONSTRAINT [DF_tblMainParameters_EntryDateTime]  DEFAULT (getdate()) FOR [EntryDateTime]
            GO
            ALTER TABLE [dbo].[tblStorePoints] ADD  CONSTRAINT [DF_tblStorePoints_EntryTimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]

            CREATE PROCEDURE [dbo].[sp_BulkInsertBatteryData]
            (
                @BatteryData BatteryDataType READONLY
            )
            AS
            BEGIN
                INSERT INTO BatteryData (TimeStamp, Parameter, Battery1, Battery2, Battery3, Battery4, Battery5)
                SELECT TimeStamp, Parameter, Battery1, Battery2, Battery3, Battery4, Battery5
                FROM @BatteryData;
            END
            
            CREATE PROCEDURE [dbo].[usp_CanbusCharts]    
                @StartDate DATETIME,      
                @EndDate DATETIME,  
                @IncludeVoltage BIT = 0,    
                @IncludeCurrents BIT = 0,  
                @IncludePower BIT = 0,  
                @IncludeSOC BIT = 0,  
                @IncludeTemp BIT = 0,  
                @SelectAll BIT = 0  -- Parameter for selecting all columns  
            AS      
            BEGIN      
                SET NOCOUNT ON;      
              
                -- Check if no columns are selected and SelectAll is not set  
                IF @SelectAll = 0 AND @IncludeVoltage = 0 AND @IncludeCurrents = 0 AND @IncludePower = 0 AND @IncludeSOC = 0 AND @IncludeTemp = 0  
                BEGIN  
                    -- If no columns are selected, do nothing (or return an empty result set)  
                    RETURN;  
                END  
                  
                -- Start building the dynamic SQL query  
                DECLARE @sql NVARCHAR(MAX)  
                SET @sql = 'SELECT mp.EntryDate as DateCheck'  
            
                -- If SelectAll is set to 1, include all columns regardless of other parameters  
                IF @SelectAll = 1  
                BEGIN  
                    SET @sql += ', mp.VoltCanbus as Voltage, mp.CurrentCanbus as Currents, mp.PowerCanbus as Power, mp.SOCCanbus as SOC, mp.SOHCanbus as Temp'  
                END  
                ELSE  
                BEGIN  
                    -- Conditionally append columns to the SELECT query based on the input parameters  
                    IF @IncludeVoltage = 1  
                        SET @sql += ', mp.VoltCanbus as Voltage'  
                    IF @IncludeCurrents = 1  
                        SET @sql += ', mp.CurrentCanbus as Currents'  
                    IF @IncludePower = 1  
                        SET @sql += ', mp.PowerCanbus as Power'  
                    IF @IncludeSOC = 1  
                        SET @sql += ', mp.SOCCanbus as SOC'  
                    IF @IncludeTemp = 1  
                        SET @sql += ', mp.SOHCanbus as Temp'  
                END  
              
                -- Complete the FROM and WHERE clauses  
                SET @sql += ' FROM tblCanbus mp WHERE mp.EntryDate BETWEEN @StartDate AND @EndDate ORDER BY mp.EntryDate ASC'  
              
                -- Execute the dynamic SQL query  
            	  PRINT @sql
                EXEC sp_executesql @sql, N'@StartDate DATETIME, @EndDate DATETIME', @StartDate, @EndDate  
            END  
            
            CREATE PROCEDURE [dbo].[usp_GetAlarmDataFrmDb] --'8/2/2024 1:27:16 PM','8/6/2024 1:27:16 PM'  
                @StartDate DATETIME,    
                @EndDate DATETIME     
            AS    
            BEGIN    
                SET NOCOUNT ON;    
                SELECT  a.EntryTime as 'TimeStamp',a.BatteryId, a.Alarm, a.OccurrenceTime FROM tblAlarm a
            	WHERE a.EntryTime  BETWEEN @StartDate AND @EndDate  
            	Order by a.EntryTime asc  
            END  
            
            CREATE PROCEDURE [dbo].[usp_GetCSVReportData]
            ( @StartDate DATETIME,  
                @EndDate DATETIME 
            	)
            AS  
            BEGIN  
              SET NOCOUNT ON;  
                    SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,SlaveId1,Battery2,SlaveId2,Battery3,SlaveId3,Battery4,SlaveId4,Battery5,SlaveId5, 
            		Battery6,SlaveId6,Battery7,SlaveId7,Battery8,SlaveId8,Battery9,SlaveId9,Battery10,SlaveId10,
            		Battery11,SlaveId11,Battery12,SlaveId12,Battery13,SlaveId13,Battery14,SlaveId14,Battery15,SlaveId15, 
            		Battery16,SlaveId16,Battery17,SlaveId17,Battery18,SlaveId18,Battery19,SlaveId19,Battery20,SlaveId20
                    FROM tblStorePoints  
                    WHERE --Parameter = 'Voltage (V)' And
            		[TimeStamp]  BETWEEN @StartDate AND @EndDate
            		Order by [TimeStamp] asc
            
            END
            
            --  exec usp_GetRdlcReportData'8/12/2023 1:27:16 PM','8/22/2025 1:27:16 PM',1,0,0,0
            CREATE PROCEDURE [dbo].[usp_GetRdlcReportData] 
            (  
            @StartDate DATETIME, @EndDate DATETIME,       
            @SelectAll BIT = 0,      @IncludeSOC BIT = 0,   
            @IncludeVoltage BIT = 0, @IncludeCurrent BIT = 0    
            )   
            AS   
            BEGIN       
            SET NOCOUNT ON;        
            SELECT            
            Parameter,[TimeStamp] as 'DateCheck',Battery1,  Battery2,  Battery3, Battery4,            
            Battery5,  Battery6,  Battery7,  Battery8,Battery9,  Battery10,    
            Battery11,  Battery12, Battery13,  Battery14,  Battery15,   
            Battery16,  Battery17, Battery18,Battery19, Battery20      
            FROM tblStorePoints       
            WHERE [TimeStamp] BETWEEN @StartDate AND @EndDate   
            AND ( @SelectAll = 1   
            OR  (@IncludeSOC = 1 AND Parameter = 'SOC')   
            OR  (@IncludeVoltage = 1 AND Parameter = 'Voltage (V)')   
            OR  (@IncludeCurrent = 1 AND Parameter = 'Current (Amps)')   
              
            ) 
            --AND Parameter = 'SERIAL' and Battery1 IS NOT NULL and Battery2 IS NOT NULL  and Battery3 IS NOT NULL  
            order by [TimeStamp] asc  
              
            END
            
            -- Transposed version of the original stored procedure with slaveId
            CREATE PROCEDURE [dbo].[usp_GetRdlcReportData_Transposed]
            (
                @StartDate DATETIME,
                @EndDate DATETIME,
                @SelectAll BIT,
                @IncludeSOC BIT,
                @IncludeVoltage BIT,
                @IncludeCurrent BIT,
                @IncludePower BIT,
                @IncludeTotalRemainingCapacity BIT
            )
            AS
            BEGIN
                SET NOCOUNT ON;
            
                -- Create a temporary table to hold transposed rows
                CREATE TABLE #TempTable (
                    DateCheck DATETIME,
                    Parameter VARCHAR(255),
                    BatteryValue FLOAT, -- Assuming all battery-related values are of type FLOAT
                    slaveId INT
                );
            
                -- Insert transposed rows into temporary table based on conditions
                INSERT INTO #TempTable (DateCheck, Parameter, BatteryValue, slaveId)
                SELECT
                    [TimeStamp] AS DateCheck,
                    CASE
                        WHEN @IncludeTotalRemainingCapacity = 1 THEN 'Total Remaining Capacity(Ah)'
                        WHEN @IncludeSOC = 1 THEN 'SOC'
                        WHEN @IncludePower = 1 THEN 'Power (kW)'
                        WHEN @IncludeVoltage = 1 THEN 'Voltage (V)'
                        WHEN @IncludeCurrent = 1 THEN 'Current (Amps)'
                    END AS Parameter,
                    CASE
                        WHEN @IncludeTotalRemainingCapacity = 1 THEN Battery1
                        WHEN @IncludeSOC = 1 THEN Battery2
                        WHEN @IncludePower = 1 THEN Battery3
                        WHEN @IncludeVoltage = 1 THEN Battery4
                        WHEN @IncludeCurrent = 1 THEN Battery5
                    END AS BatteryValue,
                    CASE
                        WHEN @IncludeTotalRemainingCapacity = 1 THEN 1
                        WHEN @IncludeSOC = 1 THEN 2
                        WHEN @IncludePower = 1 THEN 3
                        WHEN @IncludeVoltage = 1 THEN 4
                        WHEN @IncludeCurrent = 1 THEN 5
                    END AS slaveId
                FROM tblStorePoints
                WHERE
                    [TimeStamp] BETWEEN @StartDate AND @EndDate
                    AND (@SelectAll = 1
                        OR @IncludeTotalRemainingCapacity = 1
                        OR @IncludeSOC = 1
                        OR @IncludePower = 1
                        OR @IncludeVoltage = 1
                        OR @IncludeCurrent = 1
                    );
            
                -- Select final transposed result set with parameters as headers
                SELECT
                    DateCheck AS 'Date',
                    [Total Remaining Capacity(Ah)] AS 'Total Remaining Capacity(Ah)',
                    SOC AS 'SOC',
                    [Power (kW)] AS 'Power (kW)',
                    [Voltage (V)] AS 'Voltage (V)',
                    [Current (Amps)] AS 'Current (Amps)',
                    slaveId
                FROM (
                    SELECT DateCheck, Parameter, BatteryValue, slaveId
                    FROM #TempTable
                ) AS SourceTable
                PIVOT (
                    MAX(BatteryValue)
                    FOR Parameter IN (
                        [Total Remaining Capacity(Ah)],
                        SOC,
                        [Power (kW)],
                        [Voltage (V)],
                        [Current (Amps)]
                    )
                ) AS PivotTable;
            
                -- Clean up: drop the temporary table
                DROP TABLE #TempTable;
            END
            
            CREATE PROCEDURE [dbo].[usp_GetRdlcReportDataCanbus] 
            (  
            @StartDate DATETIME, @EndDate DATETIME,       
            @SelectAll BIT = 0,      @IncludeSOC BIT = 0,   
            @IncludeVoltage BIT = 0, @IncludeCurrent BIT = 0    
            )   
            AS   
            BEGIN       
            SET NOCOUNT ON;        
            SELECT            
            EntryDate as 'DateCheck',  VoltCanbus as 'Voltage',
            CurrentCanbus as 'Currents',  PowerCanbus as 'Power',  SOCCanbus as 'SOC',SOHCanbus as 'Temp'
            FROM tblCanbus       
            WHERE EntryDate BETWEEN @StartDate AND @EndDate   
            AND ( @SelectAll = 1   
            OR  (@IncludeSOC = 1 )   
            OR  (@IncludeVoltage = 1 )   
            OR  (@IncludeCurrent = 1 )   
              
            ) 
            order by EntryDate asc  
            END
            CREATE PROCEDURE [dbo].[usp_GetRdlcReportDataTest]  --'8/2/2023 1:27:16 PM','8/6/2025 1:27:16 PM',0,1,1,1,0,0
            (
                @StartDate DATETIME,
                @EndDate DATETIME,
                @SelectAll BIT,
                @IncludeSOC BIT,
                @IncludeVoltage BIT,
                @IncludeCurrent BIT,
                @IncludePower BIT,
                @IncludeTotalRemainingCapacity BIT
            )
            AS
            BEGIN
                SET NOCOUNT ON;
            
                SELECT 
                    Parameter, 
                    [TimeStamp] as 'DateCheck',
                    CASE WHEN Battery1 IS NOT NULL THEN Battery1 ELSE NULL END AS Battery1, 
                    CASE WHEN SlaveId1 IS NOT NULL THEN SlaveId1 ELSE NULL END AS SlaveId1,
                    CASE WHEN Battery2 IS NOT NULL THEN Battery2 ELSE NULL END AS Battery2, 
                    CASE WHEN SlaveId2 IS NOT NULL THEN SlaveId2 ELSE NULL END AS SlaveId2,
                    CASE WHEN Battery3 IS NOT NULL THEN Battery3 ELSE NULL END AS Battery3, 
                    CASE WHEN SlaveId3 IS NOT NULL THEN SlaveId3 ELSE NULL END AS SlaveId3,
            		  CASE WHEN Battery4 IS NOT NULL THEN Battery4 ELSE NULL END AS Battery4, 
                    CASE WHEN SlaveId4 IS NOT NULL THEN SlaveId4 ELSE NULL END AS SlaveId4,
                    -- Add similar CASE statements for other Battery and SlaveId columns up to Battery20 and SlaveId20
                    CASE WHEN Battery20 IS NOT NULL THEN Battery20 ELSE NULL END AS Battery20, 
                    CASE WHEN SlaveId20 IS NOT NULL THEN SlaveId20 ELSE NULL END AS SlaveId20
                FROM tblStorePoints
                WHERE [TimeStamp] BETWEEN @StartDate AND @EndDate
                AND (
                    @SelectAll = 1 OR
                    (@IncludeSOC = 1 AND Parameter = 'SOC') OR
                    (@IncludeVoltage = 1 AND Parameter = 'Voltage (V)') OR
                    (@IncludeCurrent = 1 AND Parameter = 'Current (Amps)') OR
                    (@IncludePower = 1 AND Parameter = 'Power (kW)') OR
                    (@IncludeTotalRemainingCapacity = 1 AND Parameter = 'Total Remaining Capacity(Ah)')
                )
                AND (
                    Battery1 IS NOT NULL OR Battery2 IS NOT NULL OR Battery3 IS NOT NULL OR 
                    -- Add other Battery columns up to Battery20
                    Battery20 IS NOT NULL
                )
            END
            CREATE PROCEDURE [dbo].[usp_InsertRecord]
            (
                @Parameter  VARCHAR(50),
                @Battery1   VARCHAR(50),
                @Battery2   VARCHAR(50),
                @Battery3   VARCHAR(50),
                @Battery4   VARCHAR(50),
                @Battery5   VARCHAR(50)
            )
            AS
            BEGIN
                -- Insert statement with parameterized queries
                INSERT INTO tblStorePoints (Parameter, Battery1, Battery2, Battery3, Battery4, Battery5)
                VALUES (@Parameter, @Battery1, @Battery2, @Battery3, @Battery4, @Battery5);
            
                -- Optionally, you can return a success message or error code if needed
                -- SELECT @@ROWCOUNT AS RowsInserted; -- Uncomment this line if you want to return the number of rows affected
            END
            CREATE PROCEDURE [dbo].[usp_MainParametersCharts]    
                @StartDate DATETIME,      
                @EndDate DATETIME,  
                @IncludeVoltage BIT = 0,    
                @IncludeCurrents BIT = 0,  
                @IncludePower BIT = 0,  
                @IncludeSOC BIT = 0,  
                @IncludeTemp BIT = 0,  
                @SelectAll BIT = 0  -- Parameter for selecting all columns  
            AS      
            BEGIN      
                SET NOCOUNT ON;      
                IF @SelectAll = 0 AND @IncludeVoltage = 0 AND @IncludeCurrents = 0 AND @IncludePower = 0 AND @IncludeSOC = 0 AND @IncludeTemp = 0  
                BEGIN  
                    -- If no columns are selected, do nothing (or return an empty result set)  
                    RETURN;  
                END  
                -- Start building the dynamic SQL query  
                DECLARE @sql NVARCHAR(MAX)  
                SET @sql = 'SELECT mp.EntryDateTime as DateCheck'  
            
                -- If SelectAll is set to 1, include all columns regardless of other parameters  
                IF @SelectAll = 1  
                BEGIN  
                    SET @sql += ', mp.Voltage, mp.Currents, mp.Power, mp.SOC, mp.Temp'  
                END  
                ELSE  
                BEGIN  
                    -- Conditionally append columns to the SELECT query based on the input parameters  
                    IF @IncludeVoltage = 1  
                        SET @sql += ', mp.Voltage'  
                    IF @IncludeCurrents = 1  
                        SET @sql += ', mp.Currents'  
                    IF @IncludePower = 1  
                        SET @sql += ', mp.Power'  
                    IF @IncludeSOC = 1  
                        SET @sql += ', mp.SOC'  
                    IF @IncludeTemp = 1  
                        SET @sql += ', mp.Temp'  
                END  
                -- Complete the FROM and WHERE clauses  
                SET @sql += ' FROM tblMainParameters mp WHERE mp.EntryDateTime BETWEEN @StartDate AND @EndDate ORDER BY mp.EntryDateTime ASC'  
              
                -- Execute the dynamic SQL query  
            	  PRINT @sql
                EXEC sp_executesql @sql, N'@StartDate DATETIME, @EndDate DATETIME', @StartDate, @EndDate  
            END  
            
            CREATE PROCEDURE [dbo].[usp_SelectStorePoints] --'8/2/2024 1:27:16 PM','8/6/2024 1:27:16 PM'
                @StartDate DATETIME,  
                @EndDate DATETIME   
            AS  
            BEGIN  
                SET NOCOUNT ON;  
                    SELECT 
            		--*  Id	
            		Parameter,	Battery1,Battery2,Battery3,Battery4,Battery5, [TimeStamp ] as 'DateCheck', SlaveId
            		--,	Battery2,	Battery3,	Battery4,	Battery5,	TimeStamp 
                    FROM tblStorePoints  
                    WHERE Parameter = 'Voltage (V)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            
            		SELECT Parameter,	Battery1,Battery2,Battery3,Battery4,Battery5, [TimeStamp ] as 'DateCheck', SlaveId
                    FROM tblStorePoints  
                    WHERE Parameter = 'Current (Amps)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            		
            		SELECT Parameter,	Battery1,Battery2,Battery3,Battery4,Battery5, [TimeStamp ] as 'DateCheck', SlaveId
                    FROM tblStorePoints  
                    WHERE Parameter = 'Power (kW)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            
            		SELECT Parameter,	Battery1,Battery2,Battery3,Battery4,Battery5, [TimeStamp ] as 'DateCheck', SlaveId
                    FROM tblStorePoints  
                    WHERE Parameter = 'SOC'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            
            		SELECT Parameter,	Battery1,Battery2,Battery3,Battery4,Battery5, [TimeStamp ] as 'DateCheck', SlaveId
                    FROM tblStorePoints  
                    WHERE Parameter = 'Total Remaining Capacity(Ah)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            
            		SELECT Parameter,	Battery1,Battery2,Battery3,Battery4,Battery5, [TimeStamp ] as 'DateCheck', SlaveId
                    FROM tblStorePoints  
                    WHERE Parameter = 'Temperature (C)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            END
            --     EXEC  usp_SelectStorePoints1   '8/2/2024 1:27:16 PM','8/16/2024 1:27:16 PM'
            CREATE PROCEDURE [dbo].[usp_SelectStorePoints1] --'8/2/2024 1:27:16 PM','8/6/2024 1:27:16 PM'
                @StartDate DATETIME,  
                @EndDate DATETIME   
            AS  
            BEGIN  
                SET NOCOUNT ON;  
                    SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,Battery2,Battery3,Battery4,Battery5, 
            		Battery6,Battery7,Battery8,Battery9,Battery10,
            		Battery11,Battery12,Battery13,Battery14,Battery15, 
            		Battery16,Battery17,Battery18,Battery19,Battery20
                    FROM tblStorePoints  
                    WHERE Parameter = 'Voltage (V)' And
            		[TimeStamp]  BETWEEN @StartDate AND @EndDate
            		Order by Parameter asc
            
            		 SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,Battery2,Battery3,Battery4,Battery5, 
            		Battery6,Battery7,Battery8,Battery9,Battery10,
            		Battery11,Battery12,Battery13,Battery14,Battery15, 
            		Battery16,Battery17,Battery18,Battery19,Battery20
                    FROM tblStorePoints   
                     WHERE Parameter = 'Current (Amps)'
            		 And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            		
            
                   SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,Battery2,Battery3,Battery4,Battery5, 
            		Battery6,Battery7,Battery8,Battery9,Battery10,
            		Battery11,Battery12,Battery13,Battery14,Battery15, 
            		Battery16,Battery17,Battery18,Battery19,Battery20
                    FROM tblStorePoints  
                  WHERE Parameter = 'Power (kW)'
                  And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            		
            
            		 SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,Battery2,Battery3,Battery4,Battery5, 
            		Battery6,Battery7,Battery8,Battery9,Battery10,
            		Battery11,Battery12,Battery13,Battery14,Battery15, 
            		Battery16,Battery17,Battery18,Battery19,Battery20
                    FROM tblStorePoints  
            		WHERE Parameter = 'SOC'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            		
            
            		 SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,Battery2,Battery3,Battery4,Battery5, 
            		Battery6,Battery7,Battery8,Battery9,Battery10,
            		Battery11,Battery12,Battery13,Battery14,Battery15, 
            		Battery16,Battery17,Battery18,Battery19,Battery20
                    FROM tblStorePoints  
            		WHERE Parameter = 'Total Remaining Capacity(Ah)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            		
            
            		SELECT 
            		Parameter, [TimeStamp] as 'DateCheck',
            		Battery1,Battery2,Battery3,Battery4,Battery5, 
            		Battery6,Battery7,Battery8,Battery9,Battery10,
            		Battery11,Battery12,Battery13,Battery14,Battery15, 
            		Battery16,Battery17,Battery18,Battery19,Battery20
                    FROM tblStorePoints    
            		WHERE Parameter = 'Temperature (C)'
            		And [TimeStamp]  BETWEEN @StartDate AND @EndDate
            
            END

            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(new BusinessLogic.DbConnection().connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(createDatabaseScript, connection);
                    command.ExecuteNonQuery(); // Executes the script to create the database and objects
                }

                MessageBox.Show("Database created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating database: {ex.Message}");
            }
        }
        #endregion
        private static void CreateDatabaseIfNotExists()
        {
            string serverName = "(localdb)\\MSSQLLocalDB";  // LocalDB server
            string databaseName = "ENCAPdb";  // Name of your database
            string mdfFilePath = @"C:\DbFile\ENCAPdb.mdf";  // Path to where the MDF file will be created
            string logFilePath = @"C:\DbFile\ENCAPdb.ldf";  // Path for the LDF log file

            string connectionString = $"Server={serverName};Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if database exists
                    string checkDbExistQuery = $@"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}')
                    BEGIN
                        CREATE DATABASE {databaseName}
                        ON PRIMARY (
                            NAME = '{databaseName}',
                            FILENAME = '{mdfFilePath}'
                        )
                        LOG ON (
                            NAME = '{databaseName}_log',
                            FILENAME = '{logFilePath}'
                        );
                    END";

                    SqlCommand command = new SqlCommand(checkDbExistQuery, connection);
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Database '{databaseName}' is ready or has been created.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
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
