using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager2 : MonoBehaviour
{

    [SerializeField]
    Transform underTheFloor;
    [SerializeField]
    VRAvatarController vrsetup;
    List<GameObject> subScene = new List<GameObject>();
    [SerializeField]
    MimicPuppetsCreator mimicPuppetCreator;

    Transform[] subScene1, subScene2, subScene3;
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            i++;
            if (i < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(i);
            }
            else
            {
                SceneManager.LoadScene(1);
            }

        }
    }

    private void ChangeChoreography(int sceneNo)
    {
        foreach (GameObject go in mimicPuppetCreator.actualPuppets)
        {
            go.GetComponent<Animator>().Play("scaleDown", 0);
        }
        Invoke("RepositionToScene", 2f);
            }
    private void RepositionToScene()
    {


    }

}
