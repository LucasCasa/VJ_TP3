using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Player p;
    int currentLevel = 1;
    public MapGenerator mg;
    public Image loadingScreen;
    bool generated = false;
    bool createOnNext = false;
    // Use this for initialization

	void Start () {        
        //gameMusic.Play();
        //gameMusic.loop = true;
    }
	
	// Update is called once per frame
	void Update () {
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
