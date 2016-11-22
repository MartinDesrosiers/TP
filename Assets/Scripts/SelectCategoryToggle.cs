using UnityEngine;
using System.Collections;

public class SelectCategoryToggle : MonoBehaviour {

	public GameObject menuImporter;

	public void OnClick(){
		LevelEditorUI topMenu = menuImporter.GetComponent<LevelEditorUI> ();
		topMenu.CategoryChanged(this.gameObject.name);
	}
}
