using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using static System.Net.WebRequestMethods;

namespace KasszaGitRepoUtil
{
    internal class Program
    {
        private static DataTable dataTableObjects { get; set; }
        private static DataTable dataTableLines { get; set; }
        private static string actualQueryObjects = "";
        private static string actualQueryLines = "";
        private static int minimumNumberOfParameters = 0;
        private static string task;
        private static string environment;
        private static OracleConnection oracleConnection;
        private static string konyvtar;
        private static string fajlnev;
        private static string kiterjesztes;

        static void Main(string[] args)
        {
            List<string> objectTypes = ["PACKAGE", "PACKAGE BODY", "PROCEDURE", "FUNCTION", "TRIGGER"];

            Console.WriteLine("Main start");

            if (args.Length != 0)
            {
                task = args[0];

                if (task == "CONNECT")
                {
                    minimumNumberOfParameters = 2;
                }

                if (args.Length < minimumNumberOfParameters)
                {
                    displayHelp(args);
                }

                if (task == "CONNECT")
                {
                    environment = args[1];
                    oracleConnection = KasszaGitRepoUtil.Database.ConnectOpen(environment);
                }

                //                actualQuery = @"select distinct(owner) Owner from all_tables order by owner";
                //                dataTable = KasszaGitRepoUtil.Database.RunQuery(actualQuery, oracleConnection);
                foreach (string objectType in objectTypes)
                {
                    Console.WriteLine(objectType);
                    konyvtar = "db/";
                    switch (objectType)
                    {
                        case "PACKAGE":
                            kiterjesztes = "pck";
                            break;
                        case "PACKAGE BODY":
                            kiterjesztes = "pcb";
                            break;
                        case "PROCEDURE":
                            kiterjesztes = "prc";
                            break;
                        case "FUNCTION":
                            kiterjesztes = "fnc";
                            break;
                        case "TRIGGER":
                            kiterjesztes = "trg";
                            break;
                        default:
                            // code block
                            break;
                    }
                    actualQueryObjects = @"select OBJECT_NAME from all_objects where object_type = '" + objectType + "' and owner = '" + environment + "' order by object_name";
                    Console.WriteLine("actualQueryObjects: <" + actualQueryObjects + ">");
                    dataTableObjects = KasszaGitRepoUtil.Database.RunQuery(actualQueryObjects, oracleConnection);
                    Console.WriteLine(dataTableObjects.Rows.Count);
                    foreach (DataRow dataTableObject in dataTableObjects.Rows)
                    {
                        Console.WriteLine(dataTableObject["OBJECT_NAME"].ToString());
                        // if (dataTableObject["OBJECT_NAME"].ToString().Equals("AEAKERT"))
                        // {
                        actualQueryLines = @"select TEXT from all_source where type = '" + objectType + "' and owner = '" + environment + "' and name = '" + dataTableObject["OBJECT_NAME"].ToString() + "' order by line";
                        dataTableLines = KasszaGitRepoUtil.Database.RunQuery(actualQueryLines, oracleConnection);
                        fajlnev = dataTableObject["OBJECT_NAME"].ToString().ToLower();
                        FileOperations.WriteToFile(dataTableLines, konyvtar + fajlnev + "." + kiterjesztes);
                        // }
                    }
                    FileOperations.WriteToFile(dataTableObjects, "Fájlnév_" + objectType);
                }

                KasszaGitRepoUtil.Database.ConnectClose(oracleConnection);

            }
            else
            {
                displayHelp(args);
            }
            Console.WriteLine("Main end");
            Console.ReadLine();

        }
        private static void displayHelp(string[] args)
        {
            Console.WriteLine("KasszaGitRepoUtil paraméterezése:");
            Console.WriteLine("  1. task (CONNECT");
            Console.WriteLine("Ha CONNECT:");
            Console.WriteLine("  2. environment");
            Console.WriteLine("  ------------------------------");
            int i = 0;
            foreach (string arg in args)
            {
                i++;
                Console.WriteLine($"  {i}. : {arg}");
            }
            Console.WriteLine("  ------------------------------");
            Console.ReadLine();
            System.Environment.Exit(0);
        }
    }
}
