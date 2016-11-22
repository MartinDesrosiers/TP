using UnityEngine;
using System.Collections;

[System.Serializable]
public class ElementData {

	public static ElementData current;

	public string type;

	public ElementData(){
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
