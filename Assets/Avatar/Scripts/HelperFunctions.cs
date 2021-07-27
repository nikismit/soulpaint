using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions {

    /// <summary>
    /// Updates given index based on the list given.
    /// </summary>
    /// <param name="listCount">List count of the index that needs to be updated</param>
    /// <param name="index">index that you want to update</param>
    public static void UpdateIndex(int listCount, ref int index)
    {
        if (index >= listCount - 1 || index < 0)
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }

    public static GameObject FindChildWithTag(this GameObject go, string tag)
    {
        foreach (Transform child in go.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    public static float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
