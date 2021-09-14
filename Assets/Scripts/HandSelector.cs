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
    Color32 lastCol= new Color32(255, 198, 11, 255);
    float intensity = 2.9f;
    [ColorUsage(true, true), SerializeField]
    Color emissiveColor; 
    bool isEmissive;


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

                   switch (brushType)
            {
                case BrushTypeEnum.ribbon:
                    lastMat = other.GetComponent<MeshRenderer>().material;
                    paintObject.materialToPaint = Instantiate(lastMat);
                    paintObject.paintingWithMaterial = true;
                    paintHead.material = other.GetComponent<MeshRenderer>().material;

                    isEmissive = _brushType.IsEmissive;
                    paintObject.customBrushShaderIsOn = _brushType.customShader;
                    if (lastBrush.transform.childCount > 0)
                    {
                        GameObject toDest = lastBrush.transform.GetChild(0).gameObject;
                        Destroy(toDest);
                    }

                    paintObject.seedBrushIsOn = false;
                    SetColor(lastCol);

                    break;
                case BrushTypeEnum.particles:
                    paintObject.paintingWithMaterial = false;
                    paintObject.customBrushShaderIsOn = false;
                    prefabToSpawn = other.GetComponent<BrushType>().tipObj;
                    paintObject.prefabToSpawn = other.GetComponent<BrushType>().prefabToSpawn;

                    if (lastBrush.transform.childCount > 0)
                    {
                        GameObject toDest = lastBrush.transform.GetChild(0).gameObject;
                        Destroy(toDest);
                    }
                    isEmissive = false;
                    GameObject go = Instantiate(prefabToSpawn, lastBrush.transform.position, lastBrush.transform.rotation);
                    go.transform.SetParent(lastBrush.transform);
                    paintHead.material = Instantiate(paintObject.materialForObj);
                    paintObject.seedBrushIsOn = false;
                    break;
                case BrushTypeEnum.seed:
                    paintObject.paintingWithMaterial = false;
                    paintObject.customBrushShaderIsOn = false; 
                    paintObject.seedBrushIsOn = true;
                    isEmissive = false;
                    paintObject.prefabToSpawn = other.GetComponent<BrushType>().prefabToSpawn;
                    if (lastBrush.transform.childCount > 0)
                    {
                        GameObject toDest = lastBrush.transform.GetChild(0).gameObject;
                        Destroy(toDest);
                    }
                    paintHead.material = Instantiate(paintObject.materialForObj);
                    break;
                case BrushTypeEnum.color:
                    emissiveColor = _brushType.emmissiveColor;
                    lastCol = _brushType.brushColor;
                    intensity = _brushType.intensityVal;
                    paintObject.materialForObj.color = _brushType.brushColor;
                    SetColor(lastCol);



                    break;
                default:
                    break;
            }
          
    

            
        }
        if (other.tag == "StartMimic")
        {
            mimicSender = paintObject.gameObject.GetComponent<MimicSender>();
            other.GetComponent<MimicButton>().SetupPuppet(mimicSender);
        }

    }


    private void SetColor(Color32 color)
    {
      
        paintObject.colorPicked = color;
        
        if (!paintObject.customBrushShaderIsOn)
        {
            paintObject.materialToPaint.color = color;
            paintHead.material.color = color;
            paintObject.materialForObj.color = color;
            isEmissive = false;

        }
        else
        {
            if (!isEmissive)
            {
                paintObject.materialToPaint.SetColor("_color", color);
                paintHead.material.SetColor("_color", color);
                isEmissive = false;
            }
            else
            {
                paintObject.materialToPaint.SetVector("_EmissiveColor", emissiveColor * intensity);
                paintHead.material.SetVector("_EmissiveColor", emissiveColor *intensity);
                isEmissive = true;
                
            }

        }
      

    }
}
