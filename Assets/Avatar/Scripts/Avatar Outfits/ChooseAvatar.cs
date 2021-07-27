using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RootMotion.FinalIK;

public class ChooseAvatar : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Reference to the AvatarController object.")]
    private VRAvatarController avatarController;
    [SerializeField]
    [Tooltip("Placeholder object of the character standing on the platform.")]
    private GameObject placeHolder;

    private List<Outfit> savedOutfits;
    private int currentAvatar = -1;
    private int currentOutfit = 0;
    private GameObject avatar;
    private bool local;

    /// <summary>
    /// Set up placeholder avatar.
    /// Also checks if playing local or not.
    /// </summary>
    void Start()
    {
      

        try
        {
            currentAvatar = PlayersData.Instance.PlayerData.Avatar;
            currentOutfit = PlayersData.Instance.PlayerData.Outfit;
        }
        catch (System.Exception)
        {
            local = true;
            if (avatarController.avatars.Count > 0 && avatarController.outfits.Count > 0)
            {
                currentAvatar = 0;
                currentOutfit = 0;
            }
        }

        savedOutfits = new List<Outfit>();
        StartCoroutine(WaitForVRSetup());
    }

    /// <summary>
    /// Change avatar of the player based on current Avatar.
    /// Also changes the outfit.
    /// </summary>
    public void PickAvatar()
    {
        try
        {
            if (avatarController.indexActualAvatar != currentAvatar)
            {
                avatarController.ChangeAvatar(currentAvatar);
            }

            ChangeOutfit();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message + " " + avatarController + "    " + avatarController.indexActualAvatar + "  " + currentAvatar);
        }

    }

    /// <summary>
    /// Setup the place holder.
    /// </summary>
    private void SetPlaceholder()
    {
        savedOutfits.Clear();
        if (local)
        {
            avatar = Instantiate(avatarController.miniatures[currentAvatar], placeHolder.transform);
            savedOutfits.AddRange(avatarController.outfits[currentAvatar].outfits);
        }
        else
        {
            avatar = Instantiate(AvatarManager.Instance.GetMiniature(currentAvatar), placeHolder.transform);
            savedOutfits.AddRange(AvatarManager.Instance.getOutfits(currentAvatar).outfits);
        }
        avatar.transform.localScale = new Vector3(1, 1, 1);
    }

    private void SetPlaceholder(Outfit outfit)
    {
        savedOutfits.Clear();
        if (local)
        {
            avatar = Instantiate(outfit.outfitMiniature, placeHolder.transform);
            savedOutfits.AddRange(avatarController.outfits[currentAvatar].outfits);
        }
        else
        {
            avatar = Instantiate(outfit.outfitMiniature, placeHolder.transform);
            savedOutfits.AddRange(AvatarManager.Instance.getOutfits(currentAvatar).outfits);
        }
        avatar.transform.localScale = new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Change outfit of the player.
    /// </summary>
    public void ChangeOutfit()
    {
        /*try
        {*/
            GameObject avatarObject = avatarController.actualAvatarVRIK.gameObject;
            ChangeOutfit changeOutfit = avatarObject.GetComponent<ChangeOutfit>();
            Outfit chosenOutfit = savedOutfits[currentOutfit];

            if (chosenOutfit.prefabOrTextureOutfit == Outfit.PrefabOrTextureOutfit.Texture)
            {
                changeOutfit.ChangeClothes(chosenOutfit);
                if (!local)
                {
                    AvatarManager.Instance.OutfitChanged(chosenOutfit);
                    PlayersData.Instance.PlayerData.Outfit = currentOutfit;
                }
            } else
            {
                avatarController.ChangeAvatar(chosenOutfit.prefab, chosenOutfit);
                AvatarManager.Instance.OutfitChanged(chosenOutfit);
                PlayersData.Instance.PlayerData.Outfit = currentOutfit;
            }
        //}
        /*catch (System.Exception)
        {
            Debug.Log("Outfit could not be changed.");
        }*/
    }

    /// <summary>
    /// Changes the clothes of the Placeholder.
    /// </summary>
    private void ChangeClothesPlaceholder()
    {
        if (savedOutfits[currentOutfit].prefabOrTextureOutfit == Outfit.PrefabOrTextureOutfit.Texture)
        {
            ChangeOutfit changeOutfit = avatar.GetComponent<ChangeOutfit>();
            changeOutfit.ChangeClothes(savedOutfits[currentOutfit]);
        } else
        {
            Destroy(avatar.gameObject);
            SetPlaceholder(savedOutfits[currentOutfit]);
        }
    }

    /// <summary>
    /// General method for each button
    /// Having next or previous as a boolean can cause confusion
    /// </summary>
    /// <param name="nextOrPrevious">Decides if the next of the previous outfit or avatar is picked.</param>
    /// <param name="avatarOrOutfitbool">Decides if you are cycling through outfits or avatars.</param>
    public void NextOrPrevious(bool nextOrPrevious, bool avatarOrOutfitbool)
    {
        int value = (nextOrPrevious) ? 1 : -1;
        switch (avatarOrOutfitbool)
        {
            case true:
                currentAvatar = currentAvatar + value;
                currentOutfit = 0;
                if (local)
                {
                    SetTrackers(ref currentAvatar, avatarController.avatars.Count);
                }
                else
                {
                    SetTrackers(ref currentAvatar, AvatarManager.Instance.avatarset.listOfAvatars.Count);
                }
                Destroy(avatar.gameObject);
                SetPlaceholder();
                break;
            case false:
                currentOutfit = currentOutfit + value;
                SetTrackers(ref currentOutfit, savedOutfits.Count);
                ChangeClothesPlaceholder();
                break;
        }
        PickAvatar();
    }


    /// <summary>
    /// Track current index of a list.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="listCount"></param>
    private void SetTrackers(ref int current, int listCount)
    {
        if (current >= listCount)
        {
            current = 0;
        }
        else if (current < 0)
        {
            current = listCount - 1;
        }
    }

    /// <summary>
    /// Waits until the avatarcontroller has been setup.
    /// After the wait set up the placeholder.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForVRSetup()
    {
        yield return new WaitUntil(() => avatarController != null);
        yield return new WaitUntil(() => avatarController.indexActualAvatar > -1);
        if (avatarController != null)
        {
            currentAvatar = avatarController.indexActualAvatar;
        }
        Invoke("SetPlaceholder", 0.5f);
        Invoke("ChangeClothesPlaceholder", 1f);
    }
}
