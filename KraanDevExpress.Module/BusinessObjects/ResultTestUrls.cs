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
    public class ResultTestUrls : BaseObject
    { 
        public ResultTestUrls(Session session)
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