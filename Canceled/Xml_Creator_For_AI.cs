using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PHP_Drugs;
namespace PHP_Drug_Interaction_Core
{
    class Xml_Creator_For_AI
    {
        private static StreamWriter fileWriter;
        private static string[] organs = { "Heart", "blood vessels", "blood", " pharynx", "stomach", "intestines", "salivary glands", "pancreas", " liver", "gallbladder", "testes", "ovaries", "hypothalamus", "kidneys", "pituitary", " thyroid", "parathyroid", "adrenal", "intestinal", "thymus", "pineal", "White blood cells", "lymph vessels and nodes", "spleen, thymus", "lymphatic tissues", "Skin", "Cartilage", "bone", "ligaments", "tendons", " joints", "skeletal muscle", "salivary glands", "gallbladder", "intestines", "urethras", "bladder", "urethra" };
        public static void Start_Create(Drugs[] drugList)
        {
            using (fileWriter = new StreamWriter("interaction.xml", true))
            {
                fileWriter.Write("<Ingredients>" + "\n");
                Random rnd = new Random();
                for (int i = 0; i < drugList.Length; i++)
                {

                    if ( drugList[i].count == 0)
                    {
                        continue;
                    }
                    fileWriter.Write(String.Format("<Ingredient Label = {0}>" + "\n", drugList[i].pop_activeIngredient()));

                    int count = rnd.Next(1, 4);
                    for (int k = 0; k < count; k++)
                    {
                        int secondDrug = rnd.Next(1, drugList.Length);
                        while (drugList[secondDrug].count == 0 || i == secondDrug )
                        {
                            secondDrug = rnd.Next(1, drugList.Length);
                        }
                        int ingRnd2 = rnd.Next(1, drugList[secondDrug].count);
                        int orgRnd = rnd.Next(1, organs.Length);
                        fileWriter.Write(String.Format("<Interaction Organ = {1}>{0}</Interaction>" + "\n", drugList[secondDrug].getIngredientAt(ingRnd2),organs[orgRnd]));
                    }

                    fileWriter.Write("</Ingredient>" + "\n");
                }
                fileWriter.Write("</Ingredients>");
                
            }
        }
    }
}
