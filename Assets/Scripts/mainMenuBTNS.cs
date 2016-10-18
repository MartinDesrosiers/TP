using UnityEngine;
using System.Collections;

public class mainMenuBTNS : MonoBehaviour {

	private string selectedButton;

	// Use this for initialization
	void Start () {
	
	}
	
	public void onClick (){
		selectedButton = this.gameObject.name;
		Debug.Log (selectedButton+" was clicked");
		switch (selectedButton) {
		case "runBTN":
			Application.LoadLevel("runMap");
		break;
		case "gymBTN":
			Application.LoadLevel ("gym");
			break;
		case "backBTN":
			Application.LoadLevel ("mainMenu");
			break;
		case "facebook":
			Application.OpenURL ("https://www.facebook.com/");
			break;
		case "twitch":
			Application.OpenURL ("https://www.twitch.tv/");
			break;
		case "youtube":
			Application.OpenURL ("https://gaming.youtube.com/");
			break;
		}
	}
}
