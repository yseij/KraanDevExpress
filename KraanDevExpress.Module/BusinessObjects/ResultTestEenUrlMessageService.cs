using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KraanDevExpress.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ResultTestEenUrlMessageService : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public ResultTestEenUrlMessageService(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(nameof(Name), ref _name, value); }
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

        [Association]
        public Url Url
        {
            get { return fUrl; }
            set { SetPropertyValue(nameof(Url), ref fUrl, value); }
        }
        Url fUrl;

        [NonPersistent]
        [Browsable(false)]
        private bool IsException
        {
            get
            {
                if (_response == null)
                {
                    return false;
                }
                return _response.Contains("ex = ");
            }
        }

        public static IEnumerable<ResultTestEenUrlMessageService> GetUrlsByResultTestUrlMessageServiceSoap(Session session, Guid resultTestUrlId)
        {
            return session.Query<ResultTestEenUrlMessageService>().Where(w => w.ResultTestUrls.Oid == resultTestUrlId);
        }
    }
}