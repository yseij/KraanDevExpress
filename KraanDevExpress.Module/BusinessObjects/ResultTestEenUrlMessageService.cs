using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KraanDevExpress.Module.BusinessObjects
{
    [DefaultClassOptions]
    [Appearance("ActionVisibility",
    TargetItems = "Soort; ResultTestWebservice; ResultTestKlant; ResultTestUrls; Url",
    Context = "DetailView",
    Visibility = ViewItemVisibility.Hide)]

    [Appearance("PromisedBold1bHeadResult", BackColor = "White", FontColor = "Red", TargetItems = "Name",
    Criteria = "[IsException]")]
    public class ResultTestEenUrlMessageService : BaseObject
    { 
        public ResultTestEenUrlMessageService(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string _name;
        [Size(SizeAttribute.Unlimited)]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(nameof(Name), ref _name, value); }
        }

        private string _webserviceWerkt;
        public string WebserviceWerkt
        {
            get { return _webserviceWerkt; }
            set { SetPropertyValue(nameof(WebserviceWerkt), ref _webserviceWerkt, value); }
        }

        private bool _kraanDll;
        public bool KraanDll
        {
            get { return _kraanDll; }
            set { SetPropertyValue(nameof(KraanDll), ref _kraanDll, value); }
        }

        private bool _kraanIni;
        public bool KraanIni
        {
            get { return _kraanIni; }
            set { SetPropertyValue(nameof(KraanIni), ref _kraanIni, value); }
        }

        private bool _databaseConnectie;
        public bool DatabaseConnectie
        {
            get { return _databaseConnectie; }
            set { SetPropertyValue(nameof(DatabaseConnectie), ref _databaseConnectie, value); }
        }

        private bool _sll;
        public bool Sll
        {
            get { return _sll; }
            set { SetPropertyValue(nameof(Sll), ref _sll, value); }
        }

        private string _sllCertificaatVervalDatum;
        [VisibleInListView(false)]
        public string SllCertificaatVervalDatum
        {
            get { return _sllCertificaatVervalDatum; }
            set { SetPropertyValue(nameof(SllCertificaatVervalDatum), ref _sllCertificaatVervalDatum, value); }
        }

        private string _messageVersie;
        public string MessageVersie
        {
            get { return _messageVersie; }
            set { SetPropertyValue(nameof(MessageVersie), ref _messageVersie, value); }
        }

        private string _interBaseVersie;
        public string InterBaseVersie
        {
            get { return _interBaseVersie; }
            set { SetPropertyValue(nameof(InterBaseVersie), ref _interBaseVersie, value); }
        }

        private string _mssqlServer;
        public string MssqlServer
        {
            get { return _mssqlServer; }
            set { SetPropertyValue(nameof(MssqlServer), ref _mssqlServer, value); }
        }

        private string _mssqlCatalog;
        public string MssqlCatalog
        {
            get { return _mssqlCatalog; }
            set { SetPropertyValue(nameof(MssqlCatalog), ref _mssqlCatalog, value); }
        }

        private string _kraan1DatabaseVersie;
        public string Kraan1DatabaseVersie
        {
            get { return _kraan1DatabaseVersie; }
            set { SetPropertyValue(nameof(Kraan1DatabaseVersie), ref _kraan1DatabaseVersie, value); }
        }

        private string _kraan2DatabaseVersie;
        public string Kraan2DatabaseVersie
        {
            get { return _kraan2DatabaseVersie; }
            set { SetPropertyValue(nameof(Kraan2DatabaseVersie), ref _kraan2DatabaseVersie, value); }
        }

        private string _response;
        [Size(SizeAttribute.Unlimited)]
        [Browsable(true)]
        [VisibleInListView(false)]
        public string Response
        {
            get { return _response; }
            set { SetPropertyValue(nameof(Response), ref _response, value); }
        }

        private string _soort;
        public string Soort
        {
            get { return _soort; }
            set { SetPropertyValue(nameof(Soort), ref _soort, value); }
        }

        [Association]
        public ResultTestKlant ResultTestKlant
        {
            get { return fResultTestKlant; }
            set { SetPropertyValue(nameof(ResultTestKlant), ref fResultTestKlant, value); }
        }
        ResultTestKlant fResultTestKlant;

        [Association]
        public ResultTestUrls ResultTestUrls
        {
            get { return fResultTestUrls; }
            set { SetPropertyValue(nameof(ResultTestUrls), ref fResultTestUrls, value); }
        }
        ResultTestUrls fResultTestUrls;

        [NonPersistent]
        [Browsable(false)]
        public bool IsException
        {
            get
            {
                return !_webserviceWerkt.Contains("true");
            }
        }

        public static IEnumerable<ResultTestEenUrlMessageService> GetUrlsByResultTestUrlMessageServiceSoap(Session session, Guid resultTestUrlId)
        {
            return session.Query<ResultTestEenUrlMessageService>().Where(w => w.ResultTestUrls.Oid == resultTestUrlId);
        }
    }
}