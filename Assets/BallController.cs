using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameMode.IsPlaying) return;
        transform.position += Vector3.up * _gameMode.GameConfiguration.BallVelocityFormula(_size, _gameMode.NormalizedGameProgress) * Time.deltaTime;
    }

    private BallsGameMode _gameMode;

    private float _size;

    public void Init(BallsGameMode gameMode, float x, float size)
    {
	    _gameMode = gameMode;
	    transform.position = new Vector3(x * (_gameMode.ScreenSize.x - size) * 2 - (_gameMode.ScreenSize.x - size), -_gameMode.ScreenSize.y);
        transform.localScale = new Vector3(size, size, size);
	    _size = size;

    }
}
