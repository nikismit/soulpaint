using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.SceneManagement;
using VRTK;


public class SceneManagerScene1 : MonoBehaviour
{
    float inititalFogDensity;
    [SerializeField] float finalDensity;
    [SerializeField] float fogStep =.001f;
    [SerializeField] AudioSource tutorialAudioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] BodyScan bodyScan;
    [SerializeField] VRAvatarController avatarController;
    [SerializeField] GameObject palette, paintBucket,startCircle, timerObject;
    [SerializeField] private float paintingTime = 180, scanStep =.02f;
    [SerializeField] GameObject finalPuppet, refPuppet;
    [SerializeField] SkinnedMeshRenderer scanMaterial, scanHeadMaterial;
    [SerializeField] Material NotDissolve;
    float scanVal;
    bool embodied;

    private VRTK_ControllerEvents rightControllerAlias = null;
    // [SerializeField] HandSelector handSelector;
    int clip;
    float fadeDuration = 2;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogDensity = .75f;
        //   StartCoroutine(StartPaintingTime());
        palette.SetActive(false);
       timerObject.SetActive(false);
        scanMaterial.material.SetFloat("_Progress", 0);
        scanHeadMaterial.material.SetFloat("_Progress", 0);
        GameManager.Instance.SetNewGamestate(Gamestate.Meditation);
        Invoke("SetControllerReferences", 2f);
    }

    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();

    }
    private void OnEnable()
    {
        GameManager.gamestateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.gamestateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(Gamestate newGameState)
    {
        switch (newGameState)
        {
            case Gamestate.WaitforStart:
              
                break;
            case Gamestate.Meditation:
                startCircle.SetActive(false);
                paintBucket.SetActive(false);
                StartCoroutine(BodyScan());
               
                break;
            case Gamestate.Painting:
                palette.SetActive(true);
                timerObject.SetActive(true);
                     StartCoroutine(StartPaintingTime());
                StartFogFade();

                break;
            case Gamestate.Embody:
                SetupAvatar();
                
                break;
            case Gamestate.Dance:
                break;
            case Gamestate.PostDance:
                break;
            default:
                break;
        }



    }
    private void Update()
    {
        if (rightControllerAlias != null && !embodied)
        {
            if (Input.GetKeyDown(KeyCode.N) || (rightControllerAlias.buttonOnePressed))
            {

                GameManager.Instance.SetNewGamestate(Gamestate.Embody);
                embodied = true;

            }
        }

    }
    private void  SetupAvatar()
    {
        scanHeadMaterial.material = NotDissolve;
        scanMaterial.material = NotDissolve;
        EyeBlinder.Instance.FadeIn();
        Collider[] colliders = finalPuppet.GetComponentsInChildren<Collider>();
        foreach (Collider cl in colliders)
        {
            cl.enabled = false;
        }
    
        GameManager.Instance.finalMimicPuppet = Instantiate(finalPuppet);
        DontDestroyOnLoad(GameManager.Instance.finalMimicPuppet);
       // GameManager.Instance.finalMimicPuppet.AddComponent<MimicPuppet>();


        VRIK vrik = finalPuppet.AddComponent<VRIK>();
        VRIKApplier vRIKApplier = finalPuppet.AddComponent<VRIKApplier>();
       // finalPuppet.AddComponent<HandGrabAnimation>();
        //Animator anim = finalPuppet.AddComponent<Animator>();
       // anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Prefabs/soulPaint", typeof(RuntimeAnimatorController));
        vRIKApplier._boneStructure = VRIKApplier.BoneStructure.spNew;
        vRIKApplier.applyVRIKComponents();
        ResetBones();
        DontDestroyOnLoad(finalPuppet);
   
 
        GameManager.Instance.finalPuppet = finalPuppet;
     
    //    GameManager.Instance.finalPuppet.AddComponent<MimicSender>();
        Invoke("ChangeScene", 2f);

    }

    private void ResetBones()
    {
        Transform[] refPuppetTR = refPuppet.GetComponent<VRIK>().references.GetTransforms();
        Transform[] puppetTr = finalPuppet.GetComponent<VRIK>().references.GetTransforms();
        for (int i = 0; i < puppetTr.Length; i++)
        {
            puppetTr[i].localEulerAngles = refPuppetTR[i].localEulerAngles;
            puppetTr[i].localPosition = refPuppetTR[i].localPosition;

        }

    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
    IEnumerator StartPaintingTime()
    {
        var timePassed = 0f;
        while (timePassed <= paintingTime)
        {
           
            var factor = (180/ paintingTime) * Time.deltaTime;
            timePassed += Time.deltaTime;
            timerObject.transform.Rotate(-Vector3.right * factor);
            yield return null;
        }
        GameManager.Instance.SetNewGamestate(Gamestate.Embody);
 
    }

    IEnumerator StartFogFade()
    {
        var timePassed = 0f;
        while (timePassed <= fadeDuration)
        {
            inititalFogDensity = RenderSettings.fogDensity;
            var factor = timePassed / fadeDuration;
            RenderSettings.fogDensity = Mathf.Lerp(inititalFogDensity, 0, factor);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator BodyScan()
    {
        while (scanVal < .69f)
        {
            yield return null;
            scanVal = scanVal + (scanStep * Time.deltaTime);
            scanMaterial.material.SetFloat("_Progress", scanVal);

        }
        scanMaterial.material.SetFloat("_Progress", 1);
        scanHeadMaterial.material.SetFloat("_Progress", 1);
        GameManager.Instance.SetNewGamestate(Gamestate.Painting);
        yield return null; 
    }
public void TriggerAudioFeedback(int i)
    {
        if (!tutorialAudioSource.isPlaying)
        {

            tutorialAudioSource.PlayOneShot(audioClips[i]);
            avatarController.actualAvatarVRIK.GetComponent<Paint>().initialPaint = true;
       
        }

        else
        {
            StartCoroutine(WaitForAudio(i));
          
        }

    
    }

    IEnumerator WaitForAudio(int i)
    {
        yield return new WaitUntil(() => !tutorialAudioSource.isPlaying);
        tutorialAudioSource.PlayOneShot(audioClips[i]);
        if (i == 1)
        {
            avatarController.actualAvatarVRIK.GetComponent<Paint>().initialPaint = true;
        }
        if (i == 2)
        {
            StartCoroutine(StartFogFade());
            //  RenderSettings.fog = false;
           // bodyScan.StartScan();
        }
    }
}
