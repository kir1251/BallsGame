using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallsGameInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    GameModeController = GetComponent<BallsGameMode>();
    }

    private BallsGameMode GameModeController;

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
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
