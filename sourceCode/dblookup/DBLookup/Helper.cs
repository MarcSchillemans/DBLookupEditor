using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DBLookup
{
    internal static class Helper
    {
        /// <summary>
        /// Masks the username and password from a connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static internal string RemoveConnectionStringSecurity(string connectionString)
        {
            var securityQualifiers = new[] { "user", "uid", "password", "pwd", "user id" };

            return securityQualifiers.Aggregate(connectionString, (current, qualifier)
                => Regex.Replace(current, qualifier + "\\s*=([^;]*)(?:$|;)", qualifier + "=********;", RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// Used to remove the data type from a column
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static internal string FormatColumnName(TreeNode node)
        {
            try
            {
                return "[" + node.Text.Substring(0, node.Text.IndexOf("(", System.StringComparison.Ordinal) - 1) + "]";
            }
            catch
            {
                return node.Text;
            }
        }
        static internal string FormatColumnName(string sVal)
        {
            return "[" + sVal + "]";
        }

        /// <summary>
        /// Used to remove the data type from a column
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static internal string FormatTableName(TreeNode node)
        {
            try
            {
                return "[" + node.Text.Replace(".", "].[") + "]";
            }
            catch
            {
                return node.Text;
            }
        }

        static internal string FormatTableName(string sVal)
        {
            return "[" + sVal.Replace(".", "].[") + "]";
        }
    }
}
