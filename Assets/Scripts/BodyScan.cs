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
    bool scanning;
    [SerializeField] SkinnedMeshRenderer scanMaterial, scanHeadMaterial;
    float scanVal = 0;
    [SerializeField]
    float scanStep, scanValHead;
    bool scanHead;



    public void StartScan()
    {

        StartCoroutine(ScanNext());
    }
    IEnumerator ScanNext()
    {
        yield return new WaitForSeconds(delaysInBetween[currentBone]);

        currentOriginPoint = vrSetUp.actualAvatarVRIK.GetComponent<VRIKApplier>().originPoints[currentBone];
        currentDestinationPoint = bonesForBodyScan[currentBone];
        GameObject go = GameObject.Instantiate(particlePrefab, currentOriginPoint);
        go.transform.position = currentOriginPoint.position;
        ReachToDestination rtd = go.GetComponent<ReachToDestination>();
        rtd.destination = currentDestinationPoint;
        rtd.goToDestination = true;
        currentBone++;
        scanning = true;
        if (currentBone == 6)
        {
            scanStep = 0.008f;
        }
        if (currentBone == 10)
        {
            scanHead = true;
        }
        if (currentBone < delaysInBetween.Length)
        {
            StartCoroutine(ScanNext());
        }
        else
        {
            scanHead = false;
            scanning = false;
            scanMaterial.material.SetFloat("_Progress", 1);
            scanHeadMaterial.material.SetFloat("_Progress", 1);
            StartCoroutine(WaitForAudioEnd());

        }
    }


    private void Update()
    {
        if (scanning && scanVal < 1)
        { scanVal = scanVal + (scanStep * Time.deltaTime);

            scanMaterial.material.SetFloat("_Progress", scanVal);
        }
        if (scanHead && scanValHead < 1)
        {
            scanValHead = scanValHead + (scanStep * Time.deltaTime);
            {
                scanHeadMaterial.material.SetFloat("_Progress", scanValHead);
            }
        }
    }
    IEnumerator WaitForAudioEnd()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        // add in future a fail safe for time, just in case ausio end is not detected 
         GameManager.Instance.SetNewGamestate(Gamestate.Painting);
    
}


}
