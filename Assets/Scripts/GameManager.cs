using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Gamestate
{
    notStarted =0, 
    WaitforStart,
    Meditation,
    Painting,
   ReadyforEmbody,
    Embody,
    SimonSays,
    Dance,
    PostDance,

}
public class GameManager : MonoBehaviour
{
    public GameObject finalPuppet;
    public GameObject finalMimicPuppet;
    public static GameManager Instance;
 private  Gamestate currentGameState;
    public float savedScale;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public delegate void GamestateChanged(Gamestate newGameState);
    public static event GamestateChanged gamestateChanged;

    // Start is called before the first frame update
    void Start()
    {
       // SetNewGamestate(Gamestate.WaitforStart);
    }

    //    private void StartAudio()
    //    {

    //        SetNewGamestate(Gamestate.WaitforStart);

    //}
    public Gamestate getCurrentGameState()
    {
        return currentGameState;
    }

    public void SetNewGamestate(Gamestate newGameState)
    {
        this.currentGameState = newGameState;
       
        if (gamestateChanged != null)
        {
            gamestateChanged(this.currentGameState);
        }
    }
}
