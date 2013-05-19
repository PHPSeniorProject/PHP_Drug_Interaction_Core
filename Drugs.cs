using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHP_Drugs
{
    class Drugs
    {
        private string label_info;
        private string prospectus_info;
        private int ingredientCount;
        private ArrayList activeIngredient;

        public Drugs(string p_label)
        {
            this.label_info = p_label;
            this.prospectus_info = "";
            this.ingredientCount = 0;
            this.activeIngredient = new ArrayList();
        }
        public void push_activeIngredient(string p_activeIngredient)
        {
            if (!p_activeIngredient.Equals(string.Empty))
            {
                this.activeIngredient.Add(p_activeIngredient);
                count++;
            }        
        }
        public string pop_activeIngredient()
        {
            if (this.activeIngredient.Count != 0)
            {
                string value = this.activeIngredient[0] as string;
                this.activeIngredient.RemoveAt(0);
                count--;
                return value;
            }
            else
                return string.Empty;
        }
        public string label 
        {
            set { this.label_info = value; }
            get { return this.label_info;}
        }
        public string prospectus
        {
            set { this.prospectus_info = value; }
            get { return this.prospectus_info; }
        }
        public int count
        {
            set { this.ingredientCount = value; }
            get { return this.ingredientCount; }
        }
        public void sort_activeIngredients()
        {
            this.activeIngredient.Sort();
        }
    }
}
