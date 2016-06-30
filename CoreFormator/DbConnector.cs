using System;
using System.Data;

using IBM.Data.Informix;

namespace CoreFormator
{
    public static class DbConnector
    {
        public static DataTable OpenSql(string sqlString, DbEnum db)
        {
            var existTable = new DataTable("");
            IfxTransaction transaction = null;
            try
            {
                string connectionString;
                switch (db)
                {
                    case DbEnum.css_pmi:
                        connectionString = "Server=ol_css;Host=192.168.224.34;Service=9088;Database=css_pmi;Password=portal_css;User ID=portal_css;Client Locale=ru_ru.CP1251;Database Locale=ru_ru.915;Max Pool Size=500;Pooling=True;Protocol=olsoctcp;Connection Lifetime=1200;Connection Timeout=60;";
                        break;
                    case DbEnum.css_work:
                        connectionString = "Server=ol_css;Host=192.168.224.34;Service=9088;Database=css_work;Password=portal_css;User ID=portal_css;Client Locale=ru_ru.CP1251;Database Locale=ru_ru.915;Max Pool Size=500;Pooling=True;Protocol=olsoctcp;Connection Lifetime=1200;Connection Timeout=60;";
                        break;
                    default:
                        throw new DBConcurrencyException("Не удалось определить connectionString");
                }

                using (IfxConnection connection = new IfxConnection(connectionString))
                {
                    connection.Open();
                    using (IfxCommand myCommand = new IfxCommand(sqlString, connection, transaction))
                    {
                        using (IfxDataReader reader = myCommand.ExecuteReader(CommandBehavior.SingleResult))
                        {
                            existTable.Load(reader, LoadOption.OverwriteChanges);
                            reader.Close();
                            myCommand.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return existTable;
        }
    }

    public enum DbEnum
    {
        css_work,
        css_pmi
    }
}