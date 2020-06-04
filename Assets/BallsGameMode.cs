using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallsGameMode : IGameMode
{

	public BallGameConfig GameConfiguration;
	public BallController BallPrefab;

	public float BallTimer;

	public Vector2 ScreenSize;
	public float NormalizedGameProgress => 1 - Timer / GameConfiguration.GameTime;

	public float Timer { get; protected set; }
	public float Score { get; protected set; }


	// Start is called before the first frame update
	void Start()
	{
		float cameraHeight = Camera.main.orthographicSize;
		ScreenSize = new Vector2(cameraHeight * Camera.main.aspect, cameraHeight);
	}

	void GameCompleted()
	{
		OnGameCompleted?.Invoke(this);
		IsPlaying = false;
		float Highscore = GetTopScore();
		if (Score > Highscore)
		{
			PlayerPrefs.SetFloat($"{ModeName}:highscore", Score);
		}

	}

	void Update()
	{
		Score += _scoreAdd * _ballsPopped;
		_scoreAdd = 0;
		_ballsPopped = 0;

		if (!IsPlaying) return;

		//Check if game completed
		Timer -= Time.deltaTime;
		if (Timer < 0)
		{
			GameCompleted();
			return;
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


	public override void StartGame()
	{
		IsPlaying = true;
		Timer = GameConfiguration.GameTime;
		Score = 0;
		BallTimer = 0;
	}


	public override void ToggleGamePause()
	{
		IsPlaying = !IsPlaying;
	}

	public override float GetScore()
	{
		return Score;
	}

	public override float GetTopScore()
	{
		return PlayerPrefs.GetFloat($"{ModeName}:highscore", 0);
	}

	public override float GetTimer()
	{
		return Mathf.Max(Timer, 0);
	}

	//stuff for C-c-combo!
	private float _scoreAdd = 0;
	private int _ballsPopped = 0;

	void OnBallPop(float ballSize)
	{
		_scoreAdd += GameConfiguration.BallScoreFormula(ballSize);
		_ballsPopped++;
	}
}
