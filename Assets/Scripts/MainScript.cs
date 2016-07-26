using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	public int coins;

	private Vector3 initialHeroPosition;

	// Use this for initialization
	void Start () {
		coins = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void coinCollected() {
		coins++;
	}
}
