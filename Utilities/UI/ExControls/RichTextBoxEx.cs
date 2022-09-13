using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilities.UI.ExControls
{
    public  class RichTextBoxEx:RichTextBox
    {

        private const int EM_GETLINECOUNT = 0xBA;

        public int LineCount
        {
            get
            {
                Message msg = Message.Create(this.Handle, EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero);
                base.DefWndProc(ref msg);
                return msg.Result.ToInt32();
            }
        }
    }
}
