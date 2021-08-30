using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPuppetsCreator : MonoBehaviour
{

    GameObject puppetToCreate;
    Transform[] transformSetup;
    // Start is called before the first frame update
    void Start()
    {
        puppetToCreate = GameManager.Instance.finalMimicPuppet;
        transformSetup = GetComponentsInChildren<Transform>();
        SetupPuppets();
    }


    private void SetupPuppets()
    {
        foreach (Transform tr in transformSetup)
        {
            GameObject go = Instantiate(puppetToCreate);
            puppetToCreate.transform.position = tr.position;
            puppetToCreate.transform.eulerAngles = tr.eulerAngles;
            puppetToCreate.transform.localScale = tr.localScale;
        }
    }

}
