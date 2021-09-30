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
    public Material materialForObj;
    public Color32 colorPicked;
    float left, right;
    [SerializeField]
    bool newBrush;
    [SerializeField]
    int skipFramesRibbon =3;
    [SerializeField]
    float width = .025f;
    int counter;
    public GameObject canvas;
    public bool customBrushShaderIsOn;
    [SerializeField]
    public int particleBudget;
    int particleBudgetCounter;
    public int color;
    public GameObject prefabToSpawn;
    [SerializeField] Transform brushTipPoint;
    public bool paintingWithMaterial;
    public bool initialPaint;
    [SerializeField]private bool paintingState;
    public bool seed;
    public bool seedBrushIsOn;
    private int seedCounter;
    private int particleCounter;
    public GameObject StartButton;
    [SerializeField] Material startPaint;
    float time = 0;
    [SerializeField] private float timeToDrawButton;
    [SerializeField] private GameObject paintBrush;


    void Start()
    {
        Invoke("SetControllerReferences", 1f);
        materialToPaint = Instantiate(materialToPaint);
        materialForObj = Instantiate(materialForObj);
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
                            if (counter % skipFramesRibbon == 0)

                            {
                                Position_Last = brushTipPoint.transform.position;

                                if (meshDrawer != null)
                                {
                                         meshDrawer.setWidth(width);
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

                    if (right >= .25f && canvas != null)
                    {
                        if (seedBrushIsOn)
                        {
   
                            if (seedCounter % 60 == 0)
                            {
                                GameObject go = Instantiate(prefabToSpawn, brushTipPoint.transform.position, brushTipPoint.transform.rotation);
                                go.transform.SetParent(canvas.transform);
                                go.GetComponent<MeshRenderer>().material = materialForObj;
                              
                            }
                            seedCounter++;


                        }
                        else
                        {
                          
                            if (particleCounter % 20 == 0)
                            {
                                //instantiates objects and material is the same as object spawned
                               
                                GameObject go = Instantiate(prefabToSpawn, brushTipPoint.transform.position, brushTipPoint.transform.rotation);
                                //     go.transform.localScale = new Vector3 (right * .01f, right * .01f, right * .01f);

                                go.transform.SetParent(canvas.transform);
                                go.GetComponent<MaterialChanger>().ChangeMaterial(color);
                                particleBudgetCounter++;
                                if (particleBudgetCounter >= particleBudget)
                                {
                                    GameManager.Instance.ParticleBudgetReached();
                                }

                            }
                            particleCounter++;
                        }
                    }
                    else
                    {
                        seedCounter = 0;
                        particleCounter = 0;
                    }
                    }

                }
            }
            if (initialPaint)
            {

                // left = leftControllerAlias.GetTriggerAxis();
                if (right >= .1f)
                {
                materialToPaint = Instantiate(startPaint);
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
                    Invoke("GiveTimeToDestroy", 2f);
    

                    }


                }
            }
        }
   private void GiveTimeToDestroy()
    {
        Destroy(StartButton);
        GameManager.Instance.SetNewGamestate(Gamestate.Meditation);
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
                paintBrush.SetActive(false);
                break;
            case Gamestate.Meditation:
                paintingState = true;
                paintBrush.SetActive(true);
                paintingWithMaterial = true;

                break;
            case Gamestate.Painting:
             
                break;
            case Gamestate.ReadyforEmbody:
                paintBrush.SetActive(false);
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
