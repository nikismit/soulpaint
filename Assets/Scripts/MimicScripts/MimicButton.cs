using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicButton : MonoBehaviour
{

    [SerializeField]
    GameObject MimicPuppetPrefab,  puppetParent ;
    [SerializeField]
    Collider boundsObj;
    private List<GameObject> puppets;
    // Start is called before the first frame update

    public void SetupPuppet(MimicSender mimicSender)
    {
        GameObject newPuppet = Instantiate(MimicPuppetPrefab);
        //  puppets.Add(newPuppet);
      MimicPuppet mp =  newPuppet.AddComponent<MimicPuppet>();
        mp.player = mimicSender.gameObject;
        Bounds bounds = boundsObj.bounds;
        newPuppet.transform.SetParent(puppetParent.transform);
        newPuppet.transform.position = new Vector3(
                                       Random.Range(bounds.min.x, bounds.max.x),
                                       Random.Range(bounds.min.y, bounds.max.y),
                                       Random.Range(bounds.min.z, bounds.max.z));
        newPuppet.transform.localRotation = Random.rotation;
        newPuppet.transform.localScale = Vector3.one * Random.Range(0.5f, 3f);
    }
  

   

}
