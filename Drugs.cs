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
        private ActiveIngredient activeIng;
        private LinkedList<ActiveIngredient> activeIngredientList;

        public Drugs(string p_label)
        {
            this.label_info = p_label;
            this.prospectus_info = "";
            this.ingredientCount = 0;
            this.activeIng = new ActiveIngredient();
            this.activeIngredientList = new LinkedList<ActiveIngredient>();
        }
        public void addActiveIngredientTransaction(string toAIngredient, double p_drugPer, string p_organName, string p_organDesc,
                                            double p_organPer, string p_foodName, string p_foodDesc, double p_foodPer)
        {
            this.activeIng.addInteraction(toAIngredient, p_drugPer, p_organName, p_organDesc, p_organPer, p_foodName, p_foodDesc, p_foodPer);
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
            this.activeIngredientList.AddLast(temp);
            count++;
            resetIngredient(this.activeIng);

        }
        private ActiveIngredient assignActiveIngredient(ActiveIngredient first ,ActiveIngredient second)
        {
            first.opLabelInfo = second.opLabelInfo;
            Interaction[] temp = new Interaction[second.InteractionList.Count];
            for (int i = 0; i < second.InteractionList.Count; i++)
            {
                temp[i] = new Interaction();
            }

            second.InteractionList.CopyTo(temp,0);
            for (int k = 0; k < second.InteractionList.Count; k++)
            {
                first.InteractionList.AddLast(temp[k]);
            }
            return first;
        }
        public LinkedList<ActiveIngredient> getActiveIngredientList()
        {
            return this.activeIngredientList;
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
    public class ActiveIngredient
    {
        private string Label_Info;
        public LinkedList<Interaction> InteractionList;

        public ActiveIngredient()
        {
            this.Label_Info = String.Empty;
            this.InteractionList = new LinkedList<Interaction>();
        }

        public string opLabelInfo
        {
            get { return this.Label_Info; }
            set { this.Label_Info = value; }
        }
        public void resetList()
        {
            this.InteractionList.Clear();
        }

        public void addInteraction(string p_toInteraction, double p_drugPer, string p_organName, string p_organDesc,
                                        double p_organPer, string p_foodName, string p_foodDesc, double p_foodPer)
        {
            Interaction value = new Interaction();
            value.opToInteraction = p_toInteraction;
            value.opDrugPer = p_drugPer;
            value.opOrganName = p_organName;
            value.opOrganDesc = p_organDesc;
            value.opOrganPer = p_organPer;
            value.opFoodName = p_foodName;
            value.opFoodDesc = p_foodDesc;
            value.opFoodPer = p_foodPer;

            if (!String.IsNullOrEmpty(p_toInteraction))
            {
                InteractionList.AddLast(value);
                value.opInteractionCount++;
            }
        }

        public LinkedList<Interaction> getInteractionList()
        {
            return this.InteractionList;
        }
    }

    public class Interaction
    {
        private string toInteraction;
        private double drugPer;
        private string organName;
        private string organDesc;
        private double organPer;
        private string foodName;
        private string foodDesc;
        private double foodPer;
        private int interactionCount;
        public Interaction()
        {
            this.toInteraction = String.Empty;
            this.drugPer = 0;
            this.organName = String.Empty;
            this.organPer = 0;
            this.foodName = String.Empty;
            this.foodPer = 0;
            this.interactionCount = 0;
        }
        public string opToInteraction
        {
            get { return this.toInteraction; }
            set { this.toInteraction = value; }
        }
        public double opDrugPer
        {
            get { return this.drugPer; }
            set { this.drugPer = value; }
        }
        public string opOrganName
        {
            get { return this.organName; }
            set { this.organName = value; }
        }
        public string opOrganDesc
        {
            get { return this.organDesc; }
            set { this.organDesc = value; }
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
        public string opFoodDesc
        {
            get { return this.foodDesc; }
            set { this.foodDesc = value; }
        }
        public double opFoodPer
        {
            get { return this.foodPer; }
            set { this.foodPer = value; }
        }
        public int opInteractionCount
        {
            get { return this.interactionCount; }
            set { this.foodPer = value; }
        }
    }
}
