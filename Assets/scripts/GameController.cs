using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject startGUI;
	public GameObject pauseGUI;
	public GameObject gameGUI;
	public GUIText scoreText;
	
	public bool started = false;
	public bool gameOver = false;
	public bool paused = false;
	
	public int score = 0;


	// Use this for initialization
	void Start () {
	  pauseGUI.SetActiveRecursively(false);
	  startGUI.SetActiveRecursively(true);
	  gameGUI.SetActiveRecursively(false);
	}
	
	// Update is called once per frame
	void Update () {
	  if (Input.GetKeyDown (KeyCode.Space))
	  {
	    if (!started)
	    {
	      started = true;
	      startGUI.SetActiveRecursively(false);
	      gameGUI.SetActiveRecursively(true);
	    }
	    else if (paused)
	    {
	      Time.timeScale = 1;
	      paused = false;
	      pauseGUI.SetActiveRecursively(false);
	      gameGUI.SetActiveRecursively(true);
	    }
	    else
	    {
	      paused = true;
	      Time.timeScale = 0;
	      pauseGUI.SetActiveRecursively(true);
	      gameGUI.SetActiveRecursively(false);
	    }
	  }
	}
	
	public void UpdateScore()
	{
	  scoreText.text = "Score: " + score.ToString();
	}
}
