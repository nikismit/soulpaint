using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using System;
using UnityEngine.XR;

public class SnapPointScript : MonoBehaviour {
 
    private VRTK_DestinationPoint[] DestinationPoints = new VRTK_DestinationPoint[0];


    //When Entering over a DestinationPoint
    public void OnDestinationMarkerEnter(object obj, DestinationMarkerEventArgs e)
    {
        if (e.target.GetComponent<VRTK_DestinationPoint>())
        {
            e.target.Find("PlayerSnapPointModel").GetChild(0).gameObject.SetActive(true);
            if (e.target.GetComponent<AudioSource>())
                e.target.GetComponent<AudioSource>().PlayOneShot(e.target.GetComponent<AudioSource>().clip);


            string productName = XRDevice.model;
            string switcho = productName.ToLower().Contains("oculus").ToString() + gameObject.name.Contains("Left").ToString();


            switch (switcho)
            {
                case "TrueTrue":
                    OVRInput.SetControllerVibration(1, 128, OVRInput.Controller.LTrackedRemote);
                    break;
                case "TrueFalse":
                    OVRInput.SetControllerVibration(1, 128, OVRInput.Controller.RTrackedRemote);
                    break;
                case "FalseTrue":
                    VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(SDK_BaseController.ControllerHand.Left), .05f);
                    break;
                case "FalseFalse":
                    VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(SDK_BaseController.ControllerHand.Right), .05f);
                    break;
            }
        }
    }

    //When Exiting a DestinationPoint
    public void OnDestinationMarkerExit(object obj, DestinationMarkerEventArgs e)
    {
        if (e.target.GetComponent<VRTK_DestinationPoint>())
        {
            e.target.Find("PlayerSnapPointModel").GetChild(0).gameObject.SetActive(false);
        }
    }

    //When TeleportButton is being Pressed
    public void PointerEnabled(object obj, ControllerInteractionEventArgs e)
    {
        if(DestinationPoints.Length <= 0)
            DestinationPoints = GameObject.FindObjectsOfType<VRTK_DestinationPoint>();
        foreach (var Point in DestinationPoints)
        {
            if (Point && Point.transform.GetChild(0))
            {
                Point.transform.GetChild(0).gameObject.SetActive(true);
                if(!Point.disabled)
                    Point.ToggleColliders(true);
            }
        }
    }

    //When TeleportButton is released
    public void PointerDisabled(object obj, ControllerInteractionEventArgs e)
    {
        if (DestinationPoints.Length <= 0)
            DestinationPoints = GameObject.FindObjectsOfType<VRTK_DestinationPoint>();
        foreach (var Point in DestinationPoints)
        {
            if (Point && Point.transform.GetChild(0))
            {
                Point.transform.GetChild(0).gameObject.SetActive(false);
                Point.ToggleColliders(false);
            }
        }
    }
    /**/
}
