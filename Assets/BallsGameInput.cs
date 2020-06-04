using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallsGameInput : MonoBehaviour
{

	private BallsGameMode GameModeController;

	void Start()
	{
		GameModeController = GetComponent<BallsGameMode>();
	}


	void Update()
	{
		if (GameModeController.IsPlaying && Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var hits = Physics2D.OverlapPointAll(pos);
			foreach (var raycastHit in hits)
			{
				raycastHit.transform.SendMessage("OnTapped");
			}
		}
	}
}
