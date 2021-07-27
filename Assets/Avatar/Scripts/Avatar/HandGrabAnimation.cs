using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

using VRTK;

/// <summary>
/// This script reads out the trigger inputs of your controllers and puts the normalized value to animation time.
/// So the animation will be as far as you have squeezed your trigger.
/// </summary>
[RequireComponent(typeof(Animator))]
public class HandGrabAnimation : MonoBehaviour
{


    private Animator animator;

    private float leftIndexTrigger;
    private float rightIndexTrigger;

    private float left;
    private float right;

    private VRTK_ControllerEvents leftControllerAlias = null;
    private VRTK_ControllerEvents rightControllerAlias = null;

    /// <summary>
    /// Set basic values.
    /// </summary>
    void Start()
    {
     
        animator = GetComponent<Animator>();
        if (animator != null)
        {
          
            
                Invoke("SetControllerReferences", 1f);

            
        }
    }

    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
        leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();
    }

    /// <summary>
    /// Play animation based on changed input values.
    /// </summary>
    void Update()
    {
        if (animator != null)
        {
      
            {
                if (leftControllerAlias != null)
                {
                    left = leftControllerAlias.GetTriggerAxis();
                }
                if (rightControllerAlias != null)
                {
                    right = rightControllerAlias.GetTriggerAxis();
                }
            }

            #region Play animation
            if (left != leftIndexTrigger)
            {
                leftIndexTrigger = left;
                PlayAnimation("Left Hand", 0, leftIndexTrigger);
            }

            if (!animator.GetBool("holdGun"))
            {
                if (right != rightIndexTrigger)
                {
                    rightIndexTrigger = right;
                    PlayAnimation("Right Hand", 1, rightIndexTrigger);
                }
            }
            else
            {
                PlayAnimation("holdGun", 1, 1);
            }
            #endregion
        }
    }

    /// <summary>
    /// Play the animation
    /// </summary>
    /// <param name="animationName">Name of the animation.</param>
    /// <param name="layer">Layer of the animator on which to find the animation.</param>
    /// <param name="value">Value of the normalized time.</param>
    private void PlayAnimation(string animationName, int layer, float value)
    {
        animator.Play(animationName, layer, value);
    }


}
