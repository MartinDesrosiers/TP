using UnityEngine;
using System.Collections;

public class SelectObjectEditor : MonoBehaviour {

	public GameObject editorImporter;
	private string selectedObject;
	private string previousSelection;

	public void OnCLick () {
		selectedObject = this.gameObject.name;

		if(selectedObject == previousSelection){
			selectedObject = "None";
		}

		//Debug.Log ("Edit Mode : One button was clicked.");
		LevelEditor levelEditor = editorImporter.GetComponent<LevelEditor> ();
		levelEditor.objectSelected = selectedObject;

		previousSelection = selectedObject;
	}
}
