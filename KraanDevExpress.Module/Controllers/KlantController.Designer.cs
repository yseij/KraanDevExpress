
namespace KraanDevExpress.Module.Controllers
{
    partial class KlantController
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
            this.TestKlantBtn = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // TestKlantBtn
            // 
            this.TestKlantBtn.Caption = "Test Klant";
            this.TestKlantBtn.Category = "View";
            this.TestKlantBtn.ConfirmationMessage = null;
            this.TestKlantBtn.Id = "TestKlantBtn";
            this.TestKlantBtn.ToolTip = null;
            this.TestKlantBtn.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.TestKlantBtn_Execute);
            // 
            // KlantControllercs
            // 
            this.Actions.Add(this.TestKlantBtn);
            this.TargetObjectType = typeof(KraanDevExpress.Module.BusinessObjects.Klant);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction TestKlantBtn;
    }
}
