using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EditorPanelUI : MonoBehaviour {

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
		this.GetComponent<LevelVisual> ().setName (newName);
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
			SceneManager.LoadScene("Levels");
		} else {
			SceneManager.LoadScene("MainMenu");
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
		levelSaved = true;
		cancel ();
	}
}
