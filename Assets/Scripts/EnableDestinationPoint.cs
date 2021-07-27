using System.Collections;
using UnityEngine;

public class EnableDestinationPoint : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<VRTK.VRTK_DestinationPoint>())
        {
            StartCoroutine(WaitForEnable());
        }
    }

    private IEnumerator WaitForEnable()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<VRTK.VRTK_DestinationPoint>().enabled = true;
    }
}
