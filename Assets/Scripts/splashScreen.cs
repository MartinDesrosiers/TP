using UnityEngine;
using System.Collections;

public class splashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Example());
	}
	
	// Update is called once per frame
	IEnumerator Example() {
		yield return new WaitForSeconds(3);
		Application.LoadLevel ("mainMenu");
	}
}
