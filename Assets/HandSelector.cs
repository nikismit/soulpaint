using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelector : MonoBehaviour
{
    Paint paintObject;
    BrushTypeEnum brushType;
    MimicSender mimicSender;
    BrushType _brushType;
    MeshRenderer paintHead;
    GameObject prefabToSpawn;
    Material lastMat;

  [SerializeField]  GameObject lastBrush;
    // Start is called before the first frame update
    void Start()
    {
        paintObject = transform.root.GetComponentInChildren<Paint>();
        paintHead = GetComponent<MeshRenderer>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Palette")
        {
            _brushType = other.GetComponent<BrushType>();
            brushType = _brushType.mybrushType;

            if(_brushType.subTypeColor)
                {

                paintObject.colorPicked = _brushType.brushColor;
                if (!paintObject.customBrushShaderIsOn)
                {
                    paintObject.materialToPaint.color = _brushType.brushColor;
                    paintHead.material = other.GetComponent<MeshRenderer>().material;
                }
                else
                {
                    paintObject.materialToPaint.SetColor("_color", _brushType.brushColor);
                    paintHead.material = other.GetComponent<MeshRenderer>().material;

                }


            }
            if (brushType == BrushTypeEnum.mat)
            {
                    lastMat = other.GetComponent<MeshRenderer>().material;
                    paintObject.materialToPaint = Instantiate(lastMat);
                    paintObject.paintingWithMaterial = true;
                    paintHead.material = other.GetComponent<MeshRenderer>().material;

                    paintObject.customBrushShaderIsOn = _brushType.customShader;
                if (lastBrush.transform.childCount > 0)
                {
                    GameObject toDest = lastBrush.transform.GetChild(0).gameObject;
                    Destroy(toDest);
                }

                //  paintObject.seedBrushIsOn = false;
            }
            else if (brushType == BrushTypeEnum.obj)
            {
                paintObject.paintingWithMaterial = false;

               prefabToSpawn = other.GetComponent<BrushType>().tipObj;
                paintObject.prefabToSpawn = other.GetComponent<BrushType>().prefabToSpawn;
                if (lastBrush.transform.childCount > 0)
                {
                    GameObject toDest = lastBrush.transform.GetChild(0).gameObject;
                    Destroy(toDest);
                }
               GameObject go = Instantiate(prefabToSpawn, lastBrush.transform.position, lastBrush.transform.rotation);
                          go.transform.SetParent(lastBrush.transform);
                        
                      
             

            }
       
            
        }
        if (other.tag == "StartMimic")
        {
            mimicSender = paintObject.gameObject.GetComponent<MimicSender>();
            other.GetComponent<MimicButton>().SetupPuppet(mimicSender);
        }

    }
}
