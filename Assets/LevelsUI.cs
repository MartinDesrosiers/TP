using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelsUI : MonoBehaviour {

	public GameObject node;

	// Use this for initialization
	void Start () {
		SaveAndLoadLevels.LoadLevel ();	

		foreach (levelCode level in SaveAndLoadLevels.savedLevels) {
			Debug.Log(level.name);
			GameObject levelNode = (GameObject)Instantiate (node,gameObject.transform);
			levelNode.transform.Find("Text").GetComponent<Text>().text = level.name;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void addNode(){
	
	}
}
