using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
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

        public void ResultTestEenUrlRestDc(object sender, DialogControllerAcceptingEventArgs e)
        {
            ResultTestEenUrl currentObject = e.AcceptActionArgs.CurrentObject as ResultTestEenUrl;
            ResultTestEenUrlRestOpslaan(null, currentObject);
        }

        public void ResultTestEenUrlRestOpslaan(Session session, ResultTestEenUrl resultTestEenUrl)
        {
            //IDataLayer dl = _dbConnectie.GetDataLayer();
            //using (var uow = new UnitOfWork(dl))
            //{
            ResultTestEenUrl resultTestEenUrl1 = new ResultTestEenUrl(session)
            {
                Name = resultTestEenUrl.Name,
                WebserviceVersie = resultTestEenUrl.WebserviceVersie,
                KraanDll = resultTestEenUrl.KraanDll,
                KaanIni = resultTestEenUrl.KaanIni,
                KraanDatabase = resultTestEenUrl.KraanDatabase,
                Sll = resultTestEenUrl.Sll,
                SllCertificaatVervalDatum = resultTestEenUrl.SllCertificaatVervalDatum,
                Response = resultTestEenUrl.Response,
                Soort = resultTestEenUrl.Soort,
                ResultTestKlant = resultTestEenUrl.ResultTestKlant,
                WebserviceWerkt = resultTestEenUrl.WebserviceWerkt,
                //Url = resultTestEenUrl.Url
            };

            resultTestEenUrl1.Save();
            //}
        }

        public void ResultTestEenUrlSoapDc(object sender, DialogControllerAcceptingEventArgs e)
        {
            ResultTestEenUrlSoap currentObject = e.AcceptActionArgs.CurrentObject as ResultTestEenUrlSoap;
            ResultTestEenUrlSoapOpslaan(currentObject);
        }

        public void ResultTestEenUrlSoapOpslaan(ResultTestEenUrlSoap resultTestEenUrlSoap)
        {
            IDataLayer dl = _dbConnectie.GetDataLayer();

            using (var uow = new UnitOfWork(dl))
            {
                ResultTestEenUrlSoap resultTestEenUrlSoap1 = new ResultTestEenUrlSoap(uow)
                {
                    Name = resultTestEenUrlSoap.Name,
                    Sll = resultTestEenUrlSoap.Sll,
                    SllCertificaatVervalDatum = resultTestEenUrlSoap.SllCertificaatVervalDatum,
                    WebserviceVersie = resultTestEenUrlSoap.WebserviceVersie,
                    DevExpressVersie = resultTestEenUrlSoap.DevExpressVersie,
                    DatabaseVersie = resultTestEenUrlSoap.DatabaseVersie,
                    Response = resultTestEenUrlSoap.Response,
                    Soort = resultTestEenUrlSoap.Soort,
                    ResultTestKlant = uow.GetObjectByKey<ResultTestKlant>(resultTestEenUrlSoap.ResultTestKlant.Oid),
                    WebserviceWerkt = resultTestEenUrlSoap.WebserviceWerkt,
                    //Url = uow.GetObjectByKey<Url>(resultTestEenUrlSoap.Url.Oid)
                };
                uow.CommitChanges();
            }
        }

        public void ResultTestEenUrlMessageDc(object sender, DialogControllerAcceptingEventArgs e)
        {
            ResultTestEenUrlMessageService currentObject = e.AcceptActionArgs.CurrentObject as ResultTestEenUrlMessageService;
            ResultTestEenUrlMessageOpslaan(currentObject);
        }

        public void ResultTestEenUrlMessageOpslaan(ResultTestEenUrlMessageService resultTestEenUrlMessage)
        {
            IDataLayer dl = _dbConnectie.GetDataLayer();

            using (var uow = new UnitOfWork(dl))
            {
                ResultTestEenUrlMessageService resultTestEenUrlMessageService = new ResultTestEenUrlMessageService(uow)
                {
                    Name = resultTestEenUrlMessage.Name,
                    KraanDll = resultTestEenUrlMessage.KraanDll,
                    KraanIni = resultTestEenUrlMessage.KraanIni,
                    DatabaseConnectie = resultTestEenUrlMessage.DatabaseConnectie,
                    Sll = resultTestEenUrlMessage.Sll,
                    SllCertificaatVervalDatum = resultTestEenUrlMessage.SllCertificaatVervalDatum,
                    MessageVersie = resultTestEenUrlMessage.MessageVersie,
                    InterBaseVersie = resultTestEenUrlMessage.InterBaseVersie,
                    MssqlServer = resultTestEenUrlMessage.MssqlServer,
                    MssqlCatalog = resultTestEenUrlMessage.MssqlCatalog,
                    Kraan1DatabaseVersie = resultTestEenUrlMessage.Kraan1DatabaseVersie,
                    Kraan2DatabaseVersie = resultTestEenUrlMessage.Kraan2DatabaseVersie,
                    Response = resultTestEenUrlMessage.Response,
                    Soort = resultTestEenUrlMessage.Soort,
                    ResultTestKlant = uow.GetObjectByKey<ResultTestKlant>(resultTestEenUrlMessage.ResultTestKlant.Oid),
                    WebserviceWerkt = resultTestEenUrlMessage.WebserviceWerkt,
                    //Url = uow.GetObjectByKey<Url>(resultTestEenUrlMessage.Url.Oid)
                };
                uow.CommitChanges();
            }
        }
    }
}
