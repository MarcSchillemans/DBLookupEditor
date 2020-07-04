using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using System.Collections.Generic;
// Derived from https://git01.codeplex.com/dblookup
namespace DBLookup
{
    public partial class Form1 : Form
    {
        private DataTable mod_dtblDBTables;
        private DataTable mod_dtblTable;
        private String mod_sCurrentConnection;
        private String mod_sTableInGrid;
        private Rectangle mod_rectDragBoxFromMouseDown;
        private int mod_iRowIndexFromMouseDown;
        private int mod_iRowIndexOfItemUnderMouseToDrop;
        private DataTable mod_dtSearch = new DataTable();
        private String mod_sAdapterSource;
        private ExecuteParameters mod_execParam;
        private bool mod_bTimedOut;
        private List<String> mod_lstLookupTables;
        private List<String> mod_lstConnectionStrings;

        public Form1()
        {
            InitializeComponent();
        }

        DateTime _dtNow;
        #region Winform Control Events

        /// <summary>
        /// Page load method
        /// </summary>
        private void Form1Load(object sender, EventArgs e)
        {
            mod_bTimedOut = false;
            //getConnectionStringsFromFile("", "connections.txt", cmbConnectString); //keep for others to have an example 
            mod_lstLookupTables = GetLookupTables();

            ComboInit();
        }


        #region Combobox
        private bool ComboInit()
        {
            bool bResult = false;

            mod_lstConnectionStrings = GetConnectionStrings();
            cmbConnectString.DataSource = mod_lstConnectionStrings;
            if (this.cmbConnectString.Items.Count > 0)
            {
                this.cmbConnectString.SelectedValueChanged += CmbConnectionString_SelectValueChanged;
                this.cmbConnectString.SelectedIndex = 0;
                SetConnectionString(cmbConnectString.Text);
                bResult = true;
            }
            return bResult;
        }
        private void CmbConnectionString_SelectValueChanged(object sender, EventArgs e)
        {
            SetConnectionString(cmbConnectString.Text);
        }
        #endregion

        private void Dgrid_TableData_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (mod_rectDragBoxFromMouseDown != Rectangle.Empty &&
                    !mod_rectDragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                   
                    DragDropEffects dropEffect = dgrid_TableData.DoDragDrop(
                             dgrid_TableData.Rows[mod_iRowIndexFromMouseDown],
                             DragDropEffects.Move);
                }
            }
        }

        private void Dgrid_TableData_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            DataGridView.HitTestInfo ht = dgrid_TableData.HitTest(e.X, e.Y);
            mod_iRowIndexFromMouseDown = ht.RowIndex;

            if (e.Button == MouseButtons.Right)
            {
                //Checks for correct column index
                if (ht.ColumnIndex != -1 && ht.RowIndex == -1)
                {
                    //Create the ContextStripMenu for Creating the PO Sub Form
                    ContextMenuStrip menu = new ContextMenuStrip();
                    ToolStripMenuItem menuClip1 = new ToolStripMenuItem("Copy to Clipboard");
                    ToolStripMenuItem menuClip2 = new ToolStripMenuItem("Export to Text");

                    menuClip1.MouseDown += ExportSQLTextToClipboard;
                    menuClip2.MouseDown += ExportCsv;

                    menu.Items.AddRange(new ToolStripItem[] { menuClip1, menuClip2 });

                    //Assign created context menu strip to the DataGridView
                    dgrid_TableData.ContextMenuStrip = menu;
                }
                else
                {
                    dgrid_TableData.ContextMenuStrip = null;
                }
            }
            else
            {
                dgrid_TableData.ContextMenuStrip = null;
            }

            if (mod_iRowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred.
                // The DragSize indicates the size that the mouse can move
                // before a drag event should be started.               
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being

                // at the center of the rectangle.
                mod_rectDragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                                        dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                mod_rectDragBoxFromMouseDown = Rectangle.Empty;
        }

        private void Dgrid_TableData_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Dgrid_TableData_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be
            // converted to client coordinates.
            Point clientPoint = dgrid_TableData.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below.
            mod_iRowIndexOfItemUnderMouseToDrop = dgrid_TableData.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                //////  ---------------- update table
                ///

                //DataGridViewRow rowToMove = e.Data.GetData(
                //             typeof(DataGridViewRow)) as DataGridViewRow;

                //dgrid_TableData.Rows.RemoveAt(mod_iRowIndexFromMouseDown);

                //dgrid_TableData.Rows.Insert(mod_iRowIndexOfItemUnderMouseToDrop, rowToMove);
            }
        }
        private void txtSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E)
            {
                String sSQL = Get_Query_From_SqlBox();
                ExecuteSQL(mod_sCurrentConnection ,sSQL);
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                txtSQL.SelectAll();
            }
        }

        /// <summary>
        /// on enter get the sql structure if possible
        /// </summary>
        private void cmbConnectString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SetConnectionString(cmbConnectString.Text);
        }
        #endregion

        #region Background Worker SQL Process

        /// <summary>
        /// background work to run sql, run in a separate thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgwConnectToDB(object sender, DoWorkEventArgs e)
        {
            e.Result = GetDataToFillDataGrid(sender, e);
        }

        /// <summary>
        /// backgound work to run sql complete
        /// </summary>
        private void BgwConnectToDB_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            FillDataGrid((ExecuteResults)e.Result);
        }
        private void BgwConnectToDB_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        #endregion
        #region Statusbar Methods
        /// <summary>
        /// Opens the log in notepad
        /// </summary>
        private void ToolStripStatusLabel2Click(object sender, EventArgs e)
        {
            try
            {
                txtSQL.Clear();
                BuildScript(treeDBtables.SelectedNode, "", "", "");
                //Process.Start(
                //    "notepad.exe", 
                //    Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName) + ".log");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Status bar message update, enabled during query execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrTableData_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = @"has been running for " + (DateTime.Now - _dtNow).Seconds + @" seconds";
        }

        // Create script to store in project
        void BuildScript(TreeNode node, String sProject, String sSchema, String sFilename)
        {
            if (node.Level == 1)
            {
                string table = node.Text;
            }
        }
        #endregion

        #region TreeView Methods
        /// <summary>
        /// Populates the treeview, this is executed when the connection string dropdown is updated
        /// </summary>
        /// <returns></returns>
        private bool Treeview_GetDatabaseTableInfo()
        {
            return treeviewGetDBTableInfo(mod_sCurrentConnection);
        }

        /// <summary>
        /// Private helper method used in all TreeView methods to add the text to the sqlbox
        /// </summary>
        /// <param name="s"></param>
        void txtSQL_AppendToFront(string s)
        {
            int selectStart = txtSQL.SelectionStart;

            txtSQL.Text = txtSQL.Text.Substring(0, selectStart) + s + txtSQL.Text.Substring(selectStart);
            txtSQL.Select(selectStart, s.Length);
            txtSQL.Focus();
        }

        /// <summary>
        /// This is requred to make sure the right click will select the correct node
        /// </summary>
        private void TreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            treeDBtables.SelectedNode = treeDBtables.GetNodeAt(e.X, e.Y);
        }

        /// <summary>
        /// Copys the node into the sql box
        /// </summary>
        private void TreeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = treeDBtables.SelectedNode;
            if (node.Level == 1)
                ShowData(treeDBtables, treeDBtables.SelectedNode.Nodes);
        }

        /// <summary>
        /// Constructs a SELECT TOP 500  query
        /// </summary>
        private void ToolStripMenuItem1Click(object sender, EventArgs e)
        {
            ShowData(treeDBtables, treeDBtables.SelectedNode.Nodes);
        }

        /// <summary>
        /// Constructs a SELECT query
        /// </summary>
        private void tsmiSelectClick(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT ");

            foreach (TreeNode node in treeDBtables.SelectedNode.Nodes)
            {
                if (builder.ToString() == "SELECT ")
                    builder.AppendLine(" " + Helper.FormatColumnName(node));
                else
                    builder.AppendLine("       ," + Helper.FormatColumnName(node));
            }

            builder.AppendLine("FROM " + Helper.FormatTableName(treeDBtables.SelectedNode));
            builder.AppendLine();

            txtSQL_AppendToFront(builder.ToString());
        }

        /// <summary>
        /// Constructs a INSERT query
        /// </summary>
        private void tsmiInsertClick(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder sqlValues = new StringBuilder();

            builder.Append("INSERT INTO ");
            sqlValues.AppendLine("VALUES ");

            foreach (TreeNode node in treeDBtables.SelectedNode.Nodes)
            {
                if (builder.ToString() == "INSERT INTO ")
                {
                    builder.AppendLine(Helper.FormatTableName(treeDBtables.SelectedNode));
                    builder.AppendLine("  (");
                    builder.AppendLine("    " + Helper.FormatColumnName(node));
                    sqlValues.AppendLine("  (");
                    sqlValues.AppendLine("    <" + node.Text + ">");
                }
                else
                {
                    builder.AppendLine("   ," + Helper.FormatColumnName(node));
                    sqlValues.AppendLine("   ,<" + node.Text + ">");
                }
            }
            builder.AppendLine("  )");
            sqlValues.AppendLine("  )");
            sqlValues.AppendLine();

            txtSQL_AppendToFront(builder + sqlValues.ToString());
        }

        /// <summary>
        /// Constructs a UPDATE query
        /// </summary>
        private void tsmiUpdateClick(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("UPDATE ");

            foreach (TreeNode node in treeDBtables.SelectedNode.Nodes)
            {
                if (builder.ToString() == "UPDATE ")
                {
                    builder.AppendLine(Helper.FormatTableName(treeDBtables.SelectedNode));
                    builder.AppendLine(" SET " + Helper.FormatColumnName(node) + " = <" + node.Text + ">");
                }
                else
                {
                    builder.AppendLine("    ," + Helper.FormatColumnName(node) + " = <" + node.Text + ">");
                }
            }
            builder.AppendLine("WHERE <Search Conditions>");
            builder.AppendLine();

            txtSQL_AppendToFront(builder.ToString());
        }

        /// <summary>
        /// Constructs a DELETE query
        /// </summary>
        private void tsmiDeleteClick(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("DELETE FROM " + Helper.FormatTableName(treeDBtables.SelectedNode));
            builder.AppendLine("WHERE <Search Conditions>");

            txtSQL_AppendToFront(builder.ToString());
            builder.AppendLine();
        }

        #endregion

        #region Private Methods

        void ExportTableDataToClipboard(object sender, MouseEventArgs e)
        {
            dgrid_TableData.SelectAll();
            var dataObj = dgrid_TableData.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        void ExportSQLTextToClipboard(object sender, MouseEventArgs e)
        {
            String sText = txtSQL.Text.Trim();
            if (sText != "")
                Clipboard.SetDataObject(sText);
        }

        #endregion

        #region button_clicks
        private void BtnCollapseTree_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeDBtables.Nodes[0];
            tn.Expand();
            foreach (TreeNode treeNode in tn.Nodes)
                treeNode.Collapse();
        }

        private void BtnSaveChangesToProjectAndDB_Click(object sender, EventArgs e)
        {

        }

        private void BtnApplyChangesToDB_Click(object sender, EventArgs e)
        {
            ClearTableDataFilter();
            switch (mod_sAdapterSource)
            {
                case "SQL":
                    UpdateGridDataToDB(mod_execParam.SqlStatement, mod_sCurrentConnection, mod_dtblTable, "Update");
                    break;
                case "OLE":
                    break;
            }
        }

        private void BtnScriptTableData(object sender, EventArgs e)
        {
            if (TableHasRows())
            {
                //Create scripts to store data in Visual Studio + Preparation to update Database
                ReadFromDataGrid(dgrid_TableData);
                //Create script per row
                if (txtSQL.Text.Trim() != "")
                    btnSaveChangesToProjectAndDB.Enabled = true;
            }
        }

        private void BtnClearTableDataFilter_Click(object sender, EventArgs e)
        {
            ClearTableDataFilter();
        }
        #endregion
        private static void PrintColumns(DataTableReader reader)
        {
            // Loop through all the rows in the DataTableReader
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write(reader[i] + " ");
                Console.WriteLine();
            }
        }
        private void Dgrid_TableData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgrid_TableData.IsCurrentCellDirty)
            {
                //placeholder
            }
        }

        private void tmrTimeOut_TableData_Tick(object sender, EventArgs e)
        {
            mod_bTimedOut = true;
        }
    }
}
