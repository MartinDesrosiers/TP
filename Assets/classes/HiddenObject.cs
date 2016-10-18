using UnityEngine;
using System.Collections;

[System.Serializable]
public class HiddenObject {

	public GameObject unlockableObject;

	public HiddenObject () {
		this.unlockableObject = new GameObject ();
	}

	public HiddenObject (string assignedObject, string objectType){
	
	}
}
