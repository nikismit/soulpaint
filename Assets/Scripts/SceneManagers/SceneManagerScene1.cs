using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.SceneManagement;
using VRTK;
using UnityEngine.XR;
using UnityEngine.Playables;

public class SceneManagerScene1 : MonoBehaviour
{
    float inititalFogDensity;
    [SerializeField] float finalDensity;
    [SerializeField] float fogStep =.001f;
    [SerializeField] AudioSource tutorialAudioSource, voiceOver;
    AudioManager audioManager;
 //   [SerializeField] AudioClip[] audioClips;
    [SerializeField] BodyScan bodyScan;
    [SerializeField] VRAvatarController avatarController;
    [SerializeField] GameObject palette, timerObject, startSilhoeutte, Timeline, embodyGlow;
    [SerializeField] private float paintingTime = 180, scanStep =.02f;
    [SerializeField] GameObject finalPuppet, refPuppet;
    [SerializeField] SkinnedMeshRenderer scanMaterial, scanHeadMaterial;
    [SerializeField] Material NotDissolve;
    [SerializeField] Transform spawnPointExtraPuppets;

    float scanVal;
    bool embodied;
    PlayableDirector playableDirector;
    private VRTK_ControllerEvents rightControllerAlias = null, leftControllerAlias = null;
    private VRTK_ControllerReference vrtkRefRight;
    // [SerializeField] HandSelector handSelector;
    int clip;
    float fadeDuration = 2;
    [SerializeField]
    bool haptic;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogDensity = .75f;
        //   StartCoroutine(StartPaintingTime());
     palette.SetActive(false);
       timerObject.SetActive(false);
        scanMaterial.material.SetFloat("_Progress", 0);
        scanHeadMaterial.material.SetFloat("_Progress", 0);
        audioManager = GetComponent<AudioManager>();
        embodyGlow.SetActive(false);
        playableDirector = Timeline.GetComponent<PlayableDirector>();
       // Timeline.SetActive(false);
        //  GameManager.Instance.SetNewGamestate(Gamestate.Meditation);
        Invoke("SetControllerReferences", 2f);
    }

    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
        leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();
        vrtkRefRight = VRTK_DeviceFinder.GetControllerReferenceRightHand();
    }
    private void OnEnable()
    {
        GameManager.gamestateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.gamestateChanged -= OnGameStateChanged;
    }
    public void ChangeState(int i)
    {
        Gamestate gamestate = (Gamestate)i;
       
        GameManager.Instance.SetNewGamestate(gamestate);
    }
    private void OnGameStateChanged(Gamestate newGameState)
    {
        switch (newGameState)
        {
            case Gamestate.WaitforStart:
              
                break;
            case Gamestate.Meditation:
              
                startSilhoeutte.SetActive(false);
                //  startCircle.SetActive(false);
                //   paintBucket.SetActive(false);
                StartCoroutine(BodyScan());
                finalPuppet.GetComponent<ReachToDestination>().goToDestination = true;
               
                break;
            case Gamestate.Painting:
            
              
                timerObject.SetActive(true);
                StartCoroutine(StartPaintingTime());
                StartFogFade();

                break;
            case Gamestate.ReadyforEmbody:
                audioManager.PlayClip();
                palette.SetActive(false);
                embodyGlow.SetActive(true);
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
        //if (rightControllerAlias != null && !embodied)
        //{
        //    if (Input.GetKeyDown(KeyCode.N) || (rightControllerAlias.buttonOnePressed))
        //    {

        //        GameManager.Instance.SetNewGamestate(Gamestate.Embody);
        //        embodied = true;

        //    }
    //    }




    }
    private void  SetupAvatar()
    {
        EyeBlinder.Instance.FadeIn();
        scanHeadMaterial.material = NotDissolve;
        scanMaterial.material = NotDissolve;
     
        Collider[] colliders = finalPuppet.GetComponentsInChildren<Collider>();
        foreach (Collider cl in colliders)
        {
            cl.enabled = false;
        }
    
        GameManager.Instance.finalMimicPuppet = Instantiate(finalPuppet);
        GameManager.Instance.finalMimicPuppet.transform.position = spawnPointExtraPuppets.position;

        DontDestroyOnLoad(GameManager.Instance.finalMimicPuppet);
       // GameManager.Instance.finalMimicPuppet.AddComponent<MimicPuppet>();


        VRIK vrik = finalPuppet.AddComponent<VRIK>();
        vrik.enabled = false;
        VRIKApplier vRIKApplier = finalPuppet.AddComponent<VRIKApplier>();
       // finalPuppet.AddComponent<HandGrabAnimation>();
        //Animator anim = finalPuppet.AddComponent<Animator>();
       // anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Prefabs/soulPaint", typeof(RuntimeAnimatorController));
        vRIKApplier._boneStructure = VRIKApplier.BoneStructure.spNew;
        vRIKApplier.applyVRIKComponents();
        ResetBones();
        DontDestroyOnLoad(finalPuppet);

     
        GameManager.Instance.finalPuppet = finalPuppet;
        GameManager.Instance.finalPuppet.transform.position = spawnPointExtraPuppets.position;
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
     
        Invoke("StartEmbody", 1f);
    }
  private void StartEmbody()
    {
        if (GameManager.Instance.getCurrentGameState() != Gamestate.ReadyforEmbody)
        { GameManager.Instance.SetNewGamestate(Gamestate.ReadyforEmbody); }

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

    //    GameManager.Instance.SetNewGamestate(Gamestate.Painting);
        yield return null; 
    }
public void TriggerAudioFeedback(int i)
    {
        if (!tutorialAudioSource.isPlaying)
        {

         //   tutorialAudioSource.PlayOneShot(audioClips[i]);
            avatarController.actualAvatarVRIK.GetComponent<Paint>().initialPaint = true;
       
        }

        else
        {
            StartCoroutine(WaitForAudio(i));
          
        }

    
    }

    public void SteppedIntoAvatar(int i)
    {
        switch (i)
        {case 1:

             //   OVRInput.SetControllerVibration(1f, 1f);
                GameManager.Instance.SetNewGamestate(Gamestate.WaitforStart);
      
                playableDirector.Play();
                break;
            case 2:

                //OVRInput.SetControllerVibration(1f, 1f);
                GameManager.Instance.SetNewGamestate(Gamestate.Embody);
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForAudio(int i)
    {
        yield return new WaitUntil(() => !tutorialAudioSource.isPlaying);
    //    tutorialAudioSource.PlayOneShot(audioClips[i]);
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
