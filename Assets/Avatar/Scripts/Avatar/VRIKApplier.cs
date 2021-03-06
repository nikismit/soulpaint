using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class VRIKApplier : MonoBehaviour
{
    private VRIK.References myVRIK;

    public enum BoneStructure
    {
        normal = 0,
        toon_kids,
        spNew,
        soulPaint,
    }

    public BoneStructure _boneStructure = BoneStructure.toon_kids;

    [SerializeField]
   public Transform[] originPoints;

    public void applyVRIKComponents()
    {
        if (_boneStructure == BoneStructure.normal)
        {
            myVRIK = this.GetComponent<VRIK>().references;
            myVRIK.root = this.transform;
            myVRIK.pelvis = findVRIKComponent(this.transform, "Hips");
            myVRIK.spine = findVRIKComponent(this.transform, "Spine");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine"));
            myVRIK.chest = findVRIKComponent(this.transform, "Spine1");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine1"));
            myVRIK.neck = findVRIKComponent(this.transform, "Neck");
            myVRIK.head = findVRIKComponent(this.transform, "Head");
            myVRIK.leftShoulder = findVRIKComponent(this.transform, "LeftShoulder");
            myVRIK.leftUpperArm = findVRIKComponent(this.transform, "LeftArm");
            myVRIK.leftForearm = findVRIKComponent(this.transform, "LeftForeArm");
            myVRIK.leftHand = findVRIKComponent(this.transform, "LeftHand");
            myVRIK.rightShoulder = findVRIKComponent(this.transform, "RightShoulder");
            myVRIK.rightUpperArm = findVRIKComponent(this.transform, "RightArm");
            myVRIK.rightForearm = findVRIKComponent(this.transform, "RightForeArm");
            myVRIK.rightHand = findVRIKComponent(this.transform, "RightHand");
            myVRIK.leftThigh = findVRIKComponent(this.transform, "LeftUpLeg");
            myVRIK.leftCalf = findVRIKComponent(this.transform, "LeftLeg");
            myVRIK.leftFoot = findVRIKComponent(this.transform, "LeftFoot");
            myVRIK.leftToes = findVRIKComponent(this.transform, "LeftToeBase");
            myVRIK.rightThigh = findVRIKComponent(this.transform, "RightUpLeg");
            myVRIK.rightCalf = findVRIKComponent(this.transform, "RightLeg");
            myVRIK.rightFoot = findVRIKComponent(this.transform, "RightFoot");
            myVRIK.rightToes = findVRIKComponent(this.transform, "RightToeBase");
        }
        else if (_boneStructure == BoneStructure.toon_kids)
        {
            myVRIK = this.GetComponent<VRIK>().references;
            myVRIK.root = this.transform;
            myVRIK.pelvis = findVRIKComponent(this.transform, "Hips");
            myVRIK.spine = findVRIKComponent(this.transform, "Spine");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine"));
            myVRIK.chest = findVRIKComponent(this.transform, "Spine1");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine1"));
            myVRIK.neck = findVRIKComponent(this.transform, "Neck");
            myVRIK.head = findVRIKComponent(this.transform, "Head");
            myVRIK.leftShoulder = findVRIKComponent(this.transform, "L Clavicle");
            myVRIK.leftUpperArm = findVRIKComponent(this.transform, "L UpperArm");
            myVRIK.leftForearm = findVRIKComponent(this.transform, "L Forearm");
            myVRIK.leftHand = findVRIKComponent(this.transform, "L Hand");
            myVRIK.rightShoulder = findVRIKComponent(this.transform, "R Clavicle");
            myVRIK.rightUpperArm = findVRIKComponent(this.transform, "R UpperArm");
            myVRIK.rightForearm = findVRIKComponent(this.transform, "R Forearm");
            myVRIK.rightHand = findVRIKComponent(this.transform, "R Hand");
            myVRIK.leftThigh = findVRIKComponent(this.transform, "L Thigh");
            myVRIK.leftCalf = findVRIKComponent(this.transform, "L Calf");
            myVRIK.leftFoot = findVRIKComponent(this.transform, "L Foot");
            myVRIK.leftToes = findVRIKComponent(this.transform, "L Toe0");
            myVRIK.rightThigh = findVRIKComponent(this.transform, "R Thigh");
            myVRIK.rightCalf = findVRIKComponent(this.transform, "R Calf");
            myVRIK.rightFoot = findVRIKComponent(this.transform, "R Foot");
            myVRIK.rightToes = findVRIKComponent(this.transform, "R Toe0");
        }
        else if (_boneStructure == BoneStructure.spNew)
        {
            myVRIK = this.GetComponent<VRIK>().references;
            myVRIK.root = myVRIK.pelvis = findVRIKComponent(this.transform, "Root");
            ;
            myVRIK.pelvis = findVRIKComponent(this.transform, "Base HumanPelvis");
            myVRIK.spine = findVRIKComponent(this.transform, "Base HumanSpine3");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine01"));
            myVRIK.chest = findVRIKComponent(this.transform, "Base HumanSpine4");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine04"));
            myVRIK.neck = findVRIKComponent(this.transform, "Base HumanNeck2");
            myVRIK.head = findVRIKComponent(this.transform, "Base HumanHead");
            myVRIK.leftShoulder = findVRIKComponent(this.transform, "Base HumanLArmCollarbone");
            myVRIK.leftUpperArm = findVRIKComponent(this.transform, "Base HumanLArmUpperarm");
            myVRIK.leftForearm = findVRIKComponent(this.transform, "Base HumanLArmForearm");
            myVRIK.leftHand = findVRIKComponent(this.transform, "Base HumanLArmPalm");
            myVRIK.rightShoulder = findVRIKComponent(this.transform, "Base HumanRCollarbone");
            myVRIK.rightUpperArm = findVRIKComponent(this.transform, "Base HumanRUpperarm");
            myVRIK.rightForearm = findVRIKComponent(this.transform, "Base HumanRForearm");
            myVRIK.rightHand = findVRIKComponent(this.transform, "Base HumanRPalm");
            myVRIK.leftThigh = findVRIKComponent(this.transform, "Base HumanLLegThigh");
            myVRIK.leftCalf = findVRIKComponent(this.transform, "Base HumanLLegCalf");
            myVRIK.leftFoot = findVRIKComponent(this.transform, "Base HumanLLegFoot");
            myVRIK.leftToes = findVRIKComponent(this.transform, "Base HumanFootBone001");
            myVRIK.rightThigh = findVRIKComponent(this.transform, "Base HumanRLegThigh");
            myVRIK.rightCalf = findVRIKComponent(this.transform, "Base HumanRLegCalf");
            myVRIK.rightFoot = findVRIKComponent(this.transform, "Base HumanRLegFoot");
            myVRIK.rightToes = findVRIKComponent(this.transform, "Base HumanFootBone001 1");
        }
        else if (_boneStructure == BoneStructure.soulPaint)
        {
            myVRIK = this.GetComponent<VRIK>().references;
            myVRIK.root = findVRIKComponent(this.transform, "ChildRig");
            myVRIK.pelvis = findVRIKComponent(this.transform, "J_Lumbar");
            myVRIK.spine = findVRIKComponent(this.transform, "J_Dorsal1");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine01"));
            myVRIK.chest = findVRIKComponent(this.transform, "J_Dorsal2");
            //			Debug.Log (findVRIKComponent (this.transform, "Spine04"));
            myVRIK.neck = findVRIKComponent(this.transform, "J_Neck1");
            myVRIK.head = findVRIKComponent(this.transform, "J_Head");
            myVRIK.leftShoulder = findVRIKComponent(this.transform, "L_Clavicle");
            myVRIK.leftUpperArm = findVRIKComponent(this.transform, "L_Arm");
            myVRIK.leftForearm = findVRIKComponent(this.transform, "L_Forearm");
            myVRIK.leftHand = findVRIKComponent(this.transform, "L_Hand");
            myVRIK.rightShoulder = findVRIKComponent(this.transform, "R_Clavicle");
            myVRIK.rightUpperArm = findVRIKComponent(this.transform, "R_Arm");
            myVRIK.rightForearm = findVRIKComponent(this.transform, "R_Forearm");
            myVRIK.rightHand = findVRIKComponent(this.transform, "R_Hand");
            myVRIK.leftThigh = findVRIKComponent(this.transform, "L_Femur");
            myVRIK.leftCalf = findVRIKComponent(this.transform, "L_Knee");
            myVRIK.leftFoot = findVRIKComponent(this.transform, "L_rotFoot1");
            myVRIK.leftToes = findVRIKComponent(this.transform, "L_jt14");
            myVRIK.rightThigh = findVRIKComponent(this.transform, "R_Femur");
            myVRIK.rightCalf = findVRIKComponent(this.transform, "R_Knee");
            myVRIK.rightFoot = findVRIKComponent(this.transform, "R_rotFoot1");
            myVRIK.rightToes = findVRIKComponent(this.transform, "R_jt14");
        }

    }


    /* Searches recursively through all children and also their children 
    *for a specific string in the end of an object's name */
    public Transform findVRIKComponent(Transform whereToFind, string nameToFind)
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
                foundComponent = findVRIKComponent(child, nameToFind);
            }
        }
        return foundComponent;
    }



}

