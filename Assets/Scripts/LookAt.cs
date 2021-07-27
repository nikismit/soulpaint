using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform lookPoint;

    private Vector3 initialLookPoint;

    [SerializeField]
    private Vector3 lookOffset;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private Vector3 randomLookDirection;

    private void Awake()
    {
        initialLookPoint = lookPoint.position;

        StartCoroutine(RandomLook());
    }

    private IEnumerator RandomLook()
    {
        while (true)
        {
            randomLookDirection.x = Random.Range(-50, 50);
            randomLookDirection.y = Random.Range(-50, 50);
            randomLookDirection.z = 50;

            yield return new WaitForSeconds(5);
        }
    }

    private void LateUpdate()
    {
        if (target)
        {
            lookPoint.position = Vector3.Lerp(lookPoint.position, target.position + lookOffset, Time.deltaTime * speed);
        }
        else
        {
            lookPoint.position = Vector3.Lerp(lookPoint.position, initialLookPoint + lookOffset + randomLookDirection, Time.deltaTime * speed);
        }

        Quaternion targetRotation = Quaternion.LookRotation(lookPoint.transform.position - transform.position);
        
        head.rotation = targetRotation;

        Vector3 headEulerAngles = head.localEulerAngles;

        headEulerAngles.x = HelperFunctions.ClampAngle(headEulerAngles.x, -50, 60);
        headEulerAngles.y = HelperFunctions.ClampAngle(headEulerAngles.y, -70, 70);
        headEulerAngles.z = HelperFunctions.ClampAngle(headEulerAngles.z, -15, 15);

        head.localEulerAngles = headEulerAngles;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}