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
            Xml_Parser a = new Xml_Parser(Xml_Parser.parserType.Drugs);
            Drugs[] d = a.getDrugList();

            string[] arr = a.getIngredientList(d);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i] + ",");
            }
            Console.Read();
        }
    }
}
