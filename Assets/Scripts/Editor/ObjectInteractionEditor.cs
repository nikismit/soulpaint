using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectInteraction))]
public class ObjectInteractionEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

ObjectInteraction myScript = (ObjectInteraction)target;

        if (GUILayout.Button("SetUpObjects"))
        {
            myScript.SetupObjects();
        }

       // base.OnInspectorGUI();
    }
}
