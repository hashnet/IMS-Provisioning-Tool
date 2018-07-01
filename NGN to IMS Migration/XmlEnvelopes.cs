using System.Configuration;
using System.Xml;

namespace NGN_to_IMS_Migration
{
    enum ServiceType
    {
        normal, suspend, resume, odbActivate, odbDeactivate
    }

    class XmlEnvelopes
    {     
        public XmlDocument PGW_LGI(int portNum)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            switch (portNum)
            {
                case 8001:
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
                                        <soapenv:Body>
                                            <LGI>
                                                <HLRSN>1</HLRSN>
                                                <OPNAME>" + parameters.userPGW() + @"</OPNAME>
                                                <PWD>" + parameters.passPGW() + @"</PWD>
                                            </LGI></soapenv:Body>
                                        </soapenv:Envelope>");
                    break;

                case 8080:
                    soapEnvelop.LoadXml(@"<?xml version=""1.0"" encoding=""UTF-8""?><soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:rm=""rm:soap""> 
                                 <soapenv:Header/> 
                                   <soapenv:Body> 
                                      <rm:LGI> 
                                         <inPara>             
                                            <Login>                
                                               <attribute> 
                                                  <key>OPNAME</key> 
                                                  <value>" + parameters.userPGW() + @"</value> 
                                               </attribute> 
                                               <attribute> 
                                                  <key>PWD</key> 
                                                  <value>" + parameters.passPGW() + @"</value> 
                                               </attribute> 
                                            </Login> 
                                         </inPara> 
                                      </rm:LGI> 
                                   </soapenv:Body> 
                                </soapenv:Envelope>");
                    break;

                default:
                    PGW_LGI(8001);

                    break;
            }

            return soapEnvelop;
        }

        public XmlDocument PGW_LGO()
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
                                    <soapenv:Body>
	                                    <LGO></LGO>
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument LST_ISDN(string imsi)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:lst=""http://www.huawei.com/HLR9820/LST_ISDN""><soapenv:Header/>
                                    <soapenv:Body>
	                                    <lst:LST_ISDN>
		                                    <lst:IMSI>" + imsi + @"</lst:IMSI>
	                                    </lst:LST_ISDN>         
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument LST_SUB(string isdn, bool byImsi = false)
        {

            XmlDocument soapEnvelop = new XmlDocument();

            switch (byImsi)
            {
                case false:
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:lst=""http://www.huawei.com/HLR9820/LST_SUB""><soapenv:Header/>
                                    <soapenv:Body>
	                                    <lst:LST_SUB>
		                                    <lst:ISDN>" + isdn + @"</lst:ISDN>
		                                    <lst:DETAIL>1</lst:DETAIL>
	                                    </lst:LST_SUB>         
                                    </soapenv:Body></soapenv:Envelope>");
                    break;

                case true:
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:lst=""http://www.huawei.com/HLR9820/LST_SUB""><soapenv:Header/>
                                    <soapenv:Body>
	                                    <lst:LST_SUB>
		                                    <lst:IMSI>" + isdn + @"</lst:IMSI>
		                                    <lst:DETAIL>1</lst:DETAIL>
	                                    </lst:LST_SUB>         
                                    </soapenv:Body></soapenv:Envelope>");

                    break;

            }

            return soapEnvelop;
        }

        public XmlDocument LST_HSUB_SPG(string SubID, int format)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            switch (format)
            {
                case 1: // by IMPU
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                            <soapenv:Header>
                                                <hss:MessageID>1</hss:MessageID>
                                                <hss:MEName/>
                                                <hss:Authentication>
                                                    <hss:Username>" + parameters.userSPG() + @"</hss:Username>
                                                    <hss:Password>" + parameters.passSPG() + @"</hss:Password>
                                                </hss:Authentication>
                                                </soapenv:Header>
                                                    <soapenv:Body>
                                                        <hss:LST_HSUB>
                                                            <hss:IMPU>" + SubID + @"</hss:IMPU>
                                                        </hss:LST_HSUB>
                                                    </soapenv:Body>
                                                </soapenv:Envelope>");

                    break;

                default: //default = IMPI (case = 0)
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                            <soapenv:Header>
	                                            <hss:MessageID>1</hss:MessageID>
	                                            <hss:MEName/>
	                                            <hss:Authentication>
		                                            <hss:Username>" + parameters.userSPG() + @"</hss:Username>
                                                    <hss:Password>" + parameters.passSPG() + @"</hss:Password>
                                                </hss:Authentication>
                                                </soapenv:Header>
                                                    <soapenv:Body>
                                                        <hss:LST_HSUB>
                                                             <hss:IMPI>" + SubID + @"</hss:IMPI>
                                                        </hss:LST_HSUB>
                                                    </soapenv:Body>
                                                </soapenv:Envelope>");

                    break;
            }

            return soapEnvelop;

        }

        public XmlDocument LST_HSUB_PGW(string SubID, int format)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            switch (format)
            {
                case 1: // by IMPU
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">                                       
                                                    <soapenv:Body>
                                                        <hss:LST_HSUB>
                                                            <hss:IMPU>" + SubID + @"</hss:IMPU>
                                                        </hss:LST_HSUB>
                                                    </soapenv:Body>
                                                </soapenv:Envelope>");

                    break;

                default: //default = IMPI 
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                                    <soapenv:Body>
                                                        <hss:LST_HSUB>
                                                             <hss:IMPI>" + SubID + @"</hss:IMPI>
                                                        </hss:LST_HSUB>
                                                    </soapenv:Body>
                                                </soapenv:Envelope>");

                    break;
            }

            return soapEnvelop;

        }

        public XmlDocument LST_HSUBDATA(string impi)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Header>    
                                    <hss:MessageID>1</hss:MessageID>
                                    <hss:MEName/>
                                    <hss:Authentication>
                                        <hss:Username>" + parameters.userSPG() + @"</hss:Username>
                                        <hss:Password>" + parameters.passSPG() + @"</hss:Password>
                                    </hss:Authentication>
                                    </soapenv:Header>
                                    <soapenv:Body>
                                        <hss:LST_HSUBDATA>
                                            <hss:IMPI>" + impi + @"</hss:IMPI>
                                        </hss:LST_HSUBDATA>
                                    </soapenv:Body>
                                    </soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument LST_HSIFC_SPG(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Body>
                                        <hss:LST_HSIFC>
                                            <hss:IMPU>" + impu + @"</hss:IMPU>
                                        </hss:LST_HSIFC>
                                    </soapenv:Body>
                                </soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument LST_HSIFC_PGW(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Body>
                                        <hss:LST_HSIFC>
                                            <hss:IMPU>" + impu + @"</hss:IMPU>
                                        </hss:LST_HSIFC>
                                    </soapenv:Body>
                                </soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument LST_DNAPTRREC_SPG(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ens=""http://www.huawei.com/ENS"">   
                                    <soapenv:Header>      
                                    <ens:MessageID>1</ens:MessageID>      
                                    <ens:Authentication>         
                                        <ens:Username>" + parameters.userSPG() + @"</ens:Username>        
                                        <ens:Password>" + parameters.passSPG() + @"</ens:Password>      
                                    </ens:Authentication>   
                                    </soapenv:Header>   
                                    <soapenv:Body>      
                                        <ens:LST_DNAPTRREC>         
                                            <ens:E164NUM>" + subID + @"</ens:E164NUM>         
                                            <ens:ZONENAME>" + parameters.imsEnsZone() + @"</ens:ZONENAME>      
                                        </ens:LST_DNAPTRREC>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument LST_DNAPTRREC_PGW(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ens=""http://www.huawei.com/ENS"">
                                    <soapenv:Body>      
                                        <ens:LST_DNAPTRREC>         
                                            <ens:E164NUM>" + subID + @"</ens:E164NUM>         
                                            <ens:ZONENAME>" + parameters.imsEnsZone() + @"</ens:ZONENAME>      
                                        </ens:LST_DNAPTRREC>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_HHSSSUB_SPG(string subID, string impi, string husername, string pwd, string impulist)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">   
                                    <soapenv:Header>      
                                        <hss:MessageID>1</hss:MessageID>      
                                        <hss:Authentication>         
                                            <hss:Username>" + parameters.userSPG() + @"</hss:Username>         
                                            <hss:Password>" + parameters.passSPG() + @"</hss:Password>      
                                        </hss:Authentication>   
                                    </soapenv:Header>   
                                    <soapenv:Body>      
                                        <hss:ADD_HHSSSUB>         
                                            <hss:SUBID>" + subID + @"</hss:SUBID>         
                                            <hss:IMPI>" + impi + @"</hss:IMPI>         
                                            <hss:IMPIAUTHTYPE>SDA</hss:IMPIAUTHTYPE>         
                                            <hss:HUSERNAME>" + husername + @"</hss:HUSERNAME>         
                                            <hss:PWD>" + pwd + @"</hss:PWD>         
                                            <hss:REALM>" + parameters.defaultRealm() + @"</hss:REALM>
                                            <hss:IMPULIST>" + impulist + @"</hss:IMPULIST>         
                                            <hss:IMPUTPLID>1</hss:IMPUTPLID>         
                                            <hss:IRSID>1</hss:IRSID>         
                                            <hss:ALIASID>1</hss:ALIASID>      
                                        </hss:ADD_HHSSSUB>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_HHSSSUB_PGW(string subID, string impi, string husername, string pwd, string impulist)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">                                 
                                    <soapenv:Body>      
                                        <hss:ADD_HHSSSUB>         
                                            <hss:SUBID>" + subID + @"</hss:SUBID>         
                                            <hss:IMPI>" + impi + @"</hss:IMPI>         
                                            <hss:IMPIAUTHTYPE>SDA</hss:IMPIAUTHTYPE>         
                                            <hss:HUSERNAME>" + husername + @"</hss:HUSERNAME>         
                                            <hss:PWD>" + pwd + @"</hss:PWD>         
                                            <hss:REALM>" + parameters.defaultRealm() + @"</hss:REALM>
                                            <hss:IMPULIST>" + impulist + @"</hss:IMPULIST>         
                                            <hss:IMPUTPLID>1</hss:IMPUTPLID>         
                                            <hss:IRSID>1</hss:IRSID>         
                                            <hss:ALIASID>1</hss:ALIASID>      
                                        </hss:ADD_HHSSSUB>   
                                    </soapenv:Body>
                                  </soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_HCAPSCSCF_PGW(string subID, string scscf, string trunkID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            if (string.IsNullOrEmpty(scscf))
                scscf = parameters.trunkSCSCF();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                     <soapenv:Body>
                                          <hss:ADD_HCAPSCSCF>
                                               <hss:SUBID>" + subID + @"</hss:SUBID>
                                               <hss:SCSCF>" + scscf + ";pbxid=" + trunkID + @"</hss:SCSCF>                                               
                                          </hss:ADD_HCAPSCSCF>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");

            return soapEnvelop;

        }

        public XmlDocument RMV_HCAPSCSCF_PGW(string subID, string scscf, string trunkID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            if (string.IsNullOrEmpty(scscf))
                scscf = parameters.trunkSCSCF();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                     <soapenv:Body>
                                          <hss:RMV_HCAPSCSCF>
                                               <hss:SUBID>" + subID + @"</hss:SUBID>
                                               <hss:SCSCF>" + scscf + ";pbxid=" + trunkID + @"</hss:SCSCF>                                               
                                          </hss:RMV_HCAPSCSCF>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");

            return soapEnvelop;

        }

        public XmlDocument LST_HCAPSCSCF_PGW(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                     <soapenv:Body>
                                          <hss:LST_HCAPSCSCF>
                                               <hss:SUBID>" + subID + @"</hss:SUBID>
                                          </hss:LST_HCAPSCSCF>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");

            return soapEnvelop;

        }

        public XmlDocument MOD_HSDAINF_SPG(string impi, string pwd)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">   
                                    <soapenv:Header>      
                                    <hss:MessageID>1</hss:MessageID>      
                                    <hss:Authentication>         
                                        <hss:Username>" + parameters.userSPG() + @"</hss:Username>         
                                        <hss:Password>" + parameters.passSPG() + @"</hss:Password>      
                                    </hss:Authentication>   
                                    </soapenv:Header>   
                                    <soapenv:Body>      
                                        <hss:MOD_HSDAINF>         
                                            <hss:IMPI>" + impi + @"</hss:IMPI>
                                            <hss:PWD>" + pwd + @"</hss:PWD>
                                        </hss:MOD_HSDAINF>
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument MOD_HSDAINF_PGW(string impi, string pwd)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Body>      
                                        <hss:MOD_HSDAINF>         
                                            <hss:IMPI>" + impi + @"</hss:IMPI>
                                            <hss:PWD>" + pwd + @"</hss:PWD>
                                        </hss:MOD_HSDAINF>
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument MOD_EIN(string isdn, int ein,  bool prov)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            switch (prov)
            {
                case true:
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Body>
	                                    <hss:MOD_EIN>
		                                    <hss:ISDN>" + isdn + @"</hss:ISDN>
		                                    <hss:OSKPROV>TRUE</hss:OSKPROV>                                        
                                            <hss:OSK>" + ein + @"</hss:OSK>
	                                    </hss:MOD_EIN>
                                    </soapenv:Body></soapenv:Envelope>");

                    return soapEnvelop;

                default:
                    soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Body>
	                                    <hss:MOD_EIN>
		                                    <hss:ISDN>" + isdn + @"</hss:ISDN>
		                                    <hss:OSKPROV>FALSE</hss:OSKPROV>                                                                                    
	                                    </hss:MOD_EIN>
                                    </soapenv:Body></soapenv:Envelope>");
                    return soapEnvelop;
            }

        }

        public XmlDocument ADD_HSIFC_SPG(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">   
                                <soapenv:Header>
                                    <hss:MessageID>1</hss:MessageID>      
                                    <hss:Authentication>         
                                        <hss:Username>" + parameters.userSPG() + @"</hss:Username>         
                                        <hss:Password>" + parameters.passSPG() + @"</hss:Password>      
                                    </hss:Authentication>   
                                </soapenv:Header>   
                                <soapenv:Body>      
                                    <hss:ADD_HSIFC>                  
                                        <hss:IMPU>" + impu + @"</hss:IMPU>         
                                        <hss:SIFCID>" + parameters.userIFC() + @"</hss:SIFCID>      
                                    </hss:ADD_HSIFC>   
                                </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_HSIFC_PGW(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                <soapenv:Body>      
                                    <hss:ADD_HSIFC>                  
                                        <hss:IMPU>" + impu + @"</hss:IMPU>         
                                        <hss:SIFCID>" + parameters.userIFC() + @"</hss:SIFCID>      
                                    </hss:ADD_HSIFC>   
                                </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument RMV_HSIFC_PGW(string impu, int ifc)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                <soapenv:Body>      
                                    <hss:RMV_HSIFC>                  
                                        <hss:IMPU>" + impu + @"</hss:IMPU>         
                                        <hss:SIFCID>" + ifc.ToString() + @"</hss:SIFCID>
                                    </hss:RMV_HSIFC>
                                </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_HSIFC_PGW(string impu, int ifc)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                <soapenv:Body>      
                                    <hss:ADD_HSIFC>                  
                                        <hss:IMPU>" + impu + @"</hss:IMPU>         
                                        <hss:SIFCID>" + ifc.ToString() + @"</hss:SIFCID>      
                                    </hss:ADD_HSIFC>   
                                </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument RMV_HHSSSUB_SPG(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">   
                                    <soapenv:Header>      
                                    <hss:MessageID>1</hss:MessageID>      
                                    <hss:Authentication>         
                                        <hss:Username>" + parameters.userSPG() + @"</hss:Username>         
                                        <hss:Password>" + parameters.passSPG() + @"</hss:Password>      
                                    </hss:Authentication>   
                                    </soapenv:Header>   
                                    <soapenv:Body>      
                                        <hss:RMV_HHSSSUB>         
                                            <hss:SUBID>" + subID + @"</hss:SUBID>      
                                        </hss:RMV_HHSSSUB>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument RMV_HHSSSUB_PGW(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:hss=""http://www.huawei.com/HSS"">
                                    <soapenv:Body>
                                        <hss:RMV_HHSSSUB>         
                                            <hss:SUBID>" + subID + @"</hss:SUBID>      
                                        </hss:RMV_HHSSSUB>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_DNAPTRREC_SPG(string subID, string regexp)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ens=""http://www.huawei.com/ENS"">   
                                    <soapenv:Header>      
                                    <ens:MessageID>1</ens:MessageID>      
                                    <ens:Authentication>         
                                        <ens:Username>" + parameters.userSPG() + @"</ens:Username>         
                                        <ens:Password>" + parameters.passSPG() + @"</ens:Password>      
                                    </ens:Authentication>   
                                    </soapenv:Header>   
                                    <soapenv:Body>      
                                        <ens:ADD_DNAPTRREC>         
                                            <ens:E164NUM>" + subID + @"</ens:E164NUM>         
                                            <ens:ZONENAME>8.6.9.e164.arpa</ens:ZONENAME>         
                                            <ens:ORDER>0</ens:ORDER>         
                                            <ens:PREFERENCE>0</ens:PREFERENCE>         
                                            <ens:FLAGS>U</ens:FLAGS>         
                                            <ens:SERVICE>sip+e2u</ens:SERVICE>         
                                            <ens:REGEXP>" + regexp + @"</ens:REGEXP>         
                                            <ens:ENUMFLAG>ENS_TYPE</ens:ENUMFLAG>      
                                        </ens:ADD_DNAPTRREC>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_DNAPTRREC_PGW(string subID, string regexp)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ens=""http://www.huawei.com/ENS""> 
                                    <soapenv:Body>      
                                        <ens:ADD_DNAPTRREC>         
                                            <ens:E164NUM>" + subID + @"</ens:E164NUM>         
                                            <ens:ZONENAME>8.6.9.e164.arpa</ens:ZONENAME>         
                                            <ens:ORDER>0</ens:ORDER>         
                                            <ens:PREFERENCE>0</ens:PREFERENCE>         
                                            <ens:FLAGS>U</ens:FLAGS>         
                                            <ens:SERVICE>sip+e2u</ens:SERVICE>         
                                            <ens:REGEXP>" + regexp + @"</ens:REGEXP>         
                                            <ens:ENUMFLAG>ENS_TYPE</ens:ENUMFLAG>      
                                        </ens:ADD_DNAPTRREC>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument RMV_DNAPTRREC_SPG(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ens=""http://www.huawei.com/ENS"">   
                                    <soapenv:Header>      
                                    <ens:MessageID>1</ens:MessageID>      
                                    <ens:Authentication>         
                                        <ens:Username>" + parameters.userSPG() + @"</ens:Username>        
                                        <ens:Password>" + parameters.passSPG() + @"</ens:Password>      
                                    </ens:Authentication>   
                                    </soapenv:Header>   
                                    <soapenv:Body>      
                                        <ens:RMV_DNAPTRREC>         
                                            <ens:E164NUM>" + subID + @"</ens:E164NUM>         
                                            <ens:ZONENAME>8.6.9.e164.arpa</ens:ZONENAME>      
                                        </ens:RMV_DNAPTRREC>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument RMV_DNAPTRREC_PGW(string subID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ens=""http://www.huawei.com/ENS"">  
                                    <soapenv:Body>      
                                        <ens:RMV_DNAPTRREC>         
                                            <ens:E164NUM>" + subID + @"</ens:E164NUM>         
                                            <ens:ZONENAME>8.6.9.e164.arpa</ens:ZONENAME>      
                                        </ens:RMV_DNAPTRREC>   
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;

        }

        public XmlDocument ADD_MSR_SIP_USER(string impu, bool prepaid, int iCallSrcCode)
        {
            switch (prepaid)
            {
                case false:
                    return ADD_MSR_USER_POSTPAID(impu, iCallSrcCode);

                case true:
                    return ADD_MSR_USER_PREPAID(impu, iCallSrcCode);

                default:
                    return null;
            }

        }

        public XmlDocument ADD_MSR_MGCP_H248(string impu, int iCallSrcCode)
        {
            return ADD_MSR_ESL_POSTPAID(impu, iCallSrcCode);
        }

        public XmlDocument ADD_MSR_SIP_TRUNK(string impu, bool pilotNumber, string trunkID)
        {
            switch (pilotNumber)
            {
                case true:
                    return ADD_MSR_TRUNK_PILOT(impu);

                case false:
                    return ADD_MSR_TRUNK_EXTENSION(impu, trunkID);

                default:
                    return null;
            }

        }

        private XmlDocument ADD_MSR_TRUNK_PILOT(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                 <soapenv:Header>
                                    <spg:ResendFlag>1</spg:ResendFlag>
                                    <spg:Authentication>
                                        <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                        <spg:Password>" + parameters.passSPG() + @"</spg:Password>     
                                    </spg:Authentication>
                                    <spg:MessageID>1</spg:MessageID>
                                 </soapenv:Header>
                                    <soapenv:Body>
                                        <spg:ADD_MSR>
                                        <spg:IMPU>" + impu + @"</spg:IMPU>
                                            <spg:SERVICEDATA>
                                                <spg:OdbForImsOrientedServices>
                                                     <spg:OdbForImsMultimediaTelephonyServices>
                                                          <spg:OutgoingBarring>1</spg:OutgoingBarring>
                                                          <spg:BarringOfSupplementaryServicesManagement>false</spg:BarringOfSupplementaryServicesManagement>
                                                          <spg:BarringOfUSSD>false</spg:BarringOfUSSD>
                                                     </spg:OdbForImsMultimediaTelephonyServices>
                                                </spg:OdbForImsOrientedServices>
                                        <spg:MMTelServices>
                                            <spg:version>1</spg:version>
                                            <spg:complete-originating-identity-presentation>
                                                  <spg:originating-identity-presentation active=""true""/>
                                                  <spg:operator-originating-identity-presentation authorized=""true"">
                                                   <spg:restriction-override>override-not-active</spg:restriction-override>
                                                  </spg:operator-originating-identity-presentation>
                                             </spg:complete-originating-identity-presentation>
                                             <spg:complete-communication-diversion>
                                                  <spg:communication-diversion active=""true"">
                                                       <spg:NoReplyTimer>30</spg:NoReplyTimer>
                                                       <spg:ruleset>
                                                            <spg:rule id=""call-forwarding-unconditional"">
                                                                <spg:conditions/>
                                                            </spg:rule>
                                                            <spg:rule id=""call-forwarding-busy"">
                                                                 <spg:conditions>
                                                                  <spg:busy/>
                                                                 </spg:conditions>
                                                            </spg:rule>
                                                            <spg:rule id=""call-forwarding-on-user-not-registered"">
                                                                 <spg:conditions>
                                                                  <spg:not-registered/>
                                                                 </spg:conditions>
                                                            </spg:rule>
                                                            <spg:rule id=""call-forwarding-no-reply"">
                                                                 <spg:conditions>
                                                                  <spg:no-answer/>
                                                                 </spg:conditions>
                                                            </spg:rule>        
                                                       </spg:ruleset>
                                                      </spg:communication-diversion>
                                                      <spg:operator-communication-diversion authorized=""true"">
                                                       <spg:communication-retention-on-invocation>clear-communication-on-invocation-of-diversion</spg:communication-retention-on-invocation>
                                                       <spg:retention-when-diverting-rejected-at-diverted-to-user>no-action-at-diverting-user</spg:retention-when-diverting-rejected-at-diverted-to-user>
                                                       <spg:total-number-of-diversions-for-each-communication>5</spg:total-number-of-diversions-for-each-communication>
                                                       <spg:cdiv-indication-timer>0</spg:cdiv-indication-timer>
                                                       <spg:communication-forwarding-on-no-reply-timer>30</spg:communication-forwarding-on-no-reply-timer>
                                                       <spg:cdivn-buffer-timer>0</spg:cdivn-buffer-timer>
                                                       <spg:call-forwarding-of-incoming-centrex-call>false</spg:call-forwarding-of-incoming-centrex-call>
                                                  </spg:operator-communication-diversion>
                                             </spg:complete-communication-diversion>
                                     <spg:complete-business-trunking>
                                      <spg:operator-business-trunking authorized=""true"">
                                       <spg:concurrent-call>0</spg:concurrent-call>
                                      </spg:operator-business-trunking>
                                     </spg:complete-business-trunking>
                                    </spg:MMTelServices>
                                    <spg:im-csi-information>
                                     <spg:supported-imssf-camel-phases>phase2</spg:supported-imssf-camel-phases>
                                     <spg:camel-subscription-info>
                                      <spg:o-IM-CSI>
                                       <spg:o-bcsm-camel-TDP-data-list>
                                        <spg:o-bcsm-camel-TDP-data/>
                                       </spg:o-bcsm-camel-TDP-data-list>
                                      </spg:o-IM-CSI>
                                     </spg:camel-subscription-info>
                                    </spg:im-csi-information>
                                    <spg:MMTel-extension>
                                     <spg:basic-part>
                                      <spg:call-source-code>0</spg:call-source-code>
                                      <spg:call-out-authority>
                                       <spg:local>true</spg:local>
                                       <spg:local-toll>true</spg:local-toll>
                                       <spg:national-toll>true</spg:national-toll>
                                       <spg:international-toll>true</spg:international-toll>
                                      </spg:call-out-authority>
                                      <spg:user-password>000000</spg:user-password>
                                     </spg:basic-part>
                                    </spg:MMTel-extension>
                                   </spg:SERVICEDATA>
                                  </spg:ADD_MSR>
                                 </soapenv:Body>
                                </soapenv:Envelope>");

            return soapEnvelop;

        }

        private XmlDocument ADD_MSR_TRUNK_EXTENSION(string impu, string trunkID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                     <soapenv:Header>
                                      <spg:ResendFlag>1</spg:ResendFlag>
                                           <spg:Authentication>
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>     
                                            </spg:Authentication>
                                      <spg:MessageID>1</spg:MessageID>
                                     </soapenv:Header>
                                     <soapenv:Body>
                                      <spg:ADD_MSR>
                                        <spg:IMPU>" + impu + @"</spg:IMPU>
                                        <spg:SERVICEDATA>
                                        <spg:OdbForImsOrientedServices>
                                         <spg:OdbForImsMultimediaTelephonyServices>
                                          <spg:OutgoingBarring>1</spg:OutgoingBarring>
                                          <spg:BarringOfSupplementaryServicesManagement>false</spg:BarringOfSupplementaryServicesManagement>
                                          <spg:BarringOfUSSD>false</spg:BarringOfUSSD>
                                         </spg:OdbForImsMultimediaTelephonyServices>
                                        </spg:OdbForImsOrientedServices>
                                        <spg:MMTelServices>
                                         <spg:version>1</spg:version>
                                         <spg:complete-originating-identity-presentation>
                                          <spg:originating-identity-presentation active=""true""/>
                                          <spg:operator-originating-identity-presentation authorized=""true"">
                                           <spg:restriction-override>override-not-active</spg:restriction-override>
                                          </spg:operator-originating-identity-presentation>
                                         </spg:complete-originating-identity-presentation>
                                         <spg:complete-communication-diversion>
                                          <spg:communication-diversion active=""true"">
                                           <spg:NoReplyTimer>30</spg:NoReplyTimer>
                                           <spg:ruleset>
                                            <spg:rule id=""call-forwarding-unconditional"">
                                             <spg:conditions/>
                                            </spg:rule>
                                            <spg:rule id=""call-forwarding-busy"">
                                             <spg:conditions>
                                              <spg:busy/>
                                             </spg:conditions>
                                            </spg:rule>
                                            <spg:rule id=""call-forwarding-on-user-not-registered"">
                                             <spg:conditions>
                                              <spg:not-registered/>
                                             </spg:conditions>
                                            </spg:rule>
                                            <spg:rule id=""call-forwarding-no-reply"">
                                             <spg:conditions>
                                              <spg:no-answer/>
                                             </spg:conditions>
                                            </spg:rule>        
                                           </spg:ruleset>
                                          </spg:communication-diversion>
                                          <spg:operator-communication-diversion authorized=""true"">
                                           <spg:communication-retention-on-invocation>clear-communication-on-invocation-of-diversion</spg:communication-retention-on-invocation>
                                           <spg:retention-when-diverting-rejected-at-diverted-to-user>no-action-at-diverting-user</spg:retention-when-diverting-rejected-at-diverted-to-user>
                                           <spg:total-number-of-diversions-for-each-communication>5</spg:total-number-of-diversions-for-each-communication>
                                           <spg:cdiv-indication-timer>0</spg:cdiv-indication-timer>
                                           <spg:communication-forwarding-on-no-reply-timer>30</spg:communication-forwarding-on-no-reply-timer>
                                           <spg:cdivn-buffer-timer>0</spg:cdivn-buffer-timer>
                                           <spg:call-forwarding-of-incoming-centrex-call>false</spg:call-forwarding-of-incoming-centrex-call>
                                          </spg:operator-communication-diversion>
                                         </spg:complete-communication-diversion>
                                         <spg:complete-business-trunking-membership>
                                          <spg:business-trunking-membership active=""true"">
                                           <spg:display-pilot-number>false</spg:display-pilot-number>
                                          </spg:business-trunking-membership>
                                          <spg:operator-business-trunking-membership authorized=""true"">
                                           <spg:GroupIdentity>" + trunkID + @"</spg:GroupIdentity>
                                           <spg:share-pilot-service>true</spg:share-pilot-service>
                                          </spg:operator-business-trunking-membership>
                                         </spg:complete-business-trunking-membership>
                                        </spg:MMTelServices>
                                        <spg:im-csi-information>
                                         <spg:supported-imssf-camel-phases>phase2</spg:supported-imssf-camel-phases>
                                         <spg:camel-subscription-info>
                                          <spg:o-IM-CSI>
                                           <spg:o-bcsm-camel-TDP-data-list>
                                            <spg:o-bcsm-camel-TDP-data/>
                                           </spg:o-bcsm-camel-TDP-data-list>
                                          </spg:o-IM-CSI>
                                         </spg:camel-subscription-info>
                                        </spg:im-csi-information>
                                        <spg:MMTel-extension>
                                         <spg:basic-part>
                                          <spg:call-source-code>0</spg:call-source-code>
                                          <spg:call-out-authority>
                                           <spg:local>true</spg:local>
                                           <spg:local-toll>true</spg:local-toll>
                                           <spg:national-toll>true</spg:national-toll>
                                           <spg:international-toll>true</spg:international-toll>
                                          </spg:call-out-authority>
                                          <spg:user-password>000000</spg:user-password>
                                         </spg:basic-part>
                                        </spg:MMTel-extension>
                                       </spg:SERVICEDATA>
                                      </spg:ADD_MSR>
                                     </soapenv:Body>
                                    </soapenv:Envelope>
");

            return soapEnvelop;

        }

        private XmlDocument ADD_MSR_USER_POSTPAID(string impu, int iCallSrcCode)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                        <soapenv:Header>      
                                        <spg:ResendFlag>0</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                        </soapenv:Header>    
                                         <soapenv:Body>
                                          <spg:ADD_MSR>
                                           <spg:IMPU>" + impu + @"</spg:IMPU>
                                                   <spg:SERVICEDATA>
                                                    <spg:OdbForImsOrientedServices>
                                                     <spg:OdbForImsMultimediaTelephonyServices>
                                                      <spg:OutgoingBarring>1</spg:OutgoingBarring>
                                                      <spg:BarringOfSupplementaryServicesManagement>false</spg:BarringOfSupplementaryServicesManagement>
                                                      <spg:BarringOfUSSD>false</spg:BarringOfUSSD>
                                                     </spg:OdbForImsMultimediaTelephonyServices>
                                                    </spg:OdbForImsOrientedServices>
                                                    <spg:MMTelServices>
                                                     <spg:version>1</spg:version>
                                                     <spg:complete-originating-identity-presentation>
                                                      <spg:originating-identity-presentation active=""true""/>
                                                      <spg:operator-originating-identity-presentation authorized=""true"">
                                                       <spg:restriction-override>override-not-active</spg:restriction-override>
                                                      </spg:operator-originating-identity-presentation>
                                                     </spg:complete-originating-identity-presentation>
                                                     <spg:complete-communication-diversion>
                                                      <spg:communication-diversion active=""true"">
                                                       <spg:NoReplyTimer>30</spg:NoReplyTimer>
                                                       <spg:ruleset>
                                                        <spg:rule id=""call-forwarding-unconditional"">
                                                         <spg:conditions/>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-busy"">
                                                         <spg:conditions>
                                                          <spg:busy/>
                                                         </spg:conditions>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-on-user-not-registered"">
                                                         <spg:conditions>
                                                          <spg:not-registered/>
                                                         </spg:conditions>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-no-reply"">
                                                         <spg:conditions>
                                                          <spg:no-answer/>
                                                         </spg:conditions>
                                                        </spg:rule>        
                                                       </spg:ruleset>
                                                      </spg:communication-diversion>
                                                      <spg:operator-communication-diversion authorized=""true"">
                                                       <spg:communication-retention-on-invocation>clear-communication-on-invocation-of-diversion</spg:communication-retention-on-invocation>
                                                       <spg:retention-when-diverting-rejected-at-diverted-to-user>no-action-at-diverting-user</spg:retention-when-diverting-rejected-at-diverted-to-user>
                                                       <spg:total-number-of-diversions-for-each-communication>5</spg:total-number-of-diversions-for-each-communication>
                                                       <spg:cdiv-indication-timer>0</spg:cdiv-indication-timer>
                                                       <spg:communication-forwarding-on-no-reply-timer>30</spg:communication-forwarding-on-no-reply-timer>
                                                       <spg:cdivn-buffer-timer>0</spg:cdivn-buffer-timer>
                                                       <spg:call-forwarding-of-incoming-centrex-call>false</spg:call-forwarding-of-incoming-centrex-call>
                                                      </spg:operator-communication-diversion>
                                                     </spg:complete-communication-diversion>
                                                    </spg:MMTelServices>
                                                    <spg:im-csi-information>
                                                     <spg:supported-imssf-camel-phases>phase2</spg:supported-imssf-camel-phases>
                                                     <spg:camel-subscription-info>
                                                      <spg:o-IM-CSI>
                                                       <spg:o-bcsm-camel-TDP-data-list>
                                                        <spg:o-bcsm-camel-TDP-data/>
                                                       </spg:o-bcsm-camel-TDP-data-list>
                                                      </spg:o-IM-CSI>
                                                     </spg:camel-subscription-info>
                                                    </spg:im-csi-information>
                                                    <spg:MMTel-extension>
                                                     <spg:basic-part>
                                                      <spg:call-source-code>" + iCallSrcCode + @"</spg:call-source-code>
                                                      <spg:call-out-authority>
                                                       <spg:local>true</spg:local>
                                                       <spg:local-toll>true</spg:local-toll>
                                                       <spg:national-toll>true</spg:national-toll>
                                                       <spg:international-toll>true</spg:international-toll>
                                                      </spg:call-out-authority>
                                                      <spg:user-password>000000</spg:user-password>
                                                     </spg:basic-part>
                                                    </spg:MMTel-extension>
                                                   </spg:SERVICEDATA>
                                          </spg:ADD_MSR>
                                         </soapenv:Body>
                                        </soapenv:Envelope>");

            return soapEnvelop;

        }

        private XmlDocument ADD_MSR_USER_PREPAID(string impu, int iCallSrcCode)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                 <soapenv:Header>      
                                        <spg:ResendFlag>0</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                        </soapenv:Header>    
                                         <soapenv:Body>
                                          <spg:ADD_MSR>
                                           <spg:IMPU>" + impu + @"</spg:IMPU>
                                                   <spg:SERVICEDATA>
                                                    <spg:OdbForImsOrientedServices>
                                                     <spg:OdbForImsMultimediaTelephonyServices>
                                                      <spg:OutgoingBarring>1</spg:OutgoingBarring>
                                                      <spg:BarringOfSupplementaryServicesManagement>false</spg:BarringOfSupplementaryServicesManagement>
                                                      <spg:BarringOfUSSD>false</spg:BarringOfUSSD>
                                                     </spg:OdbForImsMultimediaTelephonyServices>
                                                    </spg:OdbForImsOrientedServices>
                                                    <spg:MMTelServices>
                                                     <spg:version>1</spg:version>
                                                     <spg:complete-originating-identity-presentation>
                                                      <spg:originating-identity-presentation active=""true""/>
                                                      <spg:operator-originating-identity-presentation authorized=""true"">
                                                       <spg:restriction-override>override-not-active</spg:restriction-override>
                                                      </spg:operator-originating-identity-presentation>
                                                     </spg:complete-originating-identity-presentation>
                                                     <spg:complete-communication-diversion>
                                                      <spg:communication-diversion active=""true"">
                                                       <spg:NoReplyTimer>30</spg:NoReplyTimer>
                                                       <spg:ruleset>
                                                        <spg:rule id=""call-forwarding-unconditional"">
                                                         <spg:conditions/>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-busy"">
                                                         <spg:conditions>
                                                          <spg:busy/>
                                                         </spg:conditions>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-on-user-not-registered"">
                                                         <spg:conditions>
                                                          <spg:not-registered/>
                                                         </spg:conditions>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-no-reply"">
                                                         <spg:conditions>
                                                          <spg:no-answer/>
                                                         </spg:conditions>
                                                        </spg:rule>        
                                                       </spg:ruleset>
                                                      </spg:communication-diversion>
                                                      <spg:operator-communication-diversion authorized=""true"">
                                                       <spg:communication-retention-on-invocation>clear-communication-on-invocation-of-diversion</spg:communication-retention-on-invocation>
                                                       <spg:retention-when-diverting-rejected-at-diverted-to-user>no-action-at-diverting-user</spg:retention-when-diverting-rejected-at-diverted-to-user>
                                                       <spg:total-number-of-diversions-for-each-communication>5</spg:total-number-of-diversions-for-each-communication>
                                                       <spg:cdiv-indication-timer>0</spg:cdiv-indication-timer>
                                                       <spg:communication-forwarding-on-no-reply-timer>30</spg:communication-forwarding-on-no-reply-timer>
                                                       <spg:cdivn-buffer-timer>0</spg:cdivn-buffer-timer>
                                                       <spg:call-forwarding-of-incoming-centrex-call>false</spg:call-forwarding-of-incoming-centrex-call>
                                                      </spg:operator-communication-diversion>
                                                     </spg:complete-communication-diversion>
                                                    </spg:MMTelServices>
                                                    <spg:im-csi-information>
                                                     <spg:supported-imssf-camel-phases>phase2</spg:supported-imssf-camel-phases>
                                                     <spg:camel-subscription-info>
                                                      <spg:o-IM-CSI>
                                                       <spg:o-bcsm-camel-TDP-data-list>
                                                        <spg:o-bcsm-camel-TDP-data>
                                                         <spg:o-bcsm-trigger-detection-point>collected-info</spg:o-bcsm-trigger-detection-point>
                                                         <spg:service-key>" + parameters.imsSrvKey() + @"</spg:service-key>
                                                         <spg:gsm-SCF-address>" + parameters.imsScfAddr() + @"</spg:gsm-SCF-address>
                                                         <spg:default-call-handling>release-call</spg:default-call-handling>
                                                        </spg:o-bcsm-camel-TDP-data>
                                                       </spg:o-bcsm-camel-TDP-data-list>
                                                       <spg:camel-capability-handling>2</spg:camel-capability-handling>
                                                      </spg:o-IM-CSI>
                                                     </spg:camel-subscription-info>
                                                    </spg:im-csi-information>
                                                    <spg:MMTel-extension>
                                                     <spg:basic-part>
                                                      <spg:call-source-code>" + iCallSrcCode + @"</spg:call-source-code>
                                                      <spg:call-out-authority>
                                                       <spg:local>true</spg:local>
                                                       <spg:local-toll>true</spg:local-toll>
                                                       <spg:national-toll>true</spg:national-toll>
                                                       <spg:international-toll>true</spg:international-toll>
                                                      </spg:call-out-authority>
                                                      <spg:user-password>000000</spg:user-password>
                                                     </spg:basic-part>
                                                    </spg:MMTel-extension>
                                                   </spg:SERVICEDATA>
                                      </spg:ADD_MSR>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");

            return soapEnvelop;

        }

        private XmlDocument ADD_MSR_ESL_POSTPAID(string impu, int iCallSrcCode)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                        <soapenv:Header>      
                                        <spg:ResendFlag>0</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                        </soapenv:Header>    
                                         <soapenv:Body>
                                          <spg:ADD_MSR>
                                           <spg:IMPU>" + impu + @"</spg:IMPU>
                                                   <spg:SERVICEDATA>
                                                    <spg:OdbForImsOrientedServices>
                                                     <spg:OdbForImsMultimediaTelephonyServices>
                                                      <spg:OutgoingBarring>1</spg:OutgoingBarring>
                                                      <spg:BarringOfSupplementaryServicesManagement>false</spg:BarringOfSupplementaryServicesManagement>
                                                      <spg:BarringOfUSSD>false</spg:BarringOfUSSD>
                                                     </spg:OdbForImsMultimediaTelephonyServices>
                                                    </spg:OdbForImsOrientedServices>
                                                    <spg:MMTelServices>
                                                     <spg:version>1</spg:version>
                                                     <spg:complete-originating-identity-presentation>
                                                      <spg:originating-identity-presentation active=""true""/>
                                                      <spg:operator-originating-identity-presentation authorized=""true"">
                                                       <spg:restriction-override>override-not-active</spg:restriction-override>
                                                      </spg:operator-originating-identity-presentation>
                                                     </spg:complete-originating-identity-presentation>
                                                     <spg:complete-communication-diversion>
                                                      <spg:communication-diversion active=""true"">
                                                       <spg:NoReplyTimer>30</spg:NoReplyTimer>
                                                       <spg:ruleset>
                                                        <spg:rule id=""call-forwarding-unconditional"">
                                                         <spg:conditions/>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-busy"">
                                                         <spg:conditions>
                                                          <spg:busy/>
                                                         </spg:conditions>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-on-user-not-registered"">
                                                         <spg:conditions>
                                                          <spg:not-registered/>
                                                         </spg:conditions>
                                                        </spg:rule>
                                                        <spg:rule id=""call-forwarding-no-reply"">
                                                         <spg:conditions>
                                                          <spg:no-answer/>
                                                         </spg:conditions>
                                                        </spg:rule>        
                                                       </spg:ruleset>
                                                      </spg:communication-diversion>
                                                      <spg:operator-communication-diversion authorized=""true"">
                                                       <spg:communication-retention-on-invocation>clear-communication-on-invocation-of-diversion</spg:communication-retention-on-invocation>
                                                       <spg:retention-when-diverting-rejected-at-diverted-to-user>no-action-at-diverting-user</spg:retention-when-diverting-rejected-at-diverted-to-user>
                                                       <spg:total-number-of-diversions-for-each-communication>5</spg:total-number-of-diversions-for-each-communication>
                                                       <spg:cdiv-indication-timer>0</spg:cdiv-indication-timer>
                                                       <spg:communication-forwarding-on-no-reply-timer>30</spg:communication-forwarding-on-no-reply-timer>
                                                       <spg:cdivn-buffer-timer>0</spg:cdivn-buffer-timer>
                                                       <spg:call-forwarding-of-incoming-centrex-call>false</spg:call-forwarding-of-incoming-centrex-call>
                                                      </spg:operator-communication-diversion>
                                                     </spg:complete-communication-diversion>
                                                    </spg:MMTelServices>
                                                    <spg:im-csi-information>
                                                     <spg:supported-imssf-camel-phases>phase2</spg:supported-imssf-camel-phases>
                                                     <spg:camel-subscription-info>
                                                      <spg:o-IM-CSI>
                                                       <spg:o-bcsm-camel-TDP-data-list>
                                                        <spg:o-bcsm-camel-TDP-data/>
                                                       </spg:o-bcsm-camel-TDP-data-list>
                                                      </spg:o-IM-CSI>
                                                     </spg:camel-subscription-info>
                                                    </spg:im-csi-information>
                                                    <spg:MMTel-extension>
                                                     <spg:basic-part>
                                                      <spg:call-source-code>" + iCallSrcCode + @"</spg:call-source-code>
                                                      <spg:call-out-authority>
                                                       <spg:local>true</spg:local>
                                                       <spg:local-toll>true</spg:local-toll>
                                                       <spg:national-toll>true</spg:national-toll>
                                                       <spg:international-toll>true</spg:international-toll>
                                                      </spg:call-out-authority>
                                                      <spg:user-password>000000</spg:user-password>
                                                      <spg:implicit-ua-profile-subscribe>true</spg:implicit-ua-profile-subscribe>
                                                     </spg:basic-part>
                                                    </spg:MMTel-extension>
                                                   </spg:SERVICEDATA>
                                          </spg:ADD_MSR>
                                         </soapenv:Body>
                                        </soapenv:Envelope>");

            return soapEnvelop;

        }

        public XmlDocument IDD_SRV(string impu, bool withdraw)
        {
            switch (withdraw)
            {
                case false:
                    return IDD_PROVIDE(impu);

                case true:
                    return IDD_WITHDRAW(impu);

                default:
                    return null;
            }

        }

        private XmlDocument IDD_WITHDRAW(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                        <soapenv:Header>      
                                        <spg:ResendFlag>0</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                        </soapenv:Header>    
                                     <soapenv:Body>
                                      <spg:MOD_MSR>
                                           <spg:IMPU>" + impu + @"</spg:IMPU>
                                        <spg:PATH>/odbservs/OdbForImsMultimediaTelephonyServices/OutgoingBarring</spg:PATH>
                                        <spg:SERVICEDATA>
                                         <spg:OutgoingBarring>1</spg:OutgoingBarring>
                                        </spg:SERVICEDATA>
                                      </spg:MOD_MSR>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");
            return soapEnvelop;
        }

        private XmlDocument IDD_PROVIDE(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                     <soapenv:Header>      
                                        <spg:ResendFlag>1</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                     </soapenv:Header>
                                     <soapenv:Body>
                                      <spg:RMV_MSR>
                                       <spg:IMPU>" + impu + @"</spg:IMPU>
                                       <spg:PATH>/odbservs/OdbForImsMultimediaTelephonyServices/OutgoingBarring</spg:PATH>
                                      </spg:RMV_MSR>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");
            return soapEnvelop;
        }

        private XmlDocument ODB_OUTGOING_CALLS_ACT(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                     <soapenv:Header>      
                                        <spg:ResendFlag>1</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                 </soapenv:Header>
                                 <soapenv:Body>
                                  <spg:MOD_MSR>
                                   <spg:IMPU>" + impu + @"</spg:IMPU>
                                   <spg:PATH>/odbservs/OwedRestriction/OutgoingCallRestriction</spg:PATH>
                                   <spg:SERVICEDATA>
                                    <spg:OutgoingCallRestriction>
                                     <spg:ContinueAfterPlayTone>0</spg:ContinueAfterPlayTone>
                                    </spg:OutgoingCallRestriction>
                                   </spg:SERVICEDATA>
                                  </spg:MOD_MSR>
                                 </soapenv:Body>
                                </soapenv:Envelope>");
            return soapEnvelop;
        }

        private XmlDocument ODB_OUTGOING_CALLS_DEA(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                     <soapenv:Header>      
                                        <spg:ResendFlag>1</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                     </soapenv:Header>
                                 <soapenv:Body>
                                  <spg:RMV_MSR>
                                       <spg:IMPU>" + impu + @"</spg:IMPU>
                                   <spg:PATH>/odbservs/OwedRestriction</spg:PATH>
                                  </spg:RMV_MSR>
                                 </soapenv:Body>
                                </soapenv:Envelope>");
            return soapEnvelop;
        }

        private XmlDocument VOICE_SUSPENSION(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                     <soapenv:Header>      
                                        <spg:ResendFlag>0</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                     </soapenv:Header>
                                 <soapenv:Body>
                                  <spg:MOD_MSR>
                                   <spg:IMPU>" + impu + @"</spg:IMPU>
                                   <spg:PATH>/odbservs/OwedRestriction</spg:PATH>
                                   <spg:SERVICEDATA>
                                    <spg:OwedRestriction>
                                     <spg:IncomingCallRestriction></spg:IncomingCallRestriction>
                                     <spg:OutgoingCallRestriction>
                                      <spg:ContinueAfterPlayTone>0</spg:ContinueAfterPlayTone>
                                     </spg:OutgoingCallRestriction>
                                    </spg:OwedRestriction>
                                   </spg:SERVICEDATA>
                                  </spg:MOD_MSR>
                                 </soapenv:Body>
                                </soapenv:Envelope>");
            return soapEnvelop;
        }

        private XmlDocument VOICE_RESUME(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">
                                <soapenv:Header>      
                                        <spg:ResendFlag>1</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                     </soapenv:Header>
                                     <soapenv:Body>
                                             <spg:RMV_MSR>
                                                <spg:IMPU>" + impu + @"</spg:IMPU>
                                                <spg:PATH>/odbservs/OwedRestriction</spg:PATH>
                                             </spg:RMV_MSR>
                                     </soapenv:Body>
                                    </soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument VOICE_BARRING_SUSPENSION(string impu, ServiceType serviceType)
        {
            switch (serviceType)
            {

                case ServiceType.odbActivate:
                    return ODB_OUTGOING_CALLS_ACT(impu);

                case ServiceType.odbDeactivate:
                    return ODB_OUTGOING_CALLS_DEA(impu);

                case ServiceType.suspend:
                    return VOICE_SUSPENSION(impu);

                case ServiceType.resume:
                    return VOICE_RESUME(impu);

                default:
                    return null;
            }
        }
    
        public XmlDocument MOD_MSR(string impu, int imsSrvKey)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                        <soapenv:Header>      
                                        <spg:ResendFlag>0</spg:ResendFlag>      
                                            <spg:Authentication>         
                                                <spg:Username>" + parameters.userSPG() + @"</spg:Username>         
                                                <spg:Password>" + parameters.passSPG() + @"</spg:Password>      
                                            </spg:Authentication>      
                                        <spg:MessageID>1</spg:MessageID>  
                                        </soapenv:Header>    
                                        <soapenv:Body>
                                        <spg:MOD_MSR>
                                            <spg:IMPU>" + impu + @"</spg:IMPU>
                                            <spg:SERVICEDATA>                                                                                         
                                                <spg:im-csi-information> 
                                                    <spg:supported-imssf-camel-phases>phase2</spg:supported-imssf-camel-phases>
                                                        <spg:camel-subscription-info>                
                                                            <spg:o-IM-CSI>                   
                                                                <spg:o-bcsm-camel-TDP-data-list>              
                                                                    <spg:o-bcsm-camel-TDP-data>                       
                                                                    <spg:o-bcsm-trigger-detection-point>collected-info</spg:o-bcsm-trigger-detection-point>           
                                                                    <spg:service-key>" + parameters.imsSrvKey() + @"</spg:service-key>       
                                                                    <spg:gsm-SCF-address>" + parameters.imsScfAddr() + @"</spg:gsm-SCF-address>
                                                                    <spg:default-call-handling>release-call</spg:default-call-handling>
                                                                    </spg:o-bcsm-camel-TDP-data>
                                                                </spg:o-bcsm-camel-TDP-data-list>
                                                            <spg:camel-capability-handling>2</spg:camel-capability-handling>
                                                        </spg:o-IM-CSI>
                                                    </spg:camel-subscription-info>
                                                </spg:im-csi-information>                                                
                                            </spg:SERVICEDATA> 
                                        </spg:MOD_MSR> 
                                        </soapenv:Body> </soapenv:Envelope>");

            return soapEnvelop;

        }

        public XmlDocument LST_MSR(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>   
                                    <spg:ResendFlag>0</spg:ResendFlag>   
                                    <spg:Authentication>      
                                        <spg:Username>" + parameters.userSPG() + @"</spg:Username>        
                                        <spg:Password>" + parameters.passSPG() + @"</spg:Password>  
                                    </spg:Authentication>   
                                    <spg:MessageID>1</spg:MessageID>
                                    </soapenv:Header>    
                                    <soapenv:Body> 
                                    <spg:LST_MSR>
                                        <spg:IMPU>" + impu + @"</spg:IMPU>
                                    </spg:LST_MSR>  
                                    </soapenv:Body> </soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument USER_INPUT(string XmlCommand)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@XmlCommand);
            return soapEnvelop;
        }

        public XmlDocument RMV_MSR(string impu)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                    <spg:ResendFlag>0</spg:ResendFlag>     
                                    <spg:Authentication>       
                                        <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                        <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                    </spg:Authentication>   
                                    <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>   <soapenv:Body> 
                                    <spg:RMV_MSR>       
                                        <spg:IMPU>" + impu + @"</spg:IMPU>    
                                    </spg:RMV_MSR>  
                                    </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument ADD_AGCF_MGW(string sEquipmentID, string sMediaGwDescription, int iGatewayType, int iProtocolType)
        {
            XmlDocument soapEnvelop = new XmlDocument();            

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                    <soapenv:Body>
                                      <spg:AddMGW_AGCF>
                                         <spg:EID>" + sEquipmentID + @"</spg:EID>
                                         <spg:GWTP>" + iGatewayType + @"</spg:GWTP>
                                         <spg:MGWDESC>" + sMediaGwDescription + @"</spg:MGWDESC>
                                         <spg:PTYPE>" + iProtocolType + @"</spg:PTYPE>
                                         <spg:LA>10.215.40.206</spg:LA>
                                         <spg:DYNIP>2</spg:DYNIP>
                                         <spg:LISTOFCODEC>?????????11?11?1????1???????????????????????????????????????????</spg:LISTOFCODEC>
                                         <spg:MASTERAGCF>BOAGCF</spg:MASTERAGCF>
                                      </spg:AddMGW_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument ADD_AGCF_ASBR(string PUI, string PRI, string sEquipmentID, string sTerminationID, string sPassword)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                      <soapenv:Body>
                                      <spg:AddAsbr_AGCF>
                                         <spg:PUI>" + PUI + @"</spg:PUI>
                                         <spg:PRI>" + PRI + @"</spg:PRI>
                                         <spg:REGTP>0</spg:REGTP>
                                         <spg:DID>114</spg:DID>
                                         <spg:TEN>0</spg:TEN>
                                         <spg:EID>" + sEquipmentID + @"</spg:EID>
                                         <spg:TID>" + sTerminationID + @"</spg:TID>
                                         <spg:HNID>hnid</spg:HNID>
                                         <spg:NETID>vnid</spg:NETID>
                                         <spg:NETINFO>ani</spg:NETINFO>
                                         <spg:PHNCON>Oman</spg:PHNCON>
                                         <spg:DIGMAP>0</spg:DIGMAP>
                                         <spg:PWD>" + sPassword + @"</spg:PWD>
                                         <spg:DP>2</spg:DP>
                                         <spg:CONF>1</spg:CONF>
                                      </spg:AddAsbr_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument RMV_AGCF_MGW(string sEquipmentID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                      <soapenv:Body>
                                      <spg:RmvMGW_AGCF>
                                         <spg:EID>" + sEquipmentID + @"</spg:EID>
                                         <spg:MASTERAGCF>BOAGCF</spg:MASTERAGCF>
                                      </spg:RmvMGW_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument RMV_AGCF_ASBR(string PUI)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                      <soapenv:Body>
                                      <spg:RmvAsbr_AGCF>
                                         <spg:PUI>" + PUI + @"</spg:PUI>
                                      </spg:RmvAsbr_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument LST_AGCF_MGW(string sEquipmentID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                      <soapenv:Body>
                                      <spg:LstMGW_AGCF>
                                         <spg:EID>" + sEquipmentID + @"</spg:EID>                                         
                                         <spg:AGCFNAME>" + parameters.agcf() + @"</spg:AGCFNAME>
                                      </spg:LstMGW_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument LST_AGCF_ASBR(string PUI)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                      <soapenv:Body>
                                      <spg:LstAsbr_AGCF>
                                         <spg:PUI>" + PUI + @"</spg:PUI>
                                         <spg:AGCFNAME>" + parameters.agcf() + @"</spg:AGCFNAME>
                                      </spg:LstAsbr_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }

        public XmlDocument LST_AGCF_ASBR_EID (string EID)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:spg=""http://www.huawei.com/SPG"">   
                                    <soapenv:Header>     
                                        <spg:ResendFlag>0</spg:ResendFlag>     
                                        <spg:Authentication>       
                                            <spg:Username>" + parameters.userSPG() + @"</spg:Username>   
                                            <spg:Password>" + parameters.passSPG() + @"</spg:Password>    
                                        </spg:Authentication>   
                                        <spg:MessageID>1</spg:MessageID>  
                                    </soapenv:Header>  
                                      <soapenv:Body>
                                      <spg:LstAsbr_AGCF>
                                         <spg:EID>" + EID + @"</spg:EID>
                                         <spg:AGCFNAME>" + parameters.agcf() + @"</spg:AGCFNAME>
                                      </spg:LstAsbr_AGCF>
                                   </soapenv:Body></soapenv:Envelope>");
            return soapEnvelop;
        }
    }
}
