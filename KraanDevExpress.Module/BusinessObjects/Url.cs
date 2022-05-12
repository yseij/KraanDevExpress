using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KraanDevExpress.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Url : BaseObject
    { 
        public Url(Session session)
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
        
        private string _methodeName;
        [RuleRequiredField]
        public string MethodeName
        {
            get { return _methodeName; }
            set { SetPropertyValue(nameof(MethodeName), ref _methodeName, value); }
        }

        [RuleRequiredField]
        [ImmediatePostData]
        public KlantWebservice KlantWebservice
        {
            get { return fKlantWebservice; }
            set 
            { 
                SetPropertyValue(nameof(KlantWebservice), ref fKlantWebservice, value);
                OnChanged(nameof(HuidigeBasisUrl));
            }
        }
        KlantWebservice fKlantWebservice;

        
        [NonPersistent]
        public string HuidigeBasisUrl
        {
            get
            {
                if (KlantWebservice != null)
                {
                    if (KlantWebservice.BasisUrl1)
                    {
                        return KlantWebservice.Klant.BasisUrl1 + KlantWebservice.Webservice.Name;
                    }
                    return KlantWebservice.Klant.BasisUrl2 + KlantWebservice.Webservice.Name;
                }
                return null;
            }
        }

        public static IList<Url> GetUrlsByKlantWebservice(Session session, Guid klantWebserviceId)
        {
            return session.Query<Url>().Where(u => u.KlantWebservice.Oid == klantWebserviceId).ToList();
        }
    }
}