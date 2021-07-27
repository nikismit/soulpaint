using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOffset", menuName = "Create Avatar Offset", order = 1)]
public class AvatarOffset : ScriptableObject {

    [Header("Head")]
    public Vector3 headPosition;
    public Vector3 headRotation;

    [Header("Left Hand")]
    public Vector3 leftHandPosition;
    public Vector3 leftHandRotation;

    [Header("Right Hand")]
    public Vector3 rightHandPosition;
    public Vector3 rightHandRotation;
}