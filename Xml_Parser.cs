using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using PHP_Drugs;
namespace PHP_Drug_Interaction_Core
{
    class Xml_Parser
    {
        private XmlDocument drugDoc;
        private string docPath = "Drugs.xml";

        public Xml_Parser()
        {
            FileStream docIn = new FileStream(docPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            drugDoc = new XmlDocument();
            drugDoc.Load(docIn);
        }
        public Drugs[] getDrugList()
        {
            if (drugDoc != null)
            {
                XmlNodeList drugList = drugDoc.GetElementsByTagName("Drug");
                Drugs[] alldrugs = new Drugs[drugList.Count];

                for (int i = 0; i < drugList.Count; i++)
                {
                    alldrugs[i] = new Drugs(drugList[i].FirstChild.InnerText);
                    alldrugs[i].prospectus = drugList[i].FirstChild.NextSibling.InnerText;

                    XmlNode tempNode = drugList[i].FirstChild.NextSibling.NextSibling.FirstChild;
                    while (tempNode != null)
                    {
                        alldrugs[i].push_activeIngredient(tempNode.InnerText);
                        tempNode = tempNode.NextSibling;
                    }

                }
                return alldrugs;
            }
            else
            {
                return null;
            }
        }
    }
}
