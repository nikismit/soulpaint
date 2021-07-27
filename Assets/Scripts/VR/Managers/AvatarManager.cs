using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Easily accesibly database for avatars.
/// </summary>
public class AvatarManager : MonoBehaviour {

    public static AvatarManager Instance;

    public Avatars avatarset;

    private bool gonePastStart = false;

    private void Awake()
    {
        Instance = this;
       
    }

    public void Start()
    {
        gonePastStart = true;
    }

    public GameObject GetMiniature(int number)
    {
        return avatarset.listOfMiniatures[number];
    }

    public void OnJoinedRoom()
    {
        //if (ArcadeMode.Instance.playerOrArcadeBuild == ArcadeMode.PlayerOrArcade.Player)
        {
            StartCoroutine(SetupAvatar());
        }
    }

    private IEnumerator SetupAvatar()
    {
        yield return new WaitUntil(() => gonePastStart == true);
        PlayerData playerData = PlayersData.Instance.PlayerData;
        int avatar = playerData.Avatar;
        int outfit = playerData.Outfit;
        float scale = playerData.Scale;
        AvatarChanged(avatar);
        OutfitChanged(getOutfit(new int[2] { avatar, outfit }));
        ScaleChanged(scale);
    }

    /// <summary>
    /// Get the avatar corresponding to the given number with the layers for the head set to visible.
    /// </summary>
    /// <param name="number"> A number the avatar corresponds to. </param>
    /// <returns> An avatar with the VRIK script attached. </returns>
    public VRIK getAvatarWithHead(int number)
    {
        return avatarset.getAvatarWithHead(number);
    }

    /// <summary>
    /// Get the avatar corresponding to the given number with the layers for the head set to invisible.
    /// </summary>
    /// <param name="number"> A number the avatar corresponds to. </param>
    /// <returns> An avatar with the VRIK script attached. </returns>
    public VRIK getAvatarWithoutHead(int number)
    {
        return avatarset.getAvatarWithoutHead(number);
    }

    /// <summary>
    /// Get the outfits corresponding to the given index.
    /// </summary>
    /// <param name="index"> The index of the Outfits location in the avatarset. </param>
    /// <returns> An Outfits scriptableobject containing multiple outfit objects. </returns>
    public Outfits getOutfits(int index)
    {
        return avatarset.getOutfits(index);
    }

    /// <summary>
    /// Get the exact outfit corresponding to the given indexes.
    /// </summary>
    /// <param name="indexes"> At least 2 integers large. First integer corresponds to the Outfits, second corresponds to the Outfit within. </param>
    /// <returns> A specific outfit. </returns>
    public Outfit getOutfit(int[] indexes)
    {
        return avatarset.getOutfits(indexes[0]).outfits[indexes[1]];
    }

    public Sprite getFaceFor(int index)
    {
        return avatarset.getFaceFor(index);
    }

    /// <summary>
    /// Called when the player has changed their avatar.
    /// Tells all other players your avatar was changed.
    /// </summary>
    /// <param name="number"> The integer corresponding to their new avatar. </param>
    public void AvatarChanged(int number)
    {
      
    }

    public void AvatarChanged(Outfit outfit)
    {
        int[] number = avatarset.getOutfitIndex(outfit);
       
    }

    /// <summary>
    /// Called when the player changed their outfit.
    /// Tells all other players your outfit was changed.
    /// </summary>
    /// <param name="outfit"> The new Outfit the player is wearing. </param>
    public void OutfitChanged(Outfit outfit)
    {
        int[] number = avatarset.getOutfitIndex(outfit);
       
    }

    /// <summary>
    /// Called when the player changed their scale.
    /// Tells all other players your scale has changed.
    /// </summary>
    /// <param name="newScale"> The value needed to set your scale. </param>
    public void ScaleChanged(float newScale)
    {
    }

  

  


}
