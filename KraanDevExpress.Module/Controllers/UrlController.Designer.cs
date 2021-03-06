
namespace KraanDevExpress.Module.Controllers
{
    partial class UrlController
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
            this.BtnTestUrl = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // BtnTestUrl
            // 
            this.BtnTestUrl.Caption = "Test url";
            this.BtnTestUrl.Category = "View";
            this.BtnTestUrl.ConfirmationMessage = null;
            this.BtnTestUrl.Id = "BtnTestUrl";
            this.BtnTestUrl.ToolTip = null;
            this.BtnTestUrl.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.BtnTestUrl_Execute);
            // 
            // UrlController
            // 
            this.Actions.Add(this.BtnTestUrl);
            this.TargetObjectType = typeof(KraanDevExpress.Module.BusinessObjects.Url);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction BtnTestUrl;
    }
}
