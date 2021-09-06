using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Contains a list of parts to hide and also contains a number of the layer to hide.
/// </summary>
public class HideParts : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of GameObjects to change layer of.")]
    private List<GameObject> partsToHide = new List<GameObject>();
    [SerializeField]
    [Tooltip("Layer that will replace the current layer for the list of objects.")]
    private int layerToHideNumber;
    public bool isEmbodying;

    private void Start()
    {
        if (isEmbodying)
        {
      
            ShowOrHideParts(true);
        }
    }

    /// <summary>
    /// Recursive method that sets the layer of an object based on the boolean that has been given.
    /// </summary>
    /// <param name="parent">Parent Object.</param>
    /// <param name="hideOrShow">Show or hide this GameObject and its children.</param>
    private void HidePartsInChildren(GameObject parent, bool hideOrShow)
    {
        if (hideOrShow)
        {
            parent.layer = layerToHideNumber;
        }
        else
        {
            parent.layer = 0;
        }
        foreach (Transform child in parent.transform)
        {
            HidePartsInChildren(child.gameObject, hideOrShow);
        }
    }

    /// <summary>
    /// Shows or hides all the parts that are stored in the list based on the boolean.
    /// </summary>
    /// <param name="hide">If true hide all parts</param>
    public void ShowOrHideParts(bool hide)
    {
        try
        {
            foreach (GameObject partToHide in partsToHide)
            {
                HidePartsInChildren(partToHide, hide);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}