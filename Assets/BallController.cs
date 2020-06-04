using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{ 
	private BallsGameMode _gameMode;
	private float _size;
	private SpriteRenderer _renderer;
	private const float INIT_POPTIME = 0.15f;
	private bool _isAlive;

	void Update()
	{
		if (!_gameMode.isActiveAndEnabled) Destroy(gameObject);
		if (!_gameMode.IsPlaying || !_isAlive) return;
		transform.position += Vector3.up * _gameMode.GameConfiguration.BallVelocityFormula(_size, _gameMode.NormalizedGameProgress) * Time.deltaTime;
	}


	public void Init(BallsGameMode gameMode, float x, float size)
	{
		_gameMode = gameMode;
		_size = size;

		transform.position = new Vector3(x * (_gameMode.ScreenSize.x - size/2) * 2 - (_gameMode.ScreenSize.x - size/2), -_gameMode.ScreenSize.y - size);
		transform.localScale = new Vector3(size, size, size);
		
		_renderer = GetComponent<SpriteRenderer>();
		_renderer.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);

		_isAlive = true;
	}


	public void OnTapped()
	{
		if (!_isAlive) return;
		GetComponent<Collider2D>().enabled = false;
		StartCoroutine(Pop());
		_gameMode.SendMessage("OnBallPop", _size);
		_isAlive = false;
	}


	IEnumerator Pop()
	{
		float popTime = INIT_POPTIME;
		var size = transform.localScale;

		while (popTime > 0)
		{
			popTime -= Time.deltaTime;

			transform.localScale = size * Mathf.Lerp(2, 1, popTime / INIT_POPTIME);

			var color = _renderer.color;
			color.a = Mathf.Lerp(1, 0, popTime / INIT_POPTIME);
			_renderer.color = color;

			yield return null;
		}

		Destroy(gameObject);
	}
}
