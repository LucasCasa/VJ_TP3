using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour {
	StatsManager sm;
	public Text[] texts;
	public Text dungeonLevel;
	// Use this for initialization
	void Start () {
		sm = GameObject.Find ("StatsManager").GetComponent<StatsManager>();
		LoadData ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LoadData (){
		dungeonLevel.text = dungeonLevel.text + "\t" + sm.dungeonLevel;
		for (int i = 0; i < texts.Length; i++) {
			texts [i].text = texts [i].text + "\t" + sm.playerLevels [i];
		}
		Destroy (sm);
	}
}
