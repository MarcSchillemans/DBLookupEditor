using System;
using System.Data;

namespace DBLookup
{
    public class ExecuteResults
    {
        public string StatusMessage { get; set; }

        public DataTable ResultsData { get; set; }

        public Exception ExceptionDetails { get; set; }
    }

    public class ExecuteParameters
    {
        //private string provider { get; set; }

        public string SqlStatement { get; set; }

        public string ConnectionString { get; set; }
    }

}
