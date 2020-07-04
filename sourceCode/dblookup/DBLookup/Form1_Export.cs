using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace DBLookup
{
    public class Justafakeclass
    {
    }
    public partial class Form1 : Form
    {
        private int UpdateGridDataToDB(string sSelectQuery, String ConnectString, DataTable table, String sAction)
        {
            int res = 0;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(sSelectQuery, sqlConnection, sqlTransaction);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlDataAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                sqlDataAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
                sqlDataAdapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();

                sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                try
                {
                    switch (sAction)
                    {
                        case "Update":
                            res = sqlDataAdapter.Update(table);
                            break;
                        case "Delete":
                            //res = sqlDataAdapter.DeleteCommand(table);
                            break;
                        case "Insert":
                            //res = sqlDataAdapter.Insert(table);
                            break;
                    }
                    sqlTransaction.Commit();
                    MessageBox.Show(sAction + " was successful");

                }
                catch (System.Data.SqlClient.SqlException exc)
                {
                    sqlTransaction.Rollback();
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            return res;
        }

        private void LogAction(String sSubject, String sMessage)
        {
            // log history
            using (StreamWriter file = new StreamWriter(
                Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName) + ".log", true))
            {
                file.WriteLine(toolStripStatusLabel1.Text);
                file.WriteLine("{0} : {1}", sSubject, Helper.RemoveConnectionStringSecurity(sMessage));
            }

        }

        private String Get_Query_From_SqlBox()
        {
            //pick select sql first if empty then take all
            String sTruncate = GetTruncateText();
            String sSQL = txtSQL.Text.Trim();
            if (sTruncate != "" && !sSQL.Contains(sTruncate))
                sSQL = sTruncate + sSQL;
            return sSQL;
        }
        void ExportTableDataToCsv(object sender, MouseEventArgs e)
        {
            //test to see if the DataGridView has any rows
            if (dgrid_TableData.RowCount > 0)
            {

                DialogResult saveDialog = saveFileDialog1.ShowDialog();
                if (saveDialog != DialogResult.OK) return;

                StreamWriter swOut = new StreamWriter(saveFileDialog1.FileName);

                //write header rows to csv
                for (int i = 0; i <= dgrid_TableData.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }
                    swOut.Write(dgrid_TableData.Columns[i].HeaderText);
                }

                swOut.WriteLine();

                //write DataGridView rows to csv
                for (int j = 0; j <= dgrid_TableData.Rows.Count - 1; j++)
                {
                    if (j > 0)
                        swOut.WriteLine();

                    DataGridViewRow dr = dgrid_TableData.Rows[j];

                    for (int i = 0; i <= dgrid_TableData.Columns.Count - 1; i++)
                    {
                        if (i > 0)
                            swOut.Write(",");

                        String sValue = dr.Cells[i].Value.ToString();
                        //replace comma's with spaces
                        sValue = sValue.Replace(',', ' ');
                        //replace embedded newlines with spaces
                        sValue = sValue.Replace(Environment.NewLine, " ");

                        swOut.Write(sValue);
                    }
                }
                swOut.Close();
            }
            else
                MessageBox.Show(@"Nothing to export");
        }

        void ExportCsv(object sender, MouseEventArgs e)
        {
            String sText = txtSQL.Text.Trim();
            if (sText != "")
            {

                DialogResult saveDialog = saveFileDialog1.ShowDialog();
                if (saveDialog != DialogResult.OK) return;

                StreamWriter swOut = new StreamWriter(saveFileDialog1.FileName);
                swOut.Write(sText);
                swOut.Close();
            }
            else
                MessageBox.Show(@"Nothing to export");
        }
    }
}

