using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

/// <summary>
/// Enables you to interact with worldspace UI’s. 
/// When hovering over a button the button will be “highlighted” and you can invoke the OnClick of the button.
/// </summary>
[RequireComponent(typeof(VRTK_ControllerEvents))]
public class ButtonSelecting : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField]
    [Tooltip("InputManger for registering if the user has clicked the button.")]
    private VRTK_ControllerEvents inputManager;
    [SerializeField]
    [Tooltip("Which hand are we using.")]
    private HandSide side;

    [Header("Pointer Settings")]
    [SerializeField]
    [Tooltip("From how far can we interact with the UI.")]
    private float rayDistance;
    [SerializeField]
    [Tooltip("Custom cursor/pointer.")]
    private GameObject customPointer;
    [SerializeField]
    [Tooltip("Moves object from target point.")]
    private float distanceFromHitPoint;
    [SerializeField]
    [Tooltip("Highlight color when hovering over a button.")]
    private Color buttonHighlightColor;

    private bool lineActive;
    private bool selectedUIItem;
    private bool delayActive;

    private Ray rayCast;
    private RaycastHit rayCastHit;

    private Button tempButton;
    private Color tempButtonColor;

    /// <summary>
    /// Set up variables.
    /// </summary>
    private void Start()
    {
        lineActive = false;
        selectedUIItem = false;
        delayActive = false;

        inputManager = GetComponent<VRTK_ControllerEvents>();

        HandAlias handAlias = GetComponentInChildren<HandAlias>();
        if (handAlias != null)
        {
            side = GetComponentInChildren<HandAlias>().side;
        }

        StartCoroutine(Delay(.1f));
    }

    /// <summary>
    /// Cast a ray and see if the target is the UI.
    /// If so then enable the custom pointer.
    /// </summary>
    private void Update()
    {
        rayCast = new Ray(transform.position, transform.forward);

        //Debug.DrawRay(rayCast.origin, rayCast.direction * rayDistance);


        if (Physics.Raycast(rayCast, out rayCastHit, rayDistance) && rayCastHit.collider.gameObject.layer == 5)
        {
            lineActive = true;
            if (customPointer != null)
            {
                customPointer.SetActive(true);
                customPointer.transform.position = rayCastHit.point - (transform.forward * distanceFromHitPoint);
                customPointer.transform.rotation = Quaternion.Euler(new Vector3(0, rayCastHit.collider.transform.eulerAngles.y, this.transform.localEulerAngles.z));
            }
        }
        else
        {
            lineActive = false;
            if (customPointer != null)
            {
                customPointer.SetActive(false);
                DisableHighlight();
            }
        }


        //Disable/Enable's the Custom Pointer depending on the state of the bool
        if (customPointer.activeSelf != lineActive)
        {
            customPointer.SetActive(lineActive);
        }

        switch (lineActive)
        {
            case true:
                RayCast();
                break;
        }

        if (OVRManager.isHmdPresent && !delayActive && !selectedUIItem)
        {
            switch (side)
            {
                case HandSide.Left:
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
                    {
                        lineActive = !lineActive;
                        StartCoroutine(Delay(.1f));
                    }
                    break;
                case HandSide.Right:
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                    {
                        lineActive = !lineActive;
                        StartCoroutine(Delay(.1f));
                    }
                    break;
                default:
                    Debug.LogWarning("Please assign the handside");
                    break;
            }
        }
    }

    /// <summary>
    /// Returns hand side of the controller.
    /// </summary>
    /// <returns>Handside of the controller.</returns>
    public HandSide GethandSide()
    {
        return side;
    }

    /// <summary>
    /// Cast ray and look for a button.
    /// Also checks if the trigger is pressed.
    /// </summary>
    private void RayCast()
    {
        try
        {
            if (Physics.Raycast(rayCast, out rayCastHit, 200) && rayCastHit.collider.GetComponent<Button>() != null)
            {
                DisableHighlight();
                tempButton = rayCastHit.collider.GetComponent<Button>();

                //Change the state of the bool
                if (inputManager.triggerClicked && !OVRManager.isHmdPresent && !delayActive)
                {
                    tempButton.onClick.Invoke();
                    StartCoroutine(Delay(.1f));
                }
                else if (OVRManager.isHmdPresent && !delayActive)
                {
                    switch (side)
                    {
                        case HandSide.Left:
                            ButtonClick(OVRInput.Controller.LTouch);
                            break;
                        case HandSide.Right:
                            ButtonClick(OVRInput.Controller.RTouch);
                            break;
                        default:
                            Debug.Log("Assign a handside");
                            break;
                    }
                }
                selectedUIItem = true;

                //Set the line color to green and snap the position of the line
                if (lineActive)
                { }
                float left = Input.GetAxis("Oculus_GearVR_LIndexTrigger");
                float right = Input.GetAxis("Oculus_GearVR_RIndexTrigger");

                if (left > 0.8f || right > .8f)
                {
                    tempButton.onClick.Invoke();
                    StartCoroutine(Delay(.1f));
                }

            }
            else
            {
                if (lineActive && tempButton != null)
                {
                    tempButton.GetComponent<Image>().color = tempButtonColor;
                }
                tempButton = null;
                selectedUIItem = false;

                //Set the line color to red and allow the line to follow the ray
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Clicks button if trigger is pressed.
    /// </summary>
    /// <param name="controller"></param>
    private void ButtonClick(OVRInput.Controller controller)
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            tempButton.onClick.Invoke();
            StartCoroutine(Delay(.1f));
        }
    }

    /// <summary>
    /// Disables highlight of the button.
    /// </summary>
    private void DisableHighlight()
    {
        if (tempButton != null)
        {
            tempButton.GetComponent<Image>().color = Color.white;
        }
        tempButton = null;
        selectedUIItem = false;
    }

    /// <summary>
    /// Delays the pressing of buttons.
    /// </summary>
    /// <param name="delaySeconds"></param>
    /// <returns></returns>
    private IEnumerator Delay(float delaySeconds)
    {
        float delay = delaySeconds * 2;
        delayActive = true;

        while (delay > 0)
        {
            delay -= delaySeconds;
            yield return new WaitForSeconds(delaySeconds);
        }

        delayActive = false;
    }
}
