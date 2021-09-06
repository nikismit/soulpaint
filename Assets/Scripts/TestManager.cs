using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using VRTK;
using UnityEngine.SceneManagement;


public class TestManager : MonoBehaviour
{

    private Animator animator;
    private VRTK_ControllerEvents leftControllerAlias = null;
    private VRTK_ControllerEvents rightControllerAlias = null;

    [SerializeField] private float Offset;


    void Start()
    {
        Invoke("SetControllerReferences", 1f);
    }

    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
        leftControllerAlias = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();
    }


    void Update()
    {
        
        if (rightControllerAlias.buttonOnePressed)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ParticleScaler[] AllPart = FindObjectsOfType<ParticleScaler>();

            for (int i = 0; i < AllPart.Length; i++)
            {
                AllPart[i].ScaleParticles(Offset);
            }
        }
    }
}
