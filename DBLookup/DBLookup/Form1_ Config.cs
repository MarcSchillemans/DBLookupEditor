using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace DBLookup
{
    public class fakeclass2 { }

    public partial class Form1 : Form
    {
        private List<String> GetConnectionStrings()
        {
            List<String> connectionStrings = new List<String>();

            foreach(string key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key.Contains("ConnectionString"))
                {
                    var connectionString = ConfigurationManager.AppSettings[key];
                    connectionStrings.Add (connectionString);
                }
            }
            return connectionStrings;
        }
        private List<String> GetLookupTables()
        {
            List<String> ls = new List<String>();

            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key.Contains("LookupTable"))
                {
                    var l = ConfigurationManager.AppSettings[key];
                    ls.Add(l);
                }
            }
            return ls;
        }

    }


}