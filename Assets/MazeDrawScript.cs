using UnityEngine;
using System.Collections;

//Flags
public enum Directions
{
	N = 1,
	S = 2,
	E = 4,
	W = 8
}

public class MazeDrawScript : MonoBehaviour {

	public GameObject wallPrefab;
	public int width;
	public int height;
	public int cellHeight;
	public int cellWidth;
	private float halfCellWidth, halfCellHeight;
	private int[,] cells = new int[,]{{9,3,1,5},{8,8,0,4},{8,0,6,4},{10,2,2,6}};
	// Use this for initialization
	void Start () {
		halfCellWidth = cellWidth / 2.0f;
		halfCellHeight = cellHeight / 2.0f;
		Vector3 Npos = new Vector3 (0, 0, -halfCellHeight);
		Vector3 Spos = new Vector3 (0, 0, halfCellHeight);
		Vector3 Wpos = new Vector3 (-halfCellWidth, 0, 0);
		Vector3 Epos = new Vector3 (halfCellWidth, 0, 0);
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				int contents = cells[y,x];
				if (hasFlag(contents,Directions.N)) {
					makeWall(x,y,Npos,Vector3.zero);
				}
				if (hasFlag(contents,Directions.S)) {
					makeWall(x,y,Spos,Vector3.zero);
				}
				if (hasFlag(contents,Directions.W)) {
					makeWall(x,y,Wpos,new Vector3(0,90f,0));
				}
				if (hasFlag(contents,Directions.E)) {
					makeWall(x,y,Epos,new Vector3(0,90f,0));
				}
			}
		}
	
	}
	bool hasFlag(int value,Directions flag) {
				return ((value & (int)flag)>0);
		}
	void makeWall(int x, int y,Vector3 offset,Vector3 rotate) {
		Vector3 position = new Vector3 (cellWidth * x, 0, cellHeight * y) + transform.position;
				GameObject newWall = (GameObject)Instantiate (wallPrefab, position+offset, Quaternion.Euler (rotate));
				newWall.transform.parent = transform;
		}

	
	// Update is called once per frame
	void Update () {
	
	}
}
