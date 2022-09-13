using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.UI.ExMethod;

namespace Utilities.UI
{
    public partial class DataGridViewEx : DataGridView
    {
        int selectRow = 0;
        public bool IsRememberLastSelectedIndex = true;
        public Color borderColor=Color.Empty;
        private ImageList imageList1;
        private IContainer components;
        bool _AllowDragRow =false;
        public bool AllowDragRow
        {
            get { return _AllowDragRow; }
            set
            {
                _AllowDragRow = value;
                if (value) AllowDrop = true;
            }
        }
        public DataGridViewColumn[] ColumnsOfDragRow;
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Refresh();
            }
        }
        public DataGridViewEx()
        {
            InitializeComponent();
            this.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DataGridViewEx_DataBindingComplete);
            this.SelectionChanged += new EventHandler(DataGridViewEx_SelectionChanged);
            this.Click += new EventHandler(DataGridViewEx_Click);
        }
        
        public static int GetRowIndexAt(DataGridView dgv, Point pt)
        {
            if (dgv.FirstDisplayedScrollingRowIndex < 0)
            {
                return -1;  // no rows.   
            }
            if (dgv.ColumnHeadersVisible == true && pt.Y <= dgv.ColumnHeadersHeight)
            {
                return -2;//above the first displayed row
            }
            int index = dgv.FirstDisplayedScrollingRowIndex;
            int displayedCount = dgv.DisplayedRowCount(true);
            for (int k = 1; k <= displayedCount; )  // 因为行不能ReOrder，故只需要搜索显示的行   
            {
                if (dgv.Rows[index].Visible == true)
                {
                    Rectangle rect = dgv.GetRowDisplayRectangle(index, true);  // 取该区域的显示部分区域   
                    //if (rect.Top <= pt.Y && pt.Y < rect.Bottom)
                    if (rect.Contains(pt))
                    {
                        return index;
                    }
                    k++;  // 只计数显示的行;   
                }
                index++;
            }
            return -1;

            /*DataGridView.HitTestInfo hti = dgv.HitTest(pt.X, pt.Y);
            return hti.RowIndex;*/
        }
        void DataGridViewEx_Click(object sender, EventArgs e)
        {
            if (IsRememberLastSelectedIndex)
            {
                if (this.IsRowSelected())
                    selectRow = this.SelectedRows[0].Index;
                else selectRow = -1;
            }
        }

        void DataGridViewEx_SelectionChanged(object sender, EventArgs e)
        {
          
        }
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                base.OnCellMouseDown(e);
                if (!AllowDragRow)
                    return;
                int iRow = e.RowIndex;
                if (iRow == -1)
                    return;
                if (e.Clicks != 1)
                    return;
                if (ColumnsOfDragRow == null)
                {
                    ColumnsOfDragRow = new DataGridViewColumn[1];
                    ColumnsOfDragRow[0] = this.Columns[0];
                }
                if (e.Button == MouseButtons.Left && (ColumnsOfDragRow.Contains(Columns[e.ColumnIndex])))
                {
                    if (this.Rows[e.RowIndex].IsNewRow)
                        return;

                    if (DragHelper.ImageList_BeginDrag(this.imageList1.Handle, 0, 0, 0))
                    {
                        this.DoDragDrop(this.Rows[iRow], DragDropEffects.Copy | DragDropEffects.Move);
                        DragHelper.ImageList_EndDrag();
                    }
                }
            }
            catch { }
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!AllowDragRow)
                return;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow))) //判定是否现在拖动的数据是DataGridViewRow项 
            {
                e.Effect = DragDropEffects.Copy | DragDropEffects.Move;
            }
            DragHelper.ImageList_DragEnter(this.Handle, 0, 0);
        }
        protected override void OnDragDrop(DragEventArgs e)
        {
            if (e.KeyState== 4 || e.KeyState == 8)
                return;
            base.OnDragDrop(e);
            if (!AllowDragRow)
                return;
            //根据鼠标坐标确定要移动到的目标节点
            Point pt = PointToClient(new Point(e.X, e.Y));
            int iCurr = GetRowIndexAt(this, pt);
            int iAct = iCurr;//最后插入的实际位置
            if (iCurr == -2)
                iAct = 0; 
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))//判定是否现在拖动的数据是DataGridViewRow项   
            {
                DataGridViewRow dr = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                if(DataSource ==null)
                {
                    if (iCurr == dr.Index)
                    {
                    }
                    else
                    {
                        if (iCurr == -1)
                        {
                            iAct = 0;//At the first row
                            iCurr = this.Rows.Add();
                        }
                        else
                        {
                            iAct = iCurr;
                            if (iCurr > dr.Index)
                            {
                                iCurr += 1;
                            }
                            this.Rows.Insert(iCurr, 1);
                        }
                        this.Rows[iCurr].Tag = dr.Tag;
                        this[0, iCurr].Tag = dr.Cells[0].Tag;
                        for (int k = 0; k < dr.Cells.Count; k++)
                        {
                            this[k, iCurr].Value = dr.Cells[k].Value;
                        }
                        this.Rows.Remove(dr);
                        this.Rows[iAct].Selected = true;
                    }
                }
                else if(DataSource is System.Collections.IList)
                {
                    var lst = DataSource as System.Collections.IList;
                    if (lst.Count < 2)
                        return;
                    if (iCurr == dr.Index)
                    {
                    }
                    else
                    {
                        if (iCurr == -1)
                        {
                            iAct = 0;//At the first row
                          //  iCurr = this.Rows.Add();
                        }
                        else
                        {
                            var tmp = dr.DataBoundItem;
                          lst[dr.Index] = this.Rows[iAct].DataBoundItem;
                          lst[iAct] = tmp;

                        }
                        this.Rows[iCurr].Tag = dr.Tag;
                        this[0, iCurr].Tag = dr.Cells[0].Tag;
                        for (int k = 0; k < dr.Cells.Count; k++)
                        {
                           // this[k, iCurr].Value = dr.Cells[k].Value;
                        }                     
                    }
                }
            }
            //DragHelper.ImageList_DragLeave(this.Handle);
            DragHelper.ImageList_EndDrag();
            this.ClearSelection();
            this.Rows[iAct].Selected = true;
        }
        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            if (!AllowDragRow)
                return;
            DragHelper.ImageList_DragLeave(this.Handle);
        }
        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            if (!AllowDragRow)
                return;
            try
            {
                Point pt;
             //   if (e.Data.GetDataPresent(typeof(ListViewItem)) || e.Data.GetDataPresent(typeof(DataGridViewRow))) //判定是否现在拖动的数据是DataGridViewRow项 
                {
                    e.Effect = DragDropEffects.Move;
                    pt = this.PointToClient(new Point(e.X, e.Y));
                    int iRow = this.HitTest(pt.X, pt.Y).RowIndex;
                    if (iRow != -1)
                    {
                        this.Rows[iRow].Selected = true;
                    }
                }
                pt = this.PointToClient(new Point(e.X, e.Y));
                DragHelper.ImageList_DragMove(pt.X, pt.Y);
            }
            catch (Exception ex)
            {
               // MessageBox.Show("dgvTestPro_DragOver()!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void DataGridViewEx_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        { 
            if(IsRememberLastSelectedIndex)
                this.SetRowSelect(selectRow);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(BorderColor!= Color.Empty)
                using (Pen pen = new Pen(BorderColor))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewEx));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "list_48.png");
            this.imageList1.Images.SetKeyName(1, "list_32.png");
            // 
            // DataGridViewEx
            // 
            this.RowTemplate.Height = 23;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
