using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PHP_Drugs;
namespace PHP_Drug_Interaction_Core
{
    enum RiscRating { A, B, C, D, X };
    public static class RiskRatingDesc
    {
        public static double A = 3.2;
        public static double B = 6.4;
        public static double C = 9.6;
        public static double D = 12.8;
        public static double X = 16;
    }
    public static class RiskRatingMessage
    {
        public static string A = "Level Low 1";
        public static string B = "Level Low 2";
        public static string C = "Level Medium";
        public static string D = "Level High";
        public static string X = "Level High Risk";
    }
    class Analyzer
    {
        public class AnalyzeStructure
        {
            private Interaction interactionDescription;
            private RiscRating level;

            public AnalyzeStructure()
            {
                this.interactionDescription = new Interaction();
                this.level = RiscRating.A;
            }

            public void setInteractionDescription(Interaction p_interaction)
            {
                this.interactionDescription = p_interaction;
            }
            public Interaction getInteractionDescription()
            {
                return this.interactionDescription;
            }
            public void setLevel(RiscRating rating)
            {
                this.level = rating;
            }
            public RiscRating getLevel()
            {
                return this.level;
            }
        }
        public Drugs[] getProspectus(Drugs[] source, string[] pros)
        {
            Drugs[] prospectus = new Drugs[pros.Length];
            for (int i = 0; i < pros.Length; i++)
            {
                prospectus[i] = new Drugs(pros[i]);
                for (int k = 0; k < source.Length; k++)
                {
                    if (prospectus[i].label.Equals(source[k].label))
                    {
                        prospectus[i] = source[k];
                    }
                }
            }

            return prospectus;
        }
        public ArrayList getAnalyzeResult(Drugs[] prescription)
        {
            ArrayList analyzeResults = new ArrayList();
            string interactionMessage = "";
            for (int i = 0; i < prescription.Length; i++)
            {
                Drugs current = prescription[i];
                interactionMessage = current.label + "\n-----------\n";
                LinkedList<ActiveIngredient> currentAIList = current.getActiveIngredientList();
                for (int k = (i + 1); k < prescription.Length ; k++)
                {
                    Drugs target = prescription[k];
                    LinkedList<ActiveIngredient> targetAIList = target.getActiveIngredientList();
                    foreach (ActiveIngredient targetAI in targetAIList)
                    {
                       
                        foreach (ActiveIngredient currentAI in currentAIList)
                        {
                            LinkedList<Interaction> currentInteractionList = currentAI.getInteractionList();
                            foreach (Interaction currentInteraction in currentInteractionList)
                            {
                                if (targetAI.opLabelInfo == currentInteraction.opToInteraction)
                                {
                                    AnalyzeStructure analStruct = new AnalyzeStructure();
                                    analStruct.setInteractionDescription(currentInteraction);
                                    double funcResult = analyzeFunction(analStruct);
                                    analStruct.setLevel(getRiskRatingForDrug(funcResult));
                                    string riskMessage = getRiskMessage(analStruct.getLevel());
                                    interactionMessage += getMessage(target.label, currentAI.opLabelInfo, 
                                                                            currentInteraction, riskMessage);
                                    Console.WriteLine(interactionMessage);
                                    interactionMessage = String.Empty;
                                }
                            }
                            
                        }
                    }
                }


            }
            return analyzeResults;
        }
        public string getMessage(string targetDrugName ,
                                string fromInteraction, Interaction toInteraction, string riskMessage)
        {
            string message;
            message = String.Format("{1} Risk LEVEL : {0}, interact on {2} <--> {3} For {4} \n"
                                    ,riskMessage,targetDrugName, fromInteraction,
                                    toInteraction.opToInteraction,toInteraction.opOrganDesc);
            return message;
        }
        public double analyzeFunction(AnalyzeStructure analyzeStruct)
        {
            double organAffect = 0.08, drugAffect = 0.05, foodAffect = 0.03;

            double drugPerc = analyzeStruct.getInteractionDescription().opDrugPer;
            double organPerc = analyzeStruct.getInteractionDescription().opOrganPer;
            double foodPerc = analyzeStruct.getInteractionDescription().opFoodPer;

            double functionResult = 0.0;
            if (foodPerc == 0)
            {
                functionResult = (drugAffect * drugPerc) + (organAffect * organPerc);
            }
            else
                functionResult = (drugAffect * drugPerc) + (organAffect * organPerc) + (foodAffect * foodPerc);
            
            return functionResult;
        }
        public RiscRating getRiskRatingForDrug(double functionResult)
        {
            if (functionResult <= RiskRatingDesc.A)
            {
                return RiscRating.A;
            }
            else if (functionResult <= RiskRatingDesc.B)
            {
                return RiscRating.B;
            }
            else if (functionResult <= RiskRatingDesc.C)
            {
                return RiscRating.C;
            }
            else if (functionResult <= RiskRatingDesc.D)
            {
                return RiscRating.D;
            }
            else
                return RiscRating.X;
        }
        public string getRiskMessage(RiscRating rating)
        {
            switch (rating)
            {
                case RiscRating.A:
                    return RiskRatingMessage.A;
                case RiscRating.B:
                    return RiskRatingMessage.B;
                case RiscRating.C:
                    return RiskRatingMessage.C;
                case RiscRating.D:
                    return RiskRatingMessage.D;
                case RiscRating.X:
                    return RiskRatingMessage.X;
                default:
                    return String.Empty;
            }
        }
    }
}
