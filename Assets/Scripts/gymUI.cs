using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GymUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onCLick(string objectClicked){
	
		switch (objectClicked) {
		case "back":
			SceneManager.LoadScene ("MainMenu");
			break;
		case "dressing":
			SceneManager.LoadScene ("DressingRoom");
			break;
		}
	}
}
