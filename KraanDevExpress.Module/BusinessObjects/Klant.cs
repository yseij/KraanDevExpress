using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace KraanDevExpress.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Klant : BaseObject
    { 
        public Klant(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string _name;
        [RuleRequiredField]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(nameof(Name), ref _name, value); }
        }

        private string _basisUrl1;
        [RuleRequiredField]
        public string BasisUrl1
        {
            get { return _basisUrl1; }
            set { SetPropertyValue(nameof(BasisUrl1), ref _basisUrl1, value); }
        }

        private string _basisUrl2;
        public string BasisUrl2
        {
            get { return _basisUrl2; }
            set { SetPropertyValue(nameof(BasisUrl2), ref _basisUrl2, value); }
        }

        [Association]
        public XPCollection<KlantWebservice> klantWebservices
        {
            get
            {
                return GetCollection<KlantWebservice>(nameof(klantWebservices));
            }
        }
    }
}