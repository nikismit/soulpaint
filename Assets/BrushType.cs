using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushTypeEnum {mat,  obj, seed, }
public class BrushType : MonoBehaviour
{
    [SerializeField]
   public BrushTypeEnum mybrushType;
    public GameObject prefabToSpawn;
    public Color32 brushColor;
    public bool customShader;
    public bool subTypeColor;
    public GameObject tipObj;
    private void Start()
    {
        if (subTypeColor)

        {
            brushColor = GetComponent<MeshRenderer>().material.color;

        
        }
    }
}
