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
    [SerializeField]
    bool humanBreathing;

    public bool breathing;
   
    List<GameObject> inversedScaleBodyParts;
    // Start is called before the first frame update

    private void Start()
    {
        if (humanBreathing)
        {
            inversedScaleBodyParts = AllChilds(transform.gameObject);
         
        }
    }

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform bone in root.transform)
            {
             
                
                    Searcher(result, bone.gameObject);
                
            }
        }
        return result;
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
     
            list.Add(root);
        
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }

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
                float y = transform.localScale.y + (Mathf.Sin(Time.time) * Time.deltaTime * changeVal);
                transform.localScale = new Vector3(transform.localScale.x ,y, transform.localScale.z);
                if (humanBreathing && breathing)
                {
                    for (int i = 0; i < inversedScaleBodyParts.Count; i++)
                    {
                      
                        Transform t = inversedScaleBodyParts[1].transform;
                        t.localScale = new Vector3(transform.localScale.x, 1/y, transform.localScale.z);
                    }
                }
                break;
            case orientation.z:
                transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y, transform.localScale.z +  (Mathf.Sin(Time.time) *Time.deltaTime *changeVal));
                break;
            default:
                break;
        }
    }
}
