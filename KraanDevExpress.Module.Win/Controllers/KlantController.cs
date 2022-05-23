using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using KraanDevExpress.Module.BusinessObjects;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KraanDevExpress.Module.Win.Controllers
{
    public partial class KlantController : ViewController
    {
        private Session _session;
        private IObjectSpace _objectspace;
        DeleteObjectsViewController _deleteObjectsViewController;
        public KlantController()
        {
            InitializeComponent();
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
            base.OnDeactivated();
            if (_deleteObjectsViewController != null)
            {
                _deleteObjectsViewController.Deleting -= dc_deleting;
            }
        }

        private void dc_deleting(object sender, DeletingEventArgs e)
        {
            _objectspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objectspace).Session;

            foreach (Klant klant in e.Objects)
            {
                if (klant.klantWebservices.Count != 0)
                {
                    DialogResult dialogResultUrlsByKlant =
                        MessageBox.Show("Wilt u de urls van de klant " + klant.Name
                        + " ook verwijderen", "Urls bij klant", MessageBoxButtons.YesNo);
                    if (dialogResultUrlsByKlant == DialogResult.Yes)
                    {
                        foreach (KlantWebservice klantWebservice in klant.klantWebservices)
                        {
                            IList<Url> urls = Url.GetUrlsByKlantWebservice(_session, klantWebservice.Oid);
                            if (urls.Count != 0)
                            {
                                _session.Delete(urls);
                            }
                        }
                        foreach (KlantWebservice klantWebservice in klant.klantWebservices)
                        {
                            _session.Delete(_objectspace.GetObjectByKey<KlantWebservice>(klantWebservice.Oid));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Er wordt niks verwijdert");
                        e.Cancel = true;
                    }
                }
                else
                {
                    klant.Delete();
                }
            }
            _objectspace.CommitChanges();
        }
    }
}
