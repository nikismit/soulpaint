using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    Transform[] subScene1, subScene2, subScene3;
    private VRTK_ControllerEvents rightControllerAlias = null;
    bool buttonPressed;
    // Start is called before the first frame update
    void Start()
    {if (GameManager.Instance != null)
        {
            GameManager.Instance.finalPuppet.transform.position = underTheFloor.position;
            GameManager.Instance.finalMimicPuppet.transform.position = underTheFloor.position;
            GameManager.Instance.finalMimicPuppet.transform.localScale = Vector3.one;
            StartCoroutine(WaitToDestroy());

            subScene1 = subScene[0].GetComponentsInChildren<Transform>();
            subScene2 = subScene[1].GetComponentsInChildren<Transform>();
            subScene3 = subScene[2].GetComponentsInChildren<Transform>();
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
                if (currentScene >=  subScene.Count)
                {   currentScene = 0; }
                ChangeChoreography();
                buttonPressed = true;
            }
        }
   
    }

    private void ChangeChoreography()
    {

        foreach (GameObject go in mimicPuppetCreator.actualPuppets)
        {
            {
                go.GetComponent<Animator>().SetInteger("scale", 1);
                // go.GetComponent<Animator>().Play("scaleDown", 0);
            }

            Invoke("RepositionToScene", 2f);
        }
    }
    private void RepositionToScene()
    {
        for (int i = 0; i < mimicPuppetCreator.actualPuppets.Count; i++)
        {
            switch (currentScene)
            { case 0:
                    if (i < subScene1.Length)
                    {
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().notGrabbed = false;
                        mimicPuppetCreator.actualPuppets[i].transform.position = subScene1[i].position;
                        mimicPuppetCreator.actualPuppets[i].transform.eulerAngles = subScene1[i].eulerAngles;
                    }
                    break;
                case 1:
                    if (i < subScene2.Length)
                    {
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().notGrabbed = false;
                        mimicPuppetCreator.actualPuppets[i].transform.position = subScene2[i].position;
                        mimicPuppetCreator.actualPuppets[i].transform.eulerAngles = subScene2[i].eulerAngles;
                    }
                    break;
                case 2:
                    if (i < subScene3.Length)
                    {
                        mimicPuppetCreator.actualPuppets[i].GetComponent<MimicPuppet>().notGrabbed = false;
                        mimicPuppetCreator.actualPuppets[i].transform.position = subScene3[i].position;
                        mimicPuppetCreator.actualPuppets[i].transform.eulerAngles = subScene3[i].eulerAngles;
                    }
                    break;
                default:
                    break;
            }
        }
        foreach (GameObject go in mimicPuppetCreator.actualPuppets)
        {
            go.GetComponent<Animator>().SetInteger("scale", 2);
          //  go.GetComponent<Animator>().Play("scaleUp", 0);
           go.GetComponent<MimicPuppet>().notGrabbed = true;
            buttonPressed = false;
        }
    }

}
