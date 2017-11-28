using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Player p;
    int currentLevel = 1;
    public MapGenerator mg;
    public Sprite loadingScreen;
    bool generated = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!generated) {
            generateNewLevel();
        }
        if (p.winLevel) {
            currentLevel++;
            generateNewLevel();
        }
    }

    private void generateNewLevel() {
        mg.LoadConnectedMap(currentLevel * 5, 5, 10, currentLevel * 2);
        mg.FillWorld();
        generated = true;
        p.transform.position = new Vector2(2, -2);
        p.winLevel = false;
    }
}
