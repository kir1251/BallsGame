using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	/// <summary>
	/// Singleton
	/// </summary>
	public static GameController Instance => _instance ?? (_instance = FindObjectOfType<GameController>());
	private static GameController _instance;

	private IGameMode _gameMode;

	/// <summary>
	/// Are we currently in active game stage
	/// </summary>
	public bool IsPlaying
	{
		get => _gameMode.IsPlaying;
	}


	void Start()
	{
		
	}



	public Text TimerText;
	public Text ScoreText;

	public RectTransform Menu;
	public RectTransform ScorePanel;
	//public 

	void Update()
	{
		if (_gameMode)
		{
			TimerText.text = $"{_gameMode.GetTimer():0.00}";
			ScoreText.text = $"{_gameMode.GetScore():0}";
		}
	}

	public void StartGame(IGameMode gameMode)
	{
		Menu.gameObject.SetActive(false);
		ScorePanel.gameObject.SetActive(true);
		_gameMode = gameMode;
		_gameMode.gameObject.SetActive(true);
		_gameMode.OnGameCompleted += EndGame;
		_gameMode.StartGame();
		
	}

	public void EndGame(IGameMode gameMode)
	{
		Menu.gameObject.SetActive(true);
		ScorePanel.gameObject.SetActive(false);
		_gameMode.gameObject.SetActive(false);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
