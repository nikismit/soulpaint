using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepIntoAvatar : MonoBehaviour
{
    [SerializeField]
    Gamestate stateToTriggerIn;
    [SerializeField]
    bool isPaintingScene;
    [SerializeField]
    SceneManagerScene1 sceneManager1;
    [SerializeField]
    SceneManager2 sceneManager2;
    [SerializeField]
   int StateToSend;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera" && GameManager.Instance.getCurrentGameState() == stateToTriggerIn)
        {
            if (isPaintingScene)
            {
                sceneManager1.SteppedIntoAvatar(StateToSend);
            }
            else
            {
                //do something to exit.
            }

        }
    }
}
