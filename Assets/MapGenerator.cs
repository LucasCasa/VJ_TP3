using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public Texture2D txt;
	public GameObject tile;
	public int[,] map = new int[200, 200]; 
	Sprite[] sprites;
	// Use this for initialization
	enum Tiles{BORDER1, BORDER2, BOTTOM, BOX, CORNER, TOP, TOP_WITH_FLAG, TOP_WITHOUT_FLAG, FLOOR};

	void Start () {
		sprites = Resources.LoadAll<Sprite>(txt.name);
		LoadMap (80,5,10);
		FillWorld ();
		/*
		int maxI = (int)(Random.value * 20 + 10);
		int maxJ = (int)(Random.value * 10 + 5);
		for (int i = 0; i < maxI ;i++){
			for(int j = 0 ; j < maxJ ; j++){
				int x = i - 5;
				float y = j * 1.5f - 7.5f;
				if (j == maxJ - 1) {
					if (i == 0 || i == maxI - 1) {
						GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
						o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.CORNER];		
					} else {
						if (Random.value < 0.2f) {
							GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
							o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.TOP_WITH_FLAG];		
						} else {
							GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
							o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.TOP];
						}
					}
				} else if (j == 0) {
					GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
					o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BOTTOM];
				} else if (i == 0 || i == maxI - 1) {
					GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
					if(Random.value > 0.5)
						o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BORDER2];
					else
						o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BORDER1];
					
				} else {
					if (Random.value > 1) {
						GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
						o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BOX];
					} else {
						GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
						o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.FLOOR];
					}
				}	
				Debug.Log ("inst");
			}
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadMap (int rooms, int min, int variance)
	{
		for (int n = 0; n < rooms; n++) {
			int startX = (int)(Random.value * 180 + 1);
			int startY = (int)(Random.value * 180 + 1);
			int sizeX = (int)(Random.value * variance + min);
			int sizeY = (int)(Random.value * variance + min);
			for (int i = startX; i < startX + sizeX; i++) {
				for (int j = startY; j < startY + sizeY; j++) {
					if (i == startX || j == startY || j == startY + sizeY - 1 || i == startX + sizeX - 1) {
						if (map [i, j] != 1) {
							map [i, j] = 2;
						}
					} else {
						map [i, j] = 1;
					}
				}
			}
		}
		for (int i = 0; i < 200; i++) {
			string line = "";
			for (int j = 0; j < 200; j++) {
				line = line + " " + map [i, j];
			}
			Debug.Log (line);
		}
	}

	void FillWorld ()
	{
		for (int i = 0; i < 200; i++) {
			for (int j = 0; j < 200; j++) {
				if (map [i, j] == 0) {
					createEmpty (i, j);
				} else if (map [i, j] == 1) {
					createFloor (i, j);
				} else {
					calculateTypeOfWall (i, j);
				}
			}
		}
	}
	
	void createEmpty (int i, int j)
	{
		int x = i;
		float y = j * 1.5f;
		GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
		o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.FLOOR];
	}

	void createFloor (int i, int j)
	{
		int x = i;
		float y = j * 1.5f;
		GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
		o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.FLOOR];
	}

	void calculateTypeOfWall (int i, int j)
	{
		int x = i;
		float y = j * 1.5f;
		if (map [i, j - 1] == 0) {
			if (map [i, j + 1] == 2) {
				GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
				o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.CORNER];
			} else {
				GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
				o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.TOP_WITH_FLAG];
			}
		} else if (map [i, j + 1] == 0) {
			GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
			o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BOTTOM];
		} else if (map [i + 1, j] == 0) {
			GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
			o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BORDER1];
		} else if (map [i - 1, j] == 0) {
			GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
			o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BORDER2];
		} else if (map [i, j - 1] == 1) {
			GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
			o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.TOP];
		} else if (map [i, j + 1] == 1) {
			GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
			o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BORDER1];
		} else {
			GameObject o = Instantiate (tile, new Vector3 (x, y), new Quaternion (0, 0, 0, 0)) as GameObject;
			o.GetComponent<SpriteRenderer> ().sprite = sprites [(int)Tiles.BORDER1];
		}
	}
}
