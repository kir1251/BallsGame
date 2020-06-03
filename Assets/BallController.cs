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
    private SpriteRenderer _renderer;

    public void Init(BallsGameMode gameMode, float x, float size)
    {
	    _gameMode = gameMode;
	    transform.position = new Vector3(x * (_gameMode.ScreenSize.x - size/2) * 2 - (_gameMode.ScreenSize.x - size/2), -_gameMode.ScreenSize.y);
        transform.localScale = new Vector3(size, size, size);
	    _size = size;
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
    }

    public void OnTapped()
    {
	    GetComponent<Collider2D>().enabled = false;
	    StartCoroutine(Pop());
        _gameMode.SendMessage("BallPop", _size);
    }

    private const float INIT_POPTIME = 0.15f;

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
