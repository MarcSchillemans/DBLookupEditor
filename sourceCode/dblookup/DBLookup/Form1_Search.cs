using System;
using System.Data;
using System.Windows.Forms;


namespace DBLookup
{
    public class noreason
    {

    }
    public partial class Form1 : Form
    {
        private void Search_getTableColumns(TreeNodeCollection tableNode, DataGridView gridSearch)
        {
            mod_dtSearch.Clear();
            mod_dtSearch.Rows.Clear();
            mod_dtSearch.Columns.Clear();

            String sVal;
            foreach (TreeNode node in tableNode)
            {
                sVal = Helper.FormatColumnName(node).ToString().Replace("[", "").Replace("]", "");
                DataColumn newCol = mod_dtSearch.Columns.Add(sVal, typeof(string));
            }
            mod_dtSearch.Rows.Add();
            gridSearch.DataSource = mod_dtSearch;

            foreach (DataGridViewColumn col in gridSearch.Columns)
                col.Width = dgrid_TableData.Columns[col.Index].Width;
        }

        private void Dgrid_Search_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            String sColumnName = ""; String sValue = ""; String sFilter = ""; bool IsFirstFilter = true;
            // create filter
            if (TableHasRows())
            {
                DataGridViewRow row = dgrid_Search.Rows[0];
                for (int i = 0; i < (row.Cells.Count - 1); i++)
                {
                    sColumnName = dgrid_Search.Columns[i].Name;
                    sValue = dgrid_Search.Rows[e.RowIndex].Cells[i].Value.ToString().Trim();
                    if (sValue != "")
                    {
                        sFilter += (IsFirstFilter ? "" : " AND ");
                        sFilter += String.Format("([{0}] LIKE '%{1}%')", sColumnName, sValue);
                        IsFirstFilter = false;
                    }
                }
                // apply filter
                (dgrid_TableData.DataSource as DataTable).DefaultView.RowFilter = sFilter;
            }
        }
    }
}


