using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ScaleScreen : MonoBehaviour {

    [SerializeField]
    [Tooltip("Scaling script for the Avatar.")]
    private ScaleAvatar scaleAvatar;
    [SerializeField]
    private Animator animator;
    /*
    [SerializeField]
    [Tooltip("The main text field to be changed.")]
    private Text panelText;
    [SerializeField]
    [Tooltip("Loading bar of the panel.")]
    private Slider loadingBar;
    [SerializeField]
    [Tooltip("Object to show/hide.")]
    private List<GameObject> objectsToShowOrHide;*/

    [SerializeField]
    [Tooltip("Time after scaling until you can press the button again.")]
    private float waitUntilReset;
    /*[SerializeField]
    [Tooltip("Colors of the background.")]
    private List<Color> buttonBackgroundColors;*/
    [SerializeField]
    [Tooltip("Sound used for each scaling step.")]
    private AudioClip scaleStep;
    [SerializeField]
    [Tooltip("Sound used when the scaling is done.")]
    private AudioClip scaled;
    /*
    [SerializeField]
    [Tooltip("Text to appear when Done")]
    private string DoneText;
    [SerializeField]
    [Tooltip("Text to appear when reset.")]
    private string touchText;*/

    private bool reset;
    private float delay;
    private AudioSource audioSource;
    private bool changedObject;

	/// <summary>
    /// Add listeners and set the delay.
    /// </summary>
	void Start () {
        //scaleAvatar.scalingBegin.AddListener(ChangeBackground);
        scaleAvatar.heightCalcDone.AddListener(ChangeLoadingBar);
        scaleAvatar.scalingDone.AddListener(DoneScaling);
        delay = waitUntilReset;
        this.audioSource = GetComponent<AudioSource>();
        //panelText.text = touchText;

    }
	
	/// <summary>
    /// Reset's the scaling process
    /// </summary>
	void Update () {
        if (reset)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                reset = false;
                delay = waitUntilReset;
                GetComponent<BoxCollider>().enabled = true;
            }
        }
	}

    /// <summary>
    /// Updates the loading bar when the event heightCalcDone is getting invoked.
    /// </summary>
    private void ChangeLoadingBar()
    {
        PlayClip(scaleStep);
        //if (loadingBar.value == loadingBar.minValue)
        //{
        //    //ChangeBackground();
        //    GetComponent<BoxCollider>().enabled = false;
        //    loadingBar.gameObject.SetActive(true);
        //    panelText.text = "Scaling...";
        //}
        //loadingBar.value++;
    }

    /// <summary>
    /// Done scaling is getting called when the scalingDone event has been invoked.
    /// This sets the canvas to the next state.
    /// </summary>
    private void DoneScaling()
    {
        animator.SetTrigger("Rescale");
        PlayClip(scaled);
        //loadingBar.gameObject.SetActive(false);
        //panelText.text = DoneText;
        //foreach (GameObject objectToShowOrHide in objectsToShowOrHide)
        //{
        //    if (objectToShowOrHide != null && changedObject == false)
        //    {
        //        objectToShowOrHide.SetActive(!objectToShowOrHide.GetActive());
        //    }
        //}
        changedObject = true;
        reset = true;
    }

    /// <summary>
    /// Changes the emission of the button.
    /// </summary>
   
    /// <summary>
    /// Plays the audio clip.
    /// </summary>
    /// <param name="audioClip"></param>
    private void PlayClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
