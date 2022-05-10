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
        
        private string _namethodeName;
        [RuleRequiredField]
        public string MethodeName
        {
            get { return _namethodeName; }
            set { SetPropertyValue(nameof(MethodeName), ref _namethodeName, value); }
        }

        [RuleRequiredField]
        public KlantWebservice KlantWebservice
        {
            get { return fKlantWebservice; }
            set { SetPropertyValue(nameof(KlantWebservice), ref fKlantWebservice, value); }
        }
        KlantWebservice fKlantWebservice;

        public static IList<Url> GetUrlsByKlantWebservice(Session session, Guid klantWebserviceId)
        {
            return session.Query<Url>().Where(u => u.KlantWebservice.Oid == klantWebserviceId).ToList();
        }
    }
}