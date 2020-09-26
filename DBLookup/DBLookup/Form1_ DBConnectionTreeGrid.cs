using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;

namespace DBLookup
{
    public class fake3
    {
    }
    public partial class Form1 : Form
    {
        private void SetConnectionString(String sConnectionString)
        {
            if(sConnectionString == "")
            {
                splitContainer2.Panel1Collapsed = true;
            }
            else
            {
                if (mod_sCurrentConnection != sConnectionString)
                {
                    mod_sCurrentConnection = sConnectionString;
                    splitContainer2.Panel1Collapsed = !Treeview_GetDatabaseTableInfo();
                    btnApplyChangesToDB.Enabled = false;
                }
            }
        }
        void GetTable(String sConnectionString, String sSQL)
        {
            btnApplyChangesToDB.Enabled = false;
            Cursor = Cursors.WaitCursor;
            _dtNow = DateTime.Now;
            toolStripStatusLabel1.Text = @"Execute started at " + _dtNow + @". ";
            tmrTableData.Enabled = true;

            // log history
            LogAction("Connection", sConnectionString);
            // Wait until previous processes have been completed before going on (the most simplified version)
            if (bgwConnectToDB.IsBusy) { Application.DoEvents(); }
            // run sql in background worker
            bgwConnectToDB.RunWorkerAsync(new ExecuteParameters
            {
                ConnectionString = sConnectionString,
                SqlStatement = sSQL
            });
        }
        void ExecuteSQL(string sConnectionString, String sSQL)
        {
            btnApplyChangesToDB.Enabled = false;
            Cursor = Cursors.WaitCursor;
            _dtNow = DateTime.Now;
            toolStripStatusLabel1.Text = @"Execute started at " + _dtNow + @". ";

            // log history
            LogAction("Connection", Helper.RemoveConnectionStringSecurity(sConnectionString));

            // Wait until previous processes have been completed before going on (the most simplified version)
            while (bgwConnectToDB.IsBusy && !mod_bTimedOut) { Application.DoEvents(); }

            // run sql in background worker
            bgwConnectToDB.RunWorkerAsync(new ExecuteParameters
            {
                ConnectionString = sConnectionString,
                SqlStatement = sSQL
            });
        }
        private bool treeviewGetDBTableInfo(String sConnectionString)
        {
            if (sConnectionString.ToLower().Contains("provider=") || sConnectionString.ToLower().Contains("provider ="))
                return false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sConnectionString + ";Connection Timeout=3"))
                {
                    sqlConnection.Open();

                    String tables = "'" + String.Join("','", mod_lstLookupTables.ToArray()) + "'";

                    String sql = @" SELECT a.TABLE_CATALOG, 
                          a.TABLE_SCHEMA + '.' + a.TABLE_NAME AS[TABLE_NAME], 
                                   a.COLUMN_NAME + ' (' + a.DATA_TYPE + ')' AS[COLUMN_NAME]
                                   ,a.TABLE_SCHEMA + '.' + a.TABLE_NAME + '.' + a.COLUMN_NAME  AS[COLUMN_KEY],
                                   a.DATA_TYPE AS[COLUMN_TAG]
                            FROM INFORMATION_SCHEMA.COLUMNS a
                            WHERE (a.TABLE_CATALOG + '.'+ a.TABLE_SCHEMA +'.'+ a.TABLE_NAME) IN(" + tables + @")
                            ORDER BY a.TABLE_CATALOG, a.TABLE_NAME, a.ORDINAL_POSITION";
                    
                    //todo Views are currently excluded, possible enhancement to return VIEW_DEFINITION rows in new group
                    using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                        {
                            if (!sqlReader.HasRows)
                                return false;

                            treeDBtables.Nodes.Clear();
                            TreeNode databaseNode = new TreeNode("Unknown");
                            treeDBtables.HotTracking = false;
                            String sPreviousTableName = "";
                            while (sqlReader.Read())
                            {
                                // Set table
                                if (databaseNode.Text == @"Unknown") databaseNode.Text = sqlReader.GetString(0);

                                // add new table if different from last
                                String tableName = sqlReader.GetString(1);
                                if (tableName != sPreviousTableName)
                                {
                                    sPreviousTableName = tableName;
                                    TreeNode tableNode = new TreeNode(tableName)
                                    {
                                        ContextMenuStrip = cMenu_DBTree_TableScriptActions
                                    };
                                    databaseNode.Nodes.Add(tableNode);
                                }
                                //add the cols -- trying something to add the data types to tree
                                //                                TreeNode newNode = databaseNode.LastNode.Nodes.Add(sqlReader.GetString(2));

                                TreeNode newNode = databaseNode.LastNode.Nodes.Add(sqlReader.GetString(3), sqlReader.GetString(2));
                                newNode.Tag = sqlReader.GetString(4);

                                //databaseNode.LastNode.Nodes.Add(sqlReader.GetString(3));
                            }
                            treeDBtables.Nodes.Add(databaseNode);
                            treeDBtables.Nodes[0].Expand();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
                return false;
            }

            txtSQL.Focus();
            return true;
        }
        private void FillDataGrid(ExecuteResults results)
        {

            if (results.ExceptionDetails != null)
            {
                mod_dtblTable = new DataTable();

                mod_dtblTable.Columns.Add("Exception", typeof(String));
                mod_dtblTable.Columns.Add(" ", typeof(String));

                mod_dtblTable.Rows.Add("Message", results.ExceptionDetails.Message);
                mod_dtblTable.Rows.Add("StackTrace", results.ExceptionDetails.StackTrace);
                mod_dtblTable.Rows.Add("Source", results.ExceptionDetails.Source);
                mod_dtblTable.Rows.Add("Data", results.ExceptionDetails.Data);

                //add inner exception if any
                if (results.ExceptionDetails.InnerException == null)
                {
                    mod_dtblTable.Rows.Add("Inner Exception", "null");
                }
                else
                {
                    mod_dtblTable.Rows.Add("Inner Exception Message", results.ExceptionDetails.InnerException.Message);
                    mod_dtblTable.Rows.Add("Inner Exception StackTrace", results.ExceptionDetails.InnerException.StackTrace);
                    mod_dtblTable.Rows.Add("Inner Exception Source", results.ExceptionDetails.InnerException.Source);
                    mod_dtblTable.Rows.Add("Inner Exception Data", results.ExceptionDetails.InnerException.Data);
                }

                dgrid_TableData.DataSource = mod_dtblTable;
                toolStripStatusLabel1.Text = results.StatusMessage;

                btnScriptTableData.Enabled = false;
                chkTruncateFirst.Enabled = false;
                btnClearDataTableFilter.Enabled = false;
            }
            else
            {
                mod_dtblTable = results.ResultsData;
                dgrid_TableData.DataSource = mod_dtblTable;
                toolStripStatusLabel1.Text = results.StatusMessage;
                btnScriptTableData.Enabled = true;
                btnClearDataTableFilter.Enabled = true;
                chkTruncateFirst.Enabled = true;
            }
            tmrTableData.Enabled = false;
            toolStripStatusLabel3.Text = "";

            // log history
            LogAction("StatusMesage", results.StatusMessage);
            LogAction("RowsReturned", (results.ResultsData != null ? "Returned " + results.ResultsData.Rows.Count + " rows" : "None"));
            if (results.ExceptionDetails != null)
            {
                foreach (DataGridViewRow row in dgrid_TableData.Rows)
                {
                    LogAction("Exception", row.Cells[0].Value + " : " + row.Cells[1].Value);
                }
            }
            dgrid_TableData.AutoResizeColumns();
            dgrid_TableData.AutoResizeRows();

            Cursor = Cursors.Default;
            btnApplyChangesToDB.Enabled = true;
        }

        private ExecuteResults GetDataToFillDataGrid(object sender, DoWorkEventArgs e)
        {
            ExecuteResults results = new ExecuteResults();
            mod_execParam = (ExecuteParameters)e.Argument;

            try
            {
                // if provider is not specified use sqlconncetion
                if (!mod_execParam.ConnectionString.ToLower().Contains("provider=") && !mod_execParam.ConnectionString.ToLower().Contains("provider ="))
                {
                    using (SqlConnection sqlCon = new SqlConnection(mod_execParam.ConnectionString))
                    {
                        sqlCon.Open();

                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(
                            mod_execParam.SqlStatement, sqlCon))
                        {
                            mod_dtblDBTables = new DataTable();
                            sqlAdapter.Fill(mod_dtblDBTables);
                            // Render data onto the screen
                            results.ResultsData = mod_dtblDBTables;
                            mod_sAdapterSource = "SQL";
                        }
                    }
                }
                else
                {
                    using (OleDbConnection oleCon = new OleDbConnection(mod_execParam.ConnectionString))
                    {
                        oleCon.Open();

                        using (OleDbDataAdapter oleAdapter = new OleDbDataAdapter(
                            mod_execParam.SqlStatement, oleCon))
                        {
                            mod_dtblDBTables = new DataTable();
                            oleAdapter.Fill(mod_dtblDBTables);
                            results.ResultsData = mod_dtblDBTables;
                            mod_sAdapterSource = "OLE";
                        }
                    }
                }
                //Console.Beep();
                results.StatusMessage = "Completed in " + (DateTime.Now - _dtNow) + ". ";
            }
            catch (Exception ex)
            {
                results.ExceptionDetails = ex;
                results.StatusMessage = "Completed with errors in " + (DateTime.Now - _dtNow) + ". ";
            }

            if (results.ResultsData != null)
                results.StatusMessage = results.StatusMessage + results.ResultsData.Rows.Count + " row(s) returned. ";

            return results;
        }
        //Disabled code to keep as reference for others
        private void getConnectionStringsFromFile(string sPath, string sFileName, ComboBox cmbOutput)
        {
            string sFilePath = sPath;
            if (sPath != "" && sPath.Last().ToString() != "\\".ToString()) sPath += "\\";
            sPath += sFileName;
            //add connection string if they exist
            if (File.Exists("connections.txt"))
            {
                using (TextReader tr = File.OpenText("connections.txt"))
                {
                    foreach (string line in tr.ReadToEnd().Split(
                        new[]
                        {
                            (char)13,
                            (char)10
                        }).Where(line => line.Trim() != ""))
                    {
                        cmbOutput.Items.Add(line);
                    }
                }
            }

        }
        private String GetSelectQueryFromNode(TreeView tree, TreeNodeCollection tableNode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            foreach (TreeNode node in tableNode)
            {
                if (builder.ToString() == "SELECT ")
                {
                    builder.AppendLine();
                    builder.AppendLine("    " + Helper.FormatColumnName(node));
                }
                else
                    builder.AppendLine("   ," + Helper.FormatColumnName(node));
            }
            builder.AppendLine("FROM " + Helper.FormatTableName(tree.SelectedNode));
            builder.AppendLine();

            return builder.ToString();
        }
    }
}

