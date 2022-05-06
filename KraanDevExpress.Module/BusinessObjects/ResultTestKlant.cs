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
    public class ResultTestKlant : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public ResultTestKlant(Session session)
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

        private int _aantalFout;
        public int AantalFout
        {
            get { return _aantalFout; }
            set { SetPropertyValue(nameof(AantalFout), ref _aantalFout, value); }
        }

        [Association]
        public XPCollection<ResultTestEenUrl> ResultTestEenUrls
        {
            get
            {
                return GetCollection<ResultTestEenUrl>(nameof(ResultTestEenUrls));
            }
        }

        [Association]
        public XPCollection<ResultTestEenUrlMessageService> ResultTestEenUrlMessageServices
        {
            get
            {
                return GetCollection<ResultTestEenUrlMessageService>(nameof(ResultTestEenUrlMessageServices));
            }
        }

        [Association]
        public XPCollection<ResultTestEenUrlSoap> ResultTestEenUrlSoaps
        {
            get
            {
                return GetCollection<ResultTestEenUrlSoap>(nameof(ResultTestEenUrlSoaps));
            }
        }
    }
}