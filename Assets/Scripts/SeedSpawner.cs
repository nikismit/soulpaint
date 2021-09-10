using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToSpawn;
    GameObject prefabChanged;
    [SerializeField]
    private int seedMax;
    public int seedCounter;
    public bool seedBrushIsOn;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        prefabChanged = prefabToSpawn;
        canvas = transform.GetComponentInParent<Rigidbody>().gameObject;
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        prefabChanged.GetComponent<MeshRenderer>().material = Instantiate(mat);
    }

    // Update is called once per frame
    void Update()
    {
        if (seedCounter < seedMax && canvas != null)
        {
            seedCounter++;
            
            if (seedCounter % 3 == 0) //this is done to skip some frames and not make
                                       // make it immediately happen
            {
                //create a bounding space in which the objects are spawned in
                //defined by the bounding space of the collider of the "canvas" object
                Bounds bounds = canvas.transform.GetComponent<Collider>().bounds;
                Vector3 rdPoint = transform.position +(Random.insideUnitSphere * .05f); 
                //instantiates and randomizes stuff, position is important to be in
                //world scale and defined within the bounding areas of the parent 
                GameObject go = Instantiate(prefabChanged);
                go.transform.SetParent(canvas.transform);
                go.transform.position = rdPoint;
             //   go.transform.position = new Vector3(
                                            //   Random.Range(bounds.min.x, bounds.max.x),
                                              // Random.Range(bounds.min.y, bounds.max.y),
                                             //  Random.Range(bounds.min.z, bounds.max.z));
                go.transform.localRotation = Random.rotation;
                go.transform.localScale = Vector3.one * Random.Range(0.01f, .05f);
            }
        }
    }
}
