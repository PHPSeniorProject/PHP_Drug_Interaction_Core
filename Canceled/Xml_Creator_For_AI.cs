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
                        fileWriter.Write(String.Format("<Interaction>{0}</Interaction>" + "\n", drugList[secondDrug].getIngredientAt(ingRnd2)));
                    }

                    fileWriter.Write("</Ingredient>" + "\n");
                }
                fileWriter.Write("</Ingredients>");
                
            }
        }
    }
}
