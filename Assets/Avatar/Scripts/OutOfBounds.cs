using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    [SerializeField]
    [Tooltip("Tag of out of Trigger object.")]
    private string tag;
    [SerializeField]
    [Tooltip("Tag of out of Trigger object.")]
    private string tag2;
    [SerializeField]
    [Tooltip("Defines if the object needs to be reset on Exit or on Enter.")]
    private bool onExit;
    [SerializeField]
    [Tooltip("Defines if the object needs to be reset on Exit or on Enter.")]
    private bool paintgun;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    // Set the original position and rotation
    void Start () {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
	}

    private void OnTriggerExit(Collider other)
    {
        if (onExit)
        {
            ResetPosition(other.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onExit)
        {
            ResetPosition(other.tag);
        }
        else if (paintgun)
        {
            if (other.tag.Equals(tag2))
                ResetPositionInsideSpace();
        }

    }

    /// <summary>
    /// Resets the position of the object.
    /// </summary>
    /// <param name="otherTag"></param>
   public void ResetPosition(string otherTag)
    {
        if (otherTag.Equals(tag))
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
            this.transform.position = originalPosition;
            this.transform.rotation = originalRotation;
        }
    }

    public void ResetPositionInsideSpace()
    {
       
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
            this.transform.position = originalPosition;
            this.transform.rotation = originalRotation;
        
    }

}
