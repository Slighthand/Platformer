using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Direction))]
public class DirectionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float cellSize = 16f;
        float spacing = 4f;
        float startX = position.x + position.width / 2 - cellSize / 2;
        float startY = position.y + EditorGUIUtility.singleLineHeight;

        SerializedProperty top    = property.FindPropertyRelative("top");
        SerializedProperty bot  = property.FindPropertyRelative("bot");
        SerializedProperty left  = property.FindPropertyRelative("left");
        SerializedProperty right = property.FindPropertyRelative("right");

        // Optional label
        EditorGUI.PrefixLabel(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), label);

        // Draw the tic-tac-toe style grid (only 4 checkboxes in center directions)
        top.boolValue = EditorGUI.Toggle(new Rect(startX, startY, cellSize, cellSize), top.boolValue);
        left.boolValue = EditorGUI.Toggle(new Rect(startX - (cellSize + spacing), startY + cellSize + spacing, cellSize, cellSize), left.boolValue);
        right.boolValue = EditorGUI.Toggle(new Rect(startX + (cellSize + spacing), startY + cellSize + spacing, cellSize, cellSize), right.boolValue);
        bot.boolValue = EditorGUI.Toggle(new Rect(startX, startY + 2 * (cellSize + spacing), cellSize, cellSize), bot.boolValue);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + 3 * 20f; // enough space for 3 small rows
    }
}