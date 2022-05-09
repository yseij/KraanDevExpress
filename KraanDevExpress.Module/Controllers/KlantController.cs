using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using KraanDevExpress.Module.BusinessObjects;
using Newtonsoft.Json.Linq;
using System;

namespace KraanDevExpress.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class KlantController : ViewController
    {
        private string _gebruikersNaam;
        private string _wachtwoord;
        private Session _session;
        private IObjectSpace _objecspace;

        DetailView _targetView = null;

        WebRequest _webRequest;
        TestRoute _testRoute;
        DbConnectie _dbConnectie;

        ResultTestEenUrlController _resultTestEenUrlController;
        ResultTestKlantController _resultTestKlantController;
        public KlantController()
        {
            InitializeComponent();
            _webRequest = new WebRequest();
            _testRoute = new TestRoute();
            _dbConnectie = new DbConnectie();
            _resultTestEenUrlController = new ResultTestEenUrlController();
            _resultTestKlantController = new ResultTestKlantController();
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

        private void TestKlantBtn_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            Name name = new Name();

            DetailView targetView = Application.CreateDetailView(objectSpace, name);
            targetView.ViewEditMode = ViewEditMode.Edit;

            ShowViewParameters svp = new ShowViewParameters(targetView);

            DialogController dc = Application.CreateController<DialogController>();

            if (e.SelectedObjects.Count > 1)
            {
                name.Naam = "Meerdere klanten testen --- " + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                dc.Accepting += dc_Accepting_MeerdereKlanten;
            }
            else
            {
                Klant klant = e.CurrentObject as Klant;
                if (klant != null)
                {
                    name.Naam = klant.Name + " test ---" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    dc.Accepting += dc_Accepting; 
                }
            }
            svp.Controllers.Add(dc);
            svp.Context = TemplateContext.PopupWindow;
            svp.TargetWindow = TargetWindow.NewModalWindow;

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }

        void dc_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            _objecspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objecspace).Session;
            dynamic result = null;

            Name name = e.AcceptActionArgs.CurrentObject as Name;
            Klant klant = View.CurrentObject as Klant;
            ResultTestKlant resultTestKlant = new ResultTestKlant(_session);
            resultTestKlant.Name = name.Naam;
            DialogController dc = Application.CreateController<DialogController>();

            foreach (KlantWebservice klantWebservice in klant.klantWebservices)
            {
                string urlName = string.Empty;
                if (klantWebservice.BasisUrl1 && klantWebservice.BasisUrl2)
                {
                    urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                    TestUrl(urlName, klantWebservice, result, dc, resultTestKlant);
                    urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                    TestUrl(urlName, klantWebservice, result, dc, resultTestKlant);
                }
                else if (klantWebservice.BasisUrl1)
                {
                    urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                }
                else
                {
                    urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                }
                TestUrl(urlName, klantWebservice, result, dc, resultTestKlant);
            }
            _objecspace.CommitChanges();

            DetailView targetView = Application.CreateDetailView(_objecspace, resultTestKlant, false);
            CreateView(targetView, dc);
        }

        void dc_Accepting_MeerdereKlanten(object sender, DialogControllerAcceptingEventArgs e)
        {
            _objecspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objecspace).Session;

            Name name = e.AcceptActionArgs.CurrentObject as Name;

            dynamic result = null;
            DialogController dc = Application.CreateController<DialogController>();
            ResultTestKlant resultTestKlant = new ResultTestKlant(_session);
            resultTestKlant.Name = name.Naam;

            foreach (Klant klant in View.SelectedObjects)
            {
                string urlName = string.Empty;
                foreach (KlantWebservice klantWebservice in klant.klantWebservices)
                {
                    if (klantWebservice.BasisUrl1 && klantWebservice.BasisUrl2)
                    {
                        urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                        TestUrl(urlName, klantWebservice, result, dc, resultTestKlant);
                        urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                        TestUrl(urlName, klantWebservice, result, dc, resultTestKlant);
                    }
                    else if (klantWebservice.BasisUrl1)
                    {
                        urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                    }
                    else
                    {
                        urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                    }
                    TestUrl(urlName, klantWebservice, result, dc, resultTestKlant);
                }
            }
            _objecspace.CommitChanges();

            DetailView targetView = Application.CreateDetailView(_objecspace, resultTestKlant, false);
            CreateView(targetView, dc);
        }

        private void CreateView(DetailView targetView, DialogController dc)
        {
            targetView.ViewEditMode = ViewEditMode.Edit;

            ShowViewParameters svp = new ShowViewParameters(targetView);

            svp.Controllers.Add(dc);
            svp.Context = TemplateContext.PopupWindow;
            svp.TargetWindow = TargetWindow.NewModalWindow;

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }

        private void TestUrl(string urlName,
                              KlantWebservice klantWebservice,
                              dynamic result,
                              DialogController dc,
                              ResultTestKlant resultTestKlant)
        {
            string checkUrl = _webRequest.CheckUrl(urlName);
            if (klantWebservice.Webservice.Soap)
            {
                if (urlName.Contains("MessageServiceSoap31.svc"))
                {
                    ResultTestEenUrlMessageService resultTestEenUrlMessageService = GetMessageService(urlName, klantWebservice);
                    resultTestEenUrlMessageService.WebserviceWerkt = checkUrl;
                    GetSales31Credentials();
                    result = JObject.Parse(_webRequest.Get31SalesData(urlName, _gebruikersNaam, _wachtwoord));
                    if (result != null)
                    {
                        _testRoute.TestOneRouteMessageService(result, resultTestEenUrlMessageService, resultTestKlant);
                    }
                    resultTestKlant.ResultTestEenUrlMessageServices.Add(resultTestEenUrlMessageService);
                }
                else if (urlName.Contains("MessageServiceSoap.svc"))
                {
                    ResultTestEenUrlMessageService resultTestEenUrlMessageService = GetMessageService(urlName, klantWebservice);
                    resultTestEenUrlMessageService.WebserviceWerkt = checkUrl;
                    result = JObject.Parse(_webRequest.Get24SalesData(urlName));
                    if (result != null)
                    {
                        _testRoute.TestOneRouteMessageService(result, resultTestEenUrlMessageService, resultTestKlant);
                    }
                    resultTestKlant.ResultTestEenUrlMessageServices.Add(resultTestEenUrlMessageService);
                }
                else
                {
                    ResultTestEenUrlSoap resultTestEenUrlSoap = new ResultTestEenUrlSoap(_session);
                    resultTestEenUrlSoap.Soort = "Klant test - " + klantWebservice.Klant.Name;
                    resultTestEenUrlSoap.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    //resultTestEenUrlSoap.Url = url;
                    resultTestEenUrlSoap.WebserviceWerkt = checkUrl;

                    int plaatsSlech = urlName.LastIndexOf("/");
                    string service = urlName.Substring(plaatsSlech + 1, urlName.Length - plaatsSlech - 1);
                    result = JObject.Parse(_webRequest.GetWebRequestSoap(urlName, service));

                    _testRoute.TestOneRouteSoap(result,
                                                resultTestEenUrlSoap,
                                                resultTestKlant);
                    resultTestKlant.ResultTestEenUrlSoaps.Add(resultTestEenUrlSoap);
                }
            }
            else
            {
                ResultTestEenUrl resultTestEenUrl = new ResultTestEenUrl(_session);
                resultTestEenUrl.Soort = "Klant test - " + klantWebservice.Klant.Name;
                resultTestEenUrl.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                resultTestEenUrl.WebserviceWerkt = checkUrl;


                result = JObject.Parse(_webRequest.GetWebRequestRest(urlName + "/GetWebserviceVersion", true));

                _testRoute.TestOneRoute(result,
                                        resultTestEenUrl, resultTestKlant);
                resultTestKlant.ResultTestEenUrls.Add(resultTestEenUrl);
            }
        }

        private ResultTestEenUrlMessageService GetMessageService(string urlName, KlantWebservice klantWebservice)
        {
            ResultTestEenUrlMessageService resultTestEenUrlMessageService = new ResultTestEenUrlMessageService(_session);
            resultTestEenUrlMessageService.Soort = "Klant test - " + klantWebservice.Klant.Name;
            resultTestEenUrlMessageService.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

            return resultTestEenUrlMessageService;
        }

        private void GetSales31Credentials()
        {
            Sales31Credentials sales31Credentials = new Sales31Credentials();
            DetailView targetViewSales31Credentials = Application.CreateDetailView(_objecspace, sales31Credentials, false);
            targetViewSales31Credentials.ViewEditMode = ViewEditMode.Edit;

            ShowViewParameters svpSales31Credentials = new ShowViewParameters(targetViewSales31Credentials);

            DialogController dcSales31Credentials = Application.CreateController<DialogController>();

            dcSales31Credentials.Accepting += dc_Login;
            svpSales31Credentials.Controllers.Add(dcSales31Credentials);
            svpSales31Credentials.Context = TemplateContext.PopupWindow;
            svpSales31Credentials.TargetWindow = TargetWindow.NewModalWindow;

            Application.ShowViewStrategy.ShowView(svpSales31Credentials, new ShowViewSource(null, null));
        }

        private void dc_Login(object sender, DialogControllerAcceptingEventArgs e)
        {
            Sales31Credentials sales31Credentials = e.AcceptActionArgs.CurrentObject as Sales31Credentials;
            _gebruikersNaam = sales31Credentials.GebruikersNaam;
            _wachtwoord = sales31Credentials.Wachtwoord;
        }
    }
}
