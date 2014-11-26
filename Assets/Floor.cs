using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public void setSize(int newHeight,int newWidth) {
				transform.localScale = new Vector3 (newHeight, 0, newWidth);
		}
	

}
