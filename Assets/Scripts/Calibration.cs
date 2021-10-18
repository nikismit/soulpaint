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
    [SerializeField]
    GameObject calMode;
    bool calModeBool = true;
    float time;
    bool counting;
    void Start()
    {
        calMode.SetActive(false);
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
        if (leftControllerAlias != null && rightControllerAlias!= null) //detect is button 'Y' has been pressed
        {
       
            if (rightControllerAlias.buttonTwoPressed && calModeBool)
         {
                Debug.Log("why dont i reach here");
                handMarker = rightControllerAlias.transform;
                handMarkerL = leftControllerAlias.transform;
                vrsetup.CalibrateSpace(fixedMarker, handMarker, fixedMarkerL, handMarkerL);
                time = 0;
        }

            if (leftControllerAlias.buttonOnePressed && rightControllerAlias.buttonOnePressed)
            {
                time += Time.deltaTime;
                if (time > 3f)
                {
                    calModeBool = !calModeBool;
                    calMode.SetActive(calModeBool);
                    time = 0;

                    counting = true;
                }

            }
            if (!leftControllerAlias.buttonOnePressed || !rightControllerAlias.buttonOnePressed)
            {
                time = 0;
                counting = false;
            }
        }


     
    }
}