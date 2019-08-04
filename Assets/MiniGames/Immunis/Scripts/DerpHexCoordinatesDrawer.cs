using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DerpHexCoordinates))]
public class DerpHexCoordinatesDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position, SerializedProperty property, GUIContent label
    )
    {
        DerpHexCoordinates derpCoordinates = new DerpHexCoordinates(
            property.FindPropertyRelative("x").intValue,
            property.FindPropertyRelative("z").intValue
        );
        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, derpCoordinates.ToString());
    }
}