using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectObjectEditor : MonoBehaviour {

	public GameObject editorImporter;
	public GameObject cameraImporter;

	private string selectedObject;
	private string previousSelection;

	public void OnCLick () {
		selectedObject = this.gameObject.name;

		if(selectedObject == previousSelection){
			selectedObject = "None";
		}

		Debug.Log ("Edit Mode : "+selectedObject+" was clicked.");

		if (selectedObject == "Cursor") {
		
		} else {
			LevelEditorController levelEditor = editorImporter.GetComponent<LevelEditorController> ();
			levelEditor.objectSelected = selectedObject;
		}
		previousSelection = selectedObject;
	}

	public void undoClick() {
		LevelEditorController levelEditor = editorImporter.GetComponent<LevelEditorController> ();
		levelEditor.undoChanges ();
	}

	public void destroyClick() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
