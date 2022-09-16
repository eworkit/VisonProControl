using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using Cognex.VisionPro;
using Cognex.VisionPro.QuickBuild;

namespace VisionControl
{
  /// <summary>
  /// Summary description for FormQB.
  /// </summary>
  public class FormQB : Sunny.UI.UIForm
  {
    private CogJobManager mJM = null;

    private CogJobManagerEdit cogJobManagerEdit1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public FormQB(CogJobManager jm)
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();    
      this.ShowDragStretch = true;
      mJM = jm;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.cogJobManagerEdit1 = new Cognex.VisionPro.QuickBuild.CogJobManagerEdit();
            this.SuspendLayout();
            // 
            // cogJobManagerEdit1
            // 
            this.cogJobManagerEdit1.AutoSize = true;
            this.cogJobManagerEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogJobManagerEdit1.Location = new System.Drawing.Point(0, 35);
            this.cogJobManagerEdit1.Name = "cogJobManagerEdit1";
            this.cogJobManagerEdit1.ShowLocalizationTab = false;
            this.cogJobManagerEdit1.Size = new System.Drawing.Size(728, 419);
            this.cogJobManagerEdit1.Subject = null;
            this.cogJobManagerEdit1.TabIndex = 0;
            // 
            // FormQB
            // 
            this.ClientSize = new System.Drawing.Size(728, 454);
            this.Font = new System.Drawing.Font("ËÎÌו", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximumSize = new System.Drawing.Size(2640, 1563);
            this.Name = "FormQB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 728, 454);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormQB_Closing);
            this.Load += new System.EventHandler(this.FormQB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    #endregion

    private void FormQB_Load(object sender, System.EventArgs e)
        {

            this.Controls.Add(this.cogJobManagerEdit1);
            this.Text = ResourceUtility.GetString("RtQuickBuildTitle");
      cogJobManagerEdit1.Subject = mJM;   
    }

    private void FormQB_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      cogJobManagerEdit1.Subject = null;
    }
  }
}
