using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour {

    [SerializeField]
    private MultiVRSetup multiVRSetup;
    [SerializeField]
    private bool leftOrRightController;
    [SerializeField]
    private PlayAreaType playAreaType;

    private GameObject controller;
    private VRAvatarController avatarController;
    private bool showControllers;
    private bool enableButtonSelecting;

    /// <summary>
    /// Toggles the showing of controllers and button selecting on enable.
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(SetUpControllers());
    }

    /// <summary>
    /// Turns the controllers (in)visible based on the SDK used.
    /// </summary>
    /// <param name="showControllers"></param>
    private void ShowVRController()
    {

        switch (playAreaType)
        {
            case PlayAreaType.SIMULATED:
                break;
            case PlayAreaType.STEAM:
                if (!showControllers)
                {
                    SteamVR_RenderModel renderModel = GetComponent<SteamVR_RenderModel>();
                    renderModel.enabled = false;

                    foreach (Transform child in renderModel.transform)
                    {
                        Renderer renderer = child.GetComponent<MeshRenderer>();

                        if (renderer != null)
                        {
                            renderer.enabled = showControllers;
                        }
                    }
                    controller = transform.parent.gameObject;
                }
                break;
            case PlayAreaType.OCULUS:
                OvrAvatar avatar = GetComponent<OvrAvatar>();
                avatar.ShowFirstPerson = showControllers;
                avatar.ShowControllers(showControllers);
                if (controller == null)
                {
                    Transform controllerTransform;
                    if (leftOrRightController)
                    {
                        controllerTransform = multiVRSetup.rightHandAlias.transform;
                    }
                    else
                    {
                        controllerTransform = multiVRSetup.leftHandAlias.transform;
                    }

                    Transform controllerClone = controllerTransform.parent;
                    controller = controllerClone.parent.gameObject;
                }
                break;
            case PlayAreaType.GEAR:
                break;
            default:
                break;
        }
        EnableButtonSelecting(controller, enableButtonSelecting);
    }

    /// <summary>
    /// Enables/Disables the button selection for the given controller based on the value.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="value"></param>
    private void EnableButtonSelecting(GameObject controller, bool value)
    {
        ButtonSelecting buttonSelecting = controller.GetComponent<ButtonSelecting>();
        if (buttonSelecting != null)
        {
            //LineRenderer lineRenderer = controller.GetComponent<LineRenderer>();
            //lineRenderer.enabled = value;
            buttonSelecting.enabled = value;
        }
    }

    private IEnumerator SetUpControllers()
    {
        yield return new WaitForSeconds(0.5f);
        if (avatarController == null)
        {
            yield return new WaitUntil(() => multiVRSetup.transform.parent.GetComponentInChildren<VRAvatarController>() != null);
            avatarController = multiVRSetup.transform.parent.GetComponentInChildren<VRAvatarController>();
        }
        showControllers = avatarController.showControllers;
        enableButtonSelecting = avatarController.enableButtonSelecting;
        ShowVRController();
    }
}
