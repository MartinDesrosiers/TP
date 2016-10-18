using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	public int coins;
	public GameObject levelImporter;
	//public GameObject editorUI;
	//public GameObject heroUI;

	public string[] unlockedLevelObjects;
	public string[] unlockedEnemyObjects;
	public string[] unlockedCollectibleObjects;
	public string[] unlockedTrapObjects;
	public string[] unlockedGroundObjects;
	public string[] unlockedEditObjects;

	private Vector3 initialHeroPosition;

	// Use this for initialization
	void Start () {
		coins = 0;
		//editorUI.SetActive (false);
		//heroUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void coinCollected() {
		coins++;
	}
}
