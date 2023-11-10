using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace MikrospecUtiliies
{
    class Program
    {
        static void Main(string[] args)
        {
            //creating TNS entries
            string oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 2524))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldbt2.fcsm.hu)));" + "User Id=kassza_teszt;Password=kassza_teszt;";
             {
                OracleConnection conn = new OracleConnection(oradb);
                conn.Open();
                Console.Write("Connected to Oracle" + conn.ServerVersion);


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select 'egy' as egy from dual";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Console.WriteLine(dr.GetString(0));

                // Close and Dispose OracleConnection object
                conn.Close();
                conn.Dispose();
                Console.Write("Disconnected");
                Console.ReadLine();
            }
        }
    }
}
