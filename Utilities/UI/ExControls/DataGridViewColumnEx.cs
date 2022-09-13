using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace  Utilities.UI
{
    public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        bool enable = true;
        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
                
                foreach (DataGridViewRow r in DataGridView.Rows)
                {
                    ((DataGridViewDisableCheckBoxCell)r.Cells[Index]).Enabled = value;
                } 
                this.DataGridView.Refresh();
            }
        }
        public DataGridViewDisableCheckBoxColumn()
        { 
            this.CellTemplate = new DataGridViewDisableCheckBoxCell();
        }
    }

    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get
            {              
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DataGridViewDisableCheckBoxCell cell =
                (DataGridViewDisableCheckBoxCell)base.Clone();
            cell.Enabled = this.Enabled; 
            return cell;
        }

        // By default, enable the CheckBox cell.
        public DataGridViewDisableCheckBoxCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // The checkBox cell is disabled, so paint the border,  
            // background, and disabled checkBox for the cell.
            if (!this.Enabled)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                        advancedBorderStyle);
                }

                // Calculate the area in which to draw the checkBox.
                CheckBoxState state = value != null && (bool)value ?
                    CheckBoxState.CheckedDisabled : CheckBoxState.UncheckedDisabled;
                Size size = CheckBoxRenderer.GetGlyphSize(graphics, state);
                Point center = new Point(cellBounds.X, cellBounds.Y);
                center.X += (cellBounds.Width - size.Width) / 2;
                center.Y += (cellBounds.Height - size.Height) / 2;
            
                // Draw the disabled checkBox.
                CheckBoxRenderer.DrawCheckBox(graphics, center, state);
            }
            else
            {
                // The checkBox cell is enabled, so let the base class 
                // handle the painting.
            
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
}

