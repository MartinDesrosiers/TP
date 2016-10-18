using UnityEngine;
using System.Collections;

[System.Serializable]
public class editorPannel : MonoBehaviour {

	public GameObject MenuPannel;
	public GameObject FirstPage;
	public GameObject SavePage;
	public GameObject ExitPage;
	public GameObject ExitSavedPage;
	public GameObject ConfirmPage;

	private bool levelSaved;
	private bool loadOption;

	private bool pannelActive = false;

	public void openClosePannel(){
		levelSaved = false;
		pannelActive = !pannelActive;
		MenuPannel.SetActive (pannelActive);
		deactivatePages ();
		FirstPage.SetActive (true);
	}

	public void setName (string newName){
		levelCode.current.name = newName;
	}

	public void savePage() {
		deactivatePages ();
		SavePage.SetActive (true);
	}

	public void cancel() {
		deactivatePages ();
		FirstPage.SetActive (true);
	}

	public void loadPage () {
		loadOption = true;
		deactivatePages ();
		if (levelSaved) {
			ExitSavedPage.SetActive (true);
		} else {
			ExitPage.SetActive (true);
		}
	}

	public void menuPage(){
		loadOption = false;
		deactivatePages ();
		if (levelSaved) {
			ExitSavedPage.SetActive (true);
		} else {
			ExitPage.SetActive (true);
		}
	}

	public void loadScene (){
		if (loadOption) {
			Application.LoadLevel ("levels");
		} else {
			Application.LoadLevel("mainMenu");
		}
	}

	public void deactivatePages(){
		FirstPage.SetActive (false);
		SavePage.SetActive (false);
		ExitPage.SetActive (false);
		ExitSavedPage.SetActive (false);
		ConfirmPage.SetActive (false);
	}


	public void saveClick() {
		Debug.Log (levelCode.current.name);
		SaveAndLoadLevels.SaveLevel ();
		levelSaved = true;
		cancel ();
	}
}
