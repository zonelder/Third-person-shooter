using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(FireType),true)]
public class FireTypePropertyDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var enumRect = new Rect(position.min.x, position.y, position.width , EditorGUIUtility.singleLineHeight);

        GUIContent Label;
        label.text = "FireType";
        property.isExpanded = true;
        EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("_fireType"),label);
        if(property.FindPropertyRelative("_fireType").intValue ==(int)FireTypeName.Burst)
        {
            label.text = "burstAmmoCount";
            var burstIntRect = new Rect(position.min.x, position.y+ EditorGUIUtility.singleLineHeight + 1, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(burstIntRect, property.FindPropertyRelative("_burstCount"), label);
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label)+ EditorGUIUtility.singleLineHeight+5;
    }
}
