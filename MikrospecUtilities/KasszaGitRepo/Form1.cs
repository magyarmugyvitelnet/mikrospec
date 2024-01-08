using KasszaGitRepoUtil;
using MSHelpdesk;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KasszaGitRepo
{
    public partial class FrmKasszaGitRepo : Form
    {
        OracleConnection conn;
        public enum PeldanyEnum
        {
            KASSZA_TESZT,
            KASSZA
        }

        string peldany;

        public DataTable dataTable { get; set; }

        string actualQuery = "";

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

        public FrmKasszaGitRepo()
        {
            InitializeComponent();
        }

        void Connect()
        {
            string oradb;
            //teszt
            if (FcsmServer == FcsmServer.Teszt)
                oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 2524))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldbt2.fcsm.hu)));" + "User Id=kassza_teszt;Password=kassza_teszt;";
            else if (FcsmServer == FcsmServer.Eles)
                oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1500))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = szoldb.fcsm.hu)));" + "User Id=totha2;Password=totha284;";
            else
                throw new NotImplementedException();
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        DataTable RunQuery(string queryString)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = queryString;
            cmd.CommandType = CommandType.Text;
            DataTable myTable = new DataTable();
            myTable.Load(cmd.ExecuteReader());
            return myTable;
        }

        void QueryOwners(string xtables)
        {
            actualQuery = @"select distinct(owner) Owner from " + xtables + " order by owner";
            //            actualQuery = @"select distinct(owner) Owner from dba_tables order by owner";
            dataTable = RunQuery(actualQuery);
            cmbTulajdonos.DisplayMember = "Owner";
            cmbTulajdonos.DataSource = dataTable;
            int index = cmbTulajdonos.FindString(peldany);
            cmbTulajdonos.SelectedIndex = index;
        }

        void QueryOwnerTables(string owner)
        {
            //            actualQuery = @"select * from all_tables where owner = '" + owner + "'";
            //            actualQuery = @"select OWNER, TABLE_NAME, STATUS, LAST_ANALYZED from dba_tables where owner = '" + owner + "'";
            actualQuery = @"select 
            OWNER, 
            TABLE_NAME, 
            STATUS, 
            LAST_ANALYZED from all_tables where owner = '" + owner + "' order by table_name";
            dataTable = RunQuery(actualQuery);
            dgvTables.DataSource = null;
            dgvTables.Rows.Clear();
            dgvTables.DataSource = dataTable;
        }

        void QueryTableColumns(string owner, string tableName)
        {
            //            actualQuery = @"select * from all_tab_columns where table_name = '" + tableName + "' order by column_name";
            //            actualQuery = @"select OWNER, TABLE_NAME, COLUMN_NAME, DATA_TYPE, DATA_LENGTH, DATA_PRECISION, DATA_SCALE, NULLABLE, COLUMN_ID, LAST_ANALYZED from dba_tab_columns where table_name = '" + tableName + "' order by column_name";
            actualQuery = @"select 
            OWNER, 
            TABLE_NAME, 
            COLUMN_NAME, 
            DATA_TYPE, 
            DATA_LENGTH, 
            DATA_PRECISION, 
            DATA_SCALE, 
            NULLABLE, 
            COLUMN_ID, 
            LAST_ANALYZED from all_tab_columns where owner = '" + owner + "' and table_name = '" + tableName + "' order by column_name";
            dataTable = RunQuery(actualQuery);
            dgvTableColumns.DataSource = null;
            dgvTableColumns.Rows.Clear();
            dgvTableColumns.DataSource = dataTable;
        }

        void QueryOwnerConstraints(string owner)
        {
            actualQuery = @"select 
            OWNER, 
            TABLE_NAME, 
            CONSTRAINT_NAME,
            CONSTRAINT_TYPE,
            R_OWNER,
            R_CONSTRAINT_NAME, 
            INDEX_OWNER,
            INDEX_NAME,
            STATUS, 
            GENERATED,
            LAST_CHANGE from all_constraints where owner = '" + owner + "' order by table_name, constraint_name";
            dataTable = RunQuery(actualQuery);
            dgvConstraints.DataSource = null;
            dgvConstraints.Rows.Clear();
            dgvConstraints.DataSource = dataTable;
        }

        void QueryOwnerTriggers(string owner)
        {
            actualQuery = @"select 
            OWNER, 
            TABLE_NAME, 
            TRIGGER_NAME,
            TRIGGER_TYPE,
            TRIGGERING_EVENT,
            DESCRIPTION, 
            STATUS from all_triggers where owner = '" + owner + "' order by table_name, trigger_name";
            dataTable = RunQuery(actualQuery);
            dgvTriggers.DataSource = null;
            dgvTriggers.Rows.Clear();
            dgvTriggers.DataSource = dataTable;
        }

        void QueryTriggerLines(string owner, string triggerName)
        {
            actualQuery = @"select 
            OWNER, 
            NAME, 
            TYPE, 
            LINE, 
            TEXT 
            from all_source where type = 'TRIGGER' and owner = '" + owner + "' and name = '" + triggerName + "' order by line";
            dataTable = RunQuery(actualQuery);
            dgvTriggerLines.DataSource = null;
            dgvTriggerLines.Rows.Clear();
            dgvTriggerLines.DataSource = dataTable;
        }


        void QueryOwnerIndexes(string owner)
        {
            actualQuery = @"select 
            OWNER, 
            INDEX_NAME,
            TABLE_NAME,
            UNIQUENESS,
            STATUS, 
            LAST_ANALYZED from all_indexes where owner = '" + owner + "' order by table_name, index_name";
            dataTable = RunQuery(actualQuery);
            dgvIndexes.DataSource = null;
            dgvIndexes.Rows.Clear();
            dgvIndexes.DataSource = dataTable;
        }
        void QueryIndexColumns(string owner, string indexName)
        {
            //            actualQuery = @"select * from all_ind_columns where index_name = '" + indexName + "' order by column_name";
            actualQuery = @"select 
            INDEX_OWNER, 
            INDEX_NAME, 
            TABLE_NAME, 
            COLUMN_POSITION, 
            COLUMN_NAME, 
            COLUMN_LENGTH, 
            CHAR_LENGTH, 
            DESCEND from all_ind_columns where index_owner = '" + owner + "' and index_name = '" + indexName + "' order by column_position";
            dataTable = RunQuery(actualQuery);
            dgvIndexColumns.DataSource = null;
            dgvIndexColumns.Rows.Clear();
            dgvIndexColumns.DataSource = dataTable;
        }
        void QueryOwnerViews(string owner)
        {

            /*           actualQuery = @"select owner, view_name, text_length, text
                       from xmltable(
                       '/ROWSET/ROW'
                       passing(select dbms_xmlgen.getxmltype('select owner,view_name,text_length,text from all_views
                                                              where owner = ''KASSZA_TESZT''')
                       from dual)
                       columns owner varchar2(30) path 'OWNER',
                             view_name varchar2(30) path 'VIEW_NAME',
                             text_length number path 'TEXT_LENGTH',
                             text varchar2(4000) path 'TEXT')";*/
            actualQuery = @"select owner, view_name, text_length from all_views where owner = '" + owner + "' order by view_name";
            //            actualQuery = @"select owner, view_name, text_length from all_views where view_name = 'PU_BIZ_UKES' order by view_name";
            dataTable = RunQuery(actualQuery);
            dgvViews.DataSource = null;
            dgvViews.Rows.Clear();
            dgvViews.DataSource = dataTable;
        }

        void QueryOwnerPackages(string owner)
        {
            //actualQuery = @"select * from all_objects where object_type = 'PACKAGE' and owner = '" + owner + "' order by object_name";
            actualQuery = @"select 
            OWNER, 
            OBJECT_NAME,
            OBJECT_TYPE,
            STATUS,
            CREATED,
            LAST_DDL_TIME, 
            TIMESTAMP
            from all_objects where object_type = 'PACKAGE' and owner = '" + owner + "' order by object_name";
            dataTable = RunQuery(actualQuery);
            dgvPackages.DataSource = null;
            dgvPackages.Rows.Clear();
            dgvPackages.DataSource = dataTable;
        }
        DataTable QueryPackageLines(string owner, string packageName)
        {
            actualQuery = @"select 
            OWNER, 
            NAME, 
            TYPE, 
            LINE, 
            TEXT 
            from all_source where type = 'PACKAGE' and owner = '" + owner + "' and name = '" + packageName + "' order by line";
            dataTable = RunQuery(actualQuery);
            dgvPackageLines.DataSource = null;
            dgvPackageLines.Rows.Clear();
            dgvPackageLines.DataSource = dataTable;
            return dataTable;
        }

        void QueryOwnerPackageBodies(string owner)
        {
            actualQuery = @"select 
            OWNER, 
            OBJECT_NAME,
            OBJECT_TYPE,
            STATUS,
            CREATED,
            LAST_DDL_TIME, 
            TIMESTAMP
            from all_objects where object_type = 'PACKAGE BODY' and owner = '" + owner + "' order by object_name";
            dataTable = RunQuery(actualQuery);
            dgvPackageBodies.DataSource = null;
            dgvPackageBodies.Rows.Clear();
            dgvPackageBodies.DataSource = dataTable;
        }

        void QueryPackageBodyLines(string owner, string packageBodyName)
        {
            actualQuery = @"select 
            OWNER, 
            NAME, 
            TYPE, 
            LINE, 
            TEXT 
            from all_source where type = 'PACKAGE BODY' and owner = '" + owner + "' and name = '" + packageBodyName + "' order by line";
            dataTable = RunQuery(actualQuery);
            dgvPackageBodyLines.DataSource = null;
            dgvPackageBodyLines.Rows.Clear();
            dgvPackageBodyLines.DataSource = dataTable;
        }

        void QueryOwnerFunctions(string owner)
        {
            actualQuery = @"select 
            OWNER, 
            OBJECT_NAME,
            OBJECT_TYPE,
            STATUS,
            CREATED,
            LAST_DDL_TIME, 
            TIMESTAMP
            from all_objects where object_type = 'FUNCTION' and owner = '" + owner + "' order by object_name";
            dataTable = RunQuery(actualQuery);
            dgvFunctions.DataSource = null;
            dgvFunctions.Rows.Clear();
            dgvFunctions.DataSource = dataTable;
        }

        void QueryFunctionLines(string owner, string functionName)
        {
            actualQuery = @"select 
            OWNER, 
            NAME, 
            TYPE, 
            LINE, 
            TEXT 
            from all_source where type = 'FUNCTION' and owner = '" + owner + "' and name = '" + functionName + "' order by line";
            dataTable = RunQuery(actualQuery);
            dgvFunctionLines.DataSource = null;
            dgvFunctionLines.Rows.Clear();
            dgvFunctionLines.DataSource = dataTable;
        }

        void QueryOwnerProcedures(string owner)
        {
            actualQuery = @"select 
            OWNER, 
            OBJECT_NAME,
            OBJECT_TYPE,
            STATUS,
            CREATED,
            LAST_DDL_TIME, 
            TIMESTAMP
            from all_objects where object_type = 'PROCEDURE' and owner = '" + owner + "' order by object_name";
            dataTable = RunQuery(actualQuery);
            dgvProcedures.DataSource = null;
            dgvProcedures.Rows.Clear();
            dgvProcedures.DataSource = dataTable;
        }

        void QueryProcedureLines(string owner, string procedureName)
        {
            actualQuery = @"select 
            OWNER, 
            NAME, 
            TYPE, 
            LINE, 
            TEXT 
            from all_source where type = 'PROCEDURE' and owner = '" + owner + "' and name = '" + procedureName + "' order by line";
            dataTable = RunQuery(actualQuery);
            dgvProcedureLines.DataSource = null;
            dgvProcedureLines.Rows.Clear();
            dgvProcedureLines.DataSource = dataTable;
        }

        void QueryIndexes()
        {
            //MessageBox.Show("Indexek adatai");
        }
        void QueryViews()
        {
            MessageBox.Show("Nézetek adatai");
        }
        void QueryPackages()
        {
            MessageBox.Show("Pekicsek adatai");
        }
        void QueryPackageBodies()
        {
            MessageBox.Show("Pekics bádik adatai");
        }
        void QueryFunctions()
        {
            MessageBox.Show("Fánksinök adatai");
        }
        void QueryProcedures()
        {
            MessageBox.Show("Procedurák adatai");
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabTables")
            {
                QueryOwners("all_tables");
            }
            if (tabControl1.SelectedTab.Name == "tabConstraints")
            {
                QueryOwners("all_constraints");
            }
            if (tabControl1.SelectedTab.Name == "tabTriggers")
            {
                QueryOwners("all_triggers");
            }
            if (tabControl1.SelectedTab.Name == "tabIndexes")
            {
                QueryOwners("all_indexes");
            }
            if (tabControl1.SelectedTab.Name == "tabViews")
            {
                QueryOwners("all_views");
            }
            if (tabControl1.SelectedTab.Name == "tabPackages")
            {
                QueryOwners("all_objects");
            }
            if (tabControl1.SelectedTab.Name == "tabPackageBodies")
            {
                QueryOwners("all_objects");
            }
            if (tabControl1.SelectedTab.Name == "tabFunctions")
            {
                QueryOwners("all_objects");
            }
            if (tabControl1.SelectedTab.Name == "tabProcedures")
            {
                QueryOwners("all_objects");
            }
        }

        private void btnEles_Click(object sender, EventArgs e)
        {
            FcsmServer = FcsmServer.Eles;
            peldany = PeldanyEnum.KASSZA.ToString();
        }

        private void btnTeszt_Click(object sender, EventArgs e)
        {
            FcsmServer = FcsmServer.Teszt;
            peldany = PeldanyEnum.KASSZA_TESZT.ToString();
            QueryOwners("all_tables");
            QueryOwnerTables(peldany);
            QueryTableColumns(cmbTulajdonos.Text.ToString(), dgvTables[1, 0].Value.ToString());
            tabControl1.SelectedTab.Name = "tabTables";

        }

        private void cmbTulajdonos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabTables")
            {
                QueryOwnerTables(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabConstraints")
            {
                QueryOwnerConstraints(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabTriggers")
            {
                QueryOwnerTriggers(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabIndexes")
            {
                QueryOwnerIndexes(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabViews")
            {
                QueryOwnerViews(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabPackages")
            {
                QueryOwnerPackages(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabPackageBodies")
            {
                QueryOwnerPackageBodies(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabFunctions")
            {
                QueryOwnerFunctions(cmbTulajdonos.Text.ToString());
            }
            if (tabControl1.SelectedTab.Name == "tabProcedures")
            {
                QueryOwnerProcedures(cmbTulajdonos.Text.ToString());
            }

        }

        private void dgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //   MessageBox.Show(dgvTables[1, e.RowIndex].Value.ToString());
            QueryTableColumns(cmbTulajdonos.Text.ToString(), dgvTables[1, e.RowIndex].Value.ToString());
        }

        private void dgvTriggers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            QueryTriggerLines(cmbTulajdonos.Text.ToString(), dgvTriggers[2, e.RowIndex].Value.ToString());
        }

        private void dgvIndexes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dgvIndexes[1, e.RowIndex].Value.ToString());
            QueryIndexColumns(cmbTulajdonos.Text.ToString(), dgvIndexes[1, e.RowIndex].Value.ToString());
        }

        private void dgvPackages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            QueryPackageLines(cmbTulajdonos.Text.ToString(), dgvPackages[1, e.RowIndex].Value.ToString());
        }

        private void dgvPackageBodies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            QueryPackageBodyLines(cmbTulajdonos.Text.ToString(), dgvPackageBodies[1, e.RowIndex].Value.ToString());
        }

        private void dgvFunctions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            QueryFunctionLines(cmbTulajdonos.Text.ToString(), dgvFunctions[1, e.RowIndex].Value.ToString());
        }

        private void dgvProcedures_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            QueryProcedureLines(cmbTulajdonos.Text.ToString(), dgvProcedures[1, e.RowIndex].Value.ToString());
        }

        private void btnSourceToFile_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPackages")
            {
                MessageBox.Show(cmbTulajdonos.Text.ToString());
                MessageBox.Show(cmbTulajdonos.Text.ToString());
                
                DataTable dt = QueryPackageLines(cmbTulajdonos.Text.ToString(), "KODLABOR");

                FileOperations.WriteToFile(dt,"Fájlnév");



            }
        }
    }
}
