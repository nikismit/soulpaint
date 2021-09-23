using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    enum TypeOfPrefab { particles = 0, objects,}
    [SerializeField] TypeOfPrefab prefabType;
    [SerializeField]
    ParticleSystemRenderer particleSystem;
    Material typeMat;
  //  List<Material> matParticle = new List<Material>();
    [SerializeField]
    bool isCustomShader, lightWrap;
    [ColorUsage(true, true)]
    public Color[] brushColor = new Color[7];


    public void ChangeMaterial(int x)
    {
        if (prefabType == TypeOfPrefab.particles)
        {
            particleSystem = GetComponent<ParticleSystemRenderer>();
            typeMat = particleSystem.material;
            particleSystem.material = Instantiate(typeMat);
            if (isCustomShader)
            {
                if (!lightWrap)
                {
                    particleSystem.material.SetColor("_color", brushColor[x]);
                }
                else
                {

                    particleSystem.material.SetColor("_node_3384", brushColor[x]);
                }
            }
            else
            {
                particleSystem.material.SetColor("_Color", brushColor[x]);
            }
            
        }
    }
}
