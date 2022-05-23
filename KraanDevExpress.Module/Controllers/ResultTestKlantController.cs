﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using KraanDevExpress.Module.BusinessObjects;
using System.Windows.Forms;

namespace KraanDevExpress.Module.Controllers
{
    public partial class ResultTestKlantController : ViewController
    {
        private Session _session;
        private IObjectSpace _objecspace;

        DeleteObjectsViewController _deleteObjectsViewController;
        public ResultTestKlantController()
        {
            InitializeComponent();
            _deleteObjectsViewController = new DeleteObjectsViewController();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            _deleteObjectsViewController = Frame.GetController<DeleteObjectsViewController>();
            if (_deleteObjectsViewController != null)
            {
                _deleteObjectsViewController.Deleting += dc_deleting;
            }
        }
        protected override void OnDeactivated()
        {
            if (_deleteObjectsViewController != null)
            {
                _deleteObjectsViewController.Deleting -= dc_deleting;
            }
            base.OnDeactivated();
        }

        private void dc_deleting(object sender, DeletingEventArgs e)
        {
            _objecspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objecspace).Session;

            foreach (ResultTestKlant resultTestKlant in e.Objects)
            {
                if (resultTestKlant.ResultTestEenUrlMessageServices.Count == 0 && resultTestKlant.ResultTestEenUrls.Count == 0 && resultTestKlant.ResultTestEenUrlSoaps.Count == 0)
                {
                    _session.Delete(_objecspace.GetObjectByKey<ResultTestKlant>(resultTestKlant.Oid));
                }
                else
                {
                    DialogResult dialogResultUrlsByKlant = MessageBox.Show("Wilt u de tests van de klant test ook verwijderen", "Tests bij klant", MessageBoxButtons.YesNo);
                    if (dialogResultUrlsByKlant == DialogResult.Yes)
                    {
                        if (resultTestKlant.ResultTestEenUrlMessageServices.Count != 0)
                        {
                            foreach (ResultTestEenUrlMessageService resultTestEenUrlMessageService in resultTestKlant.ResultTestEenUrlMessageServices)
                            {
                                _session.Delete(_objecspace.GetObjectByKey<ResultTestEenUrlMessageService>(resultTestEenUrlMessageService.Oid));
                            }
                        }
                        if (resultTestKlant.ResultTestEenUrls.Count != 0)
                        {
                            foreach (ResultTestEenUrl resultTestEenUrl in resultTestKlant.ResultTestEenUrls)
                            {
                                _session.Delete(_objecspace.GetObjectByKey<ResultTestEenUrl>(resultTestEenUrl.Oid));
                            }
                        }
                        if (resultTestKlant.ResultTestEenUrlSoaps.Count != 0)
                        {
                            foreach (ResultTestEenUrlSoap resultTestEenUrlSoap in resultTestKlant.ResultTestEenUrlSoaps)
                            {
                                _session.Delete(_objecspace.GetObjectByKey<ResultTestEenUrlSoap>(resultTestEenUrlSoap.Oid));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Er wordt niks verwijdert");
                        e.Cancel = true;
                    }
                }  
            }
            _objecspace.CommitChanges();
        }
    }
}
