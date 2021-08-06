using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    enum TypeOfPrefab { particles = 0, objects,}
    [SerializeField] TypeOfPrefab prefabType;
    [SerializeField]
    ParticleSystemRenderer[] particleSystems;


    public void ChangeMaterial(Material mat)
    {
        if (prefabType == TypeOfPrefab.particles)
        {
            foreach (ParticleSystemRenderer pS in particleSystems)
            {
                pS.material = Instantiate(mat);
             
            }
        }
    }
}
