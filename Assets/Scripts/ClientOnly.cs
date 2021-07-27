using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientOnly : MonoBehaviour
{
    [SerializeField]
    List<GameObject> masterSideObject;
    [SerializeField]
    GameObject lightPatch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableObjects()
    {
        for (int i = 0; i < masterSideObject.Count; i++)
        {
            masterSideObject[i].SetActive(false);
            lightPatch.SetActive(true);
        }

    }
    public void EnableObjects()
    {
        for (int i = 0; i < masterSideObject.Count; i++)
        {
            masterSideObject[i].SetActive(true);
            lightPatch.SetActive(false);
        }

    }
}
