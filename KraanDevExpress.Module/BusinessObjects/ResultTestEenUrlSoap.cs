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
    public class ResultTestEenUrlSoap : BaseObject
    { 
        public ResultTestEenUrlSoap(Session session)
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

        private string _webserviceWerkt;
        public string WebserviceWerkt
        {
            get { return _webserviceWerkt; }
            set { SetPropertyValue(nameof(WebserviceWerkt), ref _webserviceWerkt, value); }
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

        private string _webserviceVersie;
        public string WebserviceVersie
        {
            get { return _webserviceVersie; }
            set { SetPropertyValue(nameof(WebserviceVersie), ref _webserviceVersie, value); }
        }

        private string _devExpressVersie;
        public string DevExpressVersie
        {
            get { return _devExpressVersie; }
            set { SetPropertyValue(nameof(DevExpressVersie), ref _devExpressVersie, value); }
        }

        private string _databaseVersie;
        public string DatabaseVersie
        {
            get { return _databaseVersie; }
            set { SetPropertyValue(nameof(DatabaseVersie), ref _databaseVersie, value); }
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

        public static IEnumerable<ResultTestEenUrlSoap> GetUrlsByResultTestUrlSoap(Session session, Guid resultTestUrlId)
        {
            return session.Query<ResultTestEenUrlSoap>().Where(w => w.ResultTestUrls.Oid == resultTestUrlId);
        }
    }
}