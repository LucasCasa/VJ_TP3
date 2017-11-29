using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
	public Player p;

	public int[] playerLevels = new int[7];
	public int dungeonLevel;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadStats(int level){
		dungeonLevel = level;
		playerLevels [0] = p.level;
		playerLevels [1] = p.RunSpeedLevel;
		playerLevels [2] = p.AttackLevel;
		playerLevels [3] = p.AttackSpeedLevel;
		playerLevels [4] = p.LifeLevel;
		playerLevels [5] = p.kills;
		playerLevels [6] = p.hitsTaken;
	}
}
