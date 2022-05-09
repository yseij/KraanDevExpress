using Newtonsoft.Json.Linq;
using System;

namespace KraanDevExpress.Module.BusinessObjects
{
    class TestRoute
    {
        public void TestOneRoute(dynamic result,
                                ResultTestEenUrl resultTestEenUrl,
                                ResultTestKlant resultTestKlant)
        {
            foreach (JProperty item in result)
            {
                switch (item.Name)
                {
                    case "WebserviceVersie":
                        string[] strlist = item.Value.ToString().Split(':');
                        resultTestEenUrl.WebserviceVersie = strlist[1];
                        break;
                    case "certVerValDatum":
                        if (item.Value.ToString() != "")
                        {
                            resultTestEenUrl.Sll = true;
                            resultTestEenUrl.SllCertificaatVervalDatum = item.Value.ToString();
                        }
                        break;
                    case "KraanDll":
                        resultTestEenUrl.KraanDll = item.Value.ToString().Contains("True");
                        break;
                    case "KraanIni":
                        resultTestEenUrl.KaanIni = item.Value.ToString().Contains("True");
                        break;
                    case "KraanDatabase":
                        resultTestEenUrl.KraanDatabase = item.Value.ToString().Contains("True");
                        break;
                    case "id":
                        break;
                    case "ex":
                        resultTestEenUrl.Response = resultTestEenUrl.Response + item.Value;
                        if (resultTestKlant != null)
                        {
                            SetAantalFouten(resultTestKlant);
                        }
                        break;
                    default:
                        resultTestEenUrl.Response = resultTestEenUrl.Response + item.Name + " = " + item.Value + Environment.NewLine;
                        break;
                }
            }
        }

        public void TestOneRouteSoap(dynamic result,
                                     ResultTestEenUrlSoap resultTestEenUrlSoap,
                                     ResultTestKlant resultTestKlant )
        {
            foreach (JProperty item in result)
            {
                switch (item.Name)
                {
                    case "Webservice Versie":
                        resultTestEenUrlSoap.WebserviceVersie = item.Value.ToString().Replace("{", "").Replace("}", "");
                        break;
                    case "DevExpress versie":
                        resultTestEenUrlSoap.DevExpressVersie = item.Value.ToString().Replace("{", "").Replace("}", "");
                        break;
                    case "DatabaseVersie":
                        resultTestEenUrlSoap.DatabaseVersie = item.Value.ToString().Replace("{", "").Replace("}", "");
                        break;
                    case "ex":
                        resultTestEenUrlSoap.Response = resultTestEenUrlSoap.Response + item.Name + " = " + item.Value + Environment.NewLine;
                        if (resultTestKlant != null)
                        {
                            SetAantalFouten(resultTestKlant);
                        }
                        break;
                    case "certVerValDatum":
                        if (item.Value.ToString() != "")
                        {
                            resultTestEenUrlSoap.Sll = true;
                            resultTestEenUrlSoap.SllCertificaatVervalDatum = item.Value.ToString();
                        }
                        break;
                    case "id":
                        break;
                    default:
                        resultTestEenUrlSoap.Response = resultTestEenUrlSoap.Response + item.Name + " = " + item.Value + Environment.NewLine;
                        break;
                }
            }
        }

        public void TestOneRouteMessageService(dynamic result,
                                               ResultTestEenUrlMessageService resultTestEenUrlMessageService,
                                               ResultTestKlant resultTestKlant)
        {
            foreach (JProperty item in result)
            {
                switch (item.Name)
                {
                    case "Versie MessageService":
                        resultTestEenUrlMessageService.MessageVersie = item.Value.ToString().Split(':')[0];
                        break;
                    case "KraanDLL aanwezig":
                        resultTestEenUrlMessageService.KraanDll = item.Value.ToString().Contains("True");
                        break;
                    case "Kraan.ini aanwezig":
                        resultTestEenUrlMessageService.KraanIni = item.Value.ToString().Contains("True");
                        break;
                    case "InterBase server":
                        resultTestEenUrlMessageService.InterBaseVersie = item.Value.ToString().Split(':')[0];
                        break;
                    case "MSSQL server":
                        resultTestEenUrlMessageService.MssqlServer = item.Value.ToString().Split(':')[0];
                        break;
                    case "MSSQL catalog":
                        resultTestEenUrlMessageService.MssqlCatalog = item.Value.ToString().Split(':')[0];
                        break;
                    case "Databaseconnectie gelukt":
                        resultTestEenUrlMessageService.DatabaseConnectie = item.Value.ToString().Contains("True");
                        break;
                    case "Kraan 1 databaseversie":
                        resultTestEenUrlMessageService.Kraan1DatabaseVersie = item.Value.ToString().Split(':')[0];
                        break;
                    case "Kraan 2 databaseversie":
                        resultTestEenUrlMessageService.Kraan2DatabaseVersie = item.Value.ToString().Split(':')[0];
                        break;
                    case "certVerValDatum":
                        if (item.Value.ToString() != "")
                        {
                            resultTestEenUrlMessageService.Sll = true;
                            resultTestEenUrlMessageService.SllCertificaatVervalDatum = item.Value.ToString();
                        }
                        break;
                    case "ex":
                        resultTestEenUrlMessageService.Response = resultTestEenUrlMessageService.Response + item.Name + " = " + item.Value + Environment.NewLine;
                        if (resultTestKlant != null)
                        {
                            SetAantalFouten(resultTestKlant);
                        }
                        break;
                    default:
                        resultTestEenUrlMessageService.Response = resultTestEenUrlMessageService.Response + item.Name + " = " + item.Value + Environment.NewLine;
                        break;
                }
            }
        }

        private void SetAantalFouten(ResultTestKlant resultTestKlant)
        {
            resultTestKlant.AantalFout = resultTestKlant.AantalFout + 1;
        }
    }
}
