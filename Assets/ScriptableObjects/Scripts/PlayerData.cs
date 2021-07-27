using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData", order = 2)]
public class PlayerData : ScriptableObject
{

    public int Avatar;
    public int Outfit;
    public float Scale;

   

    public void ResetData()
    {
        this.Avatar = 0;
        this.Outfit = 0;
        this.Scale = 1;
    }
}
