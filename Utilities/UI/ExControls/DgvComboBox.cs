//                   _ooOoo_
//                  o8888888o
//                  88" . "88
//                  (| -_- |)
//                  O\  =  /O
//               ____/`---'\____
//             .'  \\|     |//  `.
//            /  \\|||  :  |||//  \
//           /  _||||| -:- |||||-  \
//           |   | \\\  -  /// |   |
//           | \_|  ''\---/''  |   |
//           \  .-\__  `-`  ___/-. /
//         ___`. .'  /--.--\  `. . __
//      ."" '<  `.___\_<|>_/___.'  >'"".
//     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//     \  \ `-.   \_ __\ /__ _/   .-` /  /
//======`-.____`-.___\_____/___.-`____.-'======
//                   `=---='
//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//         佛祖保佑       永无BUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Utilities.ExMethod;

namespace Utilities.UI
{
    public class ComboBoxItem
    {
        public object Value { get; set; }
        public string Text { get; set; }
        public ComboBoxItem(object val, string text)
        {
            Value = val;
            Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
        public static void  Attach(ComboBox obj)
        {
            obj.DisplayMember = "Text";
            obj.ValueMember = "Value";
        }
    }
    public class DgvComboBox : ComboBox
    {
        //List<ComboBoxItem> items = new List<ComboBoxItem>();
        Dictionary<DataGridView, int> DicIndexOfComboBoxInGridView = new Dictionary<DataGridView, int>();
        Dictionary<DataGridViewCell, Dictionary<object, object>> DicCellInfoMemory;
        Predicate<int> HideConditon;
      /// <summary>
      /// 要记住上次值的DataGridviewCell的Name
      /// </summary>
        string rememberCellName;
        public string RememeberCellName
        {
            get { return rememberCellName; }
            set
            {
                rememberCellName = value;
                if (!value.IsEmpty())
                {
                    if (DicCellInfoMemory == null)
                        DicCellInfoMemory = new Dictionary<DataGridViewCell, Dictionary<object, object>>();
                    DicCellInfoMemory.Clear();
                }
            }
        }
      /// <summary>
        /// DgvComboBox
      /// </summary>     
        public DgvComboBox()
        {
         
            ValueMember = "Value";
            DisplayMember = "Text";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = Color.Honeydew;
            this.SelectedIndexChanged += new EventHandler(DgvComboBox_SelectedIndexChanged);
           // this.Click += new EventHandler(DgvComboBox_SelectedIndexChanged);
            LostFocus += new EventHandler(DgvComboBox_LostFocus);
        }
        public void ClearRememberCellValues()
        {
            if (DicCellInfoMemory != null)
                DicCellInfoMemory.Clear();
        }
        void DgvComboBox_LostFocus(object sender, EventArgs e)
        {
            Visible = false;
        }
        public object SelectedValue
        {
            get
            {
                var i = SelectedItem as ComboBoxItem;
                if (i != null)
                    return i.Value;
                return base.SelectedValue;
            }
        }
        public void ClearItems()
        {
            //items.Clear();
            Items.Clear();
        }
        /// <summary>
        /// 为DgvComboBox添加项
        /// </summary>
        /// <param name="item"></param>
        public int  Add(ComboBoxItem item)
        {
            //items.Add(item);
           // DataSource = items;
           return  Items.Add(item); 
        }
       /// <summary>
        /// 为DgvComboBox添加项
       /// </summary>
       /// <param name="val">单元格值</param>
       /// <param name="text">显示字符串</param>
        public int  Add(object val, string text)
        {
           return  Add(new ComboBoxItem(val, text));
        }

        public void Attach(DataGridView dgv, int ColIndex, Predicate<int> HideConditon = null)
        {
            this.HideConditon = HideConditon;
            if (DicIndexOfComboBoxInGridView.ContainsKey(dgv))
            {
                return;
            }
            DicIndexOfComboBoxInGridView.Add(dgv, ColIndex);
            dgv.Controls.Add(this);
            dgv.CurrentCellChanged += new EventHandler(dgv_CurrentCellChanged);
            dgv.CellClick += new DataGridViewCellEventHandler(dgv_CellClick);
            dgv.CellFormatting += new DataGridViewCellFormattingEventHandler(dgv_CellFormatting);
            dgv.RowHeightChanged += new DataGridViewRowEventHandler(dgv_RowHeightChanged);
            dgv.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgv_ColumnWidthChanged);
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.DataBindingComplete += dgv_DataBindingComplete;

            this.Visible = false;
        }
      
       /// <summary>
        /// 设置DgvComboBox所在的DataGridView。
       /// </summary>
       /// <param name="dgv"></param>
       /// <param name="ColName"></param>
        public void Attach(DataGridView dgv, string ColName,Predicate<int> HideConditon=null)
        {
            try
            {
                int idx = dgv.Columns[ColName].Index;
                Attach(dgv, idx,HideConditon );
            }
            catch { }
        }

        void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dgv = sender as DataGridView;
                if (dgv.CurrentCell == null)
                    return;
                // if (dgv.CurrentCell.OwningColumn == dgv.Columns[dgvs[dgv][0]])
                if (DicIndexOfComboBoxInGridView[dgv] == dgv.CurrentCell.ColumnIndex)
                {
                    Parent = dgv;
                    if (HideConditon != null)
                    {
                        if (HideConditon(dgv.CurrentRow.Index))
                        {
                            Visible = false;
                            return;
                        }
                    }
                    Rectangle rect = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, false);
                    if (dgv.CurrentCell.Value == null)
                        SelectedIndex = 0;
                    else
                        try
                        {
                            SelectedItem = GetItemByValue(dgv.CurrentCell.Value);
                        }
                        catch { SelectedIndex = 0; }

                    Left = rect.Left;
                    Top = rect.Top;
                    Width = rect.Width;
                    Height = rect.Height;
                     Visible = true;

                }
                else
                {
                    Visible = false;
                    //   Width = 0;
                }
            }
            catch (Exception ex) { }
            dgv_CurrentCellChanged(sender, EventArgs.Empty);
        }

        void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.NewRowIndex == 0)
                return;
            //if ( dgv.Columns[dgvs[dgv][0]]!=null && e.ColumnIndex == dgv.Columns[dgvs[dgv][0]].Index)
            if (DicIndexOfComboBoxInGridView[dgv]==e.ColumnIndex)
            {

                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    //var items = DataSource as List<ComboBoxItem>;
                    //if (items != null)
                    //{
                    //    var f = items.Find(p => p.Value.Equals(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    //    if (f != null)
                    //        e.Value = f.Text;
                    //}
                    
                    if (Items != null)
                    {
                        if (HideConditon==null || !HideConditon(e.RowIndex))
                        {
                            ComboBoxItem f = GetItemByValue(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                            if (f != null)
                                e.Value = f.Text;
                        }
                    }
                }
                else e.Value = null;
            }
        }

        void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }

        void DgvComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
                return;
            DataGridView dgv = Parent as DataGridView;
            // dgv.CurrentCell = dgv.CurrentRow.Cells[DicIndexOfComboBoxInGridView[dgv]];
            if (dgv == null || dgv.CurrentCell == null)
                return;
            //if(dgv.CurrentCell.ColumnIndex != DicIndexOfComboBoxInGridView[dgv])
            //    dgv.CurrentCell = dgv.CurrentRow.Cells[DicIndexOfComboBoxInGridView[dgv]];

            var selectedValue = ((DgvComboBox)sender).SelectedValue;
            bool edited = false;
            if (dgv.CurrentCell.Value != selectedValue)
            {
              //  dgv.BeginEdit(true);
                dgv.CurrentCell.Value = selectedValue;
                edited = true;
            }
            if (DicCellInfoMemory != null)
            {
                try
                {
                    bool bTmp = true;
                    if (DicCellInfoMemory.ContainsKey(dgv.CurrentCell))
                    {
                        if (dgv.CurrentCell.Value != null && DicCellInfoMemory[dgv.CurrentCell].ContainsKey(dgv.CurrentCell.Value))
                            dgv.CurrentRow.Cells[RememeberCellName].Value = DicCellInfoMemory[dgv.CurrentCell][dgv.CurrentCell.Value];
                        else bTmp = false;
                    }

                    if (!bTmp)
                    {
                        object curValue = dgv.CurrentCell.Value;
                        //object targetValue = null;
                        //if (dgv.CurrentRow.DataBoundItem is System.Data.DataRowView)
                        //{
                        //    curValue = ((System.Data.DataRow)dgv.CurrentRow.DataBoundItem)[dgv.CurrentCell.OwningColumn.DataPropertyName];
                        //    targetValue = ((System.Data.DataRow)dgv.CurrentRow.DataBoundItem)[RememeberCellName];
                        //}
                        //else
                        //{
                        //    var rowType = dgv.CurrentRow.DataBoundItem.GetType();
                        //    System.Reflection.PropertyInfo propertyInfo = rowType.GetProperty(dgv.CurrentCell.OwningColumn.DataPropertyName);
                        //    if(propertyInfo!=null)
                        //        curValue = propertyInfo.GetValue(dgv.CurrentRow.DataBoundItem, System.Reflection.BindingFlags.GetProperty, null, null, null);
                        //    propertyInfo = rowType.GetProperty(RememeberCellName);
                        //    if (propertyInfo != null)
                        //        targetValue = propertyInfo.GetValue(dgv.CurrentRow.DataBoundItem, System.Reflection.BindingFlags.GetProperty, null, null, null);
                        //}

                        dgv.CurrentRow.Cells[RememeberCellName].Value = null;
                    }
                  
                }

                catch (Exception ex) { }
            }
            //if (edited)
            //    dgv.EndEdit();

        }
        void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Visible = false;
            if (DicCellInfoMemory == null)
                return;
            DataGridView dgv =sender as DataGridView;
            foreach(DataGridViewRow r in dgv.Rows)
            {                  
                var cell = r.Cells[rememberCellName];
                var combcell =r.Cells[DicIndexOfComboBoxInGridView[dgv]];
               
                Dictionary<object, object> dicCell = null;
                if (DicCellInfoMemory.ContainsKey(combcell))
                    dicCell = DicCellInfoMemory[combcell];
                else
                {
                    dicCell = new Dictionary<object, object>();
                    DicCellInfoMemory.Add(combcell, dicCell);
                }
                if (combcell.Value != null)
                {
                    if (dicCell.ContainsKey(combcell.Value))
                        dicCell[combcell.Value] = cell.Value;
                    else
                        dicCell.Add(combcell.Value, cell.Value);
                }
            }
        } 
        void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if(dgv.Columns[e.ColumnIndex].Name == RememeberCellName)
            {
                var cell = dgv.Rows[e.RowIndex].Cells[ e.ColumnIndex];
                var combcell =  dgv.Rows[e.RowIndex].Cells[DicIndexOfComboBoxInGridView[dgv]];
                Dictionary<object, object> dicCell = null;
                if (DicCellInfoMemory.ContainsKey(combcell))
                    dicCell = DicCellInfoMemory[combcell];
                else
                {
                    dicCell = new Dictionary<object, object>();
                    DicCellInfoMemory.Add(combcell, dicCell);
                }
                if (dicCell.ContainsKey(combcell.Value))
                    dicCell[combcell.Value] = cell.Value;
                else
                    dicCell.Add(combcell.Value, cell.Value);
            }
        }

        void dgv_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            if (Visible)
            {
                DataGridView dgv = sender as DataGridView;
                Height = e.Row.Height;
            }
        }

        void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (Visible)
            {
                DataGridView dgv = sender as DataGridView;
                Width = e.Column.Width;
            }
        }
        ComboBoxItem GetItemByValue(object v)
        { 
            foreach (var i in Items)
            {
                var p = i as ComboBoxItem;
                if (p.Value == null && v == null)
                    return p;
                if(v.GetType()==typeof(string))
                {
                    if (v.ToStr().Equals(p.Value.ToStr()))
                        return p;
                }
                else if (p != null && p.Value!=null &&p.Value.Equals(v))
                    return p;
            }
            return null;
        }
    }
}
