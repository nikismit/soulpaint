using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScan : MonoBehaviour
{
    [SerializeField]
    VRAvatarController vrSetUp;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    GameObject particlePrefab;
    [SerializeField]
    float[] delaysInBetween;
    int currentBone;
    [SerializeField] public Transform[] bonesForBodyScan;
    Transform currentOriginPoint;
    Transform currentDestinationPoint;
  
    // Start is called before the first frame update
    void Start()
    {
     //   audioSource.Play();
        StartCoroutine(ScanNext());
    }

    IEnumerator ScanNext()
    {
        yield return new WaitForSeconds(delaysInBetween[currentBone]);
        if(currentBone == 5)
        {
            bonesForBodyScan[currentBone].GetComponent<ScaleChanger>().breathing = true;
        }
        currentOriginPoint = vrSetUp.actualAvatarVRIK.GetComponent<VRIKApplier>().originPoints[currentBone];
        currentDestinationPoint = bonesForBodyScan[currentBone];
        GameObject go = GameObject.Instantiate(particlePrefab, currentOriginPoint);
        go.transform.position = currentOriginPoint.position;
        ReachToDestination rtd = go.GetComponent<ReachToDestination>();
        rtd.destination = currentDestinationPoint;
        rtd.goToDestination = true;
        currentBone++;
        if (currentBone < delaysInBetween.Length)
        {
            StartCoroutine(ScanNext());
        }
        else
        {
            bonesForBodyScan[5].GetComponent<ScaleChanger>().breathing = false;
        }
    }

}
