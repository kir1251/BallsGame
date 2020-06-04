using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreDisplay : MonoBehaviour
{
	protected Text _Text => _text ?? (_text = GetComponent<Text>());
	private Text _text;
	public IGameMode GameMode;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		_Text.text = $"{GameMode.GetTopScore()}";
	}
}
