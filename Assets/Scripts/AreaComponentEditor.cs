using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(AreaComponent))]
public class AreaComponentEditor : Editor {
    private List<string> missingSavepoints = new List<string>();

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        AreaComponent generator = (AreaComponent)target;

        if (GUILayout.Button("Scan Scene for Savepoints")) {
            missingSavepoints.Clear();

            if (generator.Area == null) {
                EditorGUILayout.HelpBox("No AreaSO assigned.", MessageType.Warning);
                return;
            }

            GameObject[] savepointGOs = GameObject.FindGameObjectsWithTag("Savepoint");

            List<GameObject> sceneSavepointGOs = new();
            foreach (GameObject go in savepointGOs) {
                if (go.scene == generator.gameObject.scene) sceneSavepointGOs.Add(go);
            }

            Undo.RecordObject(generator.Area, "Update Savepoints");

            generator.Area.savepoints.Clear();
            for (int i=0; i<sceneSavepointGOs.Count; i++) {
                // string assetPath = $"Assets/Savepoints/Savepoint_{sp.index}.asset";
                // Savepoint so = AssetDatabase.LoadAssetAtPath<Savepoint>(assetPath);

                if (sceneSavepointGOs[i].TryGetComponent(out Flag flag)) {
                    flag.area = generator.Area;
                    flag.index = i;
                    EditorUtility.SetDirty(flag);
                }

                Savepoint sp = new Savepoint(generator.Area, i, sceneSavepointGOs[i].transform.position);
                generator.Area.savepoints.Add(sp);
            }

            EditorUtility.SetDirty(generator.Area);
            AssetDatabase.SaveAssets();
        }
    }
}