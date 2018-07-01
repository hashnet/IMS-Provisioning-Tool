using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace NGN_to_IMS_Migration
{
    class XmlResponses
    {
        const string cmdResponseCode = "ResultCode;mResultCode;responseCode";
        const string cmdResponsePattern = @"<inElement>" + @"""" + "?(.*?)" + @"""" + @"?<\/inElement>";

        static string[] app_resultString = cmdResponseCode.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);        

        Dictionary<string, string> resultCodeCmdDictionary = new Dictionary<string, string>();
        List<string> resultDescCmdDictionary = new List<string>();

        //Dictionary<string, string> resultDescCmdDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Boolean of command execution, conclude the WebRequest and process the SOAP/XML result.
        /// </summary>        
        /// <param name="isdn">isdn, used for log purposes.</param>        
        /// <param name="webRequest">webRequest used during command initialization.</param> 
        /// <param name="asyncResult">asyncResult of the webRequest which returns the SOAP results.</param> 
        /// <param name="param">specific parameter to return from the output, should be used with proper caseCode.</param> 
        /// <param name="cmd">command name, used for resultCode finding and log purposes.</param> 
        /// <param name="caseCode">
        /// 0: Full Output + bool result.
        /// 1: Result Code + bool result.
        /// 2: Result Code + true.
        /// 3: Param value + bool result.
        /// 4: Output without xml headers + bool result
        /// 5: Result Desc + bool result.
        /// 6: Result Code + Result Desc + bool result.
        /// </param>
        /// <param name="sResult">Returns the result based on the caseCode value.</param>
        /// 
        /// <returns></returns>
        public bool myResponse(string isdn, HttpWebRequest webRequest, IAsyncResult asyncResult, string param, string cmd, int caseCode, out string sResult)
        {
            WebResponse webResponse = null;
                        
            try
            {
                webResponse = webRequest.EndGetResponse(asyncResult);

            }
            catch
            {
                sResult = string.Empty;
                return false;
            }

                        
            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
            {
                string XmlResult = ParseSoapXml(rd.ReadToEnd());

                XDocument doc = XDocument.Parse(XmlResult);
                
                webResponse.Close();
                // CHECK COMMAND EXECUTION RESULT //

                // Get Result Code and evaluate it //
                string sResultCode = GetResultCode(XmlResult, cmd);
                string sResultDesc = sResultCode;

                int iResult = 1;
                bool bResult = int.TryParse(sResultCode, out iResult);

                if (iResult > 0)
                {
                    bResult = false;
                    sResultDesc = GetResultDesc(XmlResult, cmd);

                    if (string.IsNullOrEmpty(sResultDesc))
                        sResultDesc = sResultCode;
                }

                // RETURN TO CUSTOMER THE REQUEST INFORMATION //
                switch (caseCode)
                {
                    case 0: //return full output.
                        //sResult = bResult == true ? doc.ToString() : sResultDesc;
                        sResult = doc.ToString();
                        //sResult = doc.ToString();
                        return bResult;                       

                    case 1: //return result code
                        sResult = sResultDesc;
                        //sResult = sResultCode;
                        return bResult;                        

                    case 2: //return always successful result. it is usually used for cheerful operations. NOT RECOMMENDED TO USE.
                        sResult = sResultCode;
                        return true;

                    case 3: //return specific output parameter specified by the customer.
                        sResult = GetElement(XmlResult, param, cmd);
                        return bResult;

                    case 4: //return soap restult as string without xml headers
                        sResult = GetElement(XmlResult);
                        return bResult;                    

                    case 5: //return result description, in case result description value not found in the output. this function returns the result code instead.
                        sResult = GetResultDesc(XmlResult, cmd);

                        if (string.IsNullOrEmpty (sResult))
                            sResult = sResultCode;
                        
                        return bResult;

                    default:
                        sResult = string.Empty;
                        return false;                        
                }
            }
        }
        
        public static string GetElement (string XmlResult)
        {
            string output = string.Empty;
            string ElementName = string.Empty;
            string ElementValue = string.Empty;

            XmlReader xReader = XmlReader.Create(new StringReader(XmlResult));
            while (xReader.Read())
            {
                switch (xReader.NodeType)
                {                  
                    case XmlNodeType.Element:
                        ElementName = xReader.Name;
                        
                        break;
                    case XmlNodeType.Text:
                        ElementValue = xReader.Value;
                        output += string.Concat(ElementName, "==", ElementValue, Environment.NewLine);
                        ElementName = string.Empty;
                        
                        break;
                    case XmlNodeType.EndElement:
                        
                        break;

                    default:
                        break;
                }
            }

            return output;
        }
      
        public static string GetElement (string XmlResult, string inElement, string cmd)
        {

            string pattern = cmdResponsePattern;

            string regExpPattern = pattern.Replace("inElement", inElement);

            Regex regExp = new Regex(regExpPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Match regExpMatch = regExp.Match(XmlResult);

            if (regExpMatch.Success)
                return regExpMatch.Groups[regExpMatch.Groups.Count - 1].ToString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XmlResult);

            XmlNodeList elemList = doc.GetElementsByTagName (inElement);
            
            switch (elemList.Count)
            {
                case 0:
                    elemList = doc.GetElementsByTagName("*");

                    for (int i = 0; i < elemList.Count; i++)
                    {
                        switch (elemList[i].ChildNodes.Count)
                        {
                            case 0:
                                break;

                            case 1:
                                if (inElement.ToLower() == elemList[i].LocalName.ToLower())
                                    return elemList[i].InnerText;

                                break;

                            default:
                                if (inElement.ToLower() == elemList[i].FirstChild.InnerText.ToLower())
                                    return elemList[i].LastChild.InnerText;

                                break;
                        }                        
                    }

                    break;

                case 1:
                    return elemList[0].InnerText;                    

                default:
                    break;
            }       

            return null;
        }

        public static List<string> GetElements(string XmlResult, string inElement, string cmd)
        {

            List<string> sList = new List<string>();


            string pattern = cmdResponsePattern;

            string regExpPattern = pattern.Replace("inElement", inElement);

            Regex regExp = new Regex(regExpPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            MatchCollection regExpMatch = regExp.Matches(XmlResult);

            if (regExpMatch.Count > 0)
            {
                for (int x = 0; x < regExpMatch.Count; x++)
                {
                    Match match = regExpMatch[x];
                    sList.Add(match.Groups[match.Groups.Count - 1].ToString());
                }

                return sList;
            }
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XmlResult);

            XmlNodeList elemList = doc.GetElementsByTagName(inElement);

            switch (elemList.Count)
            {
                case 0:
                    elemList = doc.GetElementsByTagName("*");

                    for (int i = 0; i < elemList.Count; i++)
                    {
                        switch (elemList[i].ChildNodes.Count)
                        {
                            case 0:
                                break;

                            case 1:
                                if (inElement.ToLower() == elemList[i].LocalName.ToLower())
                                {
                                    sList.Add(elemList[i].LastChild.InnerText);
                                    continue;
                                }

                                break;

                            default:
                                if (inElement.ToLower() == elemList[i].FirstChild.InnerText.ToLower())
                                {
                                    sList.Add(elemList[i].LastChild.InnerText);
                                    continue;
                                }

                                break;
                        }
                    }

                    break;

                case 1:
                    sList.Add(elemList[0].InnerText);
                    break;

                default:
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        sList.Add(elemList[i].InnerText);

                    }
                  

                    break;
            }

            return sList;
        }

        public string GetResultCode(string xmlResult, string cmd)
        {
            if (cmd == "SYNC")
                return "0";                               

            string resultCode = null;           

            if (resultCodeCmdDictionary.ContainsKey(cmd))
            {
                resultCode = resultCodeCmdDictionary[cmd];
                string result = GetElement(xmlResult, resultCode, cmd);

                if (!string.IsNullOrEmpty(result))
                    return result;
            }                          

            string searchString = null;

            foreach (string item in app_resultString)
            {   
                resultCode = GetElement(xmlResult, item, cmd);

                searchString = item;

                if (!string.IsNullOrEmpty(resultCode))
                    break;
            }

            if (string.IsNullOrEmpty(resultCode))
                return null;

            resultCodeCmdDictionary[cmd] = searchString;
            
            return resultCode;
        }

        private string GetResultDesc(string xmlResult, string cmd)
        {

            if (resultDescCmdDictionary.Contains(cmd)) //there is no result description for this command?
                return null;

            //string resultDesc = null;

            if (!resultCodeCmdDictionary.ContainsKey(cmd))
                return null;

            string resultDesc = resultCodeCmdDictionary[cmd].ToLower().Replace("code", "desc");
            //resultDesc = resultDesc.ToLower().Replace("code", "desc");
            string result = GetElement(xmlResult, resultDesc, cmd);

            if (string.IsNullOrEmpty(result))
            {
                resultDescCmdDictionary.Add(cmd);

                return null;
            }

            if (resultDescCmdDictionary.Contains(cmd))
                resultDescCmdDictionary.Remove(cmd);

            return result;
        }

        private static string ParseSoapXml(string SoapXmlString)
        {
            StringBuilder output = new StringBuilder();

            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(SoapXmlString)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;                
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {
                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name.Replace(":", ""));
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }

                }

            }

            return output.ToString();
        }

    }
}
