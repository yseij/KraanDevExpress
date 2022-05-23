using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace KraanDevExpress.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class KlantWebservice : BaseObject
    { 
        public KlantWebservice(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [NonPersistent]
        [Browsable(false)]
        public string Name
        {
            get { return Klant.Name.ToString() + " --- " + Webservice.Name.ToString(); }
        }

        [Association]
        public Klant Klant
        {
            get { return fKlant; }
            set { SetPropertyValue(nameof(Klant), ref fKlant, value); }
        }
        Klant fKlant;

        public Webservice Webservice
        {
            get { return fWebservice; }
            set { SetPropertyValue(nameof(Webservice), ref fWebservice, value); }
        }
        Webservice fWebservice;

        private bool _basisUrl1;
        public bool BasisUrl1
        {
            get { return _basisUrl1; }
            set { SetPropertyValue(nameof(BasisUrl1), ref _basisUrl1, value); }
        }

        private bool _basisUrl2;
        public bool BasisUrl2
        {
            get { return _basisUrl2; }
            set { SetPropertyValue(nameof(BasisUrl2), ref _basisUrl2, value); }
        }
    }
}