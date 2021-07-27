using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    public enum orientation { x, y, z };
    [SerializeField]
    float changeVal;
    [SerializeField]
    orientation select;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {

        switch (select)
        {
            case orientation.x:
                transform.localScale = new Vector3(transform.localScale.x + (Mathf.Sin(Time.time)* Time.deltaTime *changeVal), transform.localScale.y, transform.localScale.z);
             //   Debug.Log("what is my scale cal" + transform.localScale.x +( Mathf.Sin(Time.time) * changeVal));
                break;
            case orientation.y:
                transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y + (Mathf.Sin(Time.time)*Time.deltaTime  *changeVal), transform.localScale.z);
                break;
            case orientation.z:
                transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y, transform.localScale.z +  (Mathf.Sin(Time.time) *Time.deltaTime *changeVal));
                break;
            default:
                break;
        }
    }
}
