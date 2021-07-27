using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjects : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Objects that you want to show")]
    private List<GameObject> objectsToShow;
    [SerializeField]
    [Tooltip("List of objects that need to be disabled for this one to be enabled at start.")]
    private List<GameObject> otherObjects;
    [SerializeField]
    private float delayBeforeCheck = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CheckObjects());
    }

    private IEnumerator CheckObjects()
    {
        yield return new WaitForSeconds(delayBeforeCheck);
        bool enable = true;
        foreach (GameObject gameObject in otherObjects)
        {
            if (gameObject.activeSelf)
            {
                enable = false;
                break;
            }
        }

        if (enable)
        {
            foreach (GameObject gameObject in objectsToShow)
            {
                gameObject.SetActive(enable);
            }
        }
    }
}
