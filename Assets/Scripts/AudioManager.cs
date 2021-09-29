using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] firstSceneClipsDutch, firstSceneClipsEnglish;
    AudioClip[] usedClips;
    SceneManagerScene1 sceneManager;
    [SerializeField]
    bool isDutch = true;
   int  currentClip;
    bool waitForPlay;
    // Start is called before the first frame update
    void Start()
    {
        if (isDutch)
        {
            usedClips = firstSceneClipsDutch;
        }
        else
        {
            usedClips = firstSceneClipsEnglish;
        }
        sceneManager = GetComponent<SceneManagerScene1>();
    }

    public void PlayClip()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(usedClips[currentClip]);
            currentClip++;
            waitForPlay = true;
        }
        else
        {
            Invoke("PlayClip", 1f);
        }
    }
  

}
