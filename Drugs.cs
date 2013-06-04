using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHP_Drugs
{
    class Drugs
    {
        public class ActiveIngredient
        {
            private string Label_Info;
            public Hashtable InteractionList;

            public ActiveIngredient()
            {
                this.Label_Info = String.Empty;
                this.InteractionList = new Hashtable();
            }

            public string opLabelInfo
            {
                get { return this.Label_Info;}
                set { this.Label_Info = value; }
            }
            public void resetList()
            {
                this.InteractionList.Clear();
            }

            public void addInteraction(string p_toInteraction, double p_drugPer, string p_organName,
                                            double p_organPer, string p_foodName, double p_foodPer)
            {
                Interaction value = new Interaction();
                value.opDrugPer = p_drugPer;
                value.opOrganName = p_organName;
                value.opOrganPer = p_organPer;
                value.opFoodName = p_foodName;
                value.opFoodPer = p_foodPer;

                if (!String.IsNullOrEmpty(p_toInteraction))
                {
                    InteractionList.Add(p_toInteraction, value);
                }
            }

            public Interaction getInteraction(string p_activeIngredientName)
            {
                try
                {
                    Interaction value = (Interaction)this.InteractionList[p_activeIngredientName];
                    return value;
                }
                catch (Exception)
                {
                    return new Interaction();
                    throw;
                }
            }
        }
        public class Interaction
        {
            double drugPer;
            string organName;
            double organPer;
            string foodName;
            double foodPer;

            public Interaction()
            { 
                this.drugPer = 0;
                this.organName = String.Empty;
                this.organPer = 0;
                this.foodName = String.Empty;
                this.foodPer = 0;
            }

            public double opDrugPer
            {
                get { return this.drugPer; }
                set {this.drugPer = value;}
            }
            public string opOrganName
            {
                get { return this.organName; }
                set { this.organName = value; }
            }
            public double opOrganPer
            {
                get { return this.organPer; }
                set { this.organPer = value; }
            }
            public string opFoodName
            {
                get { return this.foodName; }
                set { this.foodName = value; }
            }
            public double opFoodPer
            {
                get { return this.foodPer; }
                set { this.foodPer = value; }
            }
        };
        private string label_info;
        private string prospectus_info;
        private int ingredientCount;
        private ActiveIngredient activeIng;
        private Hashtable activeIngredientList;

        public Drugs(string p_label)
        {
            this.label_info = p_label;
            this.prospectus_info = "";
            this.ingredientCount = 0;
            this.activeIng = new ActiveIngredient();
            this.activeIngredientList = new Hashtable();
        }
        public void addActiveIngredientTransaction(string toAIngredient, double p_drugPer, string p_organName,
                                            double p_organPer, string p_foodName, double p_foodPer)
        {
            this.activeIng.addInteraction(toAIngredient, p_drugPer, p_organName, p_organPer, p_foodName, p_foodPer);
        }
        private void resetIngredient(ActiveIngredient value)
        {
            activeIng.opLabelInfo = String.Empty;
            value.resetList();
        }
        public void executeIngredient(string p_fromAIngredient)
        {
            this.activeIng.opLabelInfo = p_fromAIngredient;
            ActiveIngredient temp = new ActiveIngredient();
            temp = assignActiveIngredient(temp, this.activeIng);
            this.activeIngredientList.Add(activeIng.opLabelInfo,temp);
            count++;
            resetIngredient(this.activeIng);

        }
        private ActiveIngredient assignActiveIngredient(ActiveIngredient first ,ActiveIngredient second)
        {
            first.opLabelInfo = second.opLabelInfo;
            first.InteractionList = (Hashtable)second.InteractionList.Clone();
            return first;
        }
        public ActiveIngredient getActiveIngredient(string p_activeIngredientName)
        {
            try
            {
                return (ActiveIngredient)this.activeIngredientList[p_activeIngredientName];
            }
            catch (Exception)
            {
                return new ActiveIngredient();
                throw;
            }
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
    }
}
