using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace BTLProductLine
{
    class XmlClass
    {
        public XmlClass() { }
        public string Read()
        {
            string value = "Error";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("parXml.xml");
            XmlNodeList nodeListRoot = xmlDoc.SelectSingleNode("BTLProductLine").ChildNodes;//Get all nodeList from BTLProductLine
            foreach (XmlNode xmlNodeRoot in nodeListRoot)//find all nodes in nodelist
            {
                XmlElement xmlElementRoot = (XmlElement)xmlNodeRoot;//convert node to element
                if (xmlElementRoot.GetAttribute("type") == "A")//if type is A
                {
                    XmlNodeList xmlNodeList1 = xmlElementRoot.ChildNodes;//
                    foreach (XmlNode xmlNode1 in xmlNodeList1)//
                    {
                        XmlElement xmlElement1 = (XmlElement)xmlNode1;//
                        if (xmlElement1.Name == "DZ20")//
                        {
                            XmlNodeList subXmlNodeList1 = xmlElement1.ChildNodes;//
                            foreach (XmlNode subXmlNode1 in subXmlNodeList1)//
                            {
                                XmlElement SubXmlElement1 = (XmlElement)subXmlNode1;//
                                if (SubXmlElement1.Name == "userflag")//
                                {
                                    value = SubXmlElement1.InnerText;
                                }
                            }
                        }
                    }
                }
            }
            xmlDoc.Save("parXml.xml");//SAVE
            return value;
        }

        public string ReadNameSectionValue(string type, string nameSection, string subNameSection)
        {
            string value = "Error";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("parXml.xml");
            XmlNodeList nodeListRoot = xmlDoc.SelectSingleNode("BTLProductLine").ChildNodes;//Get all nodeList from BTLProductLine
            foreach (XmlNode xmlNodeRoot in nodeListRoot)//find all nodes in nodelist
            {
                XmlElement xmlElementRoot = (XmlElement)xmlNodeRoot;//convert node to element
                if (xmlElementRoot.GetAttribute("type") == type)//if type is A
                {
                    XmlNodeList xmlNodeList1 = xmlElementRoot.ChildNodes;//
                    foreach (XmlNode xmlNode1 in xmlNodeList1)//
                    {
                        XmlElement xmlElement1 = (XmlElement)xmlNode1;//
                        if (xmlElement1.Name == nameSection)//
                        {
                            XmlNodeList subXmlNodeList1 = xmlElement1.ChildNodes;//
                            foreach (XmlNode subXmlNode1 in subXmlNodeList1)//
                            {
                                XmlElement SubXmlElement1 = (XmlElement)subXmlNode1;//
                                if (SubXmlElement1.Name == subNameSection)//
                                {
                                    value = SubXmlElement1.InnerText;
                                }
                            }
                        }
                    }
                }
            }
            xmlDoc.Save("parXml.xml");//SAVE
            return value;
        }
        public void Write()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("parXml.xml");
            XmlNodeList nodeListRoot = xmlDoc.SelectSingleNode("BTLProductLine").ChildNodes;//Get all nodeList from BTLProductLine
            foreach (XmlNode xmlNodeRoot in nodeListRoot)//find all nodes in nodelist
            {
                XmlElement xmlElementRoot = (XmlElement)xmlNodeRoot;//convert node to element
                if (xmlElementRoot.GetAttribute("type") == "A")//if type is A
                {
                    XmlNodeList xmlNodeList1 = xmlElementRoot.ChildNodes;//
                    foreach (XmlNode xmlNode1 in xmlNodeList1)//
                    {
                        XmlElement xmlElement1 = (XmlElement)xmlNode1;//
                        if (xmlElement1.Name == "DZ20")//
                        {
                            XmlNodeList subXmlNodeList1 = xmlElement1.ChildNodes;//
                            foreach (XmlNode subXmlNode1 in subXmlNodeList1)//
                            {
                                XmlElement SubXmlElement1 = (XmlElement)subXmlNode1;//
                                if (SubXmlElement1.Name == "userflag")//
                                {
                                    SubXmlElement1.InnerText = "false";
                                }
                                break;
 
                            }
                            break;//
                        }
                    }
                    break;
                }
            }
            xmlDoc.Save("parXml.xml");//SAVE
        }



        public void WriteNameSectionValue(string type, string nameSection, string subNameSection, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("parXml.xml");
            XmlNodeList nodeListRoot = xmlDoc.SelectSingleNode("BTLProductLine").ChildNodes;//Get all nodeList from BTLProductLine
            foreach (XmlNode xmlNodeRoot in nodeListRoot)//find all nodes in nodelist
            {
                XmlElement xmlElementRoot = (XmlElement)xmlNodeRoot;//convert node to element
                if (xmlElementRoot.GetAttribute("type") == type)//if type is A
                {
                    XmlNodeList xmlNodeList1 = xmlElementRoot.ChildNodes;//
                    foreach (XmlNode xmlNode1 in xmlNodeList1)//
                    {
                        XmlElement xmlElement1 = (XmlElement)xmlNode1;//
                        if (xmlElement1.Name == nameSection)//
                        {
                            XmlNodeList subXmlNodeList1 = xmlElement1.ChildNodes;//
                            foreach (XmlNode subXmlNode1 in subXmlNodeList1)//
                            {
                                XmlElement SubXmlElement1 = (XmlElement)subXmlNode1;//
                                if (SubXmlElement1.Name == subNameSection)//
                                {
                                    SubXmlElement1.InnerText = value;
                                }
                                break;

                            }
                            break;//
                        }
                    }
                    break;
                }
            }
            xmlDoc.Save("parXml.xml");//SAVE
        }

    }
}
