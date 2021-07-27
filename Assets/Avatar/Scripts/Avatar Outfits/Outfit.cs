using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Outfit
{
    public enum PrefabOrTextureOutfit { Texture, Prefab }
    public PrefabOrTextureOutfit prefabOrTextureOutfit;

    [Header("Texture outfit")]
    public List<Texture> texture;
    public Color color;

    [Header("Prefab outfit")]
    public GameObject prefab;

    public GameObject prefabWithHead;

    public GameObject outfitMiniature;

    public Sprite characterFace;
    public Sprite frozencharacterFace;

    public Outfit(List<Texture> texture, Color color)
    {
        this.texture = texture;
        this.color = color;
    }
}
