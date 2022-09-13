using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Utilities.UI
{
    public class ListViewEx : ListView
    {
       public   bool IsDoubleChecked = false;
       public bool IsKeepSelected = true;
        public ListViewEx()
       {
           this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

           // Enable the OnNotifyMessage event so we get a chance to filter out 
           // Windows messages before they get to the form's WndProc
           this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            GotFocus += new EventHandler(listView1_GotFocus);
           LostFocus += new EventHandler(listView1_LostFocus);
           HideSelection = true;
          //  Invalidated += new InvalidateEventHandler(listView_Validated);
            ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listView_ItemSelectionChanged);
        }
        class ItemColor
        {
           public   Color ForeColor;
           public Color BackColor;
        }
        Dictionary<ListViewItem, ItemColor> dicItemColor = new Dictionary<ListViewItem, ItemColor>();
        void listView1_LostFocus(object sender, EventArgs e)
        {
            if (!IsKeepSelected)
                return;
            HideSelection = true;
           // dicItemColor.Clear();
            foreach (ListViewItem item in SelectedItems)         
            {
                ItemColor ic = null;
                if (!dicItemColor.ContainsKey(item))
                {
                    ic = new ItemColor() { ForeColor = item.ForeColor, BackColor = item.BackColor };
                    dicItemColor.Add(item, ic);
                }
                item.ForeColor = Color.White;
                item.BackColor = SystemColors.Highlight;
            }
            
        }

        void listView1_GotFocus(object sender, EventArgs e)
        {
            if (!IsKeepSelected)
                return;
            foreach (var ic in dicItemColor)
            {
                var item = ic.Key;
                var c = ic.Value;
                item.ForeColor = c.ForeColor;
                item.BackColor = c.BackColor;
            }
            foreach (ListViewItem item in SelectedItems)
            {
                item.ForeColor = Color.White;
               item.BackColor = SystemColors.Highlight;
            }
        }

        private void listView_Validated(object sender, EventArgs e)
        {
            if (FocusedItem != null)
            {
                FocusedItem.BackColor = SystemColors.Highlight;
                FocusedItem.ForeColor = Color.White;
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!IsKeepSelected)
                return;
          //  e.Item.ForeColor = Color.Black;
           // e.Item.BackColor = SystemColors.Window;
            listView1_LostFocus(null, null);
            listView1_GotFocus(null, null);
            return;
            if (FocusedItem != null)
            {
               // FocusedItem.Selected = true;
            }

            HideSelection = true;
            foreach (ListViewItem item in Items)
            {
                if (dicItemColor.ContainsKey(item))
                {
                    var ic = dicItemColor[item];
                    item.ForeColor = ic.ForeColor;
                    item.BackColor = ic.BackColor;
                }
            }
            foreach (ListViewItem item in SelectedItems)
            {
                item.ForeColor = Color.White;
                item.BackColor = SystemColors.Highlight;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if(m.Msg ==  (int)Win32Messages.WM_LBUTTONDBLCLK)
            {
                var point = PointToClient(Cursor.Position);
                var lvi = GetItemAt(point.X,point .Y);
                if (lvi != null)
                {
                  //  lvi.Selected = lvi.Selected;
                 //   lvi = null;
                    OnMouseDoubleClick(new MouseEventArgs( System.Windows.Forms.MouseButtons.Left, 2, point.X,point .Y,0));
                }
            }
            else
            {
                base.WndProc(ref m); 
            }
        }
        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
          
        }
    }
}
