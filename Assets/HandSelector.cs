using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelector : MonoBehaviour
{
    Paint paintObject;
    BrushTypeEnum brushType;
    MimicSender mimicSender;
    // Start is called before the first frame update
    void Start()
    {
        paintObject = transform.root.GetComponentInChildren<Paint>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Palette")
        {
            brushType = other.GetComponent<BrushType>().mybrushType;
            if ( brushType == BrushTypeEnum.mat)
            {
                paintObject.materialToPaint = other.GetComponent<MeshRenderer>().material;
                paintObject.paintingWithMaterial = true;
                paintObject.seedBrushIsOn = false;
            }
            else
            {
                paintObject.paintingWithMaterial = false;
                paintObject.prefabToSpawn = other.GetComponent<BrushType>().prefabToSpawn;
                if (brushType == BrushTypeEnum.seed)
                {
                    paintObject.seedBrushIsOn = true;

                }
                else
                {
                    paintObject.seedBrushIsOn = false; 
                }
            }
            
        }
        if (other.tag == "StartMimic")
        {
            mimicSender = paintObject.gameObject.GetComponent<MimicSender>();
            other.GetComponent<MimicButton>().SetupPuppet(mimicSender);
        }

    }
}
