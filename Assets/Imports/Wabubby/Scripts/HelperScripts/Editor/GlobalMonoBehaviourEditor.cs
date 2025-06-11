#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using static Wabubby.Extensions;
using System;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class GlobalMonoBehaviourEditor : Editor
{
    bool showDebug => EditorPrefs.GetBool("GlobalDebugMode", false);

    [MenuItem("Debug/Toggle Debug Mode &d")] // alt+d to toggle
    public static void ToggleDebugMode() {
        bool current = EditorPrefs.GetBool("GlobalDebugMode", false);
        EditorPrefs.SetBool("GlobalDebugMode", !current);
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        
        GUI.color = new Color(0.7f, 0.7f, 0.7f); 

        // Add custom non-serialized field display
        var targetType = target.GetType();
        var flags = BindingFlags.Instance | BindingFlags.Public;
        var fields = targetType.GetFields(flags);

        foreach (var field in fields)
        {
            bool isNS = field.IsDefined(typeof(NonSerializedAttribute), true);

            if (field.IsDefined(typeof(ShowInInspectorAttribute), true)) {
                object value = field.GetValue(target);
                EditorGUILayout.LabelField((isNS?"NS: ":"") + field.Name, value != null ? value.ToString() : "null");

            } else if (showDebug && field.IsDefined(typeof(ShowInDebugInspectorAttribute), true)) {
                object value = field.GetValue(target);
                EditorGUILayout.LabelField((isNS?"NS: ":"") + field.Name, value != null ? value.ToString() : "null");
            }
        }

        flags = BindingFlags.Instance | BindingFlags.NonPublic;
        fields = targetType.GetFields(flags);

        foreach (var field in fields)
        {
            if (showDebug && !field.IsDefined(typeof(SerializeField), true)) {
                object value = field.GetValue(target);
                EditorGUILayout.LabelField($"{field.Name}", value != null ? value.ToString() : "null");
            }
        }
    }
}
#endif
