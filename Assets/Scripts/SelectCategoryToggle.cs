using UnityEngine;
using System.Collections;

public class SelectCategoryToggle : MonoBehaviour {

	public GameObject menuImporter;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick(){
		menuColorSprite topMenu = menuImporter.GetComponent<menuColorSprite> ();
		topMenu.CategoryChanged(this.gameObject.name);
	}
}
