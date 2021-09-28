using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformIdentifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x == 1)
        {
            float x = GameManager.Instance.savedScale;
            transform.localScale = new Vector3(x, x, x);

        }
    }
}
