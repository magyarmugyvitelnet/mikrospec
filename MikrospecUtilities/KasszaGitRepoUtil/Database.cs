using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasszaGitRepoUtil
{
    public class Database
    {
        private static String oradb;
        public static OracleConnection ConnectOpen(string environment)
        {
            //creating TNS entries
            //            string oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 2524))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldbt2.fcsm.hu)));" + "User Id=kassza_teszt;Password=kassza_teszt;";

            {
                if (environment == "KASSZA_TESZT")
                {
                    oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 2524))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldbt2.fcsm.hu)));" + "User Id=kassza_teszt;Password=kassza_teszt;";
                }
                OracleConnection conn = new OracleConnection(oradb);
                conn.Open();
                Console.Write("Connected to Oracle " + conn.ServerVersion + "\n");


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select kod,nev from ugyfel where id=0";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Console.WriteLine("Kód: " + dr.GetString(0) + " Név: " + dr.GetString(1));

                return (conn);
            }
        }

        public static void ConnectClose(OracleConnection conn)
        {
            // Close and Dispose OracleConnection object
            conn.Close();
            conn.Dispose();
            Console.WriteLine("Disconnected");
        }

        public static DataTable RunQuery(string queryString, OracleConnection conn)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = queryString;
            cmd.CommandType = CommandType.Text;
            DataTable myTable = new DataTable();
            myTable.Load(cmd.ExecuteReader());
            return myTable;
        }

    }
}
