using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameMode : MonoBehaviour
{
	public abstract void StartGame();

	public abstract void ToggleGamePause();

	public Action<IGameMode> OnGameCompleted;

	public virtual bool IsPlaying { get; protected set; }

	public abstract float GetScore();
	public abstract float GetTopScore();

	public abstract float GetTimer();

	public string ModeName;
}
