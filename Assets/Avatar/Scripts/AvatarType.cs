using UnityEngine;

public class AvatarType : MonoBehaviour
{
    public enum AvatarTypeEnum { Kid, Teen }
    public AvatarTypeEnum CurrentAvatarType;
    [SerializeField]
    public bool gothGirl = false;

    public float xMultiplier = 1.08f , zMultiplier = 1.27f;
}
