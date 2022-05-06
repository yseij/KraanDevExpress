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
    public partial class KlantWebserviceController : ViewController
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
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public KlantWebserviceController()
        {
            InitializeComponent();
            _webRequest = new WebRequest();
            _testRoute = new TestRoute();
            _dbConnectie = new DbConnectie();
            _resultTestEenUrlController = new ResultTestEenUrlController();
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

        private void TestUrlBtn_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            Name name = new Name();

            DetailView targetView = Application.CreateDetailView(objectSpace, name);
            targetView.ViewEditMode = ViewEditMode.Edit;

            ShowViewParameters svp = new ShowViewParameters(targetView);

            DialogController dc = Application.CreateController<DialogController>();

            if (e.SelectedObjects.Count > 1)
            {
                name.Naam = "Meerdere urls testen --- " + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                dc.Accepting += dc_Accepting_MeerdereUrls;
            }
            else
            {
                KlantWebservice klantWebservice = e.CurrentObject as KlantWebservice;
                if (klantWebservice != null)
                {
                    if (klantWebservice.BasisUrl1)
                    {
                        name.Naam = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name + "---" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    }
                    else
                    {
                        name.Naam = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name + "---" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    }
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

            KlantWebservice klantWebservice = View.CurrentObject as KlantWebservice;
            Url url = new Url(_session);
            if (klantWebservice.BasisUrl1)
            {
                url.Name = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
            }
            else
            {
                url.Name = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
            }

            DialogController dc = Application.CreateController<DialogController>();

            ResultTestUrls resultTestUrls = new ResultTestUrls(_session);

            TestUrl(url, klantWebservice, result, dc, resultTestUrls, false);
            CreateView(_targetView, dc);
        }

        void dc_Accepting_MeerdereUrls(object sender, DialogControllerAcceptingEventArgs e)
        {
            _objecspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objecspace).Session;

            Name name = e.AcceptActionArgs.CurrentObject as Name;

            dynamic result = null;
            DialogController dc = Application.CreateController<DialogController>();
            ResultTestUrls resultTestUrls = new ResultTestUrls(_session);
            resultTestUrls.Name = name.Naam;

            foreach (KlantWebservice klantWebservice in View.SelectedObjects)
            {
                Url url = new Url(_session);
                if (klantWebservice.BasisUrl1)
                {
                    url.Name = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                }
                else
                {
                    url.Name = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                }
                TestUrl(url, klantWebservice, result, dc, resultTestUrls, true);
                _objecspace.CommitChanges();
            }
            DetailView targetView = Application.CreateDetailView(_objecspace, resultTestUrls, false);
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

        private void TestUrl(Url url,
                              KlantWebservice klantWebservice,
                              dynamic result,
                              DialogController dc,
                              ResultTestUrls resultTestUrls,
                              bool isMeerdereUrls)
        {
            string checkUrl = _webRequest.CheckUrl(url.Name);
            if (klantWebservice.Webservice.Soap)
            {
                if (url.Name.Contains("MessageServiceSoap31.svc"))
                {
                    ResultTestEenUrlMessageService resultTestEenUrlMessageService = GetMessageService(url);
                    resultTestEenUrlMessageService.WebserviceWerkt = checkUrl;
                    GetSales31Credentials();
                    result = JObject.Parse(_webRequest.Get31SalesData(url.Name , _gebruikersNaam, _wachtwoord));
                    if (result != null)
                    {
                        _testRoute.TestOneRouteMessageService(result, resultTestEenUrlMessageService, null);
                    }
                    if (!isMeerdereUrls)
                    {
                        dc.Accepting += _resultTestEenUrlController.ResultTestEenUrlMessageOpslaan;
                        _targetView = Application.CreateDetailView(_objecspace, resultTestEenUrlMessageService, false);
                    }
                    else
                    {
                        resultTestUrls.ResultTestEenUrlMessageServices.Add(resultTestEenUrlMessageService);
                    }
                }
                else if (url.Name.Contains("MessageServiceSoap.svc"))
                {
                    ResultTestEenUrlMessageService resultTestEenUrlMessageService = GetMessageService(url);
                    resultTestEenUrlMessageService.WebserviceWerkt = checkUrl;
                    result = JObject.Parse(_webRequest.Get24SalesData(url.Name));
                    if (result != null)
                    {
                        _testRoute.TestOneRouteMessageService(result, resultTestEenUrlMessageService, null);
                    }
                    if (!isMeerdereUrls)
                    {
                        dc.Accepting += _resultTestEenUrlController.ResultTestEenUrlMessageOpslaan;
                        _targetView = Application.CreateDetailView(_objecspace, resultTestEenUrlMessageService, false);
                    }
                    else
                    {
                        resultTestUrls.ResultTestEenUrlMessageServices.Add(resultTestEenUrlMessageService);
                    }
                }
                else
                {
                    ResultTestEenUrlSoap resultTestEenUrlSoap = new ResultTestEenUrlSoap(_session);
                    resultTestEenUrlSoap.Soort = "Url test";
                    resultTestEenUrlSoap.Name = url.Name + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    resultTestEenUrlSoap.Url = url;
                    resultTestEenUrlSoap.WebserviceWerkt = checkUrl;

                    int plaatsSlech = url.Name.LastIndexOf("/");
                    string service = url.Name.Substring(plaatsSlech + 1, url.Name.Length - plaatsSlech - 1);
                    result = JObject.Parse(_webRequest.GetWebRequestSoap(url.Name, service));

                    _testRoute.TestOneRouteSoap(result,
                                                resultTestEenUrlSoap,
                                                null);
                    if (!isMeerdereUrls)
                    {
                        dc.Accepting += _resultTestEenUrlController.ResultTestEenUrlSoapOpslaan;
                        _targetView = Application.CreateDetailView(_objecspace, resultTestEenUrlSoap, false);
                    }
                    else
                    {
                        resultTestUrls.ResultTestEenUrlSoaps.Add(resultTestEenUrlSoap);
                    }
                }
            }
            else
            {
                ResultTestEenUrl resultTestEenUrl = new ResultTestEenUrl(_session);
                resultTestEenUrl.Soort = "Url test";
                resultTestEenUrl.Name = url.Name + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                resultTestEenUrl.Url = url;
                resultTestEenUrl.WebserviceWerkt = checkUrl;


                result = JObject.Parse(_webRequest.GetWebRequestRest(url.Oid, url.Name + "/GetWebserviceVersion", true));

                _testRoute.TestOneRoute(result,
                                        resultTestEenUrl);
                if (!isMeerdereUrls)
                {
                    dc.Accepting += _resultTestEenUrlController.ResultTestEenUrlRestOpslaan;
                    _targetView = Application.CreateDetailView(_objecspace, resultTestEenUrl, false);
                }
                else
                {
                    resultTestUrls.ResultTestEenUrls.Add(resultTestEenUrl);
                }
            }
        }

        private void dc_Login(object sender, DialogControllerAcceptingEventArgs e)
        {
            Sales31Credentials sales31Credentials = e.AcceptActionArgs.CurrentObject as Sales31Credentials;
            _gebruikersNaam = sales31Credentials.GebruikersNaam;
            _wachtwoord = sales31Credentials.Wachtwoord;
        }

        private ResultTestEenUrlMessageService GetMessageService(Url url)
        {
            ResultTestEenUrlMessageService resultTestEenUrlMessageService = new ResultTestEenUrlMessageService(_session);
            resultTestEenUrlMessageService.Soort = "url test";
            resultTestEenUrlMessageService.Name = url.Name + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            resultTestEenUrlMessageService.Url = url;

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
    }
}
