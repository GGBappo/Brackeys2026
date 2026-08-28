using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class RuntimeDialogueGraph : ScriptableObject
{
    public string EntryNodeID;
    public List<RuntimeDialogueNode> AllNodes = new List<RuntimeDialogueNode>();
}

[Serializable]
public class RuntimeDialogueNode
{
    // general node data
    public string NodeID;
    public string NextNodeID;

    // dialogue node & choice node
    public string SpeakerName;
    public string DialogueText;
    public Sprite SpeakerImage;

    // choice node specific
    public List<ChoiceData> Choices = new List<ChoiceData>();

    // action node specific
    public ActionData Action;

    // conditional node specific
    public List<ConditionData> Conditions = new List<ConditionData>();
    public string TrueNodeID;
    public string FalseNodeID;
}

[Serializable]
public class ChoiceData
{
    public string ChoiceText;
    public string DestinationNodeID;
}

[Serializable]
public class ActionData
{
    public ActionNodeType Action;
    public DialogueBoxPosition dialogueBoxPosition;
}

[Serializable]
public class ConditionData
{
    public string flag;
    public OperatorEnum operatorToUse;
    public string valueToCompare;
    public LogicEnum logicOperator;
}