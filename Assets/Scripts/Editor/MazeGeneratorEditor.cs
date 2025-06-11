using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MazeGenerator generator = (MazeGenerator)target;

        if (GUILayout.Button("Regenerate Maze"))
        {
            generator.Regenerate();
            EditorUtility.SetDirty(generator.Tilemap);
        }
    }
}