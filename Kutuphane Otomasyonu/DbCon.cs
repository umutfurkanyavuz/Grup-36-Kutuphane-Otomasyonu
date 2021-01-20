using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using Oracle.DataAccess.Client;

namespace Kutuphane_Otomasyonu
{
    class DbCon
    {
        public OracleConnection connection()
        {
            OracleConnection baglanti = new OracleConnection("User Id = SYSTEM;Password = Frkn1907.;" +
                "Data Source = localhost:1521/XEPDB1;");
            baglanti.Open();
            return baglanti;
        }

    }
}
