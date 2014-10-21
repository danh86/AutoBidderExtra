using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace FUTAutoBuyer.Native
{
    public class ABDatabase
    {
        public DataSet RunProcedure(string sprocName, List<SqlParameter> sqlParams)
        {
            DataSet ds = new DataSet();
            SqlConnection conn;
            SqlCommand cmd = new SqlCommand();

            conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionInfo"]);
            cmd = new SqlCommand(sprocName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //add in params
            foreach (SqlParameter sp in sqlParams)
            {
                cmd.Parameters.Add(sp);
            }

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);

            conn.Close();

            return ds;
        }
    }
}
