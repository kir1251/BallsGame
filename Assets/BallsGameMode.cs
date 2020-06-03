using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallsGameMode : IGameMode
{

	public BallGameConfig GameConfiguration;

	public BallController BallPrefab;

    // Start is called before the first frame update
    void Start()
    {
	    float cameraHeight = Camera.main.orthographicSize;
	    ScreenSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight);
    }

    public Vector2 ScreenSize;

    public float NormalizedGameProgress => 1 - Timer / GameConfiguration.GameTime;

    // Update is called once per frame
    void Update()
    {
	    if (!IsPlaying) return;
	    Timer -= Time.deltaTime;
	    if (Timer < 0)
	    {
            OnGameCompleted?.Invoke(this);
            return;
            IsPlaying = false;
	    }

        //Spawn new ball
	    BallTimer -= Time.deltaTime;
	    if (BallTimer <= 0)
	    {
		    BallTimer += GameConfiguration.BallSpawnDelay;
            SpawnBall();
	    }


    }

    void SpawnBall()
    {
	    var ball = Instantiate(BallPrefab);
	    ball.Init(this, Random.value, Mathf.Lerp(GameConfiguration.MinBallSize, GameConfiguration.MaxBallSize, Random.value));
    }

    public float Timer;
    public float BallTimer;

    public override void StartGame()
    {
	    IsPlaying = true;
	    Timer = GameConfiguration.GameTime;
	    BallTimer = 0;
    }

    public override void ToggleGamePause()
    {
	    IsPlaying = !IsPlaying;
    }
}
