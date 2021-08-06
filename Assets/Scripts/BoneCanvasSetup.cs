using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoneCanvasSetup : MonoBehaviour
{

    private GameObject currentCanvas;
   private  Paint paint;
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

        //    Debug.Log("the body part is " + other.gameObject);
            paint.SetAnotherCanvas(other.gameObject);
            currentCanvas = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "bodyPart" && currentCanvas == other.gameObject)
        {

          paint.SetAnotherCanvas(null);
         currentCanvas = null;
        }
    }

}
