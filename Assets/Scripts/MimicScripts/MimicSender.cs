using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimicSender : MonoBehaviour
{
    [SerializeField]
    
    [Tooltip ("Change this value to false on Runtime to make the puppets move")]
    public bool stopMoving = true;

    int numOfBones = 42, numOfTransforms = 2;                          //two Variables used to determine the length of the boneTransforms Array


    public List<Vector3>bonePositionsMimic;
    public List<Vector3> boneRotationsMimic;
    public Vector3 rotation;
    public Vector3 position;
    public Vector3 startRotation;
    public Vector3 startPosition;
    public Vector3 movementDirection;
   
   

    Vector3 previousPosition;



    //two lists for holding momentary information about bones rotations and positions
    //public Vector3[,,] boneTransforms;                                  //vector3 Array that holds every frames positional and rotational data
    Transform[] bones;                                                  //transform Array referenced to all the moving bones transforms

    public GameObject mimicPrefab;                                      //this is for spawning only
   
     

    // Start is called before the first frame update
    void Start()
    {
        bones = new Transform[numOfBones];
        setUpBonesArray(transform);
        SendBones();
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //define interaction for spawning new mimic puppets


        SendBones();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            stopMoving = !stopMoving;
        }
    }


    

    public Transform FindVRIKComponent(Transform whereToFind, string nameToFind)
    {
        Transform foundComponent = null;
        foreach (Transform child in whereToFind)
        {
            if (child.name.EndsWith(nameToFind))
            {
                return child;
            }
            if (foundComponent != null)
                break;
            if (child.childCount > 0)
            {
                foundComponent = FindVRIKComponent(child, nameToFind);
            }
        }
        return foundComponent;
    }

    
 

    public Transform[] getBones()
    {
        return bones;
    }

   
       

    void setUpBonesArray(Transform gameObjectTransform)
    {
        bones[0] = FindVRIKComponent(gameObjectTransform, "ROOT");
        //  bones[0] = gameObjectTransform;       //transform of model itself
        bones[1] = FindVRIKComponent(gameObjectTransform, "TT");
        bones[2] = FindVRIKComponent(gameObjectTransform, "TT L Thigh");
        bones[3] = FindVRIKComponent(gameObjectTransform, "TT R Thigh");
        bones[4] = FindVRIKComponent(gameObjectTransform, "TT Spine");
        bones[5] = FindVRIKComponent(gameObjectTransform, "TT L Calf");
        bones[6] = FindVRIKComponent(gameObjectTransform, "TT R Calf");
        bones[7] = FindVRIKComponent(gameObjectTransform, "TT Spine2");
        bones[8] = FindVRIKComponent(gameObjectTransform, "TT L Foot");
        bones[9] = FindVRIKComponent(gameObjectTransform, "TT R Foot");
        bones[10] = FindVRIKComponent(gameObjectTransform, "TT L Toe0");
        bones[11] = FindVRIKComponent(gameObjectTransform, "TT R Toe0");
        bones[12] = FindVRIKComponent(gameObjectTransform, "TT L Clavicle");
        bones[13] = FindVRIKComponent(gameObjectTransform, "TT Neck");
        bones[14] = FindVRIKComponent(gameObjectTransform, "TT R Clavicle");
        bones[15] = FindVRIKComponent(gameObjectTransform, "TT L UpperArm");
        bones[16] = FindVRIKComponent(gameObjectTransform, "TT Head");
        bones[17] = FindVRIKComponent(gameObjectTransform, "TT R UpperArm");
        bones[18] = FindVRIKComponent(gameObjectTransform, "TT L Forearm");
        bones[19] = FindVRIKComponent(gameObjectTransform, "TT R Forearm");
        bones[20] = FindVRIKComponent(gameObjectTransform, "TT L Hand");
        bones[21] = FindVRIKComponent(gameObjectTransform, "TT R Hand");
        bones[22] = FindVRIKComponent(gameObjectTransform, "TT L Finger0_00");
        bones[23] = FindVRIKComponent(gameObjectTransform, "TT L Finger1_00");
        bones[24] = FindVRIKComponent(gameObjectTransform, "TT L Finger2_00");
        bones[25] = FindVRIKComponent(gameObjectTransform, "TT L Finger3_00");
        bones[26] = FindVRIKComponent(gameObjectTransform, "TT L Finger4_00");
        bones[27] = FindVRIKComponent(gameObjectTransform, "TT R Finger0_00");
        bones[28] = FindVRIKComponent(gameObjectTransform, "TT R Finger1_00");
        bones[29] = FindVRIKComponent(gameObjectTransform, "TT R Finger2_00");
        bones[30] = FindVRIKComponent(gameObjectTransform, "TT R Finger3_00");
        bones[31] = FindVRIKComponent(gameObjectTransform, "TT R Finger4_00");
        bones[32] = FindVRIKComponent(gameObjectTransform, "TT L Finger0_01");
        bones[33] = FindVRIKComponent(gameObjectTransform, "TT L Finger1_01");
        bones[34] = FindVRIKComponent(gameObjectTransform, "TT L Finger2_01");
        bones[35] = FindVRIKComponent(gameObjectTransform, "TT L Finger3_01");
        bones[36] = FindVRIKComponent(gameObjectTransform, "TT L Finger4_01");
        bones[37] = FindVRIKComponent(gameObjectTransform, "TT R Finger0_01");
        bones[38] = FindVRIKComponent(gameObjectTransform, "TT R Finger1_01");
        bones[39] = FindVRIKComponent(gameObjectTransform, "TT R Finger2_01");
        bones[40] = FindVRIKComponent(gameObjectTransform, "TT R Finger3_01");
        bones[41] = FindVRIKComponent(gameObjectTransform, "TT R Finger4_01");

    }


    private void SendBones()
    {

        for (int i = 0; i < numOfBones; i++)
        {

            //THIS IS NOT LOCAL POSITION, AS THE VR CONTAINER CAN MOVE


            if (bonePositionsMimic.Count <= i)
                bonePositionsMimic.Add(bones[i].localPosition);
            else
                bonePositionsMimic[i] = bones[i].localPosition;

            if (boneRotationsMimic.Count <= i)
                boneRotationsMimic.Add(bones[i].localEulerAngles);
            else
                boneRotationsMimic[i] = bones[i].localEulerAngles;


        }
        if (!stopMoving)
        {
            position = transform.position;
            rotation = transform.eulerAngles;
            movementDirection = position - previousPosition;
            previousPosition = position;
            
        }


        else if (stopMoving)
        {
             
          position = transform.position;
          rotation = transform.eulerAngles;
          previousPosition = position;
        }

    }
  
   
    public void SpawnNewPuppet()
    {
        Debug.Log("I received message to instantiate");
        GameObject puppet = null;

        puppet = mimicPrefab;

        puppet = Instantiate(mimicPrefab);
    }
}