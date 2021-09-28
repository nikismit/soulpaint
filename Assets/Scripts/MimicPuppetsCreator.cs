using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using System;

public class MimicPuppetsCreator : MonoBehaviour
{

    GameObject puppetToCreate;
    Transform[] transformSetup;
    [SerializeField] VRAvatarController vrsetup;
    [SerializeField] MimicPuppet.MovementType myMovSet;
    public List<GameObject> actualPuppets = new List<GameObject>(); 
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            puppetToCreate = GameManager.Instance.finalMimicPuppet;

            transformSetup = GetComponentsInChildren<Transform>();
        }
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

                break;
            case Gamestate.Meditation:
      

                break;
            case Gamestate.Painting:
            

                break;
            case Gamestate.Embody:
             

                break;
            case Gamestate.Dance:

                    Invoke("SetupPuppets", .5f);
            
            break;
            case Gamestate.PostDance:
                break;
            default:
                break;
        }



    }

    private void SetupPuppets()
    {
        player = vrsetup.transform.root.GetComponentInChildren<VRIK>().gameObject;
        foreach (Transform tr in transformSetup)
        {
            if (tr.childCount == 0)
            {
              if (tr.localScale.x == 1)
                {
                    float x = GameManager.Instance.savedScale;
                    tr.localScale = new Vector3(x, x, x);
                }
                GameObject go = Instantiate(puppetToCreate);
                MimicPuppet mp = go.AddComponent<MimicPuppet>();
                mp.player = player;
                mp.myMovType = myMovSet;
                go.transform.position = tr.position;
                go.transform.eulerAngles = tr.eulerAngles;
                go.transform.localScale = tr.localScale;
                //     Animator anim =  go.AddComponent<Animator>();
                //      anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Prefabs/ScaleAnim", typeof(RuntimeAnimatorController));
                actualPuppets.Add(go);
            }
          //  go.AddComponent<ParticleScaler>();
        }
        player.GetComponent<MimicSender>().stopMoving = false;
    }

}
