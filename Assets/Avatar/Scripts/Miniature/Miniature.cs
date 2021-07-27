
using UnityEngine;
using VRTK;

public class Miniature : MonoBehaviour {


    private Collider voteCollider;
    private VRTK_InteractableObject interactable;
    private bool voted;

    private GameObject miniatureModel;

 


    private void OnTriggerEnter(Collider other)
    {
        voteCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        voteCollider = null;
        voted = false;
    }

    public void ChangeOutfit(int avatarIndex, int outfitIndex)
    {
       
            int[] indexes = new int[2] { avatarIndex, outfitIndex };
           
        ChangeOutfit miniatureOutfit = miniatureModel.GetComponentInChildren<ChangeOutfit>();
        Outfit chosenOutfit = AvatarManager.Instance.avatarset.getOutfits(avatarIndex).outfits[outfitIndex];
        miniatureOutfit.ChangeClothes(chosenOutfit);
    }

    private void PUNRPC_ChangeOutfit(int[] indexes)
    {
        ChangeOutfit(indexes[0], indexes[1]);
    }

    /// <summary>
    /// Remove's Miniature from the game.
    /// </summary>
    /// <param name="playerID">Photon ID of the Player.</param>
    public void RemoveMiniature()
    {
        Destroy(miniatureModel);
    }

    public void ChangeAvatar(int newAvatar, int outfit)
    {
    
        RemoveMiniature();
        SpawnAvatar(newAvatar, outfit);
    }

    public void ChangeAvatar(Outfit newAvatar)
    {
      
            int[] outfitIndex = AvatarManager.Instance.avatarset.getOutfitIndex(newAvatar);

        
        RemoveMiniature();
        SpawnAvatar(newAvatar);
    }

  

    /// <summary>
    /// Spawn the Miniature.
    /// </summary>
    /// <param name="position">Position the Miniature will be spawned at.</param>
    /// <param name="playerID">Photon ID of the Player to spawn a Miniature for.</param>
    /// <param name="outfitIndex">Index of the Outfit of the Miniature and Player.</param>
    private void SpawnAvatar(int avatar, int outfitIndex)
    {
        Outfit outfit = AvatarManager.Instance.getOutfits(avatar).outfits[outfitIndex];

        if (outfit.prefabOrTextureOutfit == Outfit.PrefabOrTextureOutfit.Texture)
        {
            miniatureModel = Instantiate(AvatarManager.Instance.GetMiniature(avatar), this.transform.position, this.transform.rotation, this.transform);

            ChangeOutfit(avatar, outfitIndex);
        } else
        {
            Debug.Log("Applying outfit prefab" + outfit.outfitMiniature);

            miniatureModel = Instantiate(outfit.outfitMiniature, this.transform.position, this.transform.rotation, this.transform);
        }
    }

    private void SpawnAvatar(Outfit outfit)
    {
        miniatureModel = Instantiate(outfit.outfitMiniature, this.transform.position, this.transform.rotation, this.transform);
    }

   
}
