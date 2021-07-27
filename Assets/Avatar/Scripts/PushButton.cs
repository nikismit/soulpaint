
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    [Header("Button")]
    [SerializeField]
    [Tooltip("Button to be pressed down.")]
    private GameObject button;
    [SerializeField]
    [Tooltip("Delay between touches.")]
    private float touchDelay;

    [Header("Appearance")]
    [Tooltip("Makes it look like the button is being pressed when pressed.")]
    [SerializeField]
    private bool pressedAnimation;
    [SerializeField]
    [Tooltip("Scaling factor of the button. This determines how much the button will be pressed down.")]
    private float scaleFactorButton = 0.66f;
    [SerializeField]
    [Tooltip("Two materials to change between.")]
    private List<Material> materials;
    [SerializeField]
    private AudioClip pressClip;

    [Header("Ignore Objects")]
    [SerializeField]
    [Tooltip("Ignore all objects trying to interact with this button that have one of these tags.")]
    private List<string> ignoreObjectsWithTags;
    [SerializeField]
    [Tooltip("Only objects with this tag will be able to push the button.")]
    private List<string> specifiedObjectTags;

    [SerializeField]
    private bool masterOnly = false;

    [Header("Events")]
    public UnityEvent buttonPressEvent;

    private float delay;
    private float onEnterDelay;
    private Vector3 oldScale;
    private bool pressed;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    /// <summary>
    /// Set original scale.
    /// </summary>
    private void Start()
    {
        if (button != null)
        {
            oldScale = new Vector3(button.transform.localScale.x, button.transform.localScale.y, button.transform.localScale.z);
            meshRenderer = button.GetComponent<MeshRenderer>();
        }

        if (GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Update timers.
    /// </summary>
    private void Update()
    {
        UpdateDelay(ref delay);
        UpdateDelay(ref onEnterDelay);
    }

    /// <summary>
    /// Works as a timer for the parameter that gets passed on.
    /// </summary>
    /// <param name="refDelay">Current time of the timer.</param>
    private void UpdateDelay(ref float refDelay)
    {
        if (refDelay > 0)
        {
            refDelay -= Time.deltaTime;
        }

        if (refDelay < 0)
        {
            refDelay = 0;
        }
    }

    /// <summary>
    /// Invokes the button press event if it wasn't pressed already.
    /// Also changes the scale of the button in the Y value
    /// </summary>
    /// <param name="other">Collider of the object that touches it.</param>
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (!pressed)
        {
            if (onEnterDelay == 0)
            {
                if ((ignoreObjectsWithTags == null) || (!ignoreObjectsWithTags.Contains(other.tag)))
                {
                    if ((specifiedObjectTags == null) || (specifiedObjectTags.Contains(other.tag)))
                    
                    
                        {
                            buttonPressEvent.Invoke();
                            pressed = true;

                            if (pressedAnimation)
                            {
                                /*Vector3 scale = button.transform.localScale;
                                scale.y = scale.y * scaleFactorButton;
                                button.transform.localScale = scale;*/

                                button.transform.localScale *= scaleFactorButton;
                            }

                            if (audioSource && pressClip)
                            {
                                audioSource.PlayOneShot(pressClip);
                            }

                            onEnterDelay = touchDelay;
                            ChangeMaterial(1);
                        }
                    
                }
            }
        }
    }

    /// <summary>
    /// Changes button back to normal if the delay timer has reached 0
    /// </summary>
    /// <param name="other">Collider of the object that touches it.</param>
    private void OnTriggerExit(Collider other)
    {
        if (delay == 0)
        {
            if (pressedAnimation)
            {
                button.transform.localScale = oldScale;
            }
            delay = touchDelay;
            pressed = false;
            ChangeMaterial(0);
        }
    }

    /// <summary>
    /// Changes the material of the button
    /// </summary>
    /// <param name="index">Index of the color from the "materials" list.</param>
    private void ChangeMaterial(int index)
    {
        if (materials != null && materials.Count > 0)
        {
            meshRenderer.material = materials[index];
        }
    }
}
