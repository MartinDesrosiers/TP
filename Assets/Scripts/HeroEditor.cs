using UnityEngine;
using System.Collections;

public class HeroEditor : MonoBehaviour {

	public GameObject heroSprite;

	[HideInInspector] public string objectSelected;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (objectSelected == "Hair") {
				Debug.Log ("Hair were changed.");
		} else if (objectSelected == "Color") {
			Debug.Log ("Color was changed.");
			SpriteRenderer heroRenderer = heroSprite.GetComponent<SpriteRenderer> ();
			heroRenderer.color = new Color(1,0,01);
		}
	}
}
