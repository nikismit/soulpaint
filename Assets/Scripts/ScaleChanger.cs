using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    public enum orientation { x, y, z, all };
    [SerializeField]
    float changeVal;
    [SerializeField]
    orientation select;
  



    // Update is called once per frame
    void Update()
    {

        switch (select)
        {
            case orientation.x:
                {
                    float x = transform.localScale.x + (Mathf.Sin(Time.time) * Time.deltaTime * changeVal);
                    transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

                }

                break;
            case orientation.y:
                {

                    float y = transform.localScale.y + (Mathf.Sin(Time.time) * Time.deltaTime * changeVal);
                    transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
                }
             
                break;
            case orientation.z:
                { 
                    float z = transform.localScale.z + (Mathf.Sin(Time.time) * Time.deltaTime * changeVal);
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
                 
                }
                break; 
            case orientation.all:
                {
                    float x = transform.localScale.x + (Mathf.Sin(Time.time) * Time.deltaTime * changeVal);
              
                    transform.localScale = new Vector3(x,x, x);

                }

                break;
            default:
                break;
        }
    }
}
