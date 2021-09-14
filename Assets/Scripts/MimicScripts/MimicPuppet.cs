using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MimicPuppet : MonoBehaviour
{
    public enum MovementType { NoRotNoPos, YesRotYesPos, NoRotYesPos, YesRotNoPos};
    public GameObject player;
    private MimicSender masterAvatarscript;
    //int numOfBones = 42, numOfTransforms = 2;
    int numOfBones = 52, numOfTransforms = 2;

    Transform[] bones;
    public bool notGrabbed;
    [SerializeField]
    bool debugSetup;
    
    //Positions
    Vector3 masterRefPosition;
    Vector3 previousPosition;
    [SerializeField]
    Vector3 addedPosition;
    [SerializeField]
    Vector3 previousAddedPosition;
    [SerializeField]
    MovementType myMovType;
    //Rotations
    Vector3 masterRefRotation;
    Vector3 addedRotation;
    Vector3 rotationDelta;

    Quaternion localRotation;
    bool setup;

    // Start is called before the first frame update
    void Start()
    {

        addedRotation = transform.eulerAngles;
        bones = new Transform[numOfBones];
        setUpBonesArray(transform);
        notGrabbed = true;
        Invoke("Setup", 1f);
      // player = GameObject.FindGameObjectWithTag("Player");

        //if (player != null)
        //{
        //    masterAvatarscript = player.GetComponent<MimicSender>();
        //    rotationDelta = transform.eulerAngles - player.transform.eulerAngles;
        //   // print($"Rotation delta is {rotationDelta}");
        //}
     

    }
    void Setup()
    {
        if (debugSetup)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        masterAvatarscript = player.GetComponent<MimicSender>();
        rotationDelta = transform.eulerAngles - player.transform.eulerAngles;
        previousAddedPosition = transform.position;
        print($"Rotation delta is {rotationDelta}");
        masterRefPosition = masterAvatarscript.startPosition;
        masterRefRotation = masterAvatarscript.startRotation;
        previousPosition = masterAvatarscript.position;
        setup = true;
    }
  
    // Update is called once per frame
    void Update()
    {
        //if (player == null)
        //{
        //    player = GameObject.FindGameObjectWithTag("Player");
        //    masterAvatarscript = player.GetComponent<MimicSender>();
        //    rotationDelta = transform.eulerAngles - player.transform.eulerAngles;
        //    previousAddedPosition = transform.position;
        //    print($"Rotation delta is {rotationDelta}");
        //    masterRefPosition = masterAvatarscript.startPosition;
        //    masterRefRotation = masterAvatarscript.startRotation;
        //    previousPosition = masterAvatarscript.position;
        //}
        if (setup)
        {
            for (int i = 0; i < numOfBones; i++)
            {
                if ( i == 0)
                {
                    switch (myMovType)
                    {
                        case MovementType.NoRotNoPos:

                            break;
                        case MovementType.YesRotYesPos:
                            bones[i].localPosition = masterAvatarscript.bonePositionsMimic[i];
                            bones[i].localEulerAngles = masterAvatarscript.boneRotationsMimic[i];
                            break;
                        case MovementType.NoRotYesPos:
                            bones[i].localPosition = masterAvatarscript.bonePositionsMimic[i];
                         
                            break;
                        case MovementType.YesRotNoPos:
                       
                            bones[i].localEulerAngles = masterAvatarscript.boneRotationsMimic[i];
                            break;
                        default:
                            break;
                    }

                    //   Debug.Log("Do Not MOVE");
                }
                else
                {
                    //sets the first frame to the starting position
                    bones[i].localPosition = masterAvatarscript.bonePositionsMimic[i];
                    bones[i].localEulerAngles = masterAvatarscript.boneRotationsMimic[i];
                }

            }

            if (notGrabbed && !masterAvatarscript.stopMoving)
            {
                Vector3 moveDirection = masterAvatarscript.movementDirection;
                localRotation.SetFromToRotation(masterAvatarscript.transform.forward, transform.forward);
                moveDirection = localRotation * moveDirection * transform.lossyScale.x;

                transform.position += moveDirection + addedPosition;
                transform.eulerAngles = masterAvatarscript.rotation - masterRefRotation + addedRotation;
                addedPosition = Vector3.zero;
            }
            if (!notGrabbed || masterAvatarscript.stopMoving)

            {
                if (masterAvatarscript.stopMoving)
                {
                    masterRefPosition = masterAvatarscript.position;
                    masterRefRotation = masterAvatarscript.rotation;

                }
                else
                {
                    addedRotation = transform.eulerAngles;
                    addedPosition = (previousAddedPosition - transform.position);
                    previousAddedPosition = transform.position;
                    masterRefPosition = masterAvatarscript.position;
                    masterRefRotation = masterAvatarscript.rotation;
                }

            }

            previousPosition = masterAvatarscript.movementDirection;
            //float y = masterAvatarscript.rotation;
            //Debug.Log("y is " + y);
            //transform.Rotate(0, y, 0);
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
    public void StopChangingmainValues()
    {
        notGrabbed = false;
        //called when puppet is grabbed
    }
    public void StartChangingmainValues()
    {
       
        notGrabbed = true;
        //called when puppet is ungrabbed
    }
}
