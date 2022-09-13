using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms; 
using System.Data.SqlClient;
using System.IO;
using System.Net;
using MySql.Data.MySqlClient;
using Utilities.ExMethod;

namespace Utilities.Data
{
    public class DBConnInfo : ICloneable
    {
        public DBMSType dbType;
        public string dbName { get; set; }
        public string host { get; set; }
        public string pwd { get; set; }
        public string user { get; set; }
        public string port { get; set; }

        public string SqlString
        {
            get
            {
                string ConnString = "";
                if (dbType == DBMSType.MySQL)
                {
//                     ConnString = string.Format("Server={0};Port={1};Database={2};Username={3};Password={4};Connect Timeout=60;CharSet=utf8;" +
//                        "Allow Zero Datetime = true;pooling=true", host, port, dbName, user, pwd);
                    ConnString = string.Format("Server={0};Port={1};Database={2};Username={3};Password={4};Connect Timeout=30;CharSet=utf8;" +
                     "compress=true;Allow Zero Datetime = true;pooling=true;Pipe Name=MySQL", host, port, dbName, user, pwd);

                }
                else if (dbType == DBMSType.SqlServer)
                {
                    ConnString = string.Format("Server={0},{1};Database={2};User ID={3};Password={4};Connect Timeout=30",
                                       host, port, dbName, user, pwd);

                }
                return ConnString;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class DBBackInfo
    {
        public string backname { get; set; }
        public string database { get; set; }
        public string path { get; set; }
        public DateTime date { get; set; }
        public bool exist { get; set; }
    }

    /// <summary>
    /// 数据库系统类型枚举
    /// </summary>
    public enum DBMSType
    {
        MySQL = 0,
        SqlServer = 1,
        Oracle = 2
    }


    public class DbOperation
    {
        private DBConnInfo ConnInfo;
        private DbConnection sqlConn = null;
        public DbOperation(DBConnInfo conn)
        {
            ConnInfo = conn;
        }

        public DbConnection SqlConn
        {
            get
            {
                if (sqlConn == null)
                    InitConnection();
                if (sqlConn.State != ConnectionState.Open)
                    InitConnection();
                return sqlConn;
            }
        }

        public bool InitConnection(bool showErr = true)
        {
            string ConnString = ConnInfo.SqlString;
            try
            {
                // ConnInfo.dbType = (DBMS)Convert.ToInt16(MyRegistryKey.GetValue("DBMS"));    
                sqlConn = GetConn(ConnString, ConnInfo.dbType);
                if (sqlConn == null)
                    return false;
                sqlConn.Open();
                if (sqlConn.State != ConnectionState.Open)
                {
                    DebugLog.WriteLine("数据库打开失败。连接字符串：" + ConnString);
                    return false;
                }
              
                return true;
            }
            catch (System.Exception ex)
            {
                DebugLog.WriteLine("数据库打开失败。异常" + ex.Message + "\r\n连接字符串：" + ConnString);
                if (showErr)
                    MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw (new Exception("Failed to open MySql database.Error message:" + ex.Message));
                return false;
            }
        }

        public DbConnection GetConn(string conn, DBMSType db)
        {
            DbConnection sqlConn = null;
            if (db == DBMSType.MySQL)
            {
                sqlConn = new MySqlConnection(conn);
                /*var myconnString = new MySqlConnectionStringBuilder(conn);
                myconnString.PipeName = "MySQL";
                myconnString.ConnectionProtocol = MySqlConnectionProtocol.Pipe;
                sqlConn = new MySqlConnection(myconnString.ConnectionString);*/
            }
            else if (db == DBMSType.SqlServer)
            {
                sqlConn = new SqlConnection(conn);
            }
            return sqlConn;
        }
        public void Close()
        {
            if (sqlConn != null && sqlConn.State == ConnectionState.Open)
                try { sqlConn.Close(); }
                catch { }
        }
        /// <summary>
        /// 测试数据库连接，Exception
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool TestConn(DBConnInfo conn)
        {
            if (conn.host.IsEmpty() || conn.dbName.IsEmpty() || conn.port.IsEmpty() || conn.user.IsEmpty())
                return false;
            using (var mysqlConn = GetConn(conn.SqlString, conn.dbType))
            { 
                mysqlConn.Open();
                if (mysqlConn.State == ConnectionState.Open)
                {
                    mysqlConn.Close();
                    return true;
                }
            }
            return false;
        }
        public string SelectTop(int top, string select, string table, string where = null)
        {
            string strsql = "";
            if (ConnInfo.dbType == DBMSType.MySQL)
            {
                strsql = "select " + select + " from " + table;
                if (!string.IsNullOrEmpty(where))
                    strsql += " where " + where;
                strsql += "  limit 0," + top;
            }
            else if (ConnInfo.dbType == DBMSType.SqlServer)
            {
                strsql = "select top " + top + " " + select + " from " + table;
                if (!string.IsNullOrEmpty(where))
                    strsql += " where " + where;
            }
            return strsql;
        }
        public bool Exist(string table, string where, string select = "*")
        {
            string strsql = "";
            if (ConnInfo.dbType == DBMSType.MySQL)
                strsql = "select " + select + " from " + table + " where " + where + "  limit 0,1";
            else if (ConnInfo.dbType == DBMSType.SqlServer)
                strsql = "select top 1 * from " + table + " where " + where;
            try
            {
                var reader = GetReader(strsql);
                bool b = reader.Read();
                reader.Close();
                return b;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine(ex.Message + "\r\n" + strsql);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public int ExecuteSql(string strsql, DbParameter[] parameters = null,bool catchException=true)
        {
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
                {
                    sqlConn.Open();
                    DbCommand myCommand = sqlConn.CreateCommand();
                    myCommand.CommandText = strsql;
                    myCommand.CommandTimeout = 60000;
                    if (parameters != null)
                    {
                        myCommand.Parameters.Clear();
                        myCommand.Parameters.AddRange(parameters);
                    }
                    int n = myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    return n;
                }
            }
            catch (System.Exception ex)
            {
                if (catchException)
                {
                    DebugLog.WriteLine("ExecuteSql Exception:" + ex.Message + "\r\n" + strsql);
                    MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else throw ex;
                // throw (new Exception("Execute SQL unsuccessfully!.Error message:" + ex.Message));
            }
            return -1;
        }
        public int ExecuteSqlTrans(string strsql)
        {
            using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
            {
                sqlConn.Open();
                DbTransaction myTrans = sqlConn.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    DbCommand myCommand = sqlConn.CreateCommand();
                    myCommand.Transaction = myTrans;
                    myCommand.CommandText = strsql;
                    myCommand.CommandTimeout = 36000;
                    int n = myCommand.ExecuteNonQuery();
                    myTrans.Commit();
                    myCommand.Dispose();
                    return n;
                }
                catch (System.Exception ex)
                {
                    myTrans.Rollback();
                    DebugLog.WriteLine("ExecuteSql Exception:" + ex.Message + "\r\n" + strsql);
                    MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //  throw (new Exception("Execute SQL unsuccessfully!.Error message:" + ex.Message));
                }
            }


            return -1;
        }
        public bool ExecuteSql(IList<string> sTextArray)
        {
            int n = 0;
            DbTransaction myTrans;
            string sTmp = string.Empty;
            using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
            {
                sqlConn.Open();
                myTrans = sqlConn.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    DbCommand myCommand = sqlConn.CreateCommand();
                    myCommand.Transaction = myTrans;
                    myCommand.CommandTimeout = 36000;
                    foreach (string sText in sTextArray)
                    {
                        myCommand.CommandText = sText;
                        sTmp = sText;
                        n += myCommand.ExecuteNonQuery();
                    }
                    myTrans.Commit();
                    myCommand.Dispose();
                    return true;
                }
                catch (System.Exception ex)
                {
                    myTrans.Rollback();
                    DebugLog.WriteLine("ExecuteSql Exception:" + ex.Message + "\r\n" + sTmp);
                    MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //  throw (new Exception("Execute SQL unsuccessfully!.Error message:" + ex.Message));
                }
            }
            return false;
        }
        public DbDataReader GetReader(string sql)
        {
            try
            {
                var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType);
                sqlConn.Open();
                var cmd = sqlConn.CreateCommand();
                cmd.CommandTimeout = 36000;
                cmd.CommandText = sql;
                var r = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Dispose();
                return r;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine("GetReader Exception:" + ex.Message + ".\r\nSql String:" + sql);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public DataSet GetDataSet(string strsql)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
                {
                    DbDataAdapter adapter = GetAdapter(sqlConn, strsql);
                    if (adapter == null)
                        return ds;
                    adapter.Fill(ds);
                    adapter.Dispose();
                }
                return ds;
            }
            catch (System.Exception ex)
            {
                DebugLog.WriteLine("GetDataSet Exception:" + ex.Message + ".\r\nSql String:" + strsql);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // throw (new Exception("An exception occurs when reading records from database.Error message:" + ex.Message));
            }
            return ds;
        }
        public DataSet GetDataSet(string[] strsql)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var SqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
                {
                    var cmd = SqlConn.CreateCommand();
                    cmd.CommandTimeout = 36000;
                    DbDataAdapter adapter = null;
                    if (ConnInfo.dbType == DBMSType.MySQL)
                        adapter = new MySqlDataAdapter(cmd as MySqlCommand);
                    else if (ConnInfo.dbType == DBMSType.SqlServer)
                        adapter = new SqlDataAdapter(cmd as SqlCommand);
                    else
                        return ds;
                    for (int i = 0; i < strsql.Length; i++)
                    {
                        cmd.CommandText = strsql[i];
                        adapter.Fill(ds, i.ToString());
                    }
                    adapter.Dispose();
                    cmd.Dispose();
                }

                return ds;
            }
            catch (System.Exception ex)
            {
                DebugLog.WriteLine("GetDataSet Exception:" + ex.Message + ".\r\nSql String:" + strsql);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  MessageBox.Show("An exception occurs when Reading records from database.Error message:" + ex.Message,   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ds;
        }
        public DataTable GetDataTable(string strsql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
                {
                    DbDataAdapter adapter = GetAdapter(sqlConn, strsql);
                    if (adapter == null)
                        return dt;
                    adapter.Fill(dt);
                    adapter.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                DebugLog.WriteLine("GetDataTable Exception:" + ex.Message + ".\r\nSql String:" + strsql);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw (new Exception("An exception occurs when reading records from database.Error message:" + ex.Message));
            }
            return dt;
        }
        public void UpdateDataTable(string SelectSql, Action<DataTable> act)
        {
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
                {
                    DataTable dt = new DataTable();
                    var adapter = GetAdapter(sqlConn, SelectSql);
                    DbCommandBuilder builder = null;
                    if ((ConnInfo.dbType == DBMSType.MySQL))
                        builder = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
                    if (ConnInfo.dbType == DBMSType.SqlServer)
                        builder = new SqlCommandBuilder((SqlDataAdapter)adapter);
                    if (builder != null)
                    {
                        adapter.Fill(dt);
                        act(dt);
                        adapter.SelectCommand.CommandText = SelectSql;
                        builder.RefreshSchema();
                        adapter.Update(dt);
                        dt.AcceptChanges();
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                DebugLog.WriteLine("UpdateDataTable Exception:" + ex.Message);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine("UpdateDataTable Exception:" + ex.Message);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void UpdateDataSet(string[] SelectSqls, Action<DataSet> act)
        {
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlString, ConnInfo.dbType))
                {
                    DataSet ds = new DataSet();
                    var cmd = sqlConn.CreateCommand();
                    cmd.CommandTimeout = 36000;
                    DbDataAdapter adapter = null;
                    if (ConnInfo.dbType == DBMSType.MySQL)
                        adapter = new MySqlDataAdapter(cmd as MySqlCommand);
                    else if (ConnInfo.dbType == DBMSType.SqlServer)
                        adapter = new SqlDataAdapter(cmd as SqlCommand);
                    else
                        return;
                    for (int i = 0; i < SelectSqls.Length; i++)
                    {
                        cmd.CommandText = SelectSqls[i];
                        adapter.Fill(ds, i.ToString());
                    }
                    DbCommandBuilder builder = null;
                    if ((ConnInfo.dbType == DBMSType.MySQL))
                        builder = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
                    if (ConnInfo.dbType == DBMSType.SqlServer)
                        builder = new SqlCommandBuilder((SqlDataAdapter)adapter);
                    if (builder != null)
                    {
                        act(ds);
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            var dt = ds.Tables[i];
                            adapter.SelectCommand.CommandText = SelectSqls[i];
                            builder.RefreshSchema();
                            adapter.Update(ds, dt.TableName);
                            dt.AcceptChanges();
                        }
                    }
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine("UpdateDataTable Exception:" + ex.Message);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public DbDataAdapter GetAdapter(DbCommand cmd)
        {
            DbDataAdapter adapter = null;
            if (ConnInfo.dbType == DBMSType.MySQL)
                adapter = new MySqlDataAdapter(cmd as MySqlCommand);
            else if (ConnInfo.dbType == DBMSType.SqlServer)
                adapter = new SqlDataAdapter(cmd as SqlCommand);
            cmd.Dispose();
            return adapter;
        }
        DbDataAdapter GetAdapter(DbConnection SqlConn, string sql)
        {
            var cmd = SqlConn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = 36000;
            return GetAdapter(cmd);
        }

        public bool ExistTable(string tableName)
        {
            return GetDataTable("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" + tableName + "'").Rows.Count > 0;

        }
       
        public string[] GetColumns(string tableName)
        {
            return GetDataTable(string.Format("select * from {0} where 1>2", tableName)).Columns.Cast<DataColumn>().Select(p=>p.ColumnName).ToArray();
        }
    }
}
