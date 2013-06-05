using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using PHP_Drugs;
using System.Collections;
namespace PHP_Drug_Interaction_Core
{
    class Xml_Parser
    {
        public enum parserType { Drugs, Interaction };

        private XmlDocument drugDoc,InteractionDoc;
        private string docPathDrugs = "Drugs.xml";
        private string docPathInteraction = "Interactions.xml";

        public Xml_Parser(parserType type)
        {

            switch (type)
            {
                case parserType.Drugs:
                     FileStream docInDrugs = new FileStream(docPathDrugs, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                     drugDoc = new XmlDocument();
                     drugDoc.Load(docInDrugs);
                    break;
                case parserType.Interaction:
                     FileStream docInInteraction = new FileStream(docPathInteraction, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                     InteractionDoc = new XmlDocument();
                     InteractionDoc.Load(docInInteraction);
                    break;
                default:
                    break;
            }
        }
        public Drugs[] getDrugList()
        {
            if (drugDoc != null)
            {
                XmlNodeList drugList = drugDoc.GetElementsByTagName("Drug");
                Drugs[] allDrugs = new Drugs[drugList.Count];

                for (int i = 0; i < drugList.Count; i++)
                {
                    allDrugs[i] = new Drugs(drugList[i].FirstChild.InnerText);
                    allDrugs[i].prospectus = drugList[i].FirstChild.NextSibling.InnerText;

                    XmlNode iterator = drugList[i].FirstChild.NextSibling.NextSibling.FirstChild;
                    while (iterator != null)
                    {
                        XmlNode innerIterator = iterator.FirstChild;
                        while (innerIterator != null)
                        {
                            double drugPer, organPer, foodPer;
                            string actInterName, organName, foodName,organDesc ,foodDesc;

                            XmlNode temp = innerIterator.FirstChild;

                            actInterName = getAttributeFromNode(innerIterator, 0);
                            drugPer = Double.Parse(getAttributeFromNode(innerIterator, 1));
                            organName = getAttributeFromNode(temp,0);
                            organDesc = temp.InnerText;
                            organPer = Double.Parse(getAttributeFromNode(temp, 1));
                            foodName = getAttributeFromNode(temp.NextSibling, 0);
                            foodDesc = temp.NextSibling.InnerText;
                            foodPer = Double.Parse(getAttributeFromNode(temp.NextSibling, 1));

                            allDrugs[i].addActiveIngredientTransaction(actInterName, drugPer, organName, organDesc,
                                organPer, foodName, foodDesc,foodPer);
                            innerIterator = innerIterator.NextSibling;
                        }


                        allDrugs[i].executeIngredient(getAttributeFromNode(iterator, 0));
                        iterator = iterator.NextSibling;
                    }
                }
                return allDrugs;
            }
            else
                return null;

            
        }
        private string getAttributeFromNode(XmlNode node, int attOrder)
        {
            string attributeValue;
            XmlAttributeCollection attColl = node.Attributes;
            attributeValue = attColl[attOrder].Value.ToString();

            return attributeValue;
        }
        /*
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
                        //alldrugs[i].push_activeIngredient(tempNode.InnerText);
                        tempNode = tempNode.NextSibling;
                    }

                }
                return alldrugs;
            }
            else
            {
                return null;
            }
        }*/

       /* public String[] getIngredientList(Drugs[] drugs)
        {
            string[] ingList;
            string temp ="";

            for (int i = 0; i < drugs.Length; i++)
            {
                for (int k = 0; k < drugs[i].count; k++)
                {
                    string drug = "";//drugs[i].getIngredientAt(k+1);
                    if (!temp.Contains(drug))
                    {
                        //temp += String.Format(drugs[i].getIngredientAt(k + 1) + ",");
                    }
                }
            }
            return ingList = temp.Split(',');
        }*/
    }
}
