using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushTypeEnum {ribbon, particles, seed, color }
public class BrushType : MonoBehaviour
{
    [SerializeField]
   public BrushTypeEnum mybrushType;
    public GameObject prefabToSpawn;
    public Color32 brushColor;
    [ColorUsage (true, true)]
    public Color emmissiveColor;
    public Material Electric;

    
  
    public bool customShader;
    public bool subTypeColor;
    public GameObject tipObj;
    public bool IsEmissive;
    public float intensityVal;


    private void Start()
    {
        if (mybrushType == BrushTypeEnum.color)

        {
            brushColor = GetComponent<MeshRenderer>().material.color;
                   
        }

    
    }
}
