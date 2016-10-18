using UnityEngine;
using System.Collections;

public class HeroAppearance : MonoBehaviour {

	public GameObject[] bodyParts;
	public GameObject[] equipments;
	public GameObject hairCut;

	public Sprite[] maleBody;
	public Sprite[] femaleBody;

	public Sprite[] eyes;
	public Sprite[] mouths;
	public Sprite[] hair;

	public Sprite[] maleHats;
	public Sprite[] maleShirts;
	public Sprite[]	maleGloves;
	public Sprite[] malePants;
	public Sprite[]	maleBoots;

	public Sprite[] femaleHats;
	public Sprite[] femaleShirts;
	public Sprite[] femaleGloves;
	public Sprite[] femalePants;
	public Sprite[]	femaleBoots;

	public Sprite[] weapons;

	public Color primaryColor;
	public Color secondaryColor;
	public Color skinColor;
	public Color hairColor;

	public bool characterSex;
	private bool previousSex;

	// Use this for initialization
	void Start () {
		changeSex(characterSex);
		changeSkin(skinColor);
		changeHair ("standardHair", hairColor);
	}

	public void changeSex(bool isMale) {
		
		if (isMale) {
			for (int i = 0; i < bodyParts.Length; i++) {
				bodyParts [i].GetComponent<SpriteRenderer> ().sprite = maleBody [i];
			}
		}
		else {
			for (int i = 0; i < bodyParts.Length; i++) {
			bodyParts[i].GetComponent<SpriteRenderer>().sprite = femaleBody [i];
			}
		}
		changeSkin(skinColor);
	}

	public void changeSkin(Color theSkinColor){
		for (int i = 0; i < bodyParts.Length; i++) {
			bodyParts [i].GetComponent<SpriteRenderer> ().color = theSkinColor;
		}
	}

	public void changeClothes(string clotheType, string clotheID, Color primaryClothesColor, Color secondaryClothesColor){

	}

	public void changeHair(string hairID, Color theHairColor) {
		hairCut.GetComponent<SpriteRenderer> ().color = theHairColor;
	}

	public void changeClothesColor(Color primaryClothesColor, Color secondaryClothesColor){
		for(int i = 0; i < equipments.Length; i++){
			equipments [i].GetComponent<SpriteRenderer> ().color = primaryClothesColor;
		}
	}
}
