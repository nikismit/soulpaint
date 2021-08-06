using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManagerScene1 : MonoBehaviour
{
    float inititalFogDensity;
    [SerializeField] float finalDensity;
    [SerializeField] float fogStep =.001f;
    [SerializeField] AudioSource tutorialAudioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] BodyScan bodyScan;
    [SerializeField] VRAvatarController avatarController;
    [SerializeField] GameObject palette, startCircle;
   // [SerializeField] HandSelector handSelector;
    int clip;
    float fadeDuration = 2;

    // Start is called before the first frame update
    void Start()
    {
        palette.SetActive(false);
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
                startCircle.SetActive(false);
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

                break;
            case Gamestate.Embody:

                break;
            case Gamestate.Dance:
                break;
            case Gamestate.PostDance:
                break;
            default:
                break;
        }



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
