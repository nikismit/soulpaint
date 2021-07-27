using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VRIKApplier))]
public class VRIKApplierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VRIKApplier myScript = (VRIKApplier)target;

        if (GUILayout.Button("Apply Bones"))
        {
            myScript.applyVRIKComponents();
        }

        base.OnInspectorGUI();
    }
}
