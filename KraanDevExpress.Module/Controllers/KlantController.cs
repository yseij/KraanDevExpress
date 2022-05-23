using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using KraanDevExpress.Module.BusinessObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KraanDevExpress.Module.Controllers
{
    public partial class KlantController : ViewController
    {
        private string _gebruikersNaam;
        private string _wachtwoord;
        private Session _session;
        private IObjectSpace _objectspace;

        private dynamic _result = null;

        string[] kraanWebservices = { "AuthService.svc",
                                      "CrmService.svc",
                                      "WorkflowService.svc",
                                      "MaterieelService.svc",
                                      "MaterieelbeheerService.svc",
                                      "UrenService.svc" };

        DetailView _targetView = null;

        WebRequest _webRequest;
        TestRoute _testRoute;
        DbConnectie _dbConnectie;

        public KlantController()
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


        private void TestKlantBtn_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            Name name = new Name();

            DetailView targetView = Application.CreateDetailView(objectSpace, name);
            targetView.ViewEditMode = ViewEditMode.Edit;

            ShowViewParameters svp = new ShowViewParameters(targetView);

            DialogController dc = Application.CreateController<DialogController>();
            if (e.SelectedObjects.Count == 0)
            {
                MessageBox.Show("Je hebt geen klant geselecteerd");
            }
            else
            {
                if (e.SelectedObjects.Count > 1)
                {
                    name.Naam = "Meerdere klanten testen --- " + DateTime.Today.Day +
                        "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour +
                        "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                    dc.Accepting += dc_Accepting_MeerdereKlanten;
                }
                else
                {
                    Klant klant = e.CurrentObject as Klant;
                    if (klant != null)
                    {
                        name.Naam = klant.Name + " test --- " + DateTime.Today.Day +
                            "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour +
                            "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
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

            Name name = e.AcceptActionArgs.CurrentObject as Name;
            Klant klant = View.CurrentObject as Klant;
            ResultTestKlant resultTestKlant = new ResultTestKlant(_session);
            resultTestKlant.Name = name.Naam;
            DialogController dc = Application.CreateController<DialogController>();
            DetailView targetView = null;

            foreach (KlantWebservice klantWebservice in klant.klantWebservices)
            {
                string urlName = string.Empty;
                if (klantWebservice.BasisUrl1 && klantWebservice.BasisUrl2)
                {
                    urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                    if (klantWebservice.Webservice.Name == "Kraan2Webservice")
                    {
                        CheckWebserviceName(urlName, klantWebservice, resultTestKlant);
                    }
                    else
                    {
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                    }
                    urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name; 
                    if (klantWebservice.Webservice.Name == "Kraan2Webservice")
                    {
                        CheckWebserviceName(urlName, klantWebservice, resultTestKlant);
                    }
                    else
                    {
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                    }
                }
                else if (klantWebservice.BasisUrl1)
                {
                    urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                    TestUrl(urlName, klantWebservice, resultTestKlant);
                }
                else
                {
                    urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                    TestUrl(urlName, klantWebservice, resultTestKlant);
                }
                foreach (Url url in Url.GetUrlsByKlantWebservice(_session, klantWebservice.Oid))
                {
                    urlName = urlName + "/" + url.MethodeName;
                    TestUrl(urlName, klantWebservice, resultTestKlant);
                }
                if (klantWebservice.Webservice.Name == "Kraan2Webservice")
                {
                    CheckWebserviceName(urlName, klantWebservice, resultTestKlant); 
                }
            }
            _objectspace.CommitChanges();
            targetView = Application.CreateDetailView(_objectspace, resultTestKlant, false);
            CreateView(targetView, dc);
        }

        private void CheckWebserviceName(string urlName,
                                         KlantWebservice klantWebservice,
                                         ResultTestKlant resultTestKlant)
        {
            for (int i = 0; i < kraanWebservices.Length; i++)
            {
                string urlName2 = urlName + "/" + kraanWebservices[i];
                TestUrl(urlName2, klantWebservice, resultTestKlant);
            }
        }

        void dc_Accepting_MeerdereKlanten(object sender, DialogControllerAcceptingEventArgs e)
        {
            _objectspace = Application.CreateObjectSpace(View.ObjectTypeInfo.Type);
            _session = ((XPObjectSpace)_objectspace).Session;

            Name name = e.AcceptActionArgs.CurrentObject as Name;

            DialogController dc = Application.CreateController<DialogController>();
            ResultTestKlant resultTestKlant = new ResultTestKlant(_session);
            resultTestKlant.Name = name.Naam;

            foreach (Klant klant in View.SelectedObjects)
            {
                foreach (KlantWebservice klantWebservice in klant.klantWebservices)
                {
                    string urlName = string.Empty;
                    if (klantWebservice.BasisUrl1 && klantWebservice.BasisUrl2)
                    {
                        urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                        if (klantWebservice.Webservice.Name == "Kraan2Webservice")
                        {
                            CheckWebserviceName(urlName, klantWebservice, resultTestKlant);
                        }
                        urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                        if (klantWebservice.Webservice.Name == "Kraan2Webservice")
                        {
                            CheckWebserviceName(urlName, klantWebservice, resultTestKlant);
                        }
                    }
                    else if (klantWebservice.BasisUrl1)
                    {
                        urlName = klantWebservice.Klant.BasisUrl1 + klantWebservice.Webservice.Name;
                    }
                    else
                    {
                        urlName = klantWebservice.Klant.BasisUrl2 + klantWebservice.Webservice.Name;
                    }
                    foreach (Url url in Url.GetUrlsByKlantWebservice(_session, klantWebservice.Oid))
                    {
                        urlName = urlName + "/" + url.MethodeName;
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                    }
                    if (klantWebservice.Webservice.Name == "Kraan2Webservice")
                    {
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                        CheckWebserviceName(urlName, klantWebservice, resultTestKlant);
                    }
                    else
                    {
                        TestUrl(urlName, klantWebservice, resultTestKlant);
                    }
                }
            }
            _objectspace.CommitChanges();

            DetailView targetView = Application.CreateDetailView(_objectspace, resultTestKlant, false);
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
                             ResultTestKlant resultTestKlant)
        {
            if (klantWebservice.Webservice.Soap)
            {
                if (urlName.Contains("MessageServiceSoap31.svc"))
                {
                    GetSales31Credentials();
                    _result = JObject.Parse(_webRequest.Get31SalesData(urlName, _gebruikersNaam, _wachtwoord));
                    ResultTestEenUrlMessageService(urlName, klantWebservice, resultTestKlant);
                }
                else if (urlName.Contains("MessageServiceSoap.svc"))
                {
                    _result = JObject.Parse(_webRequest.Get24SalesData(urlName));
                    ResultTestEenUrlMessageService(urlName, klantWebservice, resultTestKlant);
                }
                else
                {
                    ResultTestEenUrlSoap(urlName, klantWebservice, resultTestKlant);
                }
            }
            else
            {
                ResultTestEenUrl(urlName, klantWebservice, resultTestKlant);
            }
        }

        private void ResultTestEenUrlMessageService(string urlName,
                                                    KlantWebservice klantWebservice,
                                                    ResultTestKlant resultTestKlant)
        {
            string checkUrl = _webRequest.CheckUrl(urlName);
            ResultTestEenUrlMessageService resultTestEenUrlMessageService = GetMessageService(urlName, klantWebservice, resultTestKlant);
            resultTestEenUrlMessageService.WebserviceWerkt = checkUrl;
            if (_result != null)
            {
                _testRoute.TestOneRouteMessageService(_result, resultTestEenUrlMessageService, resultTestKlant);
            }
            resultTestKlant.ResultTestEenUrlMessageServices.Add(resultTestEenUrlMessageService);
        }

        private void ResultTestEenUrlSoap(string urlName,
                                          KlantWebservice klantWebservice,
                                          ResultTestKlant resultTestKlant)
        {
            string checkUrl = _webRequest.CheckUrl(urlName);
            ResultTestEenUrlSoap resultTestEenUrlSoap = new ResultTestEenUrlSoap(_session);
            resultTestEenUrlSoap.Soort = "Klant test - " + klantWebservice.Klant.Name;
            resultTestEenUrlSoap.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            resultTestEenUrlSoap.WebserviceWerkt = checkUrl;
            resultTestEenUrlSoap.ResultTestKlant = resultTestKlant;

            int plaatsSlech = urlName.LastIndexOf("/");
            string service = urlName.Substring(plaatsSlech + 1, urlName.Length - plaatsSlech - 1);
            _result = JObject.Parse(_webRequest.GetWebRequestSoap(urlName, service));

            _testRoute.TestOneRouteSoap(_result,
                                        resultTestEenUrlSoap,
                                        resultTestKlant);
            resultTestKlant.ResultTestEenUrlSoaps.Add(resultTestEenUrlSoap);
        }

        private void ResultTestEenUrl(string urlName,
                                      KlantWebservice klantWebservice,
                                      ResultTestKlant resultTestKlant)
        {
            string checkUrl = _webRequest.CheckUrl(urlName);
            ResultTestEenUrl resultTestEenUrl = new ResultTestEenUrl(_session);
            resultTestEenUrl.Soort = "Klant test - " + klantWebservice.Klant.Name;
            resultTestEenUrl.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            resultTestEenUrl.WebserviceWerkt = checkUrl;
            resultTestEenUrl.ResultTestKlant = resultTestKlant;

            _result = JObject.Parse(_webRequest.GetWebRequestRest(urlName, true));

            _testRoute.TestOneRoute(_result,
                                    resultTestEenUrl,
                                    resultTestKlant);

            resultTestKlant.ResultTestEenUrls.Add(resultTestEenUrl);
        }

        private ResultTestEenUrlMessageService GetMessageService(string urlName, KlantWebservice klantWebservice, ResultTestKlant resultTestKlant)
        {
            ResultTestEenUrlMessageService resultTestEenUrlMessageService = new ResultTestEenUrlMessageService(_session);
            resultTestEenUrlMessageService.Soort = "Klant test - " + klantWebservice.Klant.Name;
            resultTestEenUrlMessageService.Name = urlName + "_" + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            resultTestEenUrlMessageService.ResultTestKlant = resultTestKlant;

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
