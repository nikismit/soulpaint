using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager2 : MonoBehaviour
{

    [SerializeField]
    Transform underTheFloor;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.finalPuppet.transform.position = underTheFloor.position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
