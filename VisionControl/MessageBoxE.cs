using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Utilities.UI;

namespace VisionControl
{
    public static class MessageBoxE
    { 
        public static DialogResult MessageBoxShow(IWin32Window owner, string message, string title, MessageBoxButtons buttons, UIStyle style, bool showMask = true, bool topMost = false)
        {
            Screen.GetBounds(SystemEx.GetCursorPos());
            UIMessageForm uIMessageForm = new UIMessageForm();
            uIMessageForm.StartPosition = FormStartPosition.CenterScreen;
           
            uIMessageForm.ShowMessage(message, title, buttons == MessageBoxButtons.OKCancel|| buttons == MessageBoxButtons.YesNo, style);
            uIMessageForm.ShowInTaskbar = false;
            uIMessageForm.TopMost = topMost;
            if (buttons == MessageBoxButtons.YesNo)
            {
                var btns = uIMessageForm.Controls.OfType<UIButton>().ToList();
                btns.Single(x => x.Name == "btnOK").Text = "是";
                btns.Single(x => x.Name == "btnCancel").Text = "否";
            }
            DialogResult result = DialogResult.OK;
            if (showMask)
            {
               result= uIMessageForm.ShowDialogWithMask();
            }
            else
            {
               result= uIMessageForm.ShowDialog(owner);
            } 
        
            uIMessageForm.Dispose();
            return result;
        }
        /// Displays a message box with specified text.
        public static DialogResult Show(string text)
        {  
          return MessageBoxShow(null, text, null, MessageBoxButtons.OK, UIStyle.Blue, false);
        }

        /// Displays a message box in front of the specified object and with the specified text.
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return MessageBoxShow(owner, text, null, MessageBoxButtons.OK, UIStyle.Blue, false);
        }

        /// Displays a message box with specified text and caption.
        public static DialogResult Show(string text, string caption)
        {
            return MessageBoxShow(null, text, caption, MessageBoxButtons.OK, UIStyle.Blue, false);
        }

        /// Displays a message box in front of the specified object and with the specified text and caption.
        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            return MessageBoxShow(owner, text, caption, MessageBoxButtons.OK, UIStyle.Blue, false);
        }

        /// Displays a message box with specified text, caption, and buttons.
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBoxShow(null, text, caption, buttons, UIStyle.Blue, false);
        }

        /// Displays a message box in front of the specified object and with the specified text, caption, and buttons.
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBoxShow(owner, text, caption, buttons, UIStyle.Blue, false);

        }

        /// Displays a message box with specified text, caption, buttons, and icon.
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBoxShow(null, text, caption, buttons, UIStyle.Blue, false);

        }

        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBoxShow(owner, text, caption, buttons, UIStyle.Blue, false);

        }

        /// Displays a message box with the specified text, caption, buttons, icon, and default button.
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBoxShow(null, text, caption, buttons, UIStyle.Blue, false);

        }

        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            MessageBoxIndirect mb = new MessageBoxIndirect(owner, text, caption, buttons, icon, defaultButton);
            return mb.Show();
        }

        /// Displays a message box with the specified text, caption, buttons, icon, default button, and options.
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            MessageBoxIndirect mb = new MessageBoxIndirect(text, caption, buttons, icon, defaultButton, options);
            return mb.Show();
        }

        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            MessageBoxIndirect mb = new MessageBoxIndirect(owner, text, caption, buttons, icon, defaultButton, options);
            return mb.Show();
        }

        static void A()
        {
            MessageBoxIndirect mb = new MessageBoxIndirect("Help Button", "Test", MessageBoxButtons.YesNoCancel);
            mb.ShowHelp = true;
            mb.ContextHelpID = 555;
            mb.Callback = new MessageBoxIndirect.MsgBoxCallback((s) => { }/*this.ShowHelp*/);
            mb.Show();

            mb = new MessageBoxIndirect("Custom Icon", "Test");
            // You can explicitly specify the instance handle of the module to load the icon from using
            // a line like this:
            //		mb.Instance = Marshal.GetHINSTANCE( System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0] );
            // If you don't specify anything, the MessageBoxIndirect wrapper (NOT the underlying API) defaults
            // to the currently executing assembly.
            const int Smiley = 102;
            mb.UserIcon = new IntPtr(Smiley);       // pass the icon ID as an IntPtr -- same ultimate result as MAKEINTRESOURCE macro in C++

        }
    }
}
