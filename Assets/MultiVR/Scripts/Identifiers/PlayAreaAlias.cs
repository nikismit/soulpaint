using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Identifier script for the play area alias.
/// </summary>
public class PlayAreaAlias : MultiVRAlias
{
    public PlayArea playArea;

    public void SetPlayArea()
    {
        StartCoroutine(GetPlay());
    }

    private IEnumerator GetPlay()
    {
        yield return new WaitForSeconds(1f);
        playArea = GetComponentInChildren<PlayArea>();
    }
}
