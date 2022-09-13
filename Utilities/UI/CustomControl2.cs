using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.ExMethod;

namespace Utilities.UI
{
    public partial class TextTable : FlowLayoutPanel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextTableColumnCollection Columns { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextTableRowCollection Rows { get; set; }
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Label[] Headers
        {
            get
            {
                var arr = new Label[this.Controls[0].Controls.Count];
                int i = 0;
                foreach (Control c in this.Controls[0].Controls)
                    arr[i++] = (Label)c;
                return arr;
            }
            set {
                this.Controls[0].Controls.Clear();
                foreach(var a in value)
                {
                    this.Controls[0].Controls.Add(a);
                }
            }
        }
        public TextTable()
        {
            InitializeComponent();
            Rows = new TextTableRowCollection(this);
            Columns = new TextTableColumnCollection(this);
            this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            var th = new TextTableRow(Columns, "", Columns.Select(p => p.Header).ToArray());
            
            this.Controls.Add(th);
            this.SizeChanged += TextTable_SizeChanged;
        }

        void TextTable_SizeChanged(object sender, EventArgs e)
        {
            this.Controls[0].Width = this.Width - 6;
            foreach (var r in Rows)
                r.Width = this.Width-6;
        }
        internal void UpdateHeaders()
        {
            while( this.Controls[0].Controls.Count>1)
            this.Controls[0].Controls.RemoveAt(1);
            this.Controls[0].Width = this.Width - 6;
            // var th = new TextTableRow(Columns, "", Columns.Select(p => p.Header).ToArray());
            int left = this.Controls[0].Controls[0].Width;
            if (Rows.Count > 0)
                left =Math.Max(left, Rows.Max(p => p.lab.Width) + 6);
    
            foreach (var c in Columns)
            {
                Label l = new Label();
                l.AutoSize = false;
                l.Text = c.Header;
                l.Width = c.Width;
                l.TextAlign = c.TextAlign == HorizontalAlignment.Left ? ContentAlignment.MiddleLeft : (c.TextAlign == HorizontalAlignment.Right ? ContentAlignment.MiddleRight : ContentAlignment.MiddleCenter);
                l.Left = left;
                this.Controls[0].Controls.Add(l);
                left += l.Width /*+ l.Margin.Right + l.Margin.Left*/;
            }
        }

        //  public string[] RowHeader=new  string[0];

    }
    public class TextTableRow : Panel
    {
        [Browsable(true)     ,DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextTable Owner { get; set; }
        [Browsable(true)]
        public int Index { get { if (Owner == null)return -1; return Owner.Rows.IndexOf(this); } }
        [Browsable(true)]
        public string Header { get { return lab.Text; } set { lab.Text = value; } }
        [Browsable(true)]
        public Label lab = new Label();
        public Control[] Cells = new Control[0];
      
        public TextTableRow()
        {
            lab.AutoSize = false; 
            lab.Width = 60;
            this.Height = 24;
            lab.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lab);
        }
        public TextTableRow(TextTableColumnCollection cols, string Header = "", params object[] values)
            : this()
        {
            this.Header = Header;
            Cells = new Control[cols.Count];            
            this.Controls.Add(lab); 
            int i = 0;
            foreach (var a in cols)
            {
                //var c = a.AddFunc();
                var c =(Control) a.ControlType.Assembly.CreateInstance(a.ControlType.FullName);
                Cells[i] = c;
                if (i < values.Length)
                {
                    c.Text = values[i].ToStr();
                    if (c is Button)
                    {
                        var c2 = ((Button)c);
                        c2.AutoSize = false;
                        if (a.TextAlign == HorizontalAlignment.Left)
                            c2.TextAlign = ContentAlignment.MiddleLeft;
                        else if (a.TextAlign == HorizontalAlignment.Right)
                            c2.TextAlign = ContentAlignment.MiddleRight;
                        else c2.TextAlign = ContentAlignment.MiddleCenter;

                    }
                    else if (c is Label)
                    {
                        var c2 = (Label)c;
                        if (a.TextAlign == HorizontalAlignment.Left)
                            c2.TextAlign = ContentAlignment.MiddleLeft;
                        else if (a.TextAlign == HorizontalAlignment.Right)
                            c2.TextAlign = ContentAlignment.MiddleRight;
                        else c2.TextAlign = ContentAlignment.MiddleCenter;
                        c2.AutoSize = false;
                    }
                    else if (c is TextBox)
                    {
                        var c2 = (TextBox)c;
                        c2.AutoSize = false;
                        c2.TextAlign = a.TextAlign;
                    }

                }
                c.Left = lab.Right + 3 + i * a.Width;
                c.Width = a.Width - 6;
                Controls.Add(c);
                if (Width < c.Right + 5)
                    Width = c.Right + 5;
                i++;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
    }
    public class TextTableRowCollection : List<TextTableRow>
    {
        [Browsable(true)]
        public TextTable Owner { get; set; }

        public TextTableRowCollection() { }
        public TextTableRowCollection(TextTable tt)
        {
            this.Owner = tt;
        }
        public void AddRange(TextTableRow[] arr)
        {
            foreach(var r in arr)
            {
                Add(r);
            }
        }
        public new  int Add(TextTableRow row)
        {
            row.AutoSize = false;
            row.Owner = this.Owner;
            row.Width = Owner.Width;
            base.Add(row);
            Owner.Controls.Add(row);
            return this.Count - 1;
        }
        public int Add(string Lab, params object[] values)
        {
            var r = new TextTableRow(Owner.Columns, Lab, values);
            return this.Add(r); 
        }

        public new  void Clear()
        {
            foreach (var i in this)
                Owner.Controls.Remove(i);
            base.Clear();
        }
    }
    public class TextTableColumn :Control
    {
        [Browsable(true)]
        public TextTable Owner { get; set; }
        public int Index { get { if (Owner == null)return -1; return Owner.Columns.IndexOf(this); } }
        string _Header;
        [Browsable(true)]
        public string Header { get { return _Header; } set { _Header = value; if (Owner != null)Owner.UpdateHeaders(); } }
      
        [Browsable(true), DefaultValue(typeof(HorizontalAlignment), "TextBox")]
        public HorizontalAlignment TextAlign { get; set; }
       [Browsable(true), DefaultValue(typeof(Type), "HorizontalAlignment.Left")]
        public Type ControlType { get; set; } 
       
        public TextTableColumn()
       { 
            Width = 60;
            TextAlign = HorizontalAlignment.Left;
            ControlType = typeof(TextBox);
            this.SizeChanged += TextTableColumn_SizeChanged;
        }

        void TextTableColumn_SizeChanged(object sender, EventArgs e)
        {
            if(Owner !=null)
                 Owner.UpdateHeaders();
        }
        public TextTableColumn(TextTable Owner)
            : this()
        { this.Owner = Owner; }
    }
    public class TextTableColumnCollection : List<TextTableColumn>
    {
        public TextTable Owner { get; set; }
        public TextTableColumnCollection()
        { }
        public TextTableColumnCollection(TextTable Owner)
        { this.Owner = Owner; }
        public void AddRange(TextTableColumn[] arr)
        {
            foreach(var c in arr)
            {
                c.Owner = this.Owner;

            }
            base.AddRange(arr);
            Owner.UpdateHeaders();
        }
        public new int Add(TextTableColumn c)
        {
            c.Owner = this.Owner;
            base.Add(c);
            Owner.UpdateHeaders();
            return this.Count - 1;
        }
        public int Add<T>(string Header = "") where T : Control
        {
            var c = new TextTableColumn(Owner);
            c.Header = Header;
          //  c.AddFunc = () => { return new T(); };
            c.ControlType = typeof(T);
            base.Add(c);
            Owner.UpdateHeaders();
            return this.Count - 1;
        }
    }
}
