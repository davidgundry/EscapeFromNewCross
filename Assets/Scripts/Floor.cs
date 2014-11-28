using UnityEngine;
using System.Collections;

// Sets the floor to the correct size for the Maze
public class Floor : MonoBehaviour
{

		public Material floorMaterial;

		public void setSize (int newHeight, int newWidth)
		{
				transform.localScale = new Vector3 (newHeight, 0, newWidth);
				renderer.material = floorMaterial;
		}
	

}
