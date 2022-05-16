using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

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