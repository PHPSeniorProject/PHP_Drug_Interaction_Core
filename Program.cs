using PHP_Drugs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHP_Drug_Interaction_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Drugs d = new Drugs("Majezik");
            d.prospectus = "Agri kesici";
            d.addActiveIngredientTransaction("b1",90,"Kalp",35,"",0.0);
            d.addActiveIngredientTransaction("c1", 80, "Bobrek", 45, "Ispanak", 14);
            d.addActiveIngredientTransaction("d1", 70, "Akciger", 15, "Peynir", 100);
            d.executeIngredient("a1");
            
            d.addActiveIngredientTransaction("x", 90, "Kan", 35, "Balik", 0.0);
            d.executeIngredient("f1");*/
            Xml_Parser parser = new Xml_Parser(Xml_Parser.parserType.Drugs);

            Drugs[] a = parser.getDrugList();
            Console.Read();
        }
    }
}
