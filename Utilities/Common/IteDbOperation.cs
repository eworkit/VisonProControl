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

namespace IteUtils
{
    public class DBConnInfo : ICloneable
    {
        public string dbName { get; set; }
        public string host { get; set; }
        public string pwd { get; set; }
        public string user { get; set; }
        public string port { get; set; }
        public string SqlStr(DBMSType db)
        {

            string ConnString = "";
            if (db == DBMSType.MySQL)
            {
                ConnString = string.Format("Server={0};Port={1};Database={2};Username={3};Password={4};Connect Timeout=60;CharSet=utf8;" +
                    "Allow Zero Datetime = true;", host, port, dbName, user, pwd);

            }
            else if (db == DBMSType.SqlServer)
            {
                ConnString = string.Format("Server={0},{1};Database={2};User ID={3};Password={4};Connect Timeout=60",
                                   host, port, dbName, user, pwd);

            }
            return ConnString;
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

    public enum DBMode
    {
        Single, Double
    }
    public class DBConfigInfo
    {
        public DBMSType dbType;
        public DBMode dbMode;
        public DBConnInfo dbConnInfo;
        public DBConnInfo dbTestRunConnInfo;
        public DBConfigInfo()
        {
            dbType = DBMSType.MySQL;
            dbMode = DBMode.Single;
            dbConnInfo = new DBConnInfo();
            dbTestRunConnInfo = new DBConnInfo();
        }
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


    public  class DbOperation
    {
        private  DBConnInfo ConnInfo ;
        private  DbConnection sqlConn = null;
        private  DBMSType _s_dbms;
        public DbOperation(DBMSType db, DBConnInfo conn)
        {
            _s_dbms = db;
            ConnInfo = conn; 
        }

        public  DbConnection SqlConn
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

        public bool InitConnection(bool showErr=true)
        {
            string ConnString = ConnInfo.SqlStr(_s_dbms);
            try
            {
                // _s_dbms = (DBMS)Convert.ToInt16(MyRegistryKey.GetValue("DBMS"));    
                sqlConn = GetConn(ConnString, _s_dbms);
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
                    MessageBox.Show(ex.Message,"Info" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw (new Exception("Failed to open MySql database.Error message:" + ex.Message));
                return false;
            }
        }
        
        public  DbConnection GetConn(string conn, DBMSType db)
        { 
            DbConnection sqlConn = null;
            if (db == DBMSType.MySQL)
                sqlConn = new MySqlConnection(conn);
            else if (db == DBMSType.SqlServer)
                sqlConn = new SqlConnection(conn);
            return sqlConn;
        }
        public  void Close()
        {
            if (sqlConn != null && sqlConn.State == ConnectionState.Open)
                try { sqlConn.Close(); }
                catch { }
        }

        public bool TestConn(DBConnInfo conn, DBMSType db)
        {
            try
            {
                string ConnString = conn.SqlStr(db);
                using (var mysqlConn = GetConn(ConnString, db))
                {
                    mysqlConn.Open();
                    if (mysqlConn.State == ConnectionState.Open)
                    {
                        mysqlConn.Close();
                        return true;
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
            return false;
        }

        public bool Exist(string table, string where)
        {
            string strsql = "";
            if (_s_dbms == DBMSType.MySQL)
                strsql = "select * from " + table + " where " + where + "  limit 0,1";
            else if (_s_dbms == DBMSType.SqlServer)
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
        public int ExecuteSql(string strsql)
        {
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms))
                {
                    sqlConn.Open();
                    DbCommand myCommand = sqlConn.CreateCommand();
                    myCommand.CommandText = strsql;
                    int n = myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                    return n;
                }
            }
            catch (System.Exception ex)
            {            
                DebugLog.WriteLine("ExecuteSql Exception:" + ex.Message + "\r\n" + strsql);
                MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // throw (new Exception("Execute SQL unsuccessfully!.Error message:" + ex.Message));
            }
            return -1;
        }
        public int ExecuteSqlTrans(string strsql)
        {
            using (var sqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms))
            {
                sqlConn.Open();
                DbTransaction myTrans = sqlConn.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    DbCommand myCommand = sqlConn.CreateCommand();
                    myCommand.Transaction = myTrans;
                    myCommand.CommandText = strsql;
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
        public bool ExecuteSql(string[] sTextArray)
        {
            int n = 0;
            DbTransaction myTrans;
            string sTmp = string.Empty;
            using (var sqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms))
            {
                sqlConn.Open();
                myTrans = sqlConn.BeginTransaction(IsolationLevel.ReadUncommitted);
                try
                {
                    DbCommand myCommand = sqlConn.CreateCommand();
                    myCommand.Transaction = myTrans;
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
        public  DbDataReader GetReader(string sql)
        {
            try
            {
                var sqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms);
                sqlConn.Open();
                var cmd = sqlConn.CreateCommand();
                cmd.CommandText = sql;
                var r = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 cmd.Dispose();
                return r;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLine("GetReader Exception:" + ex.Message+".\r\nSql String:"+sql);
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
                using (var sqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms))
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
        public  DataSet GetDataSet(string[] strsql)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var SqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms))
                { 
                    var cmd = SqlConn.CreateCommand();
                    DbDataAdapter adapter = null;
                    if (_s_dbms == DBMSType.MySQL)
                        adapter = new MySqlDataAdapter(cmd as MySqlCommand);
                    else if (_s_dbms == DBMSType.SqlServer)
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
        public  DataTable GetDataTable(string strsql)
        {
            DataTable dt = new DataTable(); 
            try
            {
                using (var sqlConn = GetConn(ConnInfo.SqlStr(_s_dbms), _s_dbms))
                {
                    DbDataAdapter adapter = GetAdapter(sqlConn,strsql);
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

        DbDataAdapter GetAdapter(DbConnection SqlConn, string sql)
        {
            var cmd = SqlConn.CreateCommand();
            cmd.CommandText = sql;
            DbDataAdapter adapter = null;
            if (_s_dbms == DBMSType.MySQL)
                adapter = new MySqlDataAdapter(cmd as MySqlCommand);
            else if (_s_dbms == DBMSType.SqlServer)
                adapter = new SqlDataAdapter(cmd as SqlCommand);
            cmd.Dispose();
            return adapter;
        }
    }
}
