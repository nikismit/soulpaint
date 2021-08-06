using System;
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


    private VRTK_ControllerEvents leftControllerAlias = null;
    private VRTK_ControllerEvents rightControllerAlias = null;
    public Material materialToPaint;
    public Color32 colorPicked;
    float left, right;
    [SerializeField]
    bool newBrush;
    int counter;
    public GameObject canvas;
    public bool customBrushShaderIsOn;
    public GameObject prefabToSpawn;
    [SerializeField] Transform brushTipPoint;
    public bool paintingWithMaterial;
    public bool initialPaint;
    [SerializeField]private bool paintingState;
    //  public bool seed;
    //  public bool seedBrushIsOn;
    private int particleCounter;
    public GameObject StartButton;
    float time = 0;
    [SerializeField] private float timeToDrawButton;
    [SerializeField] private GameObject paintBrush;
    void Start()
    {
        Invoke("SetControllerReferences", 1f);

    }

    private void OnEnable()
    {
        GameManager.gamestateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.gamestateChanged -= OnGameStateChanged;
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
            if (paintingState)
            {
                if (canvas != null)
                {
                    if (paintingWithMaterial) // checks if it material/object
                    {


                        // left = leftControllerAlias.GetTriggerAxis();
                        if (right >= .1f)
                        {
                            counter++;
                            if (counter % 3 == 0)

                            {
                                Position_Last = brushTipPoint.transform.position;

                                if (meshDrawer != null)
                                {
                                    //      meshDrawer.setWidth(width);
                                    meshDrawer.AddPoint(Position_Last, right); // calls on the MeshLineRenderer 
                                                                               //and sends the next point as info, essentially the value right is currently
                                                                               //not being used. check further on whether it would be needed in future
                                }
                             
                            }
                            if (meshDrawer == null) // mesh drawer value is set to null when "canvas" is changed
                            {


                                GameObject go = new GameObject();
                                go.transform.position = Position_Last;
                                tempFilter = go.AddComponent<MeshFilter>();

                                tempRenderer = go.AddComponent<MeshRenderer>();
                                go.transform.SetParent(canvas.transform);
                                meshDrawer = go.AddComponent<MeshLineRenderer>();
                                meshDrawer.lmat =  Instantiate(materialToPaint);

                            counter = 0;
                                newBrush = false;
                            }
                        }
                      
                    }

                    else
                    {
                        if (right >= .5f && canvas != null)
                        {
                            particleCounter++;
                            if (particleCounter % 20 == 0)
                            {
                                //instantiates objects and material is the same as object spawned

                                GameObject go = Instantiate(prefabToSpawn, brushTipPoint.transform.position, brushTipPoint.transform.rotation);
                                //     go.transform.localScale = new Vector3 (right * .01f, right * .01f, right * .01f);
                                
                                go.transform.SetParent(canvas.transform);
                            go.GetComponent<MaterialChanger>().ChangeMaterial(materialToPaint);
                                particleCounter++;
                            }

                        }
                    }

                }
            }
            if (initialPaint)
            {

                // left = leftControllerAlias.GetTriggerAxis();
                if (right >= .1f)
                {
                    time += Time.deltaTime;
                    if (time < timeToDrawButton)
                    {

                        Position_Last = brushTipPoint.transform.position;

                        if (meshDrawer != null)
                        {
                            meshDrawer.AddPoint(Position_Last, right); // calls on the MeshLineRenderer 
                                                                       //and sends the next point as info, essentially the value right is currently
                                                                       //not being used. check further on whether it would be needed in future
                        }


                        if (meshDrawer == null) // mesh drawer value is set to null when "canvas" is changed
                        {
                            GameObject go = new GameObject();
                            go.transform.position = Position_Last;
                            tempFilter = go.AddComponent<MeshFilter>();
                            tempRenderer = go.AddComponent<MeshRenderer>();
                            go.transform.SetParent(StartButton.transform);
                            meshDrawer = go.AddComponent<MeshLineRenderer>();
                            meshDrawer.lmat = materialToPaint;
                            newBrush = false;
                        }
                    }
                    else
                    {
                        initialPaint = false;
                        meshDrawer = null;
                        Destroy(StartButton);
                        GameManager.Instance.SetNewGamestate(Gamestate.Meditation);

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


    private void OnGameStateChanged(Gamestate newGameState)
    {
        switch (newGameState)
        {
            case Gamestate.WaitforStart:
              
                break;
            case Gamestate.Meditation:
             //   paintBrush.SetActive(false);
                break;
            case Gamestate.Painting:
                paintingState = true;
             //   paintBrush.SetActive(true);
                break;
            case Gamestate.Embody:
                paintingState = false;
                break;
            case Gamestate.Dance:
                break;
            case Gamestate.PostDance:
                break;
            default:
                break;
        }
      
        

    }


} 
