using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Crypto_Database 
{
    class conectionstr 
    {
        private static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + @"\DbHash.accdb ;Jet OLEDB:Database Password=Test++";

        public static string conected
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
    }
}
