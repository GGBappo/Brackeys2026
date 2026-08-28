using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(ConditionData))]
public class ConditionalNodeGraphView: PropertyDrawer {
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement visualElement = new VisualElement();
        visualElement.style.flexDirection = FlexDirection.Row;

        var flag = property.FindPropertyRelative("flag");
        PropertyField flag_PropertyField = new PropertyField(flag, string.Empty);
        
        var operatorToUse = property.FindPropertyRelative("operatorToUse");
        PropertyField operatorToUse_PropertyField = new PropertyField(operatorToUse);

        var valueToCompare = property.FindPropertyRelative("valueToCompare");
        PropertyField valueToCompare_PropertyField = new PropertyField(valueToCompare, string.Empty);

        var logicOperator = property.FindPropertyRelative("logicOperator");
        PropertyField logicOperator_PropertyField = new PropertyField(logicOperator);

        visualElement.Add(flag_PropertyField);
        visualElement.Add(operatorToUse_PropertyField);
        visualElement.Add(valueToCompare_PropertyField);
        visualElement.Add(logicOperator_PropertyField);

        return visualElement;
    }
}
