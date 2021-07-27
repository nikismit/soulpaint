using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data of the player's hands and head, easily accesible for other scripts.
/// </summary>
public class LocationDataPlayer : MonoBehaviour {

    public Transform leftHand;
    public Transform rightHand;
    public Transform head;
    public Transform vrtk;
    public VRIK avatar;
 

    /// <summary>
    /// When the avatar is changed set the handgrabAnimation so it can be networked.
    /// </summary>
    /// <param name="avatar">VRIK of the current avatar.</param>
    public void AvatarChanged(VRIK avatar)
    {
        this.avatar = avatar;
       
    }

    /// <summary>
    /// Changes clothes based on the index given.
    /// </summary>
    /// <param name="clothes">At least 2 integers large. First integer corresponds to the Outfits, second corresponds to the Outfit within.</param>
    public void ChangeClothes(int[] clothes)
    {
        avatar.gameObject.GetComponent<ChangeOutfit>().ChangeClothes(AvatarManager.Instance.getOutfit(clothes));
    }
    
    /// <summary>
    /// Teleport the player to the given location.
    /// </summary>
    /// <param name="location"></param>
    public void TeleportTo(Transform location)
    {
        transform.parent.SetPositionAndRotation(location.position, location.rotation);
    }
}
