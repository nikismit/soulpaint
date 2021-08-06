using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachToDestination : MonoBehaviour
{
    public bool goToDestination;
    public Transform destination;
    float speed = .5f;



    // Update is called once per frame
    void Update()
    {
        if (goToDestination)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
        }
        if (Vector3.Distance(transform.position, destination.position) < .01f) 
        {
            Destroy(this.gameObject);
        
        }
    }

   
}
