using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour {

	public static bool load = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void Save() {
		Debug.Log ("Saving game...");
		GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager> ();
		Player p = gm.p;
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("dungeonLevel", gm.currentLevel);
		PlayerPrefs.SetInt ("maxLifeLevel", p.LifeLevel);
		PlayerPrefs.SetInt ("attackLevel", p.AttackLevel);
		PlayerPrefs.SetInt ("runSpeedLevel", p.RunSpeedLevel);
		PlayerPrefs.SetInt ("attackSpeedLevel", p.AttackSpeedLevel);
		PlayerPrefs.SetFloat ("life", p.life);
		PlayerPrefs.SetInt ("xp", p.xp);
		PlayerPrefs.SetInt ("level", p.level);
	}

	public static void Load() {
		Debug.Log ("Loading game...");
		GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager> ();
		LevelUpScreenManager lu = GameObject.Find ("LevelUpPopUp").GetComponent<LevelUpScreenManager> ();
		Player p = gm.p;
		gm.currentLevel = PlayerPrefs.GetInt ("dungeonLevel");
		p.LifeLevel = PlayerPrefs.GetInt ("maxLifeLevel");
		p.AttackLevel = PlayerPrefs.GetInt ("attackLevel");
		p.RunSpeedLevel = PlayerPrefs.GetInt ("runSpeedLevel");
		p.AttackSpeedLevel = PlayerPrefs.GetInt ("attackSpeedLevel");
		p.life = PlayerPrefs.GetFloat ("life");
		p.xp = PlayerPrefs.GetInt ("xp");
		p.level = PlayerPrefs.GetInt ("level");
		lu.lastLevel = p.level;
		p.acp.SetFloat ("life", p.life);
		p.updateUI ();
	}
}
