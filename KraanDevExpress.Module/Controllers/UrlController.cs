using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using KraanDevExpress.Module.BusinessObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace KraanDevExpress.Module.Controllers
{
    public partial class UrlController : ViewController
    {
        private string _gebruikersNaam;
        private string _wachtwoord;
        private Session _session;
        private IObjectSpace _objectspace;

        private dynamic _result = null;

        DetailView _targetView = null;

        WebRequest _webRequest;
        TestRoute _testRoute;
        DbConnectie _dbConnectie;
        public UrlController()
        {
            InitializeComponent();
            _webRequest = new WebRequest();
            _testRoute = new TestRoute();
            _dbConnectie = new DbConnectie();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void BtnTestUrl_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            Name name = new Name();

            DetailView targetView = Application.CreateDetailView(objectSpace, name);
            targetView.ViewEditMode = ViewEditMode.Edit;

            ShowViewParameters svp = new ShowViewParameters(targetView);

            DialogController dc = Application.CreateController<DialogController>();
            if (e.SelectedObjects.Count == 0)
            {
                MessageBox.Show("Je hebt geen url geselecteerd");
            }
            else
            {
                if (e.SelectedObjects.Count > 1)
                {
                    name.Naam = "Meerdere urls testen --- " + DateTime.Today.Day + "_" + DateTime.Today.Month +
                        "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour +
                        "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    dc.Accepting += dc_Accepting_MeerdereUrls;
                }
                else
                {
                    Url url = e.CurrentObject as Url;
                    KlantWebservice klantWebservice = url.KlantWebservice;
                    if (klantWebservice != null)
                    {
                        if (klantWebservice.BasisUrl1)
                        {
                            name.Naam = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name + "/" + url.MethodeName + " --- "
                                + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour +
                                "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                        }
                        else
                        {
                            name.Naam = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name + "/" + url.MethodeName + " --- "
                                + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour +
                                "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                        }
                        dc.Accepting += dc_Accepting;
                    }
                }
            }

            svp.Controllers.Add(dc);
            svp.Context = TemplateContext.PopupWindow;
            svp.TargetWindow = TargetWindow.NewModalWindow;

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }

        void dc_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            _objectspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objectspace).Session;

            Url url = View.CurrentObject as Url;
;
            DialogController dc = Application.CreateController<DialogController>();
            KlantWebservice klantWebservice = url.KlantWebservice;

            string urlName;
            if (klantWebservice.BasisUrl1 && klantWebservice.BasisUrl2)
            {
                urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                TestUrl(urlName, klantWebservice, null, false);
                urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                TestUrl(urlName, klantWebservice, null, false);
            }
            else if (klantWebservice.BasisUrl1)
            {
                urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                TestUrl(urlName, klantWebservice, null, false);
            }
            else
            {
                urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                TestUrl(urlName, klantWebservice, null, false);
            }
            _objectspace.CommitChanges();
            CreateView(_targetView, dc);
        }

        void dc_Accepting_MeerdereUrls(object sender, DialogControllerAcceptingEventArgs e)
        {
            _objectspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objectspace).Session;

            DialogController dc = Application.CreateController<DialogController>();
            Name name = e.AcceptActionArgs.CurrentObject as Name;
            ResultTestUrls resultTestUrls = new ResultTestUrls(_session);
            resultTestUrls.Name = name.Naam;

            foreach (Url url in View.SelectedObjects)
            {
                KlantWebservice klantWebservice = url.KlantWebservice;
                string urlName;
                if (klantWebservice.BasisUrl1 && klantWebservice.BasisUrl2)
                {
                    urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                    TestUrl(urlName, klantWebservice, resultTestUrls, true);
                    urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                    TestUrl(urlName, klantWebservice, resultTestUrls, true);
                }
                else if (klantWebservice.BasisUrl1)
                {
                    urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                    TestUrl(urlName, klantWebservice, resultTestUrls, true);
                }
                else
                {
                    urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name + "/" + url.MethodeName;
                    TestUrl(urlName, klantWebservice, resultTestUrls, true);
                }
            }
            _objectspace.CommitChanges();
            DetailView targetView = Application.CreateDetailView(_objectspace, resultTestUrls, false);
            CreateView(targetView, dc);
        }

        private void TestUrl(string urlName,
                             KlantWebservice klantWebservice,
                             ResultTestUrls resultTestUrls,
                             bool isMeerdereUrls)
        {
            if (klantWebservice.Webservice.Soap)
            {
                if (urlName.Contains("MessageServiceSoap31.svc"))
                {
                    GetSales31Credentials();
                    _result = JObject.Parse(_webRequest.Get31SalesData(urlName, _gebruikersNaam, _wachtwoord));
                    ResultTestEenUrlMessageService(urlName, resultTestUrls, isMeerdereUrls);
                }
                else if (urlName.Contains("MessageServiceSoap.svc"))
                {
                    _result = JObject.Parse(_webRequest.Get24SalesData(urlName));
                    ResultTestEenUrlMessageService(urlName, resultTestUrls, isMeerdereUrls);
                }
                else
                {
                    ResultTestEenUrlSoap(urlName, resultTestUrls, isMeerdereUrls);
                }
            }
            else
            {
                ResultTestEenUrl(urlName, resultTestUrls, isMeerdereUrls);
            }
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

        private void ResultTestEenUrlMessageService(string urlName, ResultTestUrls resultTestUrls, bool isMeerdereUrls)
        {
            string checkUrl = _webRequest.CheckUrl(urlName);
            ResultTestEenUrlMessageService resultTestEenUrlMessageService = GetMessageService(urlName);
            resultTestEenUrlMessageService.WebserviceWerkt = checkUrl;
            if (_result != null)
            {
                _testRoute.TestOneRouteMessageService(_result, 
                                                      resultTestEenUrlMessageService, 
                                                      null);
            }
            if (!isMeerdereUrls)
            {
                _targetView = Application.CreateDetailView(_objectspace, resultTestEenUrlMessageService, false);
            }
            else
            {
                resultTestUrls.ResultTestEenUrlMessageServices.Add(resultTestEenUrlMessageService);
            }
        }

        private void ResultTestEenUrlSoap(string urlName, ResultTestUrls resultTestUrls, bool isMeerdereUrls)
        {
            string checkUrl = _webRequest.CheckUrl(urlName);
            ResultTestEenUrlSoap resultTestEenUrlSoap = new ResultTestEenUrlSoap(_session);
            resultTestEenUrlSoap.Soort = "Url Soap test";
            resultTestEenUrlSoap.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            resultTestEenUrlSoap.WebserviceWerkt = checkUrl;

            int plaatsSlech = urlName.LastIndexOf("/");
            string service = urlName.Substring(plaatsSlech + 1, urlName.Length - plaatsSlech - 1);
            _result = JObject.Parse(_webRequest.GetWebRequestSoap(urlName, service));
            if (_result != null)
            {
                _testRoute.TestOneRouteSoap(_result,
                                            resultTestEenUrlSoap,
                                            null);
            }
            if (!isMeerdereUrls)
            {
                _targetView = Application.CreateDetailView(_objectspace, resultTestEenUrlSoap, false);
            }
            else
            {
                resultTestUrls.ResultTestEenUrlSoaps.Add(resultTestEenUrlSoap);
            }
        }

        private void ResultTestEenUrl(string urlName, ResultTestUrls resultTestUrls, bool isMeerdereUrls)
        {
            Console.WriteLine(urlName);
            string checkUrl = _webRequest.CheckUrl(urlName);
            ResultTestEenUrl resultTestEenUrl = new ResultTestEenUrl(_session);
            resultTestEenUrl.Soort = "Url Rest test";
            resultTestEenUrl.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            resultTestEenUrl.WebserviceWerkt = checkUrl;

            _result = JObject.Parse(_webRequest.GetWebRequestRest(urlName, false));
            if (_result != null)
            {
                _testRoute.TestOneRoute(_result,
                                        resultTestEenUrl,
                                        null);
            }
            if (!isMeerdereUrls)
            {
                _targetView = Application.CreateDetailView(_objectspace, resultTestEenUrl, false);
            }
            else
            {
                resultTestUrls.ResultTestEenUrls.Add(resultTestEenUrl);
            }
            
        }

        private ResultTestEenUrlMessageService GetMessageService(string urlName)
        {
            ResultTestEenUrlMessageService resultTestEenUrlMessageService = new ResultTestEenUrlMessageService(_session);
            resultTestEenUrlMessageService.Soort = "Url MessageService test";
            resultTestEenUrlMessageService.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

            return resultTestEenUrlMessageService;
        }

        private void GetSales31Credentials()
        {
            Sales31Credentials sales31Credentials = new Sales31Credentials();
            DetailView targetViewSales31Credentials = Application.CreateDetailView(_objectspace, sales31Credentials, false);
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
