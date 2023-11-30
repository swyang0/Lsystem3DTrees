
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawTree))]
[CanEditMultipleObjects]
public class DrawEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawTree tree = (DrawTree)target;

        if (GUILayout.Button("Generate"))
        {
            tree.generate();
            //Debug.Log("hahaha");
        }

        if (GUILayout.Button("Grow"))
        {
            tree.growTree();
            //Debug.Log("hahaha");
        }

        if (GUILayout.Button("Clear"))
        {
            tree.dispose();
            //Debug.Log("hahaha");
        }
    }
}
