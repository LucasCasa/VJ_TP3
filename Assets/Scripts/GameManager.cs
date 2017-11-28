using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Player p;
    int currentLevel = 1;
    public MapGenerator mg;
    public Image loadingScreen;
	public Canvas pauseScreen;
    bool generated = false;
    bool createOnNext = false;
	bool paused = false;
	// Use this for initialization
	void Start () {
		pauseScreen.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(!paused){
				Cursor.lockState = CursorLockMode.None;
				paused = true;
				Time.timeScale = 0;
				pauseScreen.gameObject.SetActive (true);
			}else{
				Cursor.lockState = CursorLockMode.Locked;
				paused = false;
				Time.timeScale = 1;
				pauseScreen.gameObject.SetActive (false);
			}
		}

        if (createOnNext) {
            generateNewLevel();
            loadingScreen.gameObject.SetActive(false);
        }

        if (!generated) {
            loadingScreen.gameObject.SetActive(true);
            createOnNext = true;
        }
        if (p.winLevel) {
            loadingScreen.gameObject.SetActive(true);
            currentLevel++;
            createOnNext = true;
        }
        

    }

    private void generateNewLevel() {
        mg.LoadConnectedMap(currentLevel * 5, 5, 10, currentLevel * 2);
        mg.FillWorld();
        generated = true;
        p.transform.position = new Vector2(2, -2);
        p.winLevel = false;
        createOnNext = false;
    }
}
