using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public Player p;
    public int currentLevel = 1;
    public MapGenerator mg;
    public Image loadingScreen;
    public Canvas pauseScreen;
    bool generated = false;
    bool createOnNext = false;
	public StatsManager sm;
    public bool paused = false;

    // Use this for initialization
	void Start () {
		if (SaveLoad.load) {
			SaveLoad.Load ();
			SaveLoad.load = false;
		}
        pauseScreen.gameObject.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(!paused){
				Pause();
			}else{
				UnPause();
			}
		}
        if (createOnNext) {
			SaveLoad.Save ();
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
		if (p.isDead ()) {
			sm.LoadStats (currentLevel);
			SceneManager.LoadScene ("End");
		}
    }

    public void Pause() {
    	Cursor.lockState = CursorLockMode.None;
		paused = true;
		Time.timeScale = 0;
		pauseScreen.gameObject.SetActive (true);
    }

    public void UnPause() {
    	Cursor.lockState = CursorLockMode.Locked;
		paused = false;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive (false);
    }

    private void generateNewLevel() {
		mg.LoadConnectedMap(currentLevel * 5, 5, 10, currentLevel * 2, currentLevel);
        mg.FillWorld();
        generated = true;
        p.transform.position = new Vector2(2, -2);
        p.winLevel = false;
        createOnNext = false;
    }
}
