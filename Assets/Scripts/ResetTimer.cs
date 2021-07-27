using System.Collections;
using UnityEngine;

public class ResetTimer : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        StartCoroutine(WaitForReset());        
    }

    private IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(60);

        rigidbody.velocity = Vector3.zero;

        transform.SetPositionAndRotation(startPosition, startRotation);

        StartCoroutine(WaitForReset());
    }
}