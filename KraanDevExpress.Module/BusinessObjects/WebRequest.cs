using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KraanDevExpress.Module.BusinessObjects
{
    class WebRequest
    {
        private string _webserviceVersie;
        private string _kraanDll;
        private string _kraanIni;
        private string _kraanDatabase;

        private int _positionKraanDll;
        private int _positionKraanIni;
        private int _positionDatabaseConnect;
        private int _positionDatabaseMelding;

        private bool _certIsGoed;

        //REST
        public string GetWebRequestRest(Guid id, string host, bool isWebserviceVersion)
        {
            string url = host;
            Uri uri = new Uri(url);
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(host) as HttpWebRequest;
                HttpClient client = new HttpClient();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    X509Certificate cert = GetCertificate(request);
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        client.BaseAddress = uri;
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response1 = client.GetAsync(uri).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                        string data = response1.Content.ReadAsStringAsync().Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            //Parse the response body.
                            //Make sure to add a reference to System.Net.Http.Formatting.dll
                            if (isWebserviceVersion)
                            {
                                if (_certIsGoed)
                                {
                                    return GetDataOfWebRequest(data, cert.GetExpirationDateString().ToString());
                                }
                                else
                                {
                                    return GetDataOfWebRequest(data, "");
                                }
                            }
                            else
                            {
                                int Pos1 = data.IndexOf('{');
                                int Pos2 = data.IndexOf('}');
                                data = data.Substring(Pos1 + 1, Pos2 - Pos1 - 1);
                                if (_certIsGoed)
                                {
                                    return "{" + data + ", id: '" + id + "', certVerValDatum: '" + cert.GetExpirationDateString().ToString() + "'}";
                                }
                            }
                        }
                    }
                    return @"{ ex: '" + "krijg geen data terug" + "'}";
                }
            }
            catch (Exception ex)
            {
                return @"{ ex: '" + ex.Message.ToString() + "'}";
            }
        }

        private string GetDataOfWebRequest(string data, string verValDatum = "")
        {
            _positionKraanDll = data.IndexOf("KraanDLL");
            _positionKraanIni = data.IndexOf("Kraan.ini");
            _positionDatabaseConnect = data.IndexOf("Database connectie");
            _positionDatabaseMelding = data.IndexOf("Database melding");

            _webserviceVersie = data.Substring(0, _positionKraanDll);
            _kraanDll = data.Substring(_positionKraanDll, _positionKraanIni - _positionKraanDll);
            _kraanIni = data.Substring(_positionKraanIni, _positionDatabaseConnect - _positionKraanIni);
            _kraanDatabase = data.Substring(_positionDatabaseConnect, _positionDatabaseMelding - _positionDatabaseConnect);

            return @"{ WebserviceVersie: '" + _webserviceVersie + "', KraanDll: '" + _kraanDll + "', KraanIni: '" + _kraanIni + "', KraanDatabase: '" + _kraanDatabase + "', certVerValDatum: '" + verValDatum + "'}";
        }

        public string CheckUrl(string host)
        {
            Uri uri = new Uri(host);
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response1 = client.GetAsync(uri).Result;
                if (response1.IsSuccessStatusCode)
                {
                    return "true";
                }
                return "false met statuscode: " + response1.StatusCode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private X509Certificate GetCertificate(HttpWebRequest request)
        {
            X509Certificate cert = request.ServicePoint.Certificate;
            X509Certificate2 cert2 = null;
            if (cert != null)
            {
                cert2 = new X509Certificate2(cert);
                _certIsGoed = cert2.Verify();
            }
            return cert;
        }

        //SOAP
        public string GetWebRequestSoap(string host, string service)
        {
            string result = "";

            //YouriWebserviceAuth.AuthServiceClient clientAuth;
            YouriWebserviceCrm.CrmServiceClient clientCrm;
            YouriWebserviceWorkFlow.WorkflowServiceClient clientWorkflow;
            YouriWebserviceUren.UrenServiceClient clientUren;
            YouriWebserviceMaterieel.MaterieelServiceClient clientMaterieel;
            //YouriWebserviceWeb.WebServiceClient clientWeb;

            switch (service)
            {
                //case "AuthService.svc":
                //    clientAuth = NewAuthService(host);
                //    clientAuth.Open();
                //    result = clientAuth.GetVersion().ToString();
                //    clientAuth.Close();
                //    break;
                case "CrmService.svc":
                    clientCrm = NewCrmService(host);
                    clientCrm.Open();
                    result = clientCrm.GetVersion();
                    clientCrm.Close();
                    break;
                case "WorkflowService.svc":
                    clientWorkflow = NewWorkFlowService(host);
                    clientWorkflow.Open();
                    result = clientWorkflow.GetVersion();
                    clientWorkflow.Close();
                    break;
                case "UrenService.svc":
                    clientUren = NewUrenService(host);
                    clientUren.Open();
                    result = clientUren.GetVersion();
                    clientUren.Close();
                    break;
                case "MaterieelService.svc":
                    clientMaterieel = NewMateriaalService(host);
                    clientMaterieel.Open();
                    result = clientMaterieel.GetVersion();
                    clientMaterieel.Close();
                    break;
                //case "Webservice.svc":
                //    clientMaterieel = NewWebSerivce(host);
                //    clientMaterieel.Open();
                //    result = clientMaterieel.GetVersion().ToString();
                //    clientMaterieel.Close();
                //    break;
                default:
                    return @"{ ex: '" + " deze service bestaat niet " + "'}"; ;

            }
            return GetDataOfWebRequestSoap(result);
        }

        //private YouriWebserviceAuth.AuthServiceClient NewAuthService(string host)
        //{
        //    BasicHttpBinding binding = CreateBinding("AuthService");
        //    EndpointAddress epa = CreateEndpointAddress(host, "AuthService.svc");

        //    return new YouriWebserviceAuth.AuthServiceClient(binding, epa);
        //}

        private YouriWebserviceCrm.CrmServiceClient NewCrmService(string host)
        {
            BasicHttpBinding binding = CreateBinding("CrmService");
            EndpointAddress epa = CreateEndpointAddress(host, "");

            return new YouriWebserviceCrm.CrmServiceClient(binding, epa);
        }

        private YouriWebserviceMaterieel.MaterieelServiceClient NewMateriaalService(string host)
        {
            BasicHttpBinding binding = CreateBinding("MaterieelService");
            EndpointAddress epa = CreateEndpointAddress(host, "");

            return new YouriWebserviceMaterieel.MaterieelServiceClient(binding, epa);
        }

        private YouriWebserviceUren.UrenServiceClient NewUrenService(string host)
        {
            BasicHttpBinding binding = CreateBinding("UrenService");
            EndpointAddress epa = CreateEndpointAddress(host, "");

            return new YouriWebserviceUren.UrenServiceClient(binding, epa);
        }

        private YouriWebserviceWorkFlow.WorkflowServiceClient NewWorkFlowService(string host)
        {
            BasicHttpBinding binding = CreateBinding("WorkflowService");
            EndpointAddress epa = CreateEndpointAddress(host, "");

            return new YouriWebserviceWorkFlow.WorkflowServiceClient(binding, epa);
        }

        //private YouriWebserviceWorkFlow.WorkflowServiceClient NewWebSerivce(string host)
        //{
        //    BasicHttpBinding binding = CreateBinding("Webservice");
        //    EndpointAddress epa = CreateEndpointAddress(host, "Webservice.svc");

        //    return new YouriWebserviceWorkFlow.WorkflowServiceClient(binding, epa);
        //}

        private Sales24.MessageServiceSoapClient NewSales24Client(string host)
        {

            BasicHttpBinding binding = CreateBinding("MessageServiceSoap");
            EndpointAddress epa = CreateEndpointAddress(host, "messageservicesoap.svc");

            return new Sales24.MessageServiceSoapClient(binding, epa);
        }

        private Sales31.MessageServiceSoapClient NewSales31Client(string host)
        {
            BasicHttpBinding bindings = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
            bindings.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;

            bindings.MaxReceivedMessageSize = 2147483647;
            var elements = bindings.CreateBindingElements();
            elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
            elements.Find<SecurityBindingElement>().IncludeTimestamp = false;
            CustomBinding cusbinding = new CustomBinding(elements);
            EndpointAddress epa = CreateEndpointAddress(host, "messageservicesoap31.svc");

            return new Sales31.MessageServiceSoapClient(cusbinding, epa);
        }

        private BasicHttpBinding CreateBinding(string bindingName)
        {
            BasicHttpBinding serviceBinding = new BasicHttpBinding();
            serviceBinding.Name = bindingName;
            serviceBinding.Security.Mode = BasicHttpSecurityMode.Transport;
            serviceBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            return serviceBinding;
        }

        private EndpointAddress CreateEndpointAddress(string host, string endPointName = "")
        {
            string endPointString = host;
            if (host.ToLower().Contains("messageservicesoap.svc") || host.ToLower().Contains("messageservicesoap31.svc"))
            { return new EndpointAddress(host); }
            if (endPointString.Last() != '/')
            {
                endPointString += '/';
            }
            endPointString = endPointString + endPointName;

            EndpointAddress epa = new EndpointAddress(endPointString);
            return epa;
        }

        public string Get24SalesData(string host)
        {
            using (Sales24.MessageServiceSoapClient client = NewSales24Client(host))
            {
                try
                {
                    client.Open();
                    Sales24.MessageServiceMessage message = new Sales24.MessageServiceMessage();
                    message.MsgType = "CST_KRAAN_VERSION";

                    HttpWebRequest request = HttpWebRequest.Create(host) as HttpWebRequest;
                    X509Certificate cert = GetCertificate(request);

                    bool succes = client.PostMessage(null, null, ref message);

                    var data = "{\"" + message.Text[0]
                        .Replace("\r\n", "\",\"")
                        .Replace(": ", "\": \"")
                        .Replace(@"\", " ")
                        .Replace("Versie\": \"", "Versie: ") + "\", \"certVerValDatum\": " + "\"" + cert.GetExpirationDateString().ToString() + "\"" + "}";
                    client.Close();
                    return data;
                }
                catch (Exception ex)
                {
                    return @"{ ex: '" + ex.Message.ToString() + "'}";
                }
            }
        }

        public string Get31SalesData(string host, string TxtBxUsername, string TxtBxPassword)
        {
            using (Sales31.MessageServiceSoapClient client = NewSales31Client(host))
            {
                client.ClientCredentials.UserName.UserName = TxtBxUsername.Trim();
                client.ClientCredentials.UserName.Password = TxtBxPassword.Trim();
                if (client.ClientCredentials.UserName.UserName == string.Empty || client.ClientCredentials.UserName.Password == string.Empty)
                {
                    return @"{ ex: '" + "gebruikersnaam of wachtwoord is niet ingevuld" + "'}";
                }
                try
                {
                    client.Open();
                    Sales31.MessageType message = new Sales31.MessageType();
                    message.MsgProperties = new Sales31.MessagePropertiesType();
                    message.MsgProperties.MsgType = "CST_KRAAN_VERSION";

                    HttpWebRequest request = HttpWebRequest.Create(host) as HttpWebRequest;
                    X509Certificate cert = GetCertificate(request);
                    try
                    {
                        Sales31.MessageResponseType antwoord = client.PostMessage(null, message);
                        if (antwoord.Message.MsgContent != null)
                        {
                            var data = "{\""
                                + antwoord.Message.MsgContent.Trim()
                                .Replace("\r\n", "\", \"")
                                .Replace(": ", "\": \"")
                                .Replace(@"\", " ")
                                .Replace("application\": \"", "application: ")
                                .Replace("Versie\": \"", "Versie: ") + "\", \"certVerValDatum\": " + "\"" + cert.GetExpirationDateString().ToString() + "\"" + "}";
                            client.Close();
                            return data;
                        }
                        client.Close();
                    }
                    catch (Exception ex)
                    {
                        return @"{ ex: '" + ex.Message.ToString() + "'}"; ;
                    }
                }
                catch (FaultException fex)
                {
                    var msgFault = fex.CreateMessageFault();
                    if (msgFault.HasDetail)
                    {
                        var detailNode = msgFault.GetDetail<XmlElement>();
                        return @"{ ex: '" + "Fout bij beveiligd verbinden met Sales 3.1. \r\nFoutcode " + detailNode.GetElementsByTagName("ErrorCode", detailNode.NamespaceURI)[0].InnerText + " : " + detailNode.GetElementsByTagName("Message", detailNode.NamespaceURI)[0].InnerText + "'}"; ;
                    }
                    else
                    {
                        if (fex.Code.Name == "Server" && fex.Message == "Server error")
                        {
                            return @"{ ex: '" + "Fout bij beveiligd verbinden met Sales 3.1. Credentials incorrect." + "'}"; ;
                        }
                        else
                        {
                            return @"{ ex: '" + "Fout bij beveiligd verbinden met Sales 3.1. \r\nFoutmelding: " + fex.Message + "'}"; ;
                        }
                    }
                }
                return "Geen beveiligde verbinding mogelijk";
            }
        }

        private string GetDataOfWebRequestSoap(string result)
        {
            string data = result.Replace("----", "");
            int positionWebserviceVersie = data.IndexOf("Webservice versie");
            int positionDevExpressVersie = data.IndexOf("DevExpress versie");
            int positionDatabaseVersie = data.IndexOf("DatabaseVersie");

            string webserviceVersie = data.Substring(positionWebserviceVersie, positionDevExpressVersie - positionWebserviceVersie);
            string devExpressVersie = data.Substring(positionDevExpressVersie, positionDatabaseVersie - positionDevExpressVersie);
            string databaseVersie = data.Substring(positionDatabaseVersie, data.Length - positionDatabaseVersie);
            return "{ \"Webservice Versie\": " + "\"" + webserviceVersie.Split(':')[1] + "\"" + ", \"DevExpress versie\": " + "\"" + devExpressVersie.Split(':')[1] + "\"" + ", \"DatabaseVersie\": " + "\"" + devExpressVersie.Split(':')[1] + "\"" + "}";
        }
    }
}
