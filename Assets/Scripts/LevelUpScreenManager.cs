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
	public Image[,] yellowBar = new Image[4, 10];
	public Image[,] backBar = new Image[4, 10];
    public Button[] buttons = new Button[4];
    public Text[] texts = new Text[5];

    public Button accept;
    public Button cancel;

	Quaternion q = new Quaternion(0,0,0,0);
	bool active = false;
    private int idToLevel = -1;
	int lastLevel = 1;

    private enum Positions { SPEED,ATTACK,ATTACKSPEED,LIFE };
	// Use this for initialization
	void Start () {
		Debug.Log ("Loading bars");
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 4; j++) {
				Vector3 pos = new Vector2 (30 * i - 90, j * 50 - 100);
				blueBar[j, i] = createImage(blue, pos);
				yellowBar[j, i] = createImage(yellow, pos);
				backBar[j, i] = createImage(back, pos);
			}
		}
		//background.gameObject.SetActive (false);
        DisableAdd();
        deactivateScreen();
    }
	
	// Update is called once per frame
	void Update () {
		if (lastLevel < p.level) {
            lastLevel = p.level;
			activateScreen ();
			active = true;
		}
		if (active && Input.GetKeyDown (KeyCode.Return)) {
			deactivateScreen ();
		}
	}

	void deactivateScreen(){
		background.gameObject.SetActive (false);
		for(int i = 0; i< 4; i++){
			for (int j = 0; j < 10; j++) {
				yellowBar [i, j].gameObject.SetActive (false);
				blueBar [i, j].gameObject.SetActive (false);
			}
		}
        for (int i = 0; i < texts.Length; i++) {
            texts[i].gameObject.SetActive(false);
        }
        accept.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
    }

	void activateScreen(){
		background.gameObject.SetActive (true);
        for (int j = 0; j < 10; j++) {
            for(int i = 0; i < 4; i++) {
                CalculateBar(i, j, true);
            }
		}
        for(int i = 0; i < texts.Length; i++) {
            texts[i].gameObject.SetActive(true);
        }
        EnableAdd();
	}

	Image createImage(Image original, Vector3 pos){
		Image i  = Instantiate<Image> (original, pos, q, transform);
		i.transform.localPosition = pos;
		i.gameObject.SetActive (false);
		return i;
	}

    private void CalculateBar(int i, int position, bool state) {
        int level = p.getStat(i + 1);
        if (position < level) {
            yellowBar[i, position].gameObject.SetActive(state);
        } else {
            blueBar[i, position].gameObject.SetActive(state);
        }
    }

    public void AddLevel(int id) {
        idToLevel = id;
        DisableAdd();
        accept.gameObject.SetActive(true);
        cancel.gameObject.SetActive(true);
        yellowBar[id - 1, p.getStat(id)].gameObject.SetActive(true);
    }

    public void DisableAdd() {
        Debug.Log(buttons.Length);
        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void EnableAdd() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void Undo() {
        yellowBar[idToLevel - 1, p.getStat(idToLevel)].gameObject.SetActive(false);
        idToLevel = -1;
        EnableAdd();
        accept.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
    }

    public void Save() {
        if (idToLevel == -1)
            return;
        p.LevelUp(idToLevel);
        Hide();
    }

    private void Hide() {
        deactivateScreen();
    }
}
