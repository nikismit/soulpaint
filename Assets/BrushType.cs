using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushTypeEnum {mat,  obj, seed }
public class BrushType : MonoBehaviour
{
    [SerializeField]
   public BrushTypeEnum mybrushType;
    public GameObject prefabToSpawn;
}
