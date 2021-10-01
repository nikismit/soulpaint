using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using VRTK;
using UnityEngine.Events;

/// <summary>
/// Uses the VRRig and attaches an avatar and the controllers to it.
/// </summary>

public class VRAvatarController : MonoBehaviour
{
    [Header("Controller Settings")]
    [Tooltip("Whether you wanna setup the VR immediately or wait")]
    public bool setupOnStart = true;
    [Tooltip("Shows Controllers in game.")]
    public bool showControllers;
    [Tooltip("Enable button selecting with a line when pointing at UI.")]
    public bool enableButtonSelecting;
    [Tooltip("What kind of controller do you want to use. This covers all the basic interactions concerning the hands.")]
    public GameObject VRControllerPrefab;

    [Header("Avatar Settings")]
    [SerializeField]
    [Tooltip("The rig that handles the VR.")]
    private GameObject VRRigPrefab;
    [SerializeField]
    [Tooltip("Enable if you want to have an Avatar.")]
    private bool enableAvatar;
    [SerializeField]
    [Tooltip("Offsets of Avatar families. (Only fill in if an avatar is used)")]
    private List<AvatarOffset> avatarOffsets;

    [Header("If Playing Local")]
    [Tooltip("Avatars to be used.")]
    public List<VRIK> avatars;
    [Tooltip("Outfits to be used. Place in the same order of the avatars")]
    public List<Outfits> outfits;
    [Tooltip("List of Miniatures. Can be used as placeholders.")]
    public List<GameObject> miniatures;

    [Header("Events")]
    public UnityEvent avatarChanged;

    public int indexActualAvatar { get; private set; }
    public VRIK actualAvatarVRIK { get; private set; }
    public VRTK_SDKManager sdkManager { get; private set; }
    public GameObject containerObject { get; private set; }
    public MultiVRSetup multiVR { get; private set; }
    public GameObject leftController { get; private set; }
    public GameObject rightController { get; private set; }

    private Collider[] ownColliders;
    private GameObject VRRigObject;
    private VRTK_BezierPointerRenderer rightControllerTeleport;
    private VRTK_BezierPointerRenderer leftControllerTeleport;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private bool haveIStarted = false;
    private Transform playArea;
    private Transform container;
    private bool local;
    private float localScale = 1;
    private int offsetIndex;

    [SerializeField]
    Transform rotationPoint;
    [SerializeField]
    bool isDanceScene;
    /// <summary>
    /// Check if running as arcade.
    /// Also checks if playing locally.
    /// Calls the VRSetup.
    /// </summary>
    private void Start()
    {
        if (isDanceScene && GameManager.Instance != null)
        {
            avatars[0] = GameManager.Instance.finalPuppet.GetComponent<VRIK>();
        }

      //  PlayersData.Instance.PlayerData.Scale = 1;
        if (setupOnStart)
        {
            StartSetup();
        }
     
    }

    public void StartSetup()
    {
     
            local = true;
       

        StartCoroutine(SetPlayArea());
        indexActualAvatar = -1;
        VRSetup(this.transform.position, this.transform.rotation);
        haveIStarted = true;
    }

    /// <summary>
    /// Wait's for the multiVR to set the playarea and the container.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetPlayArea()
    {
        yield return new WaitUntil(() => multiVR != null);
        playArea = multiVR.playAreaAlias.transform;
        container = containerObject.transform;
    }

    /// <summary>
    /// After every teleport call the ResetIKSolver method.
    /// </summary>
    private void OnEnable()
    {
        //VRTK_DashTeleport.StaticHasFinishedMoving += ResetIKSolver;
    }

    /// <summary>
    /// Resets the solver.
    /// </summary>
    private void ResetIKSolver()
    {
        if (actualAvatarVRIK != null)
        {
            IKSolver solver = actualAvatarVRIK.GetIKSolver();
            IKSolverVR solverVR = solver as IKSolverVR;
            solverVR.Reset();
            Debug.Log("Local reset called");
        }
    }

    /// <summary>
    /// Initialize the VR rig for this avatar.
    /// If there is no avatar then just setup the basic structure.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    private void VRSetup(Vector3 position, Quaternion rotation)
    {
        containerObject = new GameObject("VRContainer");
        containerObject.transform.position = position;
        if (isDanceScene)
        {
            if (rotationPoint != null)
            {
                containerObject.transform.rotation = rotationPoint.rotation;
            }
            else
            { containerObject.transform.rotation = rotation; }
        }
        else
        {
            containerObject.transform.rotation = rotation;
            transform.SetParent(containerObject.transform, true);

        }


        //Current client owns this player
        //create camera rig and attach player model to it
        if (isDanceScene)
        {
            VRRigObject = Instantiate(VRRigPrefab, transform.position, transform.rotation);
        }
        else
        {
            VRRigObject = Instantiate(VRRigPrefab, rotationPoint.position, rotationPoint.rotation);
        }
        VRRigObject.transform.SetParent(containerObject.transform, false);
      VRRigObject.transform.localPosition = Vector3.zero;

        sdkManager = VRRigObject.GetComponentInChildren<VRTK_SDKManager>();
        multiVR = VRRigObject.GetComponentInChildren<MultiVRSetup>();

        VRTKSetup();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        ownColliders = transform.root.GetComponentsInChildren<Collider>();

        if (enableAvatar)
        {
            try
            {
                #region Avatar Setup
                Outfit outfit = AvatarManager.Instance.avatarset.listOfOutfits[PlayersData.Instance.PlayerData.Avatar].outfits[PlayersData.Instance.PlayerData.Outfit];

                if (outfit.prefabOrTextureOutfit == Outfit.PrefabOrTextureOutfit.Prefab)
                {
                    //ChangeAvatar(outfit.prefab, outfit);
                    ApplyAvatar(outfit.prefab, PlayersData.Instance.PlayerData.Avatar);
                }
                else
                {
                    ApplyAvatar(PlayersData.Instance.PlayerData.Avatar);
                 
                }
                #endregion
            }
            catch (System.Exception e)
            {
              //  Debug.LogError(e);

                local = true;
                ApplyAvatar(0);

            }
        }

        CapturePlayAreaTransform();
    }

    /// <summary>
    /// Set offset based on the BoneStructure.
    /// </summary>
    private void SetOffsets()
    {
        if (avatarOffsets != null && avatarOffsets.Count > 0)
        {
            VRIKApplier vrikApplier = actualAvatarVRIK.GetComponent<VRIKApplier>();
            switch (vrikApplier._boneStructure)
            {
                case VRIKApplier.BoneStructure.toon_kids:
                    Debug.Log("apply toon");
                    offsetIndex = 0;
                    break;
                case VRIKApplier.BoneStructure.normal:
                    Debug.Log("apply norm");
                    offsetIndex = 1;
                    break;
                default:
                    Debug.Log("apply def");
                    offsetIndex = 0;
                    break;
            }
        }
    }

    /// <summary>
    /// Set up the VRTK
    /// </summary>
    private void VRTKSetup()
    {
        leftController = Instantiate(VRControllerPrefab, containerObject.transform);

        rightController = Instantiate(VRControllerPrefab, containerObject.transform);

        leftController.name = "LeftController (Clone)";
        rightController.name = "RightController (Clone)";

        // Attach left hand
        MultiVRUtil.MakeReferral(multiVR.leftHandAlias.gameObject);
        multiVR.leftHandAlias.transform.SetParent(leftController.transform);

        // Attach right hand
        MultiVRUtil.MakeReferral(multiVR.rightHandAlias.gameObject);
        multiVR.rightHandAlias.transform.SetParent(rightController.transform);

        sdkManager.scriptAliasLeftController = leftController;
        sdkManager.scriptAliasRightController = rightController;

        //Get Teleport
        rightControllerTeleport = rightController.GetComponent<VRTK_BezierPointerRenderer>();
        leftControllerTeleport = leftController.GetComponent<VRTK_BezierPointerRenderer>();
    }

    /// <summary>
    /// Spawns in the new avatar and applies the VRIK
    /// </summary>
    /// <param name="index"></param>
    private void ApplyAvatar(int index)
    {
        this.indexActualAvatar = index;

        float scale = localScale;
        if (!local)
        {
            PlayersData.Instance.PlayerData.Avatar = indexActualAvatar;
            actualAvatarVRIK = Instantiate(AvatarManager.Instance.getAvatarWithoutHead(index), Vector3.zero, Quaternion.identity);
            scale = PlayersData.Instance.PlayerData.Scale;
          
        }
        else
        {
            //Instantiate actual avatar
            actualAvatarVRIK = Instantiate(avatars[index], Vector3.zero, Quaternion.identity);
        }

        SetOffsets();

        actualAvatarVRIK.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        actualAvatarVRIK.solver.spine.headTarget = transform;
        actualAvatarVRIK.transform.SetParent(containerObject.transform, false);
        actualAvatarVRIK.transform.rotation = actualAvatarVRIK.transform.rotation * Quaternion.Inverse(containerObject.transform.rotation);
        actualAvatarVRIK.solver.leftArm.target = sdkManager.scriptAliasLeftController.transform;
        actualAvatarVRIK.solver.rightArm.target = sdkManager.scriptAliasRightController.transform;
        // Set actual avatar transforms.
        actualAvatarVRIK.solver.spine.headTarget = multiVR.headAlias.transform.GetChild(offsetIndex);
        actualAvatarVRIK.solver.leftArm.target = multiVR.leftHandAlias.transform.GetChild(offsetIndex);
        actualAvatarVRIK.solver.rightArm.target = multiVR.rightHandAlias.transform.GetChild(offsetIndex);

        this.transform.SetParent(actualAvatarVRIK.solver.spine.headTarget, false);

      
    }

    private void ApplyAvatar(GameObject avatarObject, int index)
    {
        this.indexActualAvatar = index;

        float scale = localScale;
        if (!local)
        {
            PlayersData.Instance.PlayerData.Avatar = indexActualAvatar;
            actualAvatarVRIK = Instantiate(avatarObject.GetComponent<VRIK>(), Vector3.zero, Quaternion.identity);
            scale = PlayersData.Instance.PlayerData.Scale;
           
        }
        else
        {
            //Instantiate actual avatar
            actualAvatarVRIK = Instantiate(avatarObject.GetComponent<VRIK>(), Vector3.zero, Quaternion.identity);
        }

        SetOffsets();

        actualAvatarVRIK.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        actualAvatarVRIK.solver.spine.headTarget = transform;
        actualAvatarVRIK.transform.SetParent(containerObject.transform, false);
        actualAvatarVRIK.transform.rotation = actualAvatarVRIK.transform.rotation * Quaternion.Inverse(containerObject.transform.rotation);
        actualAvatarVRIK.solver.leftArm.target = sdkManager.scriptAliasLeftController.transform;
        actualAvatarVRIK.solver.rightArm.target = sdkManager.scriptAliasRightController.transform;
        // Set actual avatar transforms.
        actualAvatarVRIK.solver.spine.headTarget = multiVR.headAlias.transform.GetChild(offsetIndex);
        actualAvatarVRIK.solver.leftArm.target = multiVR.leftHandAlias.transform.GetChild(offsetIndex);
        actualAvatarVRIK.solver.rightArm.target = multiVR.rightHandAlias.transform.GetChild(offsetIndex);

        this.transform.SetParent(actualAvatarVRIK.solver.spine.headTarget, false);

    }

    private void ApplyAvatar(GameObject avatarObject)
    {
        float scale = localScale;
        if (!local)
        {
            actualAvatarVRIK = Instantiate(avatarObject.GetComponent<VRIK>(), Vector3.zero, Quaternion.identity);
            scale = PlayersData.Instance.PlayerData.Scale;
           
        }
        else
        {
            //Instantiate actual avatar
            actualAvatarVRIK = Instantiate(gameObject.GetComponent<VRIK>(), Vector3.zero, Quaternion.identity);
        }

        SetOffsets();

        actualAvatarVRIK.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        actualAvatarVRIK.solver.spine.headTarget = transform;
        actualAvatarVRIK.transform.SetParent(containerObject.transform, false);
        actualAvatarVRIK.transform.rotation = actualAvatarVRIK.transform.rotation * Quaternion.Inverse(containerObject.transform.rotation);
        actualAvatarVRIK.solver.leftArm.target = sdkManager.scriptAliasLeftController.transform;
        actualAvatarVRIK.solver.rightArm.target = sdkManager.scriptAliasRightController.transform;
        // Set actual avatar transforms.
        actualAvatarVRIK.solver.spine.headTarget = multiVR.headAlias.transform.GetChild(offsetIndex);
        actualAvatarVRIK.solver.leftArm.target = multiVR.leftHandAlias.transform.GetChild(offsetIndex);
        actualAvatarVRIK.solver.rightArm.target = multiVR.rightHandAlias.transform.GetChild(offsetIndex);

        this.transform.SetParent(actualAvatarVRIK.solver.spine.headTarget, false);

       
    }

    /// <summary>
    /// Set the container objects position according to the playarea.
    /// </summary>
    protected virtual void LateUpdate()
    {
        if (playArea != null)
        {
            if (!lastPosition.Equals(playArea.position) || !lastRotation.Equals(playArea.rotation))
            {
                CapturePlayAreaTransform();
                if (haveIStarted && container != null)
                {
                    container.position = lastPosition;
                    container.rotation = lastRotation;
                    playArea.position = container.position;
                    playArea.rotation = container.rotation;
                }
            }
        }
    }

    /// <summary>
    /// Get's the current transform of the PlayerArea
    /// </summary>
    private void CapturePlayAreaTransform()
    {
        Transform target = multiVR.playAreaAlias.transform;
        lastPosition = target.position;
        lastRotation = target.rotation;
    }

    /// <summary>
    /// Changes the current avatar with the new one at the given index.
    /// </summary>
    /// <param name="indexNewAvatar">Index of the new avatar.</param>
    public void ChangeAvatar(int indexNewAvatar)
    {
        Destroy(actualAvatarVRIK.gameObject);
        ApplyAvatar(indexNewAvatar);
        if (!local)
        {
            AvatarManager.Instance.AvatarChanged(indexNewAvatar);
        }
        avatarChanged.Invoke();
    }

    /// <summary>
    /// Changes the current avatar with the new one at the given index.
    /// </summary>
    /// <param name="indexNewAvatar">Index of the new avatar.</param>
    public void ChangeAvatar(GameObject newAvatarObject, Outfit outfit)
    {
        Destroy(actualAvatarVRIK.gameObject);
        ApplyAvatar(newAvatarObject);

        if (!local)
        {
            AvatarManager.Instance.AvatarChanged(outfit);
        }

        avatarChanged.Invoke();
    }

    public void SetLocalScale(float scale)
    {
        localScale = scale;
    }
}