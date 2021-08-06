using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using UnityEngine.SceneManagement;


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
    [SerializeField] private float paintingTime = 180;
    [SerializeField] GameObject finalPuppet;

   // [SerializeField] HandSelector handSelector;
    int clip;
    float fadeDuration = 2;

    // Start is called before the first frame update
    void Start()
    {
     //   StartCoroutine(StartPaintingTime());
        palette.SetActive(false);
       timerObject.SetActive(false);

 
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
                tutorialAudioSource.PlayOneShot(audioClips[0]);
                break;
            case Gamestate.Meditation:

                tutorialAudioSource.gameObject.GetComponent<ReachToDestination>().goToDestination = true;
                startCircle.SetActive(false);
                paintBucket.SetActive(false);
                if (!tutorialAudioSource.isPlaying)
                {

                    tutorialAudioSource.PlayOneShot(audioClips[2]);
                    StartCoroutine(StartFogFade());
                    bodyScan.StartScan();
                    //                    RenderSettings.fog = false;

                }
                else
                {
                    StartCoroutine(WaitForAudio(2));
               
                }
               
                break;
            case Gamestate.Painting:
                palette.SetActive(true);
                timerObject.SetActive(true);
             StartCoroutine(StartPaintingTime());

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
   private void  SetupAvatar()
    {
        EyeBlinder.Instance.FadeIn();
        Collider[] colliders = finalPuppet.GetComponentsInChildren<Collider>();
        foreach (Collider cl in colliders)
        {
            cl.enabled = false;
        }
        VRIK vrik = finalPuppet.AddComponent<VRIK>();
        VRIKApplier vRIKApplier = finalPuppet.AddComponent<VRIKApplier>();
        vRIKApplier._boneStructure = VRIKApplier.BoneStructure.soulPaint;
        vRIKApplier.applyVRIKComponents();
        DontDestroyOnLoad(finalPuppet);
      //  finalPuppet.AddComponent<MimicSender>();
        GameManager.Instance.finalPuppet = finalPuppet;
 
        Invoke("ChangeScene", 2f);

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
            bodyScan.StartScan();
        }
    }
}
