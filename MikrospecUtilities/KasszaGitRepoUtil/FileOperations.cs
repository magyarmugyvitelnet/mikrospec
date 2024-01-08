using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasszaGitRepoUtil
{
    public class FileOperations
    {
        public static void WriteToFile(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.GetEncoding(1250));

            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
            }
            sw.Write(sw.NewLine);
            sw.Write(".");
            sw.Write(sw.NewLine);
            sw.Write("/");
            sw.Write(sw.NewLine);
            sw.Close();
        }
    }
}
