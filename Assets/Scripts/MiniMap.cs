using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public GameObject player;
	public GameObject mainMap;
	private MazeManager mazeManager;
	private bool visible;
	// Use this for initialization
	void Start () {
		mazeManager = mainMap.GetComponent<MazeManager> ();
		visible = false;
	}
	public void setVisible(bool isVisible) {
				visible = isVisible;
		}
	
	// Update is called once per frame
	void Update () {
		if (visible) {
						Vector3 playerPos = player.transform.position;

						Vector2 cellPos = new Vector2 (Mathf.Round (playerPos.x + (mazeManager.width / 2) - 0.5f), Mathf.Round (playerPos.z + (mazeManager.height / 2) - 0.5f));

						//Debug.Log ("The player is at " + player.transform.position.ToString () + "cell=" + cellPos);
						if (!mazeManager.currentMaze.hasVisited ((int)cellPos.x, (int)cellPos.y)) {
								Debug.Log ("Draw cell at " + cellPos.ToString ());
								mazeManager.currentMaze.setVisited ((int)cellPos.x, (int)cellPos.y, true);
						}
						
				}

	}
}
