using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Xpo;
using KraanDevExpress.Module.BusinessObjects;

namespace KraanDevExpress.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ResultTestEenUrlController : ViewController
    {
        DbConnectie _dbConnectie;
        public ResultTestEenUrlController()
        {
            InitializeComponent();
            _dbConnectie = new DbConnectie();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        public void ResultTestEenUrlRestOpslaan(object sender, DialogControllerAcceptingEventArgs e)
        {
            ResultTestEenUrl currentObject = e.AcceptActionArgs.CurrentObject as ResultTestEenUrl;
            IDataLayer dl = _dbConnectie.GetDataLayer();

            using (var uow = new UnitOfWork(dl))
            {
                ResultTestEenUrl ResultTestEenUrl = new ResultTestEenUrl(uow)
                {
                    Name = currentObject.Name,
                    WebserviceVersie = currentObject.WebserviceVersie,
                    KraanDll = currentObject.KraanDll,
                    KaanIni = currentObject.KaanIni,
                    KraanDatabase = currentObject.KraanDatabase,
                    Sll = currentObject.Sll,
                    SllCertificaatVervalDatum = currentObject.SllCertificaatVervalDatum,
                    Response = currentObject.Response,
                    Soort = currentObject.Soort,
                    ResultTestKlant = currentObject.ResultTestKlant,
                    WebserviceWerkt = currentObject.WebserviceWerkt,
                    Url = uow.GetObjectByKey<Url>(currentObject.Url.Oid)
                };
                uow.CommitChanges();
            }
        }

        public void ResultTestEenUrlSoapOpslaan(object sender, DialogControllerAcceptingEventArgs e)
        {
            ResultTestEenUrlSoap currentObject = e.AcceptActionArgs.CurrentObject as ResultTestEenUrlSoap;
            IDataLayer dl = _dbConnectie.GetDataLayer();

            using (var uow = new UnitOfWork(dl))
            {
                ResultTestEenUrlSoap resultTestEenUrlSoap = new ResultTestEenUrlSoap(uow)
                {
                    Name = currentObject.Name,
                    Sll = currentObject.Sll,
                    SllCertificaatVervalDatum = currentObject.SllCertificaatVervalDatum,
                    WebserviceVersie = currentObject.WebserviceVersie,
                    DevExpressVersie = currentObject.DevExpressVersie,
                    DatabaseVersie = currentObject.DatabaseVersie,
                    Response = currentObject.Response,
                    Soort = currentObject.Soort,
                    ResultTestKlant = currentObject.ResultTestKlant,
                    WebserviceWerkt = currentObject.WebserviceWerkt,
                    Url = uow.GetObjectByKey<Url>(currentObject.Url.Oid)
                };
                uow.CommitChanges();
            }
        }

        public void ResultTestEenUrlMessageOpslaan(object sender, DialogControllerAcceptingEventArgs e)
        {
            ResultTestEenUrlMessageService currentObject = e.AcceptActionArgs.CurrentObject as ResultTestEenUrlMessageService;
            IDataLayer dl = _dbConnectie.GetDataLayer();

            using (var uow = new UnitOfWork(dl))
            {
                ResultTestEenUrlMessageService resultTestEenUrlMessageService = new ResultTestEenUrlMessageService(uow)
                {
                    Name = currentObject.Name,
                    KraanDll = currentObject.KraanDll,
                    KraanIni = currentObject.KraanIni,
                    DatabaseConnectie = currentObject.DatabaseConnectie,
                    Sll = currentObject.Sll,
                    SllCertificaatVervalDatum = currentObject.SllCertificaatVervalDatum,
                    MessageVersie = currentObject.MessageVersie,
                    InterBaseVersie = currentObject.InterBaseVersie,
                    MssqlServer = currentObject.MssqlServer,
                    MssqlCatalog = currentObject.MssqlCatalog,
                    Kraan1DatabaseVersie = currentObject.Kraan1DatabaseVersie,
                    Kraan2DatabaseVersie = currentObject.Kraan2DatabaseVersie,
                    Response = currentObject.Response,
                    Soort = currentObject.Soort,
                    ResultTestKlant = currentObject.ResultTestKlant,
                    WebserviceWerkt = currentObject.WebserviceWerkt,
                    Url = uow.GetObjectByKey<Url>(currentObject.Url.Oid)
                };
                uow.CommitChanges();
            }
        }
    }
}
