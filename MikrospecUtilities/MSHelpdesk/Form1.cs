using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MSHelpdesk
{
    public partial class Form1 : Form
    {
        OracleConnection conn;

        private FcsmServer _fcsmserver;
        public FcsmServer FcsmServer
        {
            get { return _fcsmserver; }
            set
            {
                _fcsmserver = value;
                lblSrv.Text = value.ToString();
                Connect();
            }
        }

        public string pFH { get; set; }
        public string pAZONKH { get; set; }
        public int pSORSZAMTOL { get; set; }
        public int pSORSZAMIG { get; set; }
        public int pTETELSZAM { get; set; }
        public string pEP { get; set; }

        public int pEV { get; set; }
        public int pHONAP { get; set; }
        public DataTable dataTable { get; set; }

        public Form1()
        {
            InitializeComponent();

        }

        void Connect()
        {
            string oradb;
            //teszt
            if (FcsmServer == FcsmServer.Teszt)
                oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 2524))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldbt2.fcsm.hu)));" + "User Id=totha2;Password=totha284;";
            else if (FcsmServer == FcsmServer.Eles)
                oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1500))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldb.fcsm.hu)));" + "User Id=totha2;Password=totha284;";
            else
                throw new NotImplementedException();
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        DataTable runQuery(string cmdstring)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdstring;
            cmd.CommandType = CommandType.Text;
            DataTable myTable = new DataTable();
            myTable.Load(cmd.ExecuteReader());
            return myTable;
        }

        int runCommand(string cmdstring, OracleTransaction tran = null)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdstring;
            cmd.CommandType = CommandType.Text;
            if (tran != null)
                cmd.Transaction = tran;
            return cmd.ExecuteNonQuery();
        }

        public static void WriteToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.GetEncoding(1250));

            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


        void Task602Dupla(DateTime dateTime)
        {
            //            MessageBox.Show("Task602Dupla lekérdezés");
            Connect();
            /*pEV = 2023;
             * pHONAP = 2;*/
            string ScriptToSave = "";
            string ActualScript = "";
            pEV = dateTime.Year;
            pHONAP = dateTime.Month;

            //            ActualScript = @"with gysz_count as (select count(1),gysz,ev,honap from mm_kl_adatok where ev = "+pEV+" and honap = "+pHONAP+" group by gysz,ev,honap having count(1) != 1) select mm_kl_adatok.* from mm_kl_adatok join gysz_count on mm_kl_adatok.gysz=gysz_count.gysz and mm_kl_adatok.ev=gysz_count.ev order by mm_kl_adatok.gysz";
            ActualScript = @"with gysz_count as (select count(1),gysz,ev,honap from mm_kl_adatok where ev = " + pEV.ToString() + " and honap = " + pHONAP.ToString() + " group by gysz,ev,honap having count(1) != 1) select mm_kl_adatok.* from mm_kl_adatok, gysz_count where mm_kl_adatok.gysz=gysz_count.gysz and mm_kl_adatok.ev=gysz_count.ev and mm_kl_adatok.honap=gysz_count.honap order by gysz_count.gysz,gysz_count.ev,gysz_count.honap";
            ScriptToSave += ActualScript + "\n\n";
            dataTable = runQuery(ActualScript);

            //            int darab = 0;

            int darab = dataTable.Rows.Count;

            /*            foreach (DataRow item in dataTable.Rows)
                        {
                            darab++;
                        }*/

            MessageBox.Show($"Legyűjtött rekordok száma: {darab.ToString()} darab", "Eredmény", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (darab != 0)
            {
                btnToCsv.Text = $"{btn602Dupla.Text} ({darab.ToString()}) darab rekord CSV fájlba írása";
                btnToCsv.Visible = true;
                dataGridView2.DataSource = dataTable;
            };
        }

        void Task602Kihagyott(DateTime dateTime)
        {
            //MessageBox.Show("Task602Kihagyott lekérdezés");
            Connect();
            /*pEV = 2023;
             * pHONAP = 2;*/
            string ScriptToSave = "";
            string ActualScript = "";
            pEV = dateTime.Year;
            pHONAP = dateTime.Month;

            ActualScript = @"select mm_kl_adatok.* from mm_kl_adatok where mm_kl_adatok.ev = " + pEV.ToString() + " and mm_kl_adatok.honap = " + pHONAP.ToString() + " and not exists (select 1 from V_KL_TO_603 where V_KL_TO_603.TIP = '602' and V_KL_TO_603.ev = " + pEV.ToString() + " and V_KL_TO_603.honap = " + pHONAP.ToString() + " and V_KL_TO_603.gysz = mm_kl_adatok.gysz) and (mm_kl_adatok.matol - mm_kl_adatok.matol2)!=0 order by mm_kl_adatok.gysz, mm_kl_adatok.ev, mm_kl_adatok.honap";
            ScriptToSave += ActualScript + "\n\n";
            dataTable = runQuery(ActualScript);

            //            int darab = 0;

            int darab = dataTable.Rows.Count;

            /*            foreach (DataRow item in dataTable.Rows)
                        {
                            darab++;
                        }*/

            MessageBox.Show($"Legyűjtött rekordok száma: {darab.ToString()} darab", "Eredmény", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (darab != 0)
            {
                btnToCsv.Text = $"{btn602Kihagyott.Text} ({darab.ToString()}) darab rekord CSV fájlba írása";
                btnToCsv.Visible = true;
                dataGridView2.DataSource = dataTable;
            };

        }


        void Task606()
        {
            Connect();
            string ScriptToSave = "";
            string ActualScript = "";
            /*pEP = "10303";
            pFH = "213430";
            pAZONKH = "2363069";
            pSORSZAMTOL = 1508978353;
            pSORSZAMIG = 1593695900;
            pTETELSZAM = 12;*/


            DataTable seged = runQuery("Select azonkh From mmytv01k where id#mmks In (Select ks.id From mmfh fh, mmks ks Where fh.id = ks.id#mmfh And fh.fh='" + pFH + "')");
            bool AzonKHOK = false;
            foreach (DataRow item in seged.Rows)
            {
                if (item["azonkh"].ToString() == pAZONKH)
                {
                    AzonKHOK = true;
                    break;
                }
            }
            if (!AzonKHOK)
            {
                logBox.Text += "A megadott FH-AzonKH párosítás nem megfelelő!\n";
                return;
            }
            ActualScript = @"select distinct nvl(f.fcsmbiz1, p.hivszam), nvl(p.jell_kod, s.tip), s.mmfsz_ikt, p.erv, nvl(d.atadjel, 'NINCS ÁTADVA') as atadjel, d6.*
                       from mmymd06t d6, mmszt t, mmszf s, mmyed10 d, mmyed10f f, pu_biz p, mmyid06 i6
                       where d6.mmyid06_id = i6.id and t.sidmm = d6.id and t.id#mmszf=s.id and s.id#pu_biz=p.id  and d.id#mmszf(+)=s.id
                       and f.id#mmyed10(+)=d.id    and s.id#pu_biz=p.id(+) -- and s.id#mmszf is null
                       and f.fcsmbiz1 is not null
                       and d6.azonfm = '" + pAZONKH + "'  and d6.status = 'F' and d6.sorszam between " + pSORSZAMTOL + " and " + pSORSZAMIG;
            ScriptToSave += ActualScript + "\n\n";
            seged = runQuery(ActualScript);

            bool AtadjelOk = true;
            foreach (DataRow item in seged.Rows)
            {
                if (item["atadjel"].ToString() != "I")
                {
                    AtadjelOk = false;
                    break;
                }
            }

            if (!AtadjelOk)
            {
                logBox.Text += "Van olyan sor, ahol az atadjel nem 'I'\n";
                return;
            }
            ActualScript = @" select row_number() over (order by 1) Sorrend, mmymd06t.* 
                                from mmymd06t where jelleg='F' and                  
                                azonfm in('" + pAZONKH + "')  and sorszam between " + pSORSZAMTOL + " and " + pSORSZAMIG + " and status in ('F', 'B')  order by sorszam desc ";
            seged = runQuery(ActualScript);
            ScriptToSave += ActualScript + "\n\n";
            dataGridView1.DataSource = seged;
            string filenev = @"C:\Temp\" + pEP + DateTime.Now.ToString("yyyyMMddHHmmss");

            filenev = filenev + ".csv";

            WriteToCSV(seged, filenev);
            logBox.Text += "Az állomány mentve a következő útvonalra: " + filenev + "\n";

            if (seged.Rows.Count != pTETELSZAM)
            {
                logBox.Text += "A megadott tételszám nem egyezik!\n";
                return;
            }

            OracleTransaction tran = null;
            try
            {

                tran = conn.BeginTransaction();
                ActualScript = @"update mmymd06t set status='B',szamlazando=null,id#mmfsz=null,id#mmis=null,id#mmks=null where jelleg='F' and
                                azonfm in('" + pAZONKH + "')  and sorszam  between " + pSORSZAMTOL + " and " + pSORSZAMIG;
                ScriptToSave += ActualScript + "\n\n";
                runCommand(ActualScript);
                ActualScript = @"select * from mmyid06 where azonfm='" + pAZONKH + "' and sorszam         between " + pSORSZAMTOL + " and " + pSORSZAMIG;
                seged = runQuery(ActualScript);
                if (seged.Rows.Count != pTETELSZAM)
                {
                    logBox.Text += "A megadott tételszám nem egyezik, a művelet visszavonva!\n";
                    tran.Rollback();
                    tran = null;
                    return;
                }
                else
                {
                    tran.Commit();
                    tran = null;
                }
            }
            catch
            {
                if (tran != null)
                    tran.Rollback();
            }

            finally
            {
                if (tran != null)
                    tran.Rollback();
            }

            ActualScript = @"update mmyid06 set status='B' where azonfm in('" + pAZONKH + "')  and sorszam between " + pSORSZAMTOL + " and " + pSORSZAMIG;

            ScriptToSave += ActualScript + "\n\n";
            runCommand(ActualScript);

            logBox.Text += "Scriptek mentve a következő útvonalra: " + @"C:\temp\" + pEP + ".sql\n";


            File.WriteAllText(@"C:\temp\" + pEP + ".sql", ScriptToSave);
        }

        private void btnEles_Click(object sender, EventArgs e)
        {
            FcsmServer = FcsmServer.Eles;
        }

        private void btnTeszt_Click(object sender, EventArgs e)
        {
            FcsmServer = FcsmServer.Teszt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                pEP = edtPEP.Text;
                pFH = edtFH.Text;
                pAZONKH = edtAZONKH.Text;
                pSORSZAMTOL = int.Parse(edtSORSZAMTOL.Text);
                pSORSZAMIG = int.Parse(edtSORSZAMIG.Text);
                pTETELSZAM = int.Parse(edtTETELSZAM.Text);
                Task606();
                DialogResult dialogResult = MessageBox.Show("Input mezők törlése?", "Kérdés", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    edtPEP.Text = "";
                    edtFH.Text = "";
                    edtAZONKH.Text = "";
                    edtSORSZAMIG.Text = "";
                    edtSORSZAMTOL.Text = "";
                    edtTETELSZAM.Text = "";
                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

        }

        private void dataGridView1_BindingContextChanged(object sender, EventArgs e)
        {

        }
        private void FormUrites()
        {
            lblLog.Visible = false;
            lblLog.Text = "";
            btnToCsv.Visible = false;
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
        }

        private void btn602Dupla_Click(object sender, EventArgs e)
        {
            //            lblLog.Text = string.Empty;
            FormUrites();
            DateTime dateTime = dateTimePicker1.Value;

            if (lblSrv.Text == "Kapcsolódj szerverhez!")
            {
                MessageBox.Show("Kapcsolódj szerverhez!", "Figyelem!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"A kiválasztott év: {dateTime.Year} hónap: {dateTime.Month}. Folytatod?", "602 Dupla tételek lekérdezése", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Task602Dupla(dateTime);
                }

            }


        }


        private void btn602Kihagyott_Click(object sender, EventArgs e)
        {
            //            lblLog.Text = string.Empty;
            FormUrites();
            DateTime dateTime = dateTimePicker1.Value;

            if (lblSrv.Text == "Kapcsolódj szerverhez!")
            {
                MessageBox.Show("Kapcsolódj szerverhez!");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"A kiválasztott év: {dateTime.Year} hónap: {dateTime.Month}. Folytatod?", "602 Kihagyott tételek lekérdezése", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Task602Kihagyott(dateTime);
                }
            }
        }

        private void btnToCsv_Click(object sender, EventArgs e)
        {
            string kiegNev = "";
            //            MessageBox.Show(btnToCsv.Text.Substring(4, 1));
            if (btnToCsv.Text.Substring(4, 1) == "d")
            {
                kiegNev = "-dupla";
            }
            else if ((btnToCsv.Text.Substring(4, 1) == "k"))
            {
                kiegNev = "-kihagyott";
            }

            string filehonap = pHONAP.ToString();

            if (pHONAP < 10)
            {
                filehonap = "0" + filehonap;
            }
            string filenev = @"C:\Temp\" + pEV.ToString() + filehonap + kiegNev + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            filenev = filenev + ".csv";

            WriteToCSV(dataTable, filenev);

            lblLog.Text = "Az állomány mentve a következő útvonalra: " + filenev;
            lblLog.Visible = true;
        }

    }
}
