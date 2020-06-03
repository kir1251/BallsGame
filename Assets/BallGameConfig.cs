using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "BallGameStats", order = 51)]
public class BallGameConfig : ScriptableObject
{
	/// <summary>
	/// Spawn delay in seconds
	/// </summary>
	[SerializeField]
	public float BallSpawnDelay = 3;

	/// <summary>
	/// Time to play the level
	/// </summary>
	[SerializeField]
	public float GameTime = 90;

	/// <summary>
	/// Size of a smallest ball possible
	/// </summary>
	[SerializeField]
	public float MinBallSize = .66f;

	/// <summary>
	/// Size of a bigger ball possible
	/// </summary>
	[SerializeField]
	public float MaxBallSize = 2.0f;

	/// <summary>
	/// Base velocity of a ball of size 1 in the beginning of a game
	/// </summary>
	[SerializeField]
	public float InitBallVelocity = 1.0f;

	/// <summary>
	/// time exponent base for velocity and score formulas
	/// </summary>
	[SerializeField]
	float TimeSigma = 3.0f;

	/// <summary>
	/// size exponent base for velocity and score formulas
	/// </summary>
	[SerializeField]
	float SizeSigma = .5f;

	/// <summary>
	/// multiplier for size exponent 
	/// </summary>
	[SerializeField]
	float SizePhi = 2.0f;

	public float BallVelocityFormula(float size, float levelProgress)
	{
		return InitBallVelocity * Mathf.Pow(SizeSigma, size * SizePhi) * Mathf.Pow(TimeSigma, levelProgress);
	}

	public float BallScoreFormula(float size, float levelProgress)
	{
		return Mathf.Pow(size, 2);
	}



}