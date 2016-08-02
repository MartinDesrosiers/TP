using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	public int coins;
	public GameObject LevelEditorObject;
	//public GameObject editorUI;
	//public GameObject heroUI;

	private Vector3 initialHeroPosition;

	// Use this for initialization
	void Start () {
		LevelEditor levelEditor = LevelEditorObject.GetComponent<LevelEditor> ();
		coins = 0;
		levelEditor.setLevelArray ();
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
