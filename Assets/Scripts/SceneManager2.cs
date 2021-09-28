using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRTK;

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
    int currentScene;
    TransformIdentifier[] subScene1, subScene2, subScene3;
    List<Transform> holderSceneTransform = new List<Transform>();
    private VRTK_ControllerEvents rightControllerAlias = null;
    bool buttonPressed;

    Transform refScaleObj;
    // Start is called before the first frame update
    void Start()
    { if (GameManager.Instance != null)
        {
            GameManager.Instance.finalPuppet.transform.position = underTheFloor.position;
            GameManager.Instance.finalMimicPuppet.transform.position = underTheFloor.position;
            GameManager.Instance.finalMimicPuppet.transform.localScale = Vector3.one;
            StartCoroutine(WaitToDestroy());

            subScene1 = subScene[0].GetComponentsInChildren<TransformIdentifier>();
            subScene2 = subScene[1].GetComponentsInChildren<TransformIdentifier>();
            subScene3 = subScene[2].GetComponentsInChildren<TransformIdentifier>();
        }

        Invoke("SetControllerReferences", 2f);
    }

    void SetControllerReferences()
    {
        rightControllerAlias = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();

    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitUntil(() => vrsetup.actualAvatarVRIK != null);
        vrsetup.actualAvatarVRIK.gameObject.AddComponent<MimicSender>();
        vrsetup.actualAvatarVRIK.GetComponent<HideParts>().isEmbodying = true;
        vrsetup.actualAvatarVRIK.GetComponent<HideParts>().ShowOrHideParts(true);
        vrsetup.actualAvatarVRIK.gameObject.tag = "Player";
        //  GameObject puppetToDestroy = GameManager.Instance.finalPuppet;
        // Destroy(puppetToDestroy);
    }
    private void Update()
    {
        if (rightControllerAlias != null && !buttonPressed)
        {
            if (Input.GetKeyDown(KeyCode.M) || (rightControllerAlias.buttonOnePressed))
            {
                currentScene++;
                if (currentScene >= subScene.Count)
                { currentScene = 0; }
                ChangeChoreography();
                buttonPressed = true;
            }
        }

    }

    private void ChangeChoreography()
    {


        StartCoroutine(ScaleDown(1f));
        //   go.GetComponent<Animator>().SetInteger("scale", 1);


    }

    IEnumerator ScaleDown(float time)
    {
        refScaleObj = mimicPuppetCreator.actualPuppets[0].transform;


        {
            float i = 0;
            float rate = 1 / time;

            Vector3 fromScale = refScaleObj.localScale;
            Vector3 toScale = Vector3.zero;
            while (i < 1)
            {
                foreach (GameObject go in mimicPuppetCreator.actualPuppets)
                {
                    go.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
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
        for (int i = 0; i < mimicPuppetCreator.actualPuppets.Count; i++)
        {
            switch (currentScene)
            { case 0:

                    if (i < subScene1.Length)
                    {
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
