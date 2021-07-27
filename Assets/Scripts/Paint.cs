using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


//on the root of the avatar, it handles the main painting job. It gets the input
//controller and calls on MeshLineRenderer to draw. Multiple scripts access it via 
//heirarchy/relationships. CANNOT be placed on another object without changing at 
//least the brush selector, bone canvas setup. 
//currently only works with righthand 
public class Paint : MonoBehaviour
{
    public float speed = 2f;
  
    private Vector3 Position_Last = Vector3.zero;

    public List<GameObject> clonedObjects;
  
    private MeshLineRenderer meshDrawer;
    private MeshRenderer tempRenderer;
    private MeshFilter tempFilter;

    [SerializeField]
    Mesh[] meshes;
    private VRTK_ControllerEvents leftControllerAlias = null;
    private VRTK_ControllerEvents rightControllerAlias = null;
    public Material materialToPaint;
    float left, right;
    [SerializeField]
    bool newBrush;
    int counter;
    public GameObject canvas;

   public GameObject prefabToSpawn;

    public bool paintingWithMaterial;
    public bool seed;
    public bool seedBrushIsOn;
 
 

    void Start()
    {
        Invoke("SetControllerReferences", 1f);
   
    }
    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
        //    leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rightControllerAlias != null)
        {
            right = rightControllerAlias.GetTriggerAxis();
        }
        if (paintingWithMaterial) // checks if it material/object
        {

   
            // left = leftControllerAlias.GetTriggerAxis();
            if (right >= .1f)
            {
                counter++;
                if (counter >= 3)

                {
                    Position_Last = rightControllerAlias.transform.position;

                    if (meshDrawer != null)
                    {
                        //      meshDrawer.setWidth(width);
                        meshDrawer.AddPoint(Position_Last, right); // calls on the MeshLineRenderer 
                        //and sends the next point as info, essentially the value right is currently
                        //not being used. check further on whether it would be needed in future
                    }
                    counter = 0;
                }
                if (meshDrawer == null) // mesh drawer value is set to null when "canvas" is changed
                {
                  

                    GameObject go = new GameObject();
                    go.transform.position = Position_Last;
                    tempFilter = go.AddComponent<MeshFilter>();

                    tempRenderer = go.AddComponent<MeshRenderer>();
                    go.transform.SetParent(canvas.transform);
                    meshDrawer = go.AddComponent<MeshLineRenderer>();
                    meshDrawer.lmat = materialToPaint;
                  

                    newBrush = false;
                }
            }
            else
                counter = 3;
        }

        else
        { 
        if(right >= .5f && canvas!= null)
            {
                //instantiates objects and material is the same as object spawned

                GameObject go = Instantiate(prefabToSpawn, rightControllerAlias.transform.position, rightControllerAlias.transform.rotation);
                go.transform.localScale = new Vector3 (right * .01f, right * .01f, right * .01f);
                go.transform.SetParent(canvas.transform);
                if (seed && seedBrushIsOn) //"seed" defines a new "body part" has been triggered
                    //seedBrushIsOn is defined as the current brush being used. this is to ensure
                    //multiple areas can be spawned in with a seed without having to pick the
                    // brush again and again
   

                {
                    GameObject goSeed = Instantiate(prefabToSpawn, rightControllerAlias.transform.position, rightControllerAlias.transform.rotation);
                    goSeed.transform.localScale = new Vector3(right * .01f, right * .01f, right * .01f);
                    goSeed.transform.SetParent(canvas.transform);
                    seed = false; 
                }

            }


        }
        
   
    }
    //called by bonecanvassetup to change the current canvas
    public void SetAnotherCanvas(GameObject canvasToSet) 
    {
        meshDrawer = null;
        canvas = canvasToSet;
    }
}