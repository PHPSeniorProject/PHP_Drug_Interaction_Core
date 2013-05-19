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
            Xml_Parser x = new Xml_Parser();

            x.getDrugList();
        }
    }
}
