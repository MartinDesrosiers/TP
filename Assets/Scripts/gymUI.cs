using UnityEngine;
using System.Collections;

public class gymUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onCLick(string objectClicked){
	
		switch (objectClicked) {
		case "back":
			Application.LoadLevel ("mainMenu");
			break;
		case "dressing":
			Application.LoadLevel ("dressingRoom");
			break;
		}
	}
}
