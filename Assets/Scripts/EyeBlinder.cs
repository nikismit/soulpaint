using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script let’s you fade in and/or fade out images. 
/// This is currently used as an eyeblinder for the seeker in Hide and Freeze and also for the vignette of the Warp Teleport.
/// This script can be called from anywhere using EyeBlinder.Instance.
/// </summary>
public class EyeBlinder : MonoBehaviour {

    /// <summary>
    /// Static instance that can be called from anywhere.
    /// </summary>
    public static EyeBlinder Instance;

    [SerializeField]
    [Tooltip("Blindfold color.")]
    private Color blindfold;
    [SerializeField]
    [Tooltip("Image used for blinding the eyes.")]
    private Image image;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Unblind());
    }
    
    private IEnumerator Unblind()
    {
        yield return new WaitForSeconds(1);
        FadeOut();
    }

    /// <summary>
    /// Gives the image the given material and enables the EyeBlinder.
    /// </summary>
    /// <param name="material"></param>
    public void SetMaterialAndEnable(Material material)
    {
        image.material = material;
        Toggle(true);
    }

    public void PlayerLeftCheatZone()
    {
        Color c = Color.black;
        c.a =  0;
        image.color = c;
        image.material = null;

    }

    /// <summary>
    /// Toggle the Eyeblinder based on the value given.
    /// </summary>
    /// <param name="toggle"></param>
    public void Toggle(bool toggle)
    {
        Color c = Color.black;
        c.a = toggle ? 1 : 0;
        image.color = c;
    }



    /// <summary>
    /// Enables the Blindfold Eyeblinder
    /// </summary>
    public void blind()
    {
        image.color = blindfold;
        Toggle(true);
        image.material = null;
    }

    [ContextMenu("Fade in")]
    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }

    [ContextMenu("Fade out")]
    public void FadeOut()
    {
        StartCoroutine(Fade(false));
    }

    public IEnumerator Fade(bool visible)
    {
        Color c = Color.black;
        c.a = visible ? 0 : 1;
        float target = visible ? 1f : 0f;

        while (c.a != target)
        {
            c.a = Mathf.MoveTowards(c.a, target, Time.deltaTime * 1.5f);
            image.color = c;
            yield return null;
        }
    }
}
