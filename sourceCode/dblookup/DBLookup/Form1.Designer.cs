using System;
using System.Windows.Forms;
using System.Configuration;

namespace DBLookup
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnApplyChangesToDB = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeDBtables = new System.Windows.Forms.TreeView();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.Tlp_OpenConnection = new System.Windows.Forms.TableLayoutPanel();
            this.cmbConnectString = new System.Windows.Forms.ComboBox();
            this.lblConnection = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCollapseTree = new System.Windows.Forms.Button();
            this.btnClearDataTableFilter = new System.Windows.Forms.Button();
            this.btnScriptTableData = new System.Windows.Forms.Button();
            this.chkTruncateFirst = new System.Windows.Forms.CheckBox();
            this.btnDeleteRecordsFromTableGrid = new System.Windows.Forms.Button();
            this.btnSaveChangesToProjectAndDB = new System.Windows.Forms.Button();
            this.Tlp_Datagrids = new System.Windows.Forms.TableLayoutPanel();
            this.dgrid_TableData = new System.Windows.Forms.DataGridView();
            this.dgrid_Search = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwConnectToDB = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tmrTableData = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrTimeOut_TableData = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.Tlp_OpenConnection.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.Tlp_Datagrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_TableData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_Search)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApplyChangesToDB
            // 
            this.btnApplyChangesToDB.Location = new System.Drawing.Point(371, 4);
            this.btnApplyChangesToDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnApplyChangesToDB.Name = "btnApplyChangesToDB";
            this.btnApplyChangesToDB.Size = new System.Drawing.Size(179, 28);
            this.btnApplyChangesToDB.TabIndex = 2;
            this.btnApplyChangesToDB.Text = "Apply Changes To DB";
            this.btnApplyChangesToDB.UseVisualStyleBackColor = true;
            this.btnApplyChangesToDB.Click += new System.EventHandler(this.BtnApplyChangesToDB_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Tlp_Datagrids);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(965, 657);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Tlp_OpenConnection, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(965, 257);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(4, 98);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeDBtables);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtSQL);
            this.splitContainer2.Size = new System.Drawing.Size(957, 178);
            this.splitContainer2.SplitterDistance = 157;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 3;
            // 
            // treeDBtables
            // 
            this.treeDBtables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDBtables.Location = new System.Drawing.Point(0, 0);
            this.treeDBtables.Margin = new System.Windows.Forms.Padding(4);
            this.treeDBtables.Name = "treeDBtables";
            this.treeDBtables.Size = new System.Drawing.Size(157, 178);
            this.treeDBtables.TabIndex = 0;
            this.treeDBtables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseDoubleClick);
            this.treeDBtables.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseDown);
            // 
            // txtSQL
            // 
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Margin = new System.Windows.Forms.Padding(4);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL.Size = new System.Drawing.Size(795, 178);
            this.txtSQL.TabIndex = 4;
            // 
            // Tlp_OpenConnection
            // 
            this.Tlp_OpenConnection.ColumnCount = 2;
            this.Tlp_OpenConnection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.70282F));
            this.Tlp_OpenConnection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.29719F));
            this.Tlp_OpenConnection.Controls.Add(this.cmbConnectString, 1, 0);
            this.Tlp_OpenConnection.Controls.Add(this.lblConnection, 0, 0);
            this.Tlp_OpenConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tlp_OpenConnection.Location = new System.Drawing.Point(5, 5);
            this.Tlp_OpenConnection.Margin = new System.Windows.Forms.Padding(5);
            this.Tlp_OpenConnection.Name = "Tlp_OpenConnection";
            this.Tlp_OpenConnection.RowCount = 1;
            this.Tlp_OpenConnection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_OpenConnection.Size = new System.Drawing.Size(955, 32);
            this.Tlp_OpenConnection.TabIndex = 4;
            // 
            // cmbConnectString
            // 
            this.cmbConnectString.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbConnectString.FormattingEnabled = true;
            this.cmbConnectString.Location = new System.Drawing.Point(144, 4);
            this.cmbConnectString.Margin = new System.Windows.Forms.Padding(4);
            this.cmbConnectString.MaxDropDownItems = 50;
            this.cmbConnectString.Name = "cmbConnectString";
            this.cmbConnectString.Size = new System.Drawing.Size(807, 24);
            this.cmbConnectString.TabIndex = 1;
            // 
            // lblConnection
            // 
            this.lblConnection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConnection.AutoSize = true;
            this.lblConnection.CausesValidation = false;
            this.lblConnection.Location = new System.Drawing.Point(7, 7);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(126, 17);
            this.lblConnection.TabIndex = 0;
            this.lblConnection.Text = "Select Connection:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCollapseTree);
            this.flowLayoutPanel1.Controls.Add(this.btnClearDataTableFilter);
            this.flowLayoutPanel1.Controls.Add(this.btnDeleteRecordsFromTableGrid);
            this.flowLayoutPanel1.Controls.Add(this.btnApplyChangesToDB);
            this.flowLayoutPanel1.Controls.Add(this.btnScriptTableData);
            this.flowLayoutPanel1.Controls.Add(this.chkTruncateFirst);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveChangesToProjectAndDB);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 46);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(957, 35);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnCollapseTree
            // 
            this.btnCollapseTree.Location = new System.Drawing.Point(3, 2);
            this.btnCollapseTree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCollapseTree.Name = "btnCollapseTree";
            this.btnCollapseTree.Size = new System.Drawing.Size(116, 28);
            this.btnCollapseTree.TabIndex = 0;
            this.btnCollapseTree.Text = "Collapse Tree";
            this.btnCollapseTree.UseVisualStyleBackColor = true;
            this.btnCollapseTree.Click += new System.EventHandler(this.BtnCollapseTree_Click);
            // 
            // btnClearDataTableFilter
            // 
            this.btnClearDataTableFilter.Enabled = false;
            this.btnClearDataTableFilter.Location = new System.Drawing.Point(125, 2);
            this.btnClearDataTableFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearDataTableFilter.Name = "btnClearDataTableFilter";
            this.btnClearDataTableFilter.Size = new System.Drawing.Size(108, 30);
            this.btnClearDataTableFilter.TabIndex = 6;
            this.btnClearDataTableFilter.Text = "Clear Filter";
            this.btnClearDataTableFilter.UseVisualStyleBackColor = true;
            this.btnClearDataTableFilter.Click += new System.EventHandler(this.BtnClearTableDataFilter_Click);
            // 
            // btnScriptTableData
            // 
            this.btnScriptTableData.Enabled = false;
            this.btnScriptTableData.Location = new System.Drawing.Point(558, 4);
            this.btnScriptTableData.Margin = new System.Windows.Forms.Padding(4);
            this.btnScriptTableData.Name = "btnScriptTableData";
            this.btnScriptTableData.Size = new System.Drawing.Size(134, 28);
            this.btnScriptTableData.TabIndex = 3;
            this.btnScriptTableData.Text = "Script Table Data";
            this.btnScriptTableData.UseVisualStyleBackColor = true;
            this.btnScriptTableData.Click += new System.EventHandler(this.BtnScriptTableData);
            // 
            // chkTruncateFirst
            // 
            this.chkTruncateFirst.AutoSize = true;
            this.chkTruncateFirst.Checked = true;
            this.chkTruncateFirst.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTruncateFirst.Enabled = false;
            this.chkTruncateFirst.Location = new System.Drawing.Point(699, 2);
            this.chkTruncateFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkTruncateFirst.Name = "chkTruncateFirst";
            this.chkTruncateFirst.Size = new System.Drawing.Size(118, 21);
            this.chkTruncateFirst.TabIndex = 5;
            this.chkTruncateFirst.Text = "Truncate First";
            this.chkTruncateFirst.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRecordsFromTableGrid
            // 
            this.btnDeleteRecordsFromTableGrid.Location = new System.Drawing.Point(239, 3);
            this.btnDeleteRecordsFromTableGrid.Name = "btnDeleteRecordsFromTableGrid";
            this.btnDeleteRecordsFromTableGrid.Size = new System.Drawing.Size(125, 29);
            this.btnDeleteRecordsFromTableGrid.TabIndex = 7;
            this.btnDeleteRecordsFromTableGrid.Text = "Delete Records";
            this.btnDeleteRecordsFromTableGrid.UseVisualStyleBackColor = true;
            this.btnDeleteRecordsFromTableGrid.Click += new System.EventHandler(this.BtnDeleteRecordsFromTableGrild_Click);
            // 
            // btnSaveChangesToProjectAndDB
            // 
            this.btnSaveChangesToProjectAndDB.Enabled = false;
            this.btnSaveChangesToProjectAndDB.Location = new System.Drawing.Point(4, 40);
            this.btnSaveChangesToProjectAndDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveChangesToProjectAndDB.Name = "btnSaveChangesToProjectAndDB";
            this.btnSaveChangesToProjectAndDB.Size = new System.Drawing.Size(245, 28);
            this.btnSaveChangesToProjectAndDB.TabIndex = 4;
            this.btnSaveChangesToProjectAndDB.Text = "Save Changes to Project and DB";
            this.btnSaveChangesToProjectAndDB.UseVisualStyleBackColor = true;
            this.btnSaveChangesToProjectAndDB.Click += new System.EventHandler(this.BtnSaveChangesToProjectAndDB_Click);
            // 
            // Tlp_Datagrids
            // 
            this.Tlp_Datagrids.ColumnCount = 1;
            this.Tlp_Datagrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Tlp_Datagrids.Controls.Add(this.dgrid_TableData, 0, 1);
            this.Tlp_Datagrids.Controls.Add(this.dgrid_Search, 0, 0);
            this.Tlp_Datagrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tlp_Datagrids.Location = new System.Drawing.Point(0, 0);
            this.Tlp_Datagrids.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tlp_Datagrids.Name = "Tlp_Datagrids";
            this.Tlp_Datagrids.RowCount = 2;
            this.Tlp_Datagrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.41899F));
            this.Tlp_Datagrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.58101F));
            this.Tlp_Datagrids.Size = new System.Drawing.Size(965, 360);
            this.Tlp_Datagrids.TabIndex = 5;
            // 
            // dgrid_TableData
            // 
            this.dgrid_TableData.AllowDrop = true;
            this.dgrid_TableData.AllowUserToOrderColumns = true;
            this.dgrid_TableData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgrid_TableData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgrid_TableData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrid_TableData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid_TableData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrid_TableData.Location = new System.Drawing.Point(4, 95);
            this.dgrid_TableData.Margin = new System.Windows.Forms.Padding(4);
            this.dgrid_TableData.Name = "dgrid_TableData";
            this.dgrid_TableData.RowHeadersWidth = 51;
            this.dgrid_TableData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrid_TableData.Size = new System.Drawing.Size(957, 261);
            this.dgrid_TableData.TabIndex = 4;
            this.dgrid_TableData.MouseDown += Dgrid_TableData_MouseDown;
            // 
            // dgrid_Search
            // 
            this.dgrid_Search.AllowDrop = true;
            this.dgrid_Search.AllowUserToAddRows = false;
            this.dgrid_Search.AllowUserToDeleteRows = false;
            this.dgrid_Search.AllowUserToResizeRows = false;
            this.dgrid_Search.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgrid_Search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrid_Search.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrid_Search.Location = new System.Drawing.Point(4, 4);
            this.dgrid_Search.Margin = new System.Windows.Forms.Padding(4);
            this.dgrid_Search.MultiSelect = false;
            this.dgrid_Search.Name = "dgrid_Search";
            this.dgrid_Search.RowHeadersWidth = 51;
            this.dgrid_Search.Size = new System.Drawing.Size(957, 83);
            this.dgrid_Search.TabIndex = 3;
            this.dgrid_Search.CellValueChanged += Dgrid_Search_CellValueChanged;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 360);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(965, 35);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(2, 5, 5, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(100, 28);
            this.toolStripStatusLabel2.Text = "View History";
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.ToolStripStatusLabel2Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 29);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 29);
            // 
            // bgwConnectToDB
            // 
            this.bgwConnectToDB.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwConnectToDB);
            this.bgwConnectToDB.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgwConnectToDB_ProgressChanged);
            this.bgwConnectToDB.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgwConnectToDB_Completed);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            // 
            // tmrTableData
            // 
            this.tmrTableData.Interval = 1000;
            this.tmrTableData.Tick += new System.EventHandler(this.tmrTableData_Tick);
            // 
            // contextMenuStripTable
            // 
            this.contextMenuStripTable.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStripTable.Name = "contextMenuStripTable";
            this.contextMenuStripTable.Size = new System.Drawing.Size(215, 52);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(214, 24);
            this.toolStripMenuItem1.Text = "Select Top 500 Rows";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelect,
            this.tsmiInsert,
            this.tsmiUpdate,
            this.tsmiDelete});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(214, 24);
            this.toolStripMenuItem2.Text = "Script Table as";
            // 
            // tsmiSelect
            // 
            this.tsmiSelect.Name = "tsmiSelect";
            this.tsmiSelect.Size = new System.Drawing.Size(146, 26);
            this.tsmiSelect.Text = "SELECT";
            this.tsmiSelect.Click += new System.EventHandler(this.tsmiSelectClick);
            // 
            // tsmiInsert
            // 
            this.tsmiInsert.Name = "tsmiInsert";
            this.tsmiInsert.Size = new System.Drawing.Size(146, 26);
            this.tsmiInsert.Text = "INSERT";
            this.tsmiInsert.Click += new System.EventHandler(this.tsmiInsertClick);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(146, 26);
            this.tsmiUpdate.Text = "UPDATE";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdateClick);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(146, 26);
            this.tsmiDelete.Text = "DELETE";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDeleteClick);
            // 
            // tmrTimeOut_TableData
            // 
            this.tmrTimeOut_TableData.Interval = 60000;
            this.tmrTimeOut_TableData.Tick += new System.EventHandler(this.tmrTimeOut_TableData_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 657);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "DB Lookup";
            this.Load += new System.EventHandler(this.Form1Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.Tlp_OpenConnection.ResumeLayout(false);
            this.Tlp_OpenConnection.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.Tlp_Datagrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_TableData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_Search)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApplyChangesToDB;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.ComponentModel.BackgroundWorker bgwConnectToDB;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer tmrTableData;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeDBtables;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsert;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.DataGridView dgrid_Search;
        private System.Windows.Forms.TableLayoutPanel Tlp_Datagrids;
        private System.Windows.Forms.DataGridView dgrid_TableData;
        private System.Windows.Forms.TableLayoutPanel Tlp_OpenConnection;
        private System.Windows.Forms.ComboBox cmbConnectString;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCollapseTree;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Button btnScriptTableData;
        private System.Windows.Forms.Button btnSaveChangesToProjectAndDB;
        private CheckBox chkTruncateFirst;
        private Button btnClearDataTableFilter;
        private Timer tmrTimeOut_TableData;
        private Button btnDeleteRecordsFromTableGrid;
    }
}

