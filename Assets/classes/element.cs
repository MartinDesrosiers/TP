using UnityEngine;
using System.Collections;

[System.Serializable]
public class element {

	public static element current;

	public string type;

	public element(){
		this.type = "Forest";
		applyAttributes(type);
	}

	public void changeElement(string newElement) {
		this.type = newElement;
		applyAttributes(type);
	}

	private void applyAttributes(string newType){
	
	}
}
