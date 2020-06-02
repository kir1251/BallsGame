using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameMode : MonoBehaviour
{
	public abstract void StartGame();

	public abstract void ToggleGamePause();

	public Action<IGameMode> OnGameCompleted;

	public bool IsPlaying { get; protected set; }
}
