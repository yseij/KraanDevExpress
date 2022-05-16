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
    public class Webservice : BaseObject
    { 
        public Webservice(Session session)
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

        private bool _soap;
        [RuleRequiredField]
        public bool Soap
        {
            get { return _soap; }
            set { SetPropertyValue(nameof(Soap), ref _soap, value); }
        }

        private string _securityId;
        public string SecurityId
        {
            get { return _securityId; }
            set { SetPropertyValue(nameof(_securityId), ref _securityId, value); }
        }

        public static IEnumerable<Webservice> GetKWebservices(Session session)
        {
            return session.Query<Webservice>();
        }
    }
}