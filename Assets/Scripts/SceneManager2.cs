using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRTK;
using UnityEngine.Playables;
using RootMotion.FinalIK;

public class SceneManager2 : MonoBehaviour
{

    [SerializeField]
    Transform underTheFloor;
    [SerializeField]
    VRAvatarController vrsetup;
    [SerializeField]
    List<GameObject> subScene = new List<GameObject>();
    [SerializeField]
    MimicPuppetsCreator mimicPuppetCreator;
    [SerializeField]
    GameObject mirror;
    int currentScene;
    TransformIdentifier[] subScene1, subScene2, subScene3;
    List<Transform> holderSceneTransform = new List<Transform>();
    private VRTK_ControllerEvents rightControllerAlias = null;
    bool buttonPressed;
    [SerializeField]
    PlayableDirector playableDir;
    ParticleSystem[] particleSystems;
    Transform refScaleObj;
    VRIK vrikObj;
    MimicPuppet.MovementType setMovType;
    [SerializeField]
    AudioSource feedbackAudio;
    // Start is called before the first frame update
    void Start()

    {
        mirror.SetActive(false);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.finalPuppet.transform.position = underTheFloor.position;
            GameManager.Instance.finalMimicPuppet.transform.position = underTheFloor.position;
            GameManager.Instance.finalMimicPuppet.transform.localScale = Vector3.one;
            StartCoroutine(WaitToDestroy());

            subScene1 = subScene[0].GetComponentsInChildren<TransformIdentifier>();
            subScene2 = subScene[1].GetComponentsInChildren<TransformIdentifier>();
            subScene3 = subScene[2].GetComponentsInChildren<TransformIdentifier>();
            Invoke("ChangeToSimonSays", 7f);
        }

        Invoke("SetControllerReferences", 2f);
       
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
            case Gamestate.ReadyforEmbody:

                break;
            case Gamestate.Embody:
         

                break;


            case Gamestate.Dance:
                break;
            case Gamestate.PostDance:
                break;
            case Gamestate.End:
                Destroy(GameManager.Instance.finalMimicPuppet);
                Destroy(GameManager.Instance.finalPuppet);
                Destroy(GameManager.Instance);
                EyeBlinder.Instance.FadeIn();
                SceneManager.LoadScene(0);
                break;

            default:
                break;
        }



    }
    private void ChangeToSimonSays()
    {
        GameManager.Instance.SetNewGamestate(Gamestate.SimonSays);
        particleSystems = GameManager.Instance.finalPuppet.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pr in particleSystems)
        {
            pr.transform.localScale = new Vector3(-pr.transform.localScale.x, pr.transform.localScale.y, pr.transform.localScale.z);
        }        
        mirror.SetActive(true);
      //  playableDir.Play();
        //Invoke("ChangeToMimic", 20f);

    }
    public void ChangeToMimic()
    {
        mirror.SetActive(false);
       GameManager.Instance.finalPuppet.SetActive(false);
        GameManager.Instance.SetNewGamestate(Gamestate.Dance);
      

    }
    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();

    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitUntil(() => vrsetup.actualAvatarVRIK != null);
        vrikObj = vrsetup.actualAvatarVRIK;
         vrikObj.gameObject.AddComponent<MimicSender>();
        vrikObj.GetComponent<HideParts>().isEmbodying = true;

        vrikObj.GetComponent<HideParts>().ShowOrHideParts(true);
        float s = GameManager.Instance.savedScale;
        vrikObj.transform.localScale = new Vector3(s, s, s);
        vrikObj.solver.plantFeet = false;
        vrikObj.solver.spine.minHeadHeight = .01f;
        vrikObj.solver.spine.bodyPosStiffness = .55f;
        vrikObj.solver.leftArm.shoulderRotationWeight = .01f;
        vrikObj.solver.leftArm.shoulderTwistWeight = .01f;
        vrikObj.solver.rightArm.shoulderRotationWeight = .01f;
        vrikObj.solver.rightArm.shoulderTwistWeight = .01f;
        vrikObj.solver.locomotion.footDistance = .15f;
        vrikObj.gameObject.tag = "Player";

        vrikObj.gameObject.AddComponent<PlantFeet>();
        //  GameObject puppetToDestroy = GameManager.Instance.finalPuppet;
        // Destroy(puppetToDestroy);
    }
    private void Update()
    {
        if (rightControllerAlias != null && !buttonPressed)
        {
            if (Input.GetKeyDown(KeyCode.M) || (rightControllerAlias.buttonOnePressed))
            {
                //currentScene++;
                //if (currentScene >= subScene.Count)
                //{ currentScene = 0; }
                //ChangeChoreography(currentScene);
                //buttonPressed = true;
                if (GameManager.Instance.getCurrentGameState() == Gamestate.PostDance)
                {
                    feedbackAudio.Play();
                    Invoke("EndGame", 2f);
                }
            }
        }

    }
    private void EndGame()
    {
        GameManager.Instance.SetNewGamestate(Gamestate.End);
    }
    public void ChangeChoreography(int i)
    {

        currentScene = i;
        StartCoroutine(ScaleDown(1f));
        //   go.GetComponent<Animator>().SetInteger("scale", 1);


    }

    IEnumerator ScaleDown(float time)
    {
        refScaleObj = mimicPuppetCreator.actualPuppets[0].transform;


        {
            float i = 0;
            float rate = 1 / time;
            List<Vector3> fromScalesList = new List<Vector3>();
            for (int n = 0; n < mimicPuppetCreator.actualPuppets.Count; n++)
            {
                fromScalesList.Add(mimicPuppetCreator.actualPuppets[n].transform.localScale);
             
            }
            //Vector3 fromScale = refScaleObj.localScale;
            Vector3 toScale = Vector3.zero;
            while (i < 1)
            {

                for (int j = 0; j < mimicPuppetCreator.actualPuppets.Count; j++)
                {
                    mimicPuppetCreator.actualPuppets[j].transform.localScale = Vector3.Lerp(fromScalesList[j], toScale, i);
                }
               
                i += Time.deltaTime * rate;

                yield return 0;
            }
        }

        RepositionToScene();


    }

    private void RepositionToScene()
    {
        vrsetup.actualAvatarVRIK.GetComponent<MimicSender>().stopMoving = true;
        holderSceneTransform.Clear();
        setMovType = subScene[currentScene].GetComponent<MovementType>().myMovementType;
        for (int i = 0; i < mimicPuppetCreator.actualPuppets.Count; i++)
        {
            switch (currentScene)
            { case 0:
                    
                    if (i < subScene1.Length)
                    {
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().myMovType = setMovType;
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().notGrabbed = false;
                        mimicPuppetCreator.actualPuppets[i].transform.position = subScene1[i].transform.position;
                        mimicPuppetCreator.actualPuppets[i].transform.eulerAngles = subScene1[i].transform.eulerAngles;
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().addedRotation = subScene1[i].transform.eulerAngles;
                        holderSceneTransform.Add(subScene1[i].transform);
                    }
                    break;
                case 1:
                    if (i < subScene2.Length)
                    {
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().myMovType = setMovType;
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().notGrabbed = false;
                        mimicPuppetCreator.actualPuppets[i].transform.position = subScene2[i].transform.position;
                        mimicPuppetCreator.actualPuppets[i].transform.eulerAngles = subScene2[i].transform.eulerAngles;
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().addedRotation = subScene2[i].transform.eulerAngles;
                        holderSceneTransform.Add(subScene2[i].transform);
                    }
                    break;
                case 2:
                    if (i < subScene3.Length)
                    {
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().myMovType = subScene3[i].GetComponent<MovementType>().myMovementType;
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().notGrabbed = false;
                        mimicPuppetCreator.actualPuppets[i].transform.position = subScene3[i].transform.position;
                        mimicPuppetCreator.actualPuppets[i].transform.eulerAngles = subScene3[i].transform.eulerAngles;
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().addedRotation = subScene3[i].transform.eulerAngles;
                        holderSceneTransform.Add(subScene3[i].transform);
                    }

                    break;
                default:
                    break;
            }
        }
        StartCoroutine(ScaleUp(1f));

    }
    public void Crescendo()
    {
        StartCoroutine(ShrinkToOne(40f));
        Debug.Log("I called cooldown");

    }
    public void ChangeState(int i)
    {
        Gamestate gamestate = (Gamestate)i;

        GameManager.Instance.SetNewGamestate(gamestate);
    }
    IEnumerator ScaleUp(float time)
    {

        float i = 0;
        float rate = 1 / time;
        Vector3 fromScale = refScaleObj.localScale;

        while (i < 1)
        {
            for (int n = 0; n < holderSceneTransform.Count; n++)
            {
                mimicPuppetCreator.actualPuppets[n].transform.localScale = Vector3.Lerp(fromScale, holderSceneTransform[n].localScale, i);
            }
            i += Time.deltaTime * rate;

            yield return 0;
        }
        StartPuppeteering();
    }

   IEnumerator ShrinkToOne(float time)
    {
      //  refScaleObj = mimicPuppetCreator.actualPuppets[0].transform;


        {
            float i = 0;
            float rate = 1 / time;

        //    Vector3 fromScale = refScaleObj.localScale;
            Vector3 toScale = Vector3.zero;
            List<Vector3> fromScales = new List<Vector3>();
            List<Vector3> fromPos = new List<Vector3>();
            Transform T = vrsetup.actualAvatarVRIK.references.spine;
            for (int n = 0; n < mimicPuppetCreator.actualPuppets.Count; n++)
            {
                fromScales.Add(mimicPuppetCreator.actualPuppets[n].transform.localScale);
                fromPos.Add(mimicPuppetCreator.actualPuppets[n].transform.position);
            }
            while (i < 1)
            {
            
                for (int j = 0; j < mimicPuppetCreator.actualPuppets.Count; j++)
                {
                    mimicPuppetCreator.actualPuppets[j].transform.localScale = Vector3.Lerp(fromScales[j], toScale, i);
                    mimicPuppetCreator.actualPuppets[j].transform.position = Vector3.Lerp(fromPos[j], T.position, i);
                }
               
                i += Time.deltaTime * rate;

                yield return 0;
            }
        }

    }
    private void StartPuppeteering()
    {
        foreach (GameObject go in mimicPuppetCreator.actualPuppets)
        {
            //  go.GetComponent<Animator>().SetInteger("scale", 2);
          
           go.GetComponent<MimicPuppet>().notGrabbed = true;
            buttonPressed = false;
        }
        vrsetup.actualAvatarVRIK.GetComponent<MimicSender>().stopMoving = false;
}
}
