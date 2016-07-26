using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

	public GameObject Tile;
	public GameObject Coin;

	public string objectSelected;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Vector3 MouseXY = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			MouseXY.x =Mathf.Floor(MouseXY.x)+0.5f;
			MouseXY.y =Mathf.Floor(MouseXY.y)+0.5f;
			MouseXY.z =0;
			Debug.Log ("An object was added at these coordinates : " + MouseXY);
			if (objectSelected == "Tile") {
				Instantiate (Tile, MouseXY, transform.rotation);
			} else if (objectSelected == "Coin") {
				Instantiate (Coin, MouseXY, transform.rotation);
			}
		}
	}
}
