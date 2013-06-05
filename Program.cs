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
            Analyzer x = new Analyzer();
            Drugs[] a = parser.getDrugList();
            Drugs[] b = x.getProspectus(a, new String[] { "ALFASIN 1 GR 16 TABLET", "CORBINAL 250 MG 28 TABLET", 
                                                            "INFEX 200 MG 20 FILM TABLET", "KLAMOKS BID 1000 MG 14 FILM TABLET" ,
                                                            "PAXERA 20 MG 28 TABLET", "THERAFLU FORTE 20 FILM TABLET", 
                                                            "CEC 1000 MG 10 EFERVESAN TABLET", "AUGMENTIN BID 1000 MG 10 FILM TABLET",
                                                            "LARGOPEN 1000 MG 1 FLAKON", "CIPRO 500 MG 14 FILM TABLET"});
            x.getAnalyzeResult(b);
            Console.Read();
        }
        
    }
}
