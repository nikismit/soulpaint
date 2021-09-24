using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

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
            Invoke("SetupPuppets", 3f); }
    }


    private void SetupPuppets()
    {
        player = vrsetup.transform.root.GetComponentInChildren<VRIK>().gameObject;
        foreach (Transform tr in transformSetup)
        {
            GameObject go = Instantiate(puppetToCreate);
        MimicPuppet mp =    go.AddComponent<MimicPuppet>();
            mp.player = player;
            mp.myMovType = myMovSet;
            go.transform.position = tr.position;
            go.transform.eulerAngles = tr.eulerAngles;
            go.transform.localScale = tr.localScale;
          Animator anim =  go.AddComponent<Animator>();
            anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Prefabs/ScaleAnim", typeof(RuntimeAnimatorController));
            actualPuppets.Add(go);
           
          //  go.AddComponent<ParticleScaler>();
        }
       
    }

}
