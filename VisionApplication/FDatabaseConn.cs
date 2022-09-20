﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Utilities;
using Utilities.Data;
using Utilities.ExMethod;
using VisionControl;

namespace VisionApplication
{
    public partial class FDatabaseConn : Sunny.UI.UIEditForm
    {
        public DBConnInfo dbInfo;
        public FDatabaseConn()
        {
            InitializeComponent();
            dbInfo = MyAppConfig.DbInfo ?? new DBConnInfo();
            setDefaultValue(dbInfo, dbInfo.dbType);
            ucdbConfig1.DBInfo = dbInfo;
        }
        void setDefaultValue(DBConnInfo dbInfo, DBMSType dbType)
        {
            if (dbInfo.port.IsEmpty())
            {
                if (dbType == DBMSType.MySQL)
                    dbInfo.port = "3306";
                else if (dbType == DBMSType.SqlServer)
                    dbInfo.port = "1433";
            }
            if (dbInfo.user.IsEmpty())
            {
                if (dbType == DBMSType.MySQL)
                    dbInfo.user = "root";
                else if (dbType == DBMSType.SqlServer)
                    dbInfo.user = "sa";
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            var db = ucdbConfig1.DBInfo;
           
                db.dbType = DBMSType.MySQL;
            
            try
            {
                if (!DbOperation.TestConn(db))
                {
                    MessageBoxE.Show(this, "数据库连接失败");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
                return;
            }
            //if (!new DbOperation(db).ExistTable("VisionData"))
            //{
            //    MessageBoxE.Show(this, $"数据库“{db.dbName}”无效");
            //    return;
            //}


            dbInfo = db;
            MyAppConfig.Save(db);

            MessageBoxE.Show(this, "数据库设置成功");
            this.DialogResult = DialogResult.OK;
        }
    }
}