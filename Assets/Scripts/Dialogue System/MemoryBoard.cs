using UnityEngine;
using System.Collections.Generic;


public static class MemoryBoard {
    public static Dictionary<string, string> memoryBoard = new Dictionary<string, string>();

    #region Methods
    public static void SetVariable(string key, string value) {
        if (memoryBoard.ContainsKey(key)) {
            memoryBoard[key] = value;
        } else {
            memoryBoard.Add(key, value);
        }
    }

    public static string GetVariable(string key) {
        if (memoryBoard.ContainsKey(key)) {
            return memoryBoard[key];
        } else {
            return null;
        }
    }

    public static bool EvaluateConditions(List<ConditionData> conditions) 
    {
        if (conditions == null || conditions.Count == 0) 
        {
            return true;
        }

        bool result = false;
        
        float varValue;
        float compareValue;

        for (int i = 0; i < conditions.Count; i++) 
        {
            var condition = conditions[i];
            string variableValue = GetVariable(condition.flag);
            bool conditionResult = false;

            switch (condition.operatorToUse) 
            {
                case OperatorEnum.Equal:
                    conditionResult = variableValue == condition.valueToCompare;
                    break;
                case OperatorEnum.NotEqual:
                    conditionResult = variableValue != condition.valueToCompare;
                    break;
                case OperatorEnum.GreaterThan:
                    if (float.TryParse(variableValue, out varValue) && float.TryParse(condition.valueToCompare, out compareValue)) 
                    {
                        conditionResult = varValue > compareValue;
                    }
                    break;
                case OperatorEnum.LessThan:
                    if (float.TryParse(variableValue, out varValue) && float.TryParse(condition.valueToCompare, out compareValue)) 
                    {
                        conditionResult = varValue < compareValue;
                    }
                    break;
                case OperatorEnum.GreaterThanOrEqual:
                    if (float.TryParse(variableValue, out varValue) && float.TryParse(condition.valueToCompare, out compareValue)) 
                    {
                        conditionResult = varValue >= compareValue;
                    }
                    break;
                case OperatorEnum.LessThanOrEqual:
                    if (float.TryParse(variableValue, out varValue) && float.TryParse(condition.valueToCompare, out compareValue)) 
                    {
                        conditionResult = varValue <= compareValue;
                    }
                    break;
            }

            if (i == 0) 
            {
                result = conditionResult;
            } 
            else 
            {
                if (condition.logicOperator == LogicEnum.AND) 
                {
                    result &= conditionResult;
                } 
                else if (condition.logicOperator == LogicEnum.OR) 
                {
                    result |= conditionResult;
                } 
                else 
                {
                    result &= conditionResult; 
                }
            }
        }

        return result;
    }

    public static void InitializeDefaults() 
    {
        memoryBoard.Clear(); 

        // location & state flags
        SetVariable("PlayerLocation", "Bedroom");
        SetVariable("SuspicionLevel", "0");
        SetVariable("CoffeeDelivered", "false");
        SetVariable("DrankCoffee", "false");
        SetVariable("HidInCloset", "false");

        // scavenger hunt flags
        SetVariable("Item_PillBottle", "false");
        SetVariable("Item_Flowers", "false");
        SetVariable("Item_Passport", "false");
        SetVariable("Item_Sentimental", "false");
        SetVariable("Item_Hammer", "false");
        SetVariable("TotalItemsFound", "0");
        SetVariable("Item_Ring", "false");

        // misc flags
        SetVariable("FriendlyIntroPlayed", "false");
    }
    #endregion 
}