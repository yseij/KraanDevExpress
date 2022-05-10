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
    public class KlantWebservice : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public KlantWebservice(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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