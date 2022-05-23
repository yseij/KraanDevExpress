
namespace KraanDevExpress.Module.Controllers
{
    partial class KlantWebserviceController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TestUrlBtn = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // TestUrlBtn
            // 
            this.TestUrlBtn.Caption = "Test url";
            this.TestUrlBtn.Category = "View";
            this.TestUrlBtn.ConfirmationMessage = null;
            this.TestUrlBtn.Id = "Test";
            this.TestUrlBtn.TargetObjectType = typeof(KraanDevExpress.Module.BusinessObjects.KlantWebservice);
            this.TestUrlBtn.ToolTip = null;
            this.TestUrlBtn.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.TestUrlBtn_Execute);
            // 
            // KlantWebserviceController
            // 
            this.Actions.Add(this.TestUrlBtn);
            this.TargetObjectType = typeof(KraanDevExpress.Module.BusinessObjects.KlantWebservice);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction TestUrlBtn;
    }
}
