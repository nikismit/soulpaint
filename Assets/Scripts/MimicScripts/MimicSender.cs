﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimicSender : MonoBehaviour
{
    [SerializeField]
    
    [Tooltip ("Change this value to false on Runtime to make the puppets move")]
    public bool stopMoving = true;

   // int numOfBones = 42, numOfTransforms = 2;                          //two Variables used to determine the length of the boneTransforms Array
    int numOfBones = 52, numOfTransforms = 2;                          //two Variables used to determine the length of the boneTransforms Array

    public List<Vector3>bonePositionsMimic = new List<Vector3>();
    public List<Vector3> boneRotationsMimic = new List<Vector3>();
    public Vector3 rotation;
    public Vector3 position;
    public Vector3 startRotation;
    public Vector3 startPosition;
    public Vector3 movementDirection;
    bool setup;
    [SerializeField]
    bool debugSetup;
   

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

        if (setup)
        {
            SendBones();
        }
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

   
       

    //void setUpBonesArray(Transform gameObjectTransform)
    //{
    //    bones[0] = FindVRIKComponent(gameObjectTransform, "ROOT");
    //    //  bones[0] = gameObjectTransform;       //transform of model itself
    //    bones[1] = FindVRIKComponent(gameObjectTransform, "TT");
    //    bones[2] = FindVRIKComponent(gameObjectTransform, "TT L Thigh");
    //    bones[3] = FindVRIKComponent(gameObjectTransform, "TT R Thigh");
    //    bones[4] = FindVRIKComponent(gameObjectTransform, "TT Spine");
    //    bones[5] = FindVRIKComponent(gameObjectTransform, "TT L Calf");
    //    bones[6] = FindVRIKComponent(gameObjectTransform, "TT R Calf");
    //    bones[7] = FindVRIKComponent(gameObjectTransform, "TT Spine2");
    //    bones[8] = FindVRIKComponent(gameObjectTransform, "TT L Foot");
    //    bones[9] = FindVRIKComponent(gameObjectTransform, "TT R Foot");
    //    bones[10] = FindVRIKComponent(gameObjectTransform, "TT L Toe0");
    //    bones[11] = FindVRIKComponent(gameObjectTransform, "TT R Toe0");
    //    bones[12] = FindVRIKComponent(gameObjectTransform, "TT L Clavicle");
    //    bones[13] = FindVRIKComponent(gameObjectTransform, "TT Neck");
    //    bones[14] = FindVRIKComponent(gameObjectTransform, "TT R Clavicle");
    //    bones[15] = FindVRIKComponent(gameObjectTransform, "TT L UpperArm");
    //    bones[16] = FindVRIKComponent(gameObjectTransform, "TT Head");
    //    bones[17] = FindVRIKComponent(gameObjectTransform, "TT R UpperArm");
    //    bones[18] = FindVRIKComponent(gameObjectTransform, "TT L Forearm");
    //    bones[19] = FindVRIKComponent(gameObjectTransform, "TT R Forearm");
    //    bones[20] = FindVRIKComponent(gameObjectTransform, "TT L Hand");
    //    bones[21] = FindVRIKComponent(gameObjectTransform, "TT R Hand");
    //    bones[22] = FindVRIKComponent(gameObjectTransform, "TT L Finger0_00");
    //    bones[23] = FindVRIKComponent(gameObjectTransform, "TT L Finger1_00");
    //    bones[24] = FindVRIKComponent(gameObjectTransform, "TT L Finger2_00");
    //    bones[25] = FindVRIKComponent(gameObjectTransform, "TT L Finger3_00");
    //    bones[26] = FindVRIKComponent(gameObjectTransform, "TT L Finger4_00");
    //    bones[27] = FindVRIKComponent(gameObjectTransform, "TT R Finger0_00");
    //    bones[28] = FindVRIKComponent(gameObjectTransform, "TT R Finger1_00");
    //    bones[29] = FindVRIKComponent(gameObjectTransform, "TT R Finger2_00");
    //    bones[30] = FindVRIKComponent(gameObjectTransform, "TT R Finger3_00");
    //    bones[31] = FindVRIKComponent(gameObjectTransform, "TT R Finger4_00");
    //    bones[32] = FindVRIKComponent(gameObjectTransform, "TT L Finger0_01");
    //    bones[33] = FindVRIKComponent(gameObjectTransform, "TT L Finger1_01");
    //    bones[34] = FindVRIKComponent(gameObjectTransform, "TT L Finger2_01");
    //    bones[35] = FindVRIKComponent(gameObjectTransform, "TT L Finger3_01");
    //    bones[36] = FindVRIKComponent(gameObjectTransform, "TT L Finger4_01");
    //    bones[37] = FindVRIKComponent(gameObjectTransform, "TT R Finger0_01");
    //    bones[38] = FindVRIKComponent(gameObjectTransform, "TT R Finger1_01");
    //    bones[39] = FindVRIKComponent(gameObjectTransform, "TT R Finger2_01");
    //    bones[40] = FindVRIKComponent(gameObjectTransform, "TT R Finger3_01");
    //    bones[41] = FindVRIKComponent(gameObjectTransform, "TT R Finger4_01");

    //}

    void setUpBonesArray(Transform gameObjectTransform)
    {
        if (!debugSetup)
        {
            bones[0] = gameObjectTransform;       //transform of model itself
            bones[1] = FindVRIKComponent(gameObjectTransform, "Base HumanPelvis");
            bones[2] = FindVRIKComponent(gameObjectTransform, "Base HumanLLegThigh");
            bones[3] = FindVRIKComponent(gameObjectTransform, "Base HumanRLegThigh");
            bones[4] = FindVRIKComponent(gameObjectTransform, "Base HumanSpine1");
            bones[5] = FindVRIKComponent(gameObjectTransform, "Base HumanSpine4");
            bones[6] = FindVRIKComponent(gameObjectTransform, "Base HumanLLegCalf");
            bones[7] = FindVRIKComponent(gameObjectTransform, "Base HumanRLegCalf");
            bones[8] = FindVRIKComponent(gameObjectTransform, "Base HumanLLegFoot");
            bones[9] = FindVRIKComponent(gameObjectTransform, "Base HumanRLegFoot");
            bones[10] = FindVRIKComponent(gameObjectTransform, "Base HumanFootBone001");
            bones[11] = FindVRIKComponent(gameObjectTransform, "Base HumanFootBone001 1");
            bones[12] = FindVRIKComponent(gameObjectTransform, "Base HumanLArmCollarbone");
            bones[13] = FindVRIKComponent(gameObjectTransform, "Base HumanNeck2");
            bones[14] = FindVRIKComponent(gameObjectTransform, "Base HumanRCollarbone");
            bones[15] = FindVRIKComponent(gameObjectTransform, "Base HumanLArmUpperarm");
            bones[16] = FindVRIKComponent(gameObjectTransform, "Base HumanHead");
            bones[17] = FindVRIKComponent(gameObjectTransform, "Base HumanRUpperarm");
            bones[18] = FindVRIKComponent(gameObjectTransform, "Base HumanLArmForearm");
            bones[19] = FindVRIKComponent(gameObjectTransform, "Base HumanRForearm");
            bones[20] = FindVRIKComponent(gameObjectTransform, "Base HumanLArmPalm");
            bones[21] = FindVRIKComponent(gameObjectTransform, "Base HumanRPalm");
            bones[22] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone001");
            bones[23] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone001Bone001");
            bones[24] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone001Bone001Bone001");
            bones[25] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone002");
            bones[26] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone002Bone001");
            bones[27] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone002Bone001Bone001");
            bones[28] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone003");
            bones[29] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone003Bone001");
            bones[30] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone003Bone001Bone001");
            bones[31] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone004");
            bones[32] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone004Bone001");
            bones[33] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone004Bone001Bone001");
            bones[34] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone005");
            bones[35] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone005Bone001");
            bones[36] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone005Bone001Bone001");
            bones[37] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone001 1");
            bones[38] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone001Bone001 1");
            bones[39] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone001Bone001Bone001 1");
            bones[40] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone002 1");
            bones[41] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone002Bone001 1");
            bones[42] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone002Bone001Bone001 1");
            bones[43] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone003 1");
            bones[44] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone003Bone001 1");
            bones[45] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone003Bone001Bone001 1");
            bones[46] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone004 1");
            bones[47] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone004Bone001 1");
            bones[48] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone004Bone001Bone001 1");
            bones[49] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone005 1");
            bones[50] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone005Bone001 1");
            bones[51] = FindVRIKComponent(gameObjectTransform, "Base HumanPalmBone005Bone001Bone001 1");
        }
        else
        {
            bones[0] = FindVRIKComponent(gameObjectTransform, "ChildRig");
            //  bones[0] = gameObjectTransform;       //transform of model itself
            bones[1] = FindVRIKComponent(gameObjectTransform, "J_Lumbar");
            bones[2] = FindVRIKComponent(gameObjectTransform, "L_Femur");
            bones[3] = FindVRIKComponent(gameObjectTransform, "R_Femur");
            bones[4] = FindVRIKComponent(gameObjectTransform, "J_Dorsal1");
            bones[5] = FindVRIKComponent(gameObjectTransform, "L_Knee");
            bones[6] = FindVRIKComponent(gameObjectTransform, "R_Knee");
            bones[7] = FindVRIKComponent(gameObjectTransform, "J_Dorsal2");
            bones[8] = FindVRIKComponent(gameObjectTransform, "L_rotFoot1");
            bones[9] = FindVRIKComponent(gameObjectTransform, "R_rotFoot1");
            bones[10] = FindVRIKComponent(gameObjectTransform, "L_jt14");
            bones[11] = FindVRIKComponent(gameObjectTransform, "R_jt14");
            bones[12] = FindVRIKComponent(gameObjectTransform, "L_Clavicle");
            bones[13] = FindVRIKComponent(gameObjectTransform, "J_Neck1");
            bones[14] = FindVRIKComponent(gameObjectTransform, "R_Clavicle");
            bones[15] = FindVRIKComponent(gameObjectTransform, "L_Arm");
            bones[16] = FindVRIKComponent(gameObjectTransform, "J_Head");
            bones[17] = FindVRIKComponent(gameObjectTransform, "R_Arm");
            bones[18] = FindVRIKComponent(gameObjectTransform, "L_Forearm");
            bones[19] = FindVRIKComponent(gameObjectTransform, "R_Forearm");
            bones[20] = FindVRIKComponent(gameObjectTransform, "L_Hand");
            bones[21] = FindVRIKComponent(gameObjectTransform, "R_Hand");
            bones[22] = FindVRIKComponent(gameObjectTransform, "L_Index1");
            bones[23] = FindVRIKComponent(gameObjectTransform, "L_Index2");
            bones[24] = FindVRIKComponent(gameObjectTransform, "L_Index3");
            bones[25] = FindVRIKComponent(gameObjectTransform, "L_Little1");
            bones[26] = FindVRIKComponent(gameObjectTransform, "L_Little2");
            bones[27] = FindVRIKComponent(gameObjectTransform, "L_Little3");
            bones[28] = FindVRIKComponent(gameObjectTransform, "L_Middle1");
            bones[29] = FindVRIKComponent(gameObjectTransform, "L_Middle2");
            bones[30] = FindVRIKComponent(gameObjectTransform, "L_Middle3");
            bones[31] = FindVRIKComponent(gameObjectTransform, "L_Thumb3");
            bones[32] = FindVRIKComponent(gameObjectTransform, "L_Thumb1");
            bones[33] = FindVRIKComponent(gameObjectTransform, "L_Thumb2");
            bones[34] = FindVRIKComponent(gameObjectTransform, "L_Third1");
            bones[35] = FindVRIKComponent(gameObjectTransform, "L_Third2");
            bones[36] = FindVRIKComponent(gameObjectTransform, "L_Third3");
            bones[37] = FindVRIKComponent(gameObjectTransform, "R_Index1");
            bones[38] = FindVRIKComponent(gameObjectTransform, "R_Index2");
            bones[39] = FindVRIKComponent(gameObjectTransform, "R_Index3");
            bones[40] = FindVRIKComponent(gameObjectTransform, "R_Little1");
            bones[41] = FindVRIKComponent(gameObjectTransform, "R_Little2");
            bones[42] = FindVRIKComponent(gameObjectTransform, "R_Little3");
            bones[43] = FindVRIKComponent(gameObjectTransform, "R_Middle1");
            bones[44] = FindVRIKComponent(gameObjectTransform, "R_Middle2");
            bones[45] = FindVRIKComponent(gameObjectTransform, "R_Middle3");
            bones[46] = FindVRIKComponent(gameObjectTransform, "R_Thumb3");
            bones[47] = FindVRIKComponent(gameObjectTransform, "R_Thumb1");
            bones[48] = FindVRIKComponent(gameObjectTransform, "R_Thumb2");
            bones[49] = FindVRIKComponent(gameObjectTransform, "R_Third1");
            bones[50] = FindVRIKComponent(gameObjectTransform, "R_Third2");
            bones[51] = FindVRIKComponent(gameObjectTransform, "R_Third3");

        }
    }

    private void SendBones()
    {

        for (int i = 0; i < numOfBones; i++)
        {

            //THIS IS NOT LOCAL POSITION, AS THE VR CONTAINER CAN MOVE


            if (bonePositionsMimic.Count <= i)
            {
                bonePositionsMimic.Add(bones[i].localPosition);
            }
            else
                bonePositionsMimic[i] = bones[i].localPosition;

            if (boneRotationsMimic.Count <= i)
            { boneRotationsMimic.Add(bones[i].localEulerAngles);
                setup = true;
            }
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