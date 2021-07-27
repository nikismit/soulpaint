using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoneCanvasSetup : MonoBehaviour
{

    GameObject currentCanvas;
    Paint paint;
    private void Start()
    {
        paint = transform.root.GetComponentInChildren<Paint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // checks if the other object is a "body part" of the avatarcanvas and tells the paint object to use 
        //it as a "canvas" canvas essentially defines what the mesh is a child of
        
        if (other.tag == "bodyPart" && currentCanvas != other.gameObject)
        {
            if (paint.seedBrushIsOn)
            {
                paint.seed = true;
            }
            
            paint.SetAnotherCanvas(other.gameObject);
            currentCanvas = other.gameObject;
        }
    }

 

}
