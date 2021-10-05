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
    bool called;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera" && GameManager.Instance.getCurrentGameState() == stateToTriggerIn)
        {
            if (isPaintingScene  && !called)
            {
                sceneManager1.SteppedIntoAvatar(StateToSend);
                called = true;
            }
            else
            {
                if (!called)
                {
                    GameManager.Instance.SetNewGamestate(Gamestate.End);
                    called = true;
                }
                //do something to exit.
            }

        }
    }
}
