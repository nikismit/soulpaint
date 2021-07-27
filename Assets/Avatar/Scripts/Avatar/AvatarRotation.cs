using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Avatar rotation enables rotation with a joystick for the Oculus.
/// </summary>

public class AvatarRotation : MonoBehaviour
{

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float _rotationPerStep = 45;
    [SerializeField]
    private float _thumbstickThreshold = 0.75f;

    [SerializeField]
    private float _thumbstickCooldown = 0.5f;

    private GameObject player;
    private VRAvatarController avatarController;
    private Transform transformPlayer;
    private float _nextUse = 0;

    public bool allowRotate = true;

    private VRTK_ControllerEvents leftControllerAlias = null;
    private VRTK_ControllerEvents rightControllerAlias = null;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForContainer());
        Invoke("SetControllerReferences", 1f);
    }

    private IEnumerator WaitForContainer()
    {
        yield return new WaitForSeconds(1f);
        avatarController = GetComponent<VRAvatarController>();
        player = avatarController.containerObject;

        yield return new WaitUntil(() => player != null);
        this.transformPlayer = player.transform;
    }

    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
        leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();
    }

    //Update is called once per frame
    //void Update()
    //{
    //    try
    //    {
    //        //Debug.Log(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger));
    //        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp) && OVRInput.GetDown(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Touch.Four))
    //        {
    //            Debug.Log("Insert Easter egg here");
    //        }
    //        if (transformPlayer != null)
    //        {
    //            float newXRotation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
    //            if (newXRotation != 0)
    //            {
    //                Vector3 rotateAround = Vector3.zero;
    //                if (avatarController.actualAvatarVRIK == null)
    //                {
    //                    rotateAround = Camera.main.transform.position;
    //                }
    //                else
    //                {
    //                    rotateAround = avatarController.transform.position;
    //                }
    //                transformPlayer.RotateAround(rotateAround, transformPlayer.up, newXRotation * rotationSpeed);
    //            }
    //        }
    //    }
    //    catch (System.Exception)
    //    {
    //        enabled = false;
    //    }
    //}




    //This is a non-smoothed version of the Oculus rotaion 
    void Update()
    {
        try
        {

            if (transformPlayer != null && allowRotate)
            {
                if (leftControllerAlias == null || rightControllerAlias == null)
                {
                    return;
                }

                //float newXRotation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
                float leftXRotation = leftControllerAlias.GetTouchpadAxis().x;
                float rightXRotation = rightControllerAlias.GetTouchpadAxis().x;
                if (Time.time >= _nextUse)
                {
                    if (leftXRotation > _thumbstickThreshold || rightXRotation > _thumbstickThreshold)
                    {
                        Vector3 rotateAround = Vector3.zero;
                        if (avatarController.actualAvatarVRIK == null)
                        {
                            rotateAround = Camera.main.transform.position;
                        }
                        else
                        {
                            rotateAround = avatarController.transform.position;
                        }
                        _nextUse = Time.time + _thumbstickCooldown;
                        transformPlayer.RotateAround(rotateAround, transformPlayer.up, _rotationPerStep);

                    }
                    else if (leftXRotation < -_thumbstickThreshold || rightXRotation < -_thumbstickThreshold)
                    {
                        Vector3 rotateAround = Vector3.zero;
                        if (avatarController.actualAvatarVRIK == null)
                        {
                            rotateAround = Camera.main.transform.position;
                        }
                        else
                        {
                            rotateAround = avatarController.transform.position;
                        }
                        _nextUse = Time.time + _thumbstickCooldown;
                        transformPlayer.RotateAround(rotateAround, transformPlayer.up, -_rotationPerStep);

                    }
                }
            }
        }
        catch (System.Exception)
        {
            enabled = false;
        }
    }
}
