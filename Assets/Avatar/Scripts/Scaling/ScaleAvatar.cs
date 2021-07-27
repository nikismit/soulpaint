
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ScaleAvatar : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Avatar Controller that is stored on the player.")]
    private VRAvatarController avatarController;
    [SerializeField]
    [Tooltip("Amount of times to calculate the medians.")]
    private int timesToScale;
    [SerializeField]
    [Tooltip("Button press sound.")]
    private AudioClip buttonPress;
    [SerializeField]
    [Tooltip("Layer to Hit.")]
    private LayerMask layerToHit;
    [SerializeField]
    [Tooltip("Minimum height.")]
    private float minimumHeight;
    [SerializeField]
    [Tooltip("Change scaling method")]
    public bool scaleTypeisAvatar;
    [SerializeField]
  


    private AudioSource audio;
    private GameObject eye;
    private List<float> scales;
    private GameObject avatar;
    private bool scaling;
    public float scaleVar;


    private float teenScaleModifier = 0.65677131222f;
    public float AVATAR_EYE_HEIGHT;
    private bool scaled;

    public UnityEvent heightCalcDone = new UnityEvent();
    public UnityEvent scalingDone = new UnityEvent();

    [SerializeField]
    private bool scaleOnStart = false;
    [SerializeField]
    private bool rescale = false;
    private bool quietScale = true;
    public bool scaleAvatar = false;
    public bool scalePlayer = false;
    public float resetScale;
    private bool pressed;


    private void Start()
    {
        //Sets default scale variable

        
      
  
        if (scaleOnStart)
        {
            StartCoroutine(WaitForScale());
        }
    }

    void Update()
    {
        // to be reviewed, to not do it on update but call the function directly
        if (scaleAvatar)
        {

            StartCoroutine(WaitForScale());
          
        }

        if (scalePlayer)
        {

            ResetAvatarScale();
            ScalePlayer();
            //Debug.Log("StartedScalingPlayerToAvatar");
        }



    }

    private IEnumerator WaitForScale()
    {
        yield return new WaitForSeconds(10f);
        SetupQuiet();
       // print("Scaling initiated");
    }


    // Update is called once per frame


    /// <summary>
    /// Begin scaling when a collider has entered the trigger.
    /// </summary>
    /// <param name="other">Collider of the object that touched this.</param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("HandController"))
        {
            if (rescale)
            {
                Setup();
            }
        }
    }





    /// <summary>
    /// Setup variables that are needed for the calculation.
    /// After the setup has been done start the calculation.
    /// </summary>
    private void Setup()
    {
        try
        {
            if (!scaling)
            {

                quietScale = false;
                //audio.clip = buttonPress;
                //audio.Play();
                scaleAvatar = false;
                scales = new List<float>();
                if (avatarController != null)
                {

                 //   ResetPlayerRigScale();
                    scaling = true;

                    avatar = avatarController.actualAvatarVRIK.gameObject;
                    StartCoroutine(StartCalculation());
                }
            }
        }
        catch (System.Exception)
        {
            scaling = false;
        }
    }

    /// <summary>
    /// Scale the player but without all the noise
    /// </summary>
    private void SetupQuiet()
    {
        try
        {
            if (!scaling)
            {
                quietScale = true;
                scaleAvatar = false;
                scales = new List<float>();
                if (avatarController != null)
                {

                  //  ResetPlayerRigScale();
                    scaling = true;
                    avatar = avatarController.actualAvatarVRIK.gameObject;
                    StartCoroutine(StartCalculation());

                }
            }
        }
        catch (System.Exception)
        {
            scaling = false;

        }
    }

    /// <summary>
    /// Calls the Calculate function for the amount of times that has been given to the variable "timesToScale".
    /// </summary>
    /// <returns>Delay of 0.3 seconds.</returns>
    private IEnumerator StartCalculation()
    {
        yield return new WaitForSeconds(0.4f);

        int time = timesToScale;
        for (int x = 0; x < time; x++)
        {
            //scales.Add(Calculate(avatarController.gameObject));
            scales.Add(Calculate(Camera.main.gameObject));
            if (!quietScale)
            {

                heightCalcDone.Invoke();
            }
            yield return new WaitForSeconds(0.3f);
        }
        ApplyScale(CalculateMedian());
    }

    /// <summary>
    /// Calculates the player's height.
    /// </summary>
    /// <param name="heightObject"></param>
    /// <returns>Calculated height.</returns>
    private float Calculate(GameObject heightObject)
    {
        Ray ray = new Ray(heightObject.transform.position, Vector3.down);
        RaycastHit hit;
        float height = 1;

        if (Physics.Raycast(ray, out hit, 5, layerToHit))
        {
            height = hit.distance;
        }

        return height;
    }


    private float CalculatePlayerEyeHeight()
    {
        GameObject cameraObject = Camera.main.gameObject;
        Ray ray = new Ray(cameraObject.transform.position, Vector3.down);
        RaycastHit hit;
        float height = 1;

        if (Physics.Raycast(ray, out hit, 5, layerToHit))
        {
            height = hit.distance;
        }
        return height;
    }
    /// <summary>
    /// Calculates the median in the scales list.
    /// </summary>
    /// <returns>The median height.</returns>
    private float CalculateMedian()
    {
        float median;
        int scalesCount = scales.Count;
        scales.Sort();
        foreach (float item in scales)
        {
            Debug.Log("median: " + item);
        }
        // Even amount of elements
        if (scales.Count % 2 == 0)
        {
            median = 0.5f * (scales[scalesCount / 2 - 1] + scales[scalesCount / 2]);
        }
        // Odd amount of elements
        else
        {
            median = scales[(scalesCount + 1) / 2 - 1];
        }
        return median;
    }

    /// <summary>
    /// Applies the new scale to the player.
    /// Also invokes the scalingDone event.
    /// </summary>
    /// <param name="scale">Median height.</param>
    private void ApplyScale(float scale)
    {
        //sizeType.currentSize = SizeType.size.relative;
        if (!quietScale)
        {
            scalingDone.Invoke();
            //Debug.Log("scaling is done and feedback invoked");
        }

  
            //Debug.Log("Applying teen scale modifier of " + teenScaleModifier);
            scale *= teenScaleModifier;
   
        //else
        //{
        //    Debug.Log("Applying kid scale modifier of " + kidScaleModifier);
        //    scale *= kidScaleModifier;
        //}

        if (scale < minimumHeight)
        {
            scale = minimumHeight;
        }
        avatar.transform.localScale = new Vector3(scale, scale, scale);

    //  AvatarManager.Instance.ScaleChanged(scale);
      
        scaling = false;
        pressed = false;
    }

    private void ScalePlayer()
    {
        //if (scaled)
        //{
        //    return;
        //}

        Transform camera = Camera.main.transform;
        //print($"Main cam is {camera.name}");
        //Transform playerContainer = GameObject.Find("[MultiVR Setup](Clone)").transform;
        //changing this to make sure that it doesn't look for objects in the scene
        Transform playerContainer = avatarController.multiVR.transform;
        //print($"player container is {playerContainer.name}");
        playerContainer.localScale = new Vector3(1, 1, 1);

        //Debug.Log("Camera check there is a camera: " + (Camera.main != null));

        float playerEyeHeight = CalculatePlayerEyeHeight();
       
        //Debug.Log("player eye height is: " + playerEyeHeight);

        if (AvatarManager.Instance.avatarset.listOfAvatars[avatarController.indexActualAvatar].GetComponent<AvatarType>().CurrentAvatarType == AvatarType.AvatarTypeEnum.Teen)
        {
            AVATAR_EYE_HEIGHT = 1.6f * scaleVar;
        }

        else
        {

            AVATAR_EYE_HEIGHT = 1.25f * scaleVar;
        }

        float newScale = AVATAR_EYE_HEIGHT / playerEyeHeight;
        if (newScale < 0.01)
        {newScale = 0.2f; }
        //Debug.Log("New Scale is: " + newScale);

        playerContainer.localScale = new Vector3(newScale, newScale, newScale);

      

      
        scalePlayer = false;


    }

    private void ResetAvatarScale()
    {
        resetScale = scaleVar;
        avatar = avatarController.actualAvatarVRIK.gameObject;
        avatar.transform.localScale = new Vector3(resetScale, resetScale, resetScale);
        AvatarManager.Instance.ScaleChanged(resetScale);
        PlayersData.Instance.PlayerData.Scale = resetScale;
        scaling = false;
    }
    private void ResetPlayerRigScale()
    {
        float newScale = 1;
        //Transform playerContainer = GameObject.Find("[MultiVR Setup](Clone)").transform;
        //changing this to make sure that it doesn't look for objects in the scene
        Transform playerContainer = avatarController.multiVR.transform;
        playerContainer.localScale = new Vector3(newScale, newScale, newScale);
        
    }


}
