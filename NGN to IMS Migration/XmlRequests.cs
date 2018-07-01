using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Xml;
using System.Resources;

namespace NGN_to_IMS_Migration
{
  
    class XmlRequests
    {           

        static XmlEnvelopes soapEnvelope = new XmlEnvelopes();
        static XmlResponses xmlResponse = new XmlResponses();

        private static Object thisLock = new Object();

        public bool LST_HSUB(string subID, int caseCode, int iPort, out string sResult, int format = 1)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            if (!PGW_LGI(0, out webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";

                return false;
            }
            
            if (format == 1) subID = Program.NormalizeMSISDN(subID, 13);

            XmlDocument soapEnvelopeXml = soapEnvelope.LST_HSUB_PGW (subID, format);

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef, iPort);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "LST_HSUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResut = xmlResponse.myResponse(subID, webRequest, asyncResult, null, "LST_HSUB", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResut;
        }
      
        public bool HCAPSCSCF (string subscriber_id, string scscf, string pbxID, int caseCode, servRequest eRequest, out string sResult)
        {
            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 10);

            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            if (!PGW_LGI(0, out webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";
                return false;
            }

            XmlDocument soapEnvelopeXml = null;

            switch (eRequest)
            {
                case servRequest.prov:
                    soapEnvelopeXml = soapEnvelope.ADD_HCAPSCSCF_PGW (subscriber_id, scscf, pbxID);
                    break;
                case servRequest.remove:
                    soapEnvelopeXml = soapEnvelope.RMV_HCAPSCSCF_PGW (subscriber_id, scscf, pbxID);
                    break;
                case servRequest.printOut:
                    soapEnvelopeXml = soapEnvelope.LST_HCAPSCSCF_PGW (subscriber_id);
                    break;
                case servRequest.none:
                    break;
                default:
                    break;
            }
            

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subscriber_id, "HCAPSCSCF", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subscriber_id, webRequest, asyncResult, null, "HCAPSCSCF", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResult;
        }

        public bool ADD_HHSSSUB (System.Collections.Generic.Dictionary <string, string> InputKeys, UserType userType, out string sResult, int caseCode = 0)
        {
            string sSubscriberNumber = InputKeys["SUBSCRIBER NUMBER"];           

            sSubscriberNumber = Program.NormalizeMSISDN(sSubscriberNumber, 10);

            var _url = parameters.UrlPGW();
            var _action = "Notification";            

            if (!PGW_LGI(0, out string webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";                
                return false;
            }

            string sImpuList = string.Empty;
            string sIMPI = string.Empty;

            switch (userType)
            {
                case UserType.sipuser:
                    sIMPI = InputKeys["SIP ID"];
                    sImpuList = CreateIMPUList(sSubscriberNumber, sIMPI);                    
                    break;
                case UserType.trunkuser:
                    sIMPI = Program.NormalizeMSISDN (sSubscriberNumber, 11);
                    sImpuList = CreateIMPUList(sSubscriberNumber);
                    break;
                case UserType.trunkpilot:
                    sIMPI = Program.NormalizeMSISDN(sSubscriberNumber, 11);
                    string sPBX = InputKeys["PBX ID"];
                    sImpuList = CreateIMPUListSIPTrunk(sSubscriberNumber, sPBX);
                    break;
                case UserType.esluser:
                    sIMPI = Program.NormalizeMSISDN(sSubscriberNumber, 11);
                    sImpuList = CreateIMPUList(sSubscriberNumber);
                    break;
                case UserType.password:
                    break;
                case UserType.stp:
                    break;
                case UserType.none:
                    break;
                default:
                    break;
            }

            XmlDocument soapEnvelopeXml = soapEnvelope.ADD_HHSSSUB_PGW(sSubscriberNumber, sIMPI, sIMPI, InputKeys["PASSWORD"], sImpuList);

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(sSubscriberNumber, "ADD_HHSSSUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(sSubscriberNumber, webRequest, asyncResult, null, "ADD_HHSSSUB", caseCode, out sResult);

            PGW_LGO (0, webRef);

            return bResult;
        }

        public bool ADD_HHSSSUB(string subscriber_id, string impi, string husername, string pwd, bool bPilot, string trunkID, int caseCode, string webRef, out string sResult)
        {
            // SIP users & SIP trunks.

            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 10);

            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string impulist = CreateIMPUList(subscriber_id, impi);

            if (bPilot)
                impulist = CreateIMPUListSIPTrunk(subscriber_id, trunkID);  //(subscriber_id, impi, trunkID);

            XmlDocument soapEnvelopeXml = soapEnvelope.ADD_HHSSSUB_PGW(subscriber_id, impi, husername, pwd, impulist); //Program.hsspwd

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subscriber_id, "ADD_HHSSSUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subscriber_id, webRequest, asyncResult, null, "ADD_HHSSSUB", caseCode, out sResult);

            return bResult;
        }

        public bool ADD_HHSSSUB(string subscriber_id, string pwd, int caseCode, string webRef, out string sResult) // ESL Users
        {
            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 10);            

            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string impulist = CreateIMPUList(subscriber_id);

            string impi = Program.NormalizeMSISDN(subscriber_id, 11);            

            XmlDocument soapEnvelopeXml = soapEnvelope.ADD_HHSSSUB_PGW(subscriber_id, impi, impi, pwd, impulist); //Program.hsspwd

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subscriber_id, "ADD_HHSSSUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subscriber_id, webRequest, asyncResult, null, "ADD_HHSSSUB", caseCode, out sResult);

            return bResult;
        }

        public bool HSIFC(string impu, int caseCode, string webRef, int ifc, servRequest eRequest, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            if (string.IsNullOrEmpty(webRef))
            {
                if (!PGW_LGI(0, out webRef))
                {
                    sResult = "Login to PGW failed, please check user credentials.";

                    return false;
                }
            }

            impu = Program.NormalizeMSISDN(impu, 13); //TELURI

            XmlDocument soapEnvelopeXml = null;

            switch (eRequest)
            {
                case servRequest.prov:
                    soapEnvelopeXml = soapEnvelope.ADD_HSIFC_PGW(impu, ifc);
                    break;
                case servRequest.remove:
                    soapEnvelopeXml = soapEnvelope.RMV_HSIFC_PGW(impu, ifc);
                    break;
                case servRequest.printOut:
                    soapEnvelopeXml = soapEnvelope.LST_HSIFC_PGW(impu);
                    break;
                case servRequest.none:
                    break;
                default:
                    break;
            }                        

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(impu, "ADD_HSIFC", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(impu, webRequest, asyncResult, null, "ADD_HSIFC", caseCode, out sResult);

            return bResult;
        }       
  
        public bool RMV_HHSSSUB(string subID, int caseCode, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            if (!PGW_LGI(0, out webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";

                return false;
            }
            
            //subscriber_id = Program.NormalizeMSISDN(subscriber_id, 10);  from ADD HHSSSUB command.
            subID = Program.NormalizeMSISDN(subID, 10);

            XmlDocument soapEnvelopeXml = soapEnvelope.RMV_HHSSSUB_PGW (subID);

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "RMV_HHSSSUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subID, webRequest, asyncResult, null, "RMV_HHSSSUB", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResult;
        }

        public bool RMV_HHSSSUB(string subID, int caseCode, string webRef, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            subID = Program.NormalizeMSISDN(subID, 10);

            XmlDocument soapEnvelopeXml = soapEnvelope.RMV_HHSSSUB_PGW(subID);

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "RMV_HHSSSUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subID, webRequest, asyncResult, null, "RMV_HHSSSUB", caseCode, out sResult);            

            return bResult;
        }

        public bool ADD_DNAPTRREC(string subscriber_id, int caseCode, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            if (!PGW_LGI(0, out webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";

                return false;
            }

            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 10);

            string regexp = "!^.*$!sip:" + subscriber_id + "@" + parameters.defaultRealm() + "!";

            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 4);

            XmlDocument soapEnvelopeXml = soapEnvelope.ADD_DNAPTRREC_PGW (subscriber_id, regexp);

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subscriber_id, "ADD_DNAPTRREC", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subscriber_id, webRequest, asyncResult, null, "ADD_DNAPTRREC", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResult;            
        }

        public bool ADD_DNAPTRREC(string subscriber_id, int caseCode, string webRef, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 10);

            string regexp = "!^.*$!sip:" + subscriber_id + "@" + parameters.defaultRealm() + "!";

            subscriber_id = Program.NormalizeMSISDN(subscriber_id, 4);

            XmlDocument soapEnvelopeXml = soapEnvelope.ADD_DNAPTRREC_PGW(subscriber_id, regexp);

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subscriber_id, "ADD_DNAPTRREC", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subscriber_id, webRequest, asyncResult, null, "ADD_DNAPTRREC", caseCode, out sResult);

            return bResult;
        }

        public bool RMV_DNAPTRREC(string subID, int caseCode, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            if (!PGW_LGI(0, out webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";

                return false;
            }

            subID = Program.NormalizeMSISDN(subID, 4);

            XmlDocument soapEnvelopeXml = soapEnvelope.RMV_DNAPTRREC_PGW (subID);

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "RMV_DNAPTRREC", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subID, webRequest, asyncResult, null, "RMV_DNAPTRREC", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResult;
        }

        public bool RMV_DNAPTRREC(string subID, int caseCode, string webRef, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            subID = Program.NormalizeMSISDN(subID, 4);

            XmlDocument soapEnvelopeXml = soapEnvelope.RMV_DNAPTRREC_PGW(subID);

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "RMV_DNAPTRREC", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subID, webRequest, asyncResult, null, "RMV_DNAPTRREC", caseCode, out sResult);

            return bResult;
        }

        public bool LST_DNAPTRREC(string subID, int caseCode, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            if (!PGW_LGI(0, out webRef))
            {
                sResult = "Login to PGW failed, please check user credentials.";

                return false;
            }

            subID = Program.NormalizeMSISDN(subID, 4);

            XmlDocument soapEnvelopeXml = soapEnvelope.LST_DNAPTRREC_PGW(subID);

            HttpWebRequest webRequest = CreateWebRequestPGW(_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "LST_DNAPTRREC", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            bool bResult = xmlResponse.myResponse(subID, webRequest, asyncResult, null, "LST_DNAPTRREC", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResult;
        }

        public bool AGCF_MGW (string sEquipmentID, string sMediaGwDescription, int iGatewayType, int iProtocolType, servRequest eRequest, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            XmlDocument soapEnvelopeXml = null;

            switch (eRequest)
            {
                case servRequest.prov:
                    soapEnvelopeXml = soapEnvelope.ADD_AGCF_MGW(sEquipmentID, sMediaGwDescription, iGatewayType, iProtocolType);
                    break;
                case servRequest.remove:
                    soapEnvelopeXml = soapEnvelope.RMV_AGCF_MGW(sEquipmentID);
                    break;
                case servRequest.printOut:
                    soapEnvelopeXml = soapEnvelope.LST_AGCF_MGW(sEquipmentID);
                    break;
                case servRequest.none:
                    break;
                default:
                    break;
            }

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest (sEquipmentID, "AGCF_MGW", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(sEquipmentID, webRequest, asyncResult, null, "AGCF_MGW", caseCode, out sResult);
        }

        public bool AGCF_ASBR (string sSubscriberNumber, string sEquipmentID, string sTerminationID, string sPassword, int iGatewayType, int iProtocolType, servRequest eRequest, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            XmlDocument soapEnvelopeXml = null;

            string PUI = Program.NormalizeMSISDN(sSubscriberNumber, 12);
            string PRI = Program.NormalizeMSISDN(sSubscriberNumber, 11);

            switch (eRequest)
            {
                case servRequest.prov:
                    soapEnvelopeXml = soapEnvelope.ADD_AGCF_ASBR(PUI, PRI, sEquipmentID, sTerminationID, sPassword);
                    break;
                case servRequest.remove:
                    soapEnvelopeXml = soapEnvelope.RMV_AGCF_ASBR(PUI);
                    break;
                case servRequest.printOut:
                    soapEnvelopeXml = soapEnvelope.LST_AGCF_ASBR(PUI);
                    break;
                case servRequest.printAsbrMgw:
                    soapEnvelopeXml = soapEnvelope.LST_AGCF_ASBR_EID (sEquipmentID);
                    break;
                case servRequest.none:
                    break;
                default:
                    break;
            }            

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(sSubscriberNumber, "AGCF_MGW", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(sSubscriberNumber, webRequest, asyncResult, null, "AGCF_MGW", caseCode, out sResult);
        }

        public bool ADD_MSR(string impu, bool prepaid, int caseCode, int iCallSrcCode, out string sResult)
        {

            var _url = parameters.urlSPG();
            var _action = "Notification";            

            impu = Program.NormalizeMSISDN(impu, 13);

            XmlDocument soapEnvelopeXml = null;

            switch (iCallSrcCode)
            {
                case 0:
                case 1:
                    soapEnvelopeXml = soapEnvelope.ADD_MSR_SIP_USER(impu, prepaid, iCallSrcCode);

                    break;
                case 2:
                case 3:
                    soapEnvelopeXml = soapEnvelope.ADD_MSR_MGCP_H248 (impu, iCallSrcCode);

                    break;
                default:
                    sResult = string.Format("{0}: {1}", "INVLAID CALCULATED CALL SOURCE CODE", iCallSrcCode);
                    return false;
            }            

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(impu, "ADD_MSR", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(impu, webRequest, asyncResult, null, "ADD_MSR", caseCode, out sResult);            
        }

        public bool ADD_MSR(string impu, bool pilot, string trunkID, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            impu = Program.NormalizeMSISDN(impu, 13);

            XmlDocument soapEnvelopeXml = soapEnvelope.ADD_MSR_SIP_TRUNK(impu, pilot, trunkID);

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(impu, "ADD_MSR", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(impu, webRequest, asyncResult, null, "ADD_MSR", caseCode, out sResult);
        }

        public bool VOICE_BARRING_SUSPENSION (string impu, ServiceType serviceType,  int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            impu = Program.NormalizeMSISDN(impu, 13);

            XmlDocument soapEnvelopeXml = soapEnvelope.VOICE_BARRING_SUSPENSION(impu, serviceType);

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(impu, "VOICE_BAR_SUS", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(impu, webRequest, asyncResult, null, "VOICE_BAR_SUS", caseCode, out sResult);
        }

        public bool IDD_SERVICE (string impu, bool withdraw, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            impu = Program.NormalizeMSISDN(impu, 13);

            XmlDocument soapEnvelopeXml = soapEnvelope.IDD_SRV(impu, withdraw);

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(impu, "IDD_SRV", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(impu, webRequest, asyncResult, null, "IDD_SRV", caseCode, out sResult);
        }

        public bool USER_INPUT_SPG (string XmlInput, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";
            
            XmlDocument soapEnvelopeXml = soapEnvelope.USER_INPUT(XmlInput);

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(null, "USER_INPUT", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(null, webRequest, asyncResult, null, "USER_INPUT", caseCode, out sResult);
        }

        public bool USER_INPUT_PGW (string XmlInput, int caseCode, out string sResult)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string webRef = string.Empty;

            PGW_LGI(0, out webRef);

            XmlDocument soapEnvelopeXml = soapEnvelope.USER_INPUT(XmlInput);

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(null, "USER_INPUT", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            PGW_LGO(0, webRef);

            return xmlResponse.myResponse(null, webRequest, asyncResult, null, "USER_INPUT", caseCode, out sResult);
        }

        public bool LST_MSR(string subID, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            string impu = Program.NormalizeMSISDN(subID, 13); //TELURI

            XmlDocument soapEnvelopeXml = soapEnvelope.LST_MSR(impu);

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "LST_MSR", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(subID, webRequest, asyncResult, null, "LST_MSR", caseCode, out sResult);
        }

        public bool RMV_MSR(string subID, int caseCode, out string sResult)
        {
            var _url = parameters.urlSPG();
            var _action = "Notification";

            string impu = Program.NormalizeMSISDN(subID, 13); //TELURI

            XmlDocument soapEnvelopeXml = soapEnvelope.RMV_MSR(impu);            

            HttpWebRequest webRequest = CreateWebRequestSPG(_url, _action);            

            if (!InsertSoapEnvelopeIntoWebRequest(subID, "RMV_MSR", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            return xmlResponse.myResponse(subID, webRequest, asyncResult, null, "RMV_MSR", caseCode, out sResult);            
        }

        public bool PGW_LGI(int caseCode, out string webRef)
        {
            webRef = null;

            var _url = parameters.UrlPGW();
            var _action = "Notification";

            string sResult = string.Empty;

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, null);

            XmlDocument soapEnvelopeXml = soapEnvelope.PGW_LGI (webRequest.Address.Port);

            if (!InsertSoapEnvelopeIntoWebRequest(null, "PGW_LGI", soapEnvelopeXml, webRequest, out sResult))
                return false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            //string sResult = string.Empty;

            webRef = webRequest.Address.AbsolutePath;

            return xmlResponse.myResponse(null, webRequest, asyncResult, null, "PGW_LGI", caseCode, out sResult);            
        }  

        public void PGW_LGO(int caseCode, string webRef)
        {
            var _url = parameters.UrlPGW();
            var _action = "Notification";

            XmlDocument soapEnvelopeXml = soapEnvelope.PGW_LGO();

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(null, "PGW_LGO", soapEnvelopeXml, webRequest, out webRef))
                return;

            webRequest.AllowAutoRedirect = false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            string sResult = string.Empty;

            xmlResponse.myResponse(null, webRequest, asyncResult, null, "PGW_LGO", caseCode, out sResult);
        }
     
        public bool LST_SUB(string SubID, string param, int caseCode, out string sResult, bool byImsi = false) //string[] OutParam
        {
            string webRef = string.Empty;

            bool bResult = PGW_LGI(0, out webRef);            

            var _url = parameters.UrlPGW();
            var _action = "Notification";

            if (byImsi == false)
                SubID = Program.NormalizeMSISDN(SubID, 1);

            XmlDocument soapEnvelopeXml = soapEnvelope.LST_SUB(SubID, byImsi);

            HttpWebRequest webRequest = CreateWebRequestPGW (_url, _action, webRef);

            if (!InsertSoapEnvelopeIntoWebRequest(SubID, "LST_SUB", soapEnvelopeXml, webRequest, out sResult))
                return false;

            webRequest.AllowAutoRedirect = false;

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            bResult = xmlResponse.myResponse(SubID, webRequest, asyncResult, param, "LST_SUB", caseCode, out sResult);

            PGW_LGO(0, webRef);

            return bResult;
        }        
   
        public static HttpWebRequest CreateWebRequestPGW(string url, string action, string webRef, int iPort = 3999)
        {
            reTry:

            string _url = url;

            string[] address = _url.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            _url = GetActiveUrl(address);

            if (string.IsNullOrEmpty(_url))
                goto reTry;

            _url += webRef;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_url);           

            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            //ServicePoint currentServicePoint = webRequest.ServicePoint;

            //webRequest.ServicePoint.BindIPEndPointDelegate = (servicePoint, remoteEp, retryCount) =>
            //{
            //    return new IPEndPoint(IPAddress.Any, iPort);
            //};
            

            return webRequest;
        }
   
        private static HttpWebRequest CreateWebRequestSPG(string url, string action)
        {
            reTry:

            string _url = url;

            string[] address = _url.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            _url = GetActiveUrl(address);

            if (string.IsNullOrEmpty(_url))
                goto reTry;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            return webRequest;
        }    

        private static bool InsertSoapEnvelopeIntoWebRequest(string isdn, string cmd, XmlDocument soapEnvelopeXml, HttpWebRequest webRequest, out string sResult)
        {            
            sResult = "connected";

            try
            {
                using (System.IO.Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }
            }
            catch
            {   
                sResult = "communication failure. could not connect to " + webRequest.RequestUri.ToString();

                System.Threading.Thread.CurrentThread.Abort();

                return false;
            }
            return true;
        }

        private static string CreateIMPUList(string SubID, string impi)
        {
            //"\"sip:+96822000000@ims.ooredoo.om\"&\"tel:+96822000000\"&\"sip:LTEXXXXXXXXXXXX@ims.ooredoo.om\""
            string imputlist = "\"" + "sip:" + SubID + "@" + parameters.defaultRealm() + "\"" + "&amp;" + "\"" + "tel:" + SubID + "\"" + "&amp;" + "\"" + "sip:" + impi + "@" + parameters.defaultRealm() + "\"";

            return imputlist;
        }

        private static string CreateIMPUList(string SubID)
        {
            //"\"sip:+96822000000@ims.ooredoo.om\"&\"tel:+96822000000\"&\"sip:LTEXXXXXXXXXXXX@ims.ooredoo.om\""
            string imputlist = "\"" + "sip:" + SubID + "@" + parameters.defaultRealm() + "\"" + "&amp;" + "\"" + "tel:" + SubID + "\"";

            return imputlist;
        }

        /// <summary>
        /// This funtion will create IMPU list for SIP Trunk customers.
        /// </summary>
        /// <param name="SubID"></param>
        /// <param name="trunkID"></param>
        /// <returns></returns>
        private static string CreateIMPUListSIPTrunk(string SubID, string trunkID)
        {
            //string imputlist = "\"" + "sip:" + SubID + "@" + parameters.defaultRealm() + "\"" + "&amp;" + "\"" + "tel:" + SubID + "\"" + "&amp;" + "\"" + "sip:" + impi + "@" + parameters.defaultRealm() + "\"" + "&amp;" + "\"" + "sip:" + trunkID + "\"";
            string imputlist = "\"" + "sip:" + SubID + "@" + parameters.defaultRealm() + "\"" + "&amp;" + "\"" + "tel:" + SubID + "\"" + "&amp;" + "\"" + "sip:" + trunkID + "\"";

            return imputlist;
        }

        private static string GetActiveUrl(string[] address)
        {            
            HttpWebRequest webRequest = null;

            try
            {
                foreach (var item in address)
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(item);

                    var ping = new System.Net.NetworkInformation.Ping();
                    var reply = ping.Send(webRequest.Address.Host, 3000);

                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                        return item;
                }

                throw new Exception("No Active IP adddress found, make sure network IP is correct and conneted to the right network.");

            }
            catch (Exception)
            {
                return null;
            }
        }        
    }
}
