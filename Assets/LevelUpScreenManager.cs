using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreenManager : MonoBehaviour {
	public Player p;

	public Image blue;
	public Image yellow;
	public Image back;
	public Image background;
	public Image[,] blueBar = new Image[5, 10];
	public Image[,] yellowBar = new Image[5, 10];
	public Image[,] backBar = new Image[5, 10];
	Quaternion q = new Quaternion(0,0,0,0);
	bool active = false;

	int lastLevel = 1;
	// Use this for initialization
	void Start () {
		Debug.Log ("Loading bars");
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 5; j++) {
				Vector3 pos = new Vector2 (30 * i - 150, j * 60 - 150);
				blueBar[j, i] = createImage(blue, pos);
				yellowBar[j, i] = createImage(yellow, pos);
				backBar[j, i] = createImage(back, pos);
			}
		}
		background.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (lastLevel < p.level) {
			activateScreen ();
			active = true;
		}
		if (active && Input.GetKeyDown (KeyCode.Return)) {
			deactivateScreen ();
		}
	}

	void deactivateScreen(){
		background.gameObject.SetActive (true);
		for(int i = 0; i< 5; i++){
			for (int j = 0; j < 10; j++) {
				if (j < p.level)
					yellowBar [i, j].gameObject.SetActive (false);
				else
					blueBar [i, j].gameObject.SetActive (false);
			}
		}
	}

	void activateScreen(){
		background.gameObject.SetActive (true);
		for(int i = 0; i< 5; i++){
			for (int j = 0; j < 10; j++) {
				if (j < p.level)
					yellowBar [i, j].gameObject.SetActive (true);
				else
					blueBar [i, j].gameObject.SetActive (true);
			}
		}
	}

	Image createImage(Image original, Vector3 pos){
		Image i  = Instantiate<Image> (original, pos, q, transform);
		i.transform.localPosition = pos;
		i.gameObject.SetActive (false);
		return i;
	}
}
