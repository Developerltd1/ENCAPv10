using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public  class DbConnection
    {
        //From Debug Bin Folder
        // public static string connectionString= @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = " + System.IO.Path.GetFullPath("ENCAPdb.mdf") + "; Integrated Security = True";  //System.Configuration.ConfigurationManager.ConnectionStrings["DbConnectionString"].ToString();

        //AqibMachine
        // public static string connectionString = @"Data Source =.; Initial Catalog = D:\COMPANY\APPSPARQTECH\0.ALL_ASSETS\SUPPORTEDPROJECTS\SERIES\MODIFICATIONSERIES\EMView - CHART LINE ISSUE\EMView\ENCAPDB.MDF; Integrated Security = True";
        //public  string connectionString = ConfigurationManager.ConnectionStrings["ENCAPdb"].ConnectionString;

        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ENCAPdb"].ToString();
    }
}
