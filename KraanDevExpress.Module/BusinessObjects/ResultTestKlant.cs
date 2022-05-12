﻿using DevExpress.Data.Filtering;
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
    public class ResultTestKlant : BaseObject
    { 
        public ResultTestKlant(Session session)
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