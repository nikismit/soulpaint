using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK;
using VRTK.SecondaryControllerGrabActions;


public class ObjectInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> ObjectsToSetUp;



    public void SetupObjects()
    {
        for (int i = 0; i < ObjectsToSetUp.Count; i++)
        {
            if (ObjectsToSetUp[i].GetComponent<Collider>() == null)
            { ObjectsToSetUp[i].AddComponent<BoxCollider>(); }
           VRTK_InteractableObject pickupable = ObjectsToSetUp[i].AddComponent<VRTK_InteractableObject>();
           VRTK_ChildOfControllerGrabAttach vRTK_ChildOfControllerGrabAttach=  ObjectsToSetUp[i].AddComponent<VRTK_ChildOfControllerGrabAttach>();
           VRTK_SwapControllerGrabAction vRTK_SwapControllerGrabAction = ObjectsToSetUp[i].AddComponent<VRTK_SwapControllerGrabAction>();
            pickupable.grabAttachMechanicScript = vRTK_ChildOfControllerGrabAttach;
            pickupable.secondaryGrabActionScript = vRTK_SwapControllerGrabAction;
            pickupable.disableWhenIdle = false;
            pickupable.isGrabbable = true;
            ObjectsToSetUp[i].AddComponent<Rigidbody>();  
            vRTK_ChildOfControllerGrabAttach.precisionGrab = true;
         
      

        }


    }
   
}
