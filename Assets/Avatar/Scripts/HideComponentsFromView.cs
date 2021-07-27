using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideComponentsFromView : MonoBehaviour {

    [SerializeField]
    private int timesToLookIntoParent;
    [SerializeField]
    private int layerToExclude;

    private GameObject child;

	// Use this for initialization
	void Start () {
        StartCoroutine(HideParts());
        //Invoke("HideParts", 2f);
	}

    /// <summary>
    /// Looks for a camera in the parent.
    /// If not found look into the parent's parent.
    /// </summary>
    /// <param name="child"></param>
    /// <param name="counter"></param>
    private IEnumerator HideParts()
    {
        GameObject parent = null;
        if (child == null)
        {
            parent = this.transform.parent.gameObject;
        }
        else
        {
            parent = child.transform.parent.gameObject;
        }
        yield return new WaitUntil(() => Camera.main != null);
        Camera cam = Camera.main;
        if (cam != null)
        {
            cam.cullingMask = cam.cullingMask & ~(1 << layerToExclude);
            Debug.Log("Setting cam to " + cam.name, cam.gameObject);
        }
        else if(timesToLookIntoParent > 0)
        {
            child = parent;
            timesToLookIntoParent--;
            HideParts();
        }
    }
}
