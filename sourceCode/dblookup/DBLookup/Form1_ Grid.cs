using System;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace DBLookup
{
    public class fake2
    {

    }
    public partial class Form1 : Form
    {
        private void ShowData(TreeView tree, TreeNodeCollection tableNode)
        {
            mod_sTableInGrid = tree.SelectedNode.Text;
            GetTable(mod_sCurrentConnection ,GetSelectQueryFromNode(tree, tableNode));
            tmrTableData.Enabled = true;
            mod_bTimedOut = false;

            while (bgwConnectToDB.IsBusy && !mod_bTimedOut) { Application.DoEvents(); }
            tmrTableData.Enabled = false;
            if (!mod_bTimedOut)
                Search_getTableColumns(treeDBtables.SelectedNode.Nodes, dgrid_Search);
            mod_bTimedOut = false;
        }

        private void ClearTableDataFilter()
        {
            if (TableHasRows())
            {
                DataGridViewRow row = dgrid_Search.Rows[0];
                for (int i = 0; i < (row.Cells.Count - 1); i++)
                    row.Cells[i].Value = "";
            }

        }

        private Boolean TableHasRows()
        {
            return (dgrid_TableData.Rows.Count > 0);
        }
        private String AddSingleQuotes(String sValue, DataGridViewCell oCell)
        {
            //first make contents safe
            sValue.Replace("'", "''");
            //add quotes to the outside of the string
            return (CellValueIsString(oCell) ? "'" + sValue + "'" : sValue);
        }
        private bool CellValueIsString(DataGridViewCell oCell)
        {
            return oCell.ValueType.ToString().Contains("String");
        }

        private String AddMiliseconds(String sValue)
        {
            return (HasMiliseconds(sValue) ? ".000" : "");
        }

        private bool HasMiliseconds(String dtTimeDate)
        {
            return dtTimeDate.Contains(".");
        }

        private String GetColumnDataType(DataGridView dgrid, int iColumnIndex)
        {
            //get tag with datatype in it
            String sColumn = dgrid.Columns[iColumnIndex].Name;
            String sSchemaTable = mod_sTableInGrid + '.' + sColumn;
            String sDataType = treeDBtables.Nodes.Find(sSchemaTable, true).FirstOrDefault().Tag.ToString();

            return sDataType;
        }

        private String CreateConversionScript(String sDataType, DataGridViewCell cell)
        {
            String sConvertedValue = ""; // sValue;
            String sValue = cell.Value.ToString();
            switch (sDataType)
            {
                case "date": // ((DateTime)row.Cells[4].Value).ToString("MM-dd-yyyy");
                    sConvertedValue = "'" + ((DateTime)cell.Value).ToString("yyyyMMdd") + "'";
                    break;
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    sConvertedValue = "'" + ((DateTime)cell.Value).ToString("yyyyMMdd HH:mm:ss") + AddMiliseconds(sValue) + "'";
                    break;
                case "time":
                    sConvertedValue = "'" + ((DateTime)cell.Value).ToString("HH:mm:ss") + AddMiliseconds(sValue) + "'";
                    break;
                case "decimal":
                case "float":
                case "money":
                case "numeric":
                    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                    sConvertedValue = ((decimal)cell.Value).ToString("N", nfi);
                    break;
                default:
                    sConvertedValue = AddSingleQuotes(cell.Value.ToString(), cell);
                    break;
            }
            return sConvertedValue;

        }

        private void ReadFromDataGrid(DataGridView dgrid)
        {
            txtSQL.Clear();
            String txt = "";
            // Given a DataTable, retrieve a DataTableReader
            // allowing access to all the tables' data:
            String sbColumnHeaders = "";

            foreach (DataGridViewColumn col in dgrid.Columns)
                sbColumnHeaders += (sbColumnHeaders == "" ? "" : ", ") + Helper.FormatColumnName(col.Name);

            txt += "----------- " + mod_sTableInGrid + " ----------"
                + Environment.NewLine + GetTruncateText()
                + Environment.NewLine + "INSERT INTO " + Helper.FormatTableName(mod_sTableInGrid) + " (" + sbColumnHeaders + ")"
                + Environment.NewLine + "SELECT " + sbColumnHeaders
                + Environment.NewLine + "FROM ("
                + Environment.NewLine + "   VALUES";

            //get values from eachrow
            bool bIsFirstRow = true;

            foreach (DataGridViewRow row in dgrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    bool IsFirstColumn = true;
                    String sRowValues = "";
                    for (int i = 0; i < (row.Cells.Count - 1); i++)
                    {
                        //Prepare var for use in queries
                        String sDataType = GetColumnDataType(dgrid, row.Cells[i].ColumnIndex);
                        String sConvertedValue = CreateConversionScript(sDataType, row.Cells[i]);
                        sRowValues += (IsFirstColumn ? "" : ", ") + sConvertedValue;
                        IsFirstColumn = false;
                    }
                    txt += Environment.NewLine + "    " + (bIsFirstRow ? "  " : ", ") + "(" + sRowValues + ")";
                    bIsFirstRow = false;
                }
            }
            txt += Environment.NewLine + ") AS REF(" + sbColumnHeaders + ")";
            txtSQL.Text = txt;
        }

        private String GetTruncateText()
        {
            String sTruncate = "";
            if (TableHasRows() && chkTruncateFirst.Checked)
            {
                sTruncate = "TRUNCATE TABLE " + mod_sTableInGrid + Environment.NewLine;
            }
            return sTruncate;
        }

    }
}

