using UnityEngine;
using System.Collections;

public class SelectObjectEditor : MonoBehaviour {

	public GameObject editorImporter;
	private string selectedObject;

	public void OnCLick () {
		Debug.Log ("Edit Mode : One button was clicked.");
		LevelEditor levelEditor = editorImporter.GetComponent<LevelEditor> ();
		switch (this.gameObject.name) {
		case "TileButton":
			Debug.Log ("Edit Mode : Tiles Selected");
			levelEditor.objectSelected = "Tile";
			break;
		case "CoinButton":
			Debug.Log ("Edit Mode : Coin Selected");
			levelEditor.objectSelected = "Coin";
			break;
		}
	}
}
