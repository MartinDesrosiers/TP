using UnityEngine;
using System.Collections;

public class SelectHeroEditor : MonoBehaviour {

	public GameObject heroImporter;
	private string selectedObject;

	public void OnCLick () {
		Debug.Log ("Hero Mode : One button was clicked.");
		HeroEditor heroEditor = heroImporter.GetComponent<HeroEditor> ();
		switch (this.gameObject.name) {
		case "HairButton":
			Debug.Log ("Hero Mode : Hair Selected");
			//heroEditor.objectSelected = "Hair";
			break;
		case "ColorButton":
			Debug.Log ("Edit Mode : Color Selected");
			//heroEditor.objectSelected = "Color";
			break;
		}
	}
}
