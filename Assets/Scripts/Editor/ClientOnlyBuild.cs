using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClientOnly))]
public class ClientOnlyBuild : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ClientOnly myScript = (ClientOnly)target;

        if (GUILayout.Button("Client Only"))
        {
            myScript.DisableObjects();
        }

        if (GUILayout.Button("Master"))
        {
            myScript.EnableObjects();
        }
        base.OnInspectorGUI();
    }
}
