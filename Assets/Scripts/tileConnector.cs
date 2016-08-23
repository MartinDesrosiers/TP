using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tileConnector : MonoBehaviour {

	public GameObject EditorImporter;

	public bool isConnectable(string objectType) {
		switch (objectType) {
		case ("Tile"):
			return true;
			break;
		case ("Coin"):
			return false;
			break;
		default :
			return false;
			break;
		}
	}

	public bool[] detectSurroundingTiles (string objectType, int levelRow, int levelColumn, List<List<string>> levelArray) {

		bool[] surroundingTiles = new bool[9];

		//Debug.Log ("Object type id is :"+levelArray [levelRow] [levelColumn]);

		//For a ground object type connectable => Detect all surrounding tiles
		if (objectType == "Tile") {
			//Stores in an array if the surrounding tiles are connected (bool)
			int t = 0;
			//r for rows and c for columns around the tile (-1,0,1)
			for (int r = 1; r > -2; r--) {
				for (int c = -1; c < 2; c++) {
					//Debug.Log ("Position relative vérifiée = Row :"+r+"("+(levelRow + r)+")"+" Column :"+c+"("+(levelColumn + c)+")"+" == "+levelArray[levelRow + r][levelColumn + c]);
					//Except for the tile itself
					//if (r != 0 && c != 0) {
					//If they are the same = true
					if (objectType == levelArray [levelRow + r] [levelColumn + c]) {
						//Debug.Log("Case Row :"+(levelRow+r)+" column :"+(levelColumn + c)+" is a sibbling Tile");
						surroundingTiles [t] = true;
					} else {
						surroundingTiles [t] = false;
					}
					t++;
					//}
				}
			}
		}		//Debug.Log ("surroundingTiles[] ==" + "[0 :"+surroundingTiles[0]+"], [1 :"+surroundingTiles[1]+"], [2 :"+surroundingTiles[2]+"], [3 :"+surroundingTiles[3]+"], [4 :"+surroundingTiles[4]+"], [5 :"+surroundingTiles[5]+"], [6 :"+surroundingTiles[6]+"], [7 :"+surroundingTiles[7]+"], [8 :"+surroundingTiles[8]+"]");
		return surroundingTiles;
	}


	public int getConnectedSprite (bool[] surroundingTiles)
	{
		int correctTile = 0;

		if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 0; //Dirt
		}
		//1 tile missing (Done)
		else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 16; //DirtCTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 26; //DirtTop
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 5; //DirtCBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 17; //DirtCTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 18; //DirtLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 20; //DirtRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 4; //DirtCBottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 1; //DirtBottom
		}
		//2 tiles missing (Done)
		else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 26; //DirtTop
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 26; //DirtTop
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 1; //DirtBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 1; //DirtBottom
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 18; //DirtLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 18; //DirtLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 20; //DirtRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 20; //DirtRight
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 40; //Dirt2CTop
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 37; //Dirt2CBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 39; //Dirt2CRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 38; //Dirt2CLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 46; //Dirt2CTopBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 45; //Dirt2CBottomTop
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 14; //DirtCTBottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 15; //DirtCTBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 7; //DirCBTopRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 6; //DirtCBTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 11; //DirCLBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 12; //DirtCLTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 11; //DirCLBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 12; //DirtCLTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 13; //DirCRBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 31; //DirtCRTopLeft
		}
		//3 tiles missing (Done)
		else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 26; //DirtTop
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 18; //DirtLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 1; //DirtBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 20; //DirtRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [0] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [8]) {
			correctTile = 3; //DirtBottomRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 2; //DirtBottomLeft
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 3; //DirtBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 2; //DirtBottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 3; //DirtBottomRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 2; //DirtBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 12; //DirtClTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 12; //DirtClTopRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 15; //DirtCTBottomRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 15; //DirtCTBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 13; //DirtCRBottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 13; //DirtCRBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 6; //DirtCBTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 6; //DirtCBTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 11; //DirtCLBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 14; //DirtCTBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 31; //DirtCRTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 7; //DirtCBTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 7; //DirtCBTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 11; //DirtCLBottomRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 14; //DirtCTBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 31; //DirtCRTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 7; //DirtCBTopRight
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 42; //Dirt3CBottomRight
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 41; //Dirt3CBottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 43; //Dirt3CTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 44; //Dirt3CTopRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 34; //DirtCrossLeft
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 33; //DirtCrossBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 35; //DirtCrossRight
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 36; //DirtCrossTop
		}
		//4 tiles missing
		else if (surroundingTiles [0] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [8]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [8]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [1] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 27; //TopBottom
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 27; //TopBottom
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 27; //TopBottom
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 27; //TopBottom
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 27; //TopBottom
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 27; //TopBottom
		} else if (surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3]) {
			correctTile = 3; //BottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 3; //BottomRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [8]) {
			correctTile = 3; //BottomRight
		} else if (surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5]) {
			correctTile = 2; //BottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5]) {
			correctTile = 2; //BottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 2; //BottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 2; //BottomLeft
		} else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 15; //DirtCTBottomRight
		} else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 14; //DirtCTBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 31; //DirtCRTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 13; //DirtCRBottomLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 7; //DirtCBTopRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 6; //DirtCBTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 11; //DirtCLBottomRight
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 12; //DirtCLTopRight
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [8]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [0] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 32; //DirtCross
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 33; //DirtCrossBottom
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 33; //DirtCrossBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 35; //DirtCrossRight
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 35; //DirtCrossRight
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 36; //DirtCrossTop
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 36; //DirtCrossTop
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 34; //DirtCrossLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 34; //DirtCrossLeft
		}
		//5 tiles missing
		else if (surroundingTiles [6] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [2]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [0] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [2] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [0] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [2] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [8]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [2]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [6]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [8]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [6]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [8]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [0] && surroundingTiles [2] && surroundingTiles [5]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [8]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [5]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [2] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		} else if (surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [6]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [5] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [3]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [0] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [8]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [1] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [0] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [6]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [3] && surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [2] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [0] && surroundingTiles [1] && surroundingTiles [3]) {
			correctTile = 3; //DirtBottomRight
		} else if (surroundingTiles [1] && surroundingTiles [2] && surroundingTiles [5]) {
			correctTile = 2; //DirtBottomLeft
		} else if (surroundingTiles [3] && surroundingTiles [6] && surroundingTiles [7]) {
			correctTile = 29; //DirtTopRight
		} else if (surroundingTiles [5] && surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 28; //DirtTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 36; //DirtCrossTop
		} else if (surroundingTiles [1] && surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 35; //DirtCrossRight
		} else if (surroundingTiles [3] && surroundingTiles [7] && surroundingTiles [5]) {
			correctTile = 33; //DirtCrossBottom
		} else if (surroundingTiles [1] && surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 34; //DirtCrossLeft
		}
		//6 Tiles missing (Done)
		else if (surroundingTiles [1] && surroundingTiles [8]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [7] && surroundingTiles [8]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [3] && surroundingTiles [8]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [5] && surroundingTiles [8]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [1]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [0] && surroundingTiles [7]) {
			correctTile = 25; //DirtSingleTop
		} else if (surroundingTiles [0] && surroundingTiles [5]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [5]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [0] && surroundingTiles [3]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [1] && surroundingTiles [7]) {
			correctTile = 19; //DirtLeftRight
		} else if (surroundingTiles [3] && surroundingTiles [5]) {
			correctTile = 27; //DirtTopBottom
		} else if (surroundingTiles [1] && surroundingTiles [3]) {
			correctTile = 9; //DirtCCTopLeft
		} else if (surroundingTiles [1] && surroundingTiles [5]) {
			correctTile = 10; //DirtCCTopRight
		} else if (surroundingTiles [3] && surroundingTiles [7]) {
			correctTile = 8; //DirtCCBottomLeft
		} else if (surroundingTiles [5] && surroundingTiles [7]) {
			correctTile = 30; //DirtCCBottomRight
		}
		//7 tiles missing (Done)
		else if (surroundingTiles [1]) {
			correctTile = 22; //DirtSingleBottom
		} else if (surroundingTiles [3]) {
			correctTile = 24; //DirtSingleRight
		} else if (surroundingTiles [5]) {
			correctTile = 23; //DirtSingleLeft
		} else if (surroundingTiles [7]) {
			correctTile = 25; //DirtSingleTop
		}
		//All tiles missing (Done)
		else {
			correctTile = 21; //DirtSingle
		}
		return correctTile;
	}

	public void updateSurroundingObjects (string objectType, int levelRow, int levelColumn, bool[] surroundingTiles, List<List<string>> levelArray){
		bool[] tilesToModify = new bool[9];
		int thisRow = levelRow;
		int thisColumn = levelColumn;
		//Check the surrounding objects nature

		for (int t = 0; t < (tilesToModify.Length); t++) {
			//Save center tile data in array
			tilesToModify[t]=surroundingTiles[t];
		}

		for(int i = 0; i < (tilesToModify.Length); i++){
			//If the tiles needs to be modified and not center tile
			if (tilesToModify[i] && i !=4) {
				//Find its position
				switch (i) {
				case 0:
					thisRow = (levelRow + 1);
					thisColumn = (levelColumn - 1);
					break;
				case 1:
					thisRow = (levelRow + 1);
					thisColumn = levelColumn;
					break;
				case 2:
					thisRow = (levelRow + 1);
					thisColumn = (levelColumn + 1);
					break;
				case 3:
					thisRow = levelRow;
					thisColumn = (levelColumn - 1);
					break;
				case 5:
					thisRow = levelRow;
					thisColumn = (levelColumn + 1);
					break;
				case 6:
					thisRow = (levelRow - 1);
					thisColumn = (levelColumn - 1);
					break;
				case 7:
					thisRow = (levelRow - 1);
					thisColumn = levelColumn;
					break;
				case 8:
					thisRow = (levelRow - 1);
					thisColumn = (levelColumn + 1);
					break;
				default:
					break;
				}
				//Debug.Log ("To modify : tilesToModify["+i+"]");
				surroundingTiles = detectSurroundingTiles(objectType,thisRow,thisColumn, levelArray);
				//int resultObject = selectTileSprite();
				//Debug.Log("Correct tile is :"+resultObject);
				LevelEditor levelEditor = EditorImporter.GetComponent<LevelEditor> ();
				levelEditor.instantiateObject (objectType, thisRow, thisColumn, surroundingTiles);
			}
		}
	}
}
