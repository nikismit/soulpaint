using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Change outfit changes the textures on the given gameobjects.
/// You also have to specify the index of the material, because an object can have multiple materials.
/// </summary>
public class ChangeOutfit : MonoBehaviour
{

    //[SerializeField]
    //[Tooltip("GameObject and the index of the material that needs to be changed.")]
   // private List<MaterialBody> materialBodies;

    /// <summary>
    /// Changes the Materials texture.
    /// </summary>
    /// <param name="textures">New Textures to apply.</param>
    /*public void ChangeClothes(List<Texture> textures)
    {
        try
        {
            for (int i = 0; i < textures.Count; i++)
            {
                GameObject body = materialBodies[i].body;
                SkinnedMeshRenderer skinnedMeshRenderer = body.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.materials[materialBodies[i].indexMaterial].mainTexture = textures[i];
                }
                else
                {
                    MeshRenderer meshRenderer = body.GetComponent<MeshRenderer>();
                    meshRenderer.materials[materialBodies[i].indexMaterial].mainTexture = textures[i];
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }*/

    public void ChangeClothes(Outfit outfit) {  }
    //{
    //    try
    //    {
    //        for (int i = 0; i < outfit.texture.Count; i++)
    //        {
    //          //  GameObject body = materialBodies[i].body;
    //          //  SkinnedMeshRenderer skinnedMeshRenderer = body.GetComponent<SkinnedMeshRenderer>();
    //            if (skinnedMeshRenderer != null)
    //            {
    //           //     skinnedMeshRenderer.materials[materialBodies[i].indexMaterial].mainTexture = outfit.texture[i];
    //            }
    //            else
    //            {
    //            //    MeshRenderer meshRenderer = body.GetComponent<MeshRenderer>();
    //           //     meshRenderer.materials[materialBodies[i].indexMaterial].mainTexture = outfit.texture[i];
    //            }
    //        }
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.Log(e.Message);
    //    }
    //}
}
