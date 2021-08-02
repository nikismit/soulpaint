using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCanvasInstance : MonoBehaviour
{
    public static AvatarCanvasInstance instance;
  
    Collider[] collidersForCanvas; 

    private void Awake()
    {

        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
