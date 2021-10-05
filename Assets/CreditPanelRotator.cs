using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CreditPanelRotator : MonoBehaviour
{
    [SerializeField] float rotationFollowSpeed;
    [SerializeField] XRNode m_VRNode    = XRNode.Head;
    Vector3 trackingPos;

    void Update()
    {
        Vector3 rot = this.transform.rotation.eulerAngles;
        Quaternion quaternion  = InputTracking.GetLocalRotation(m_VRNode);
        Quaternion newRot = Quaternion.Lerp(this.transform.rotation, quaternion, rotationFollowSpeed);
        rot.y = newRot.eulerAngles.y;
        this.transform.rotation = newRot;// Quaternion.Euler(rot);

    }
}
