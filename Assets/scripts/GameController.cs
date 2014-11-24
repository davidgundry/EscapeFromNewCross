using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject startGUI;
	public GameObject pauseGUI;
	public GameObject gameGUI;
	public GameObject endGUI;
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
	  endGUI.SetActiveRecursively(false);
	  Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
	  if (Input.GetKeyDown (KeyCode.Space))
	  {
	    if (gameOver)
	    {
	      started = false;
	      gameOver = false;
	      startGUI.SetActiveRecursively(true);
	      endGUI.SetActiveRecursively(false);
	      score = 0;
	      Application.LoadLevel(Application.loadedLevel);
	    }
	    else if (!started)
	    {
	      Time.timeScale = 1;
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
	  
	  if (score == 4)
	  {
	    gameOver = true;
	    endGUI.SetActiveRecursively(true);
	    Time.timeScale = 0;
	  }
	}
	
	public void UpdateScore()
	{
	  scoreText.text = "Score: " + score.ToString();
	}
}
