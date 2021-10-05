using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class Calibration : MonoBehaviour
{
    [SerializeField]
    VRAvatarController vrsetup;
    public Transform handMarker, handMarkerL;  //the controller on the hand
    public Transform fixedMarker, fixedMarkerL; //the fixed controller
    private VRTK_ControllerEvents rightControllerAlias = null, leftControllerAlias = null;
    public Transform lookat;

    void Start()
    {
        Invoke("SetControllerReferences", 1f);

    }

 void SetControllerReferences()
    {
      
            rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
            leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();


    }
    // Update is called once per frame


    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) && rightControllerAlias != null) //detect is button 'B' has been pressed
        {
            handMarker = rightControllerAlias.transform;
            handMarkerL = leftControllerAlias.transform;
            vrsetup.CalibrateSpace(fixedMarker, handMarker, fixedMarkerL, handMarkerL);
          
        }
    }
}