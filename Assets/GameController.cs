using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GameController : MonoBehaviour
{

    /// <summary>
    /// Singleton
    /// </summary>
    public static GameController Instance => _instance ?? (_instance = FindObjectOfType<GameController>());
    private static GameController _instance;

    /// <summary>
    /// Are we currently in active game stage
    /// </summary>
    public bool IsPlaying
    {
	    get => GameMode.IsPlaying;
    }
    


    public IGameMode GameMode;

    // Start is called before the first frame update
    void Start()
    {
        GameMode.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
