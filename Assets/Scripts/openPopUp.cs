using UnityEngine;
using System.Collections;

public class openPopUp : MonoBehaviour {

	private string selectedLevel;
	public GameObject overlay;

	// Use this for initialization
	void Start () {
	
	}
	
	public void onCLick(){
		selectedLevel = this.gameObject.name;
		activatePopUp(selectedLevel);
	}

	public void playLevel(){
		Application.LoadLevel ("Prototype");
	}

	void activatePopUp(string theLevel){
		overlay.SetActive(true);
	}

	void deactivatePopUp(string theLevel){
	}
}
