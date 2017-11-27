using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Human {
	List<Enemy> Targets = new List<Enemy>();
	public RectTransform healthBar;
	protected float healthBarWidth;

	public RectTransform expBar;
	protected float expBarWidth;
	public Text levelText;
	public int level = 1;
	int xp = 0;
	int xpNecessary = 10;
    private float attackSpeed = 1;

	// Use this for initialization
	new void Start () {
        base.Start();   
		healthBarWidth = healthBar.sizeDelta.x;
		expBarWidth = expBar.sizeDelta.x;
        life = 10;
		maxLifeBase = 10;
        attackBase = 1;
		acp.SetFloat ("attackSpeed", attackSpeed);
		acp.SetFloat ("life", life);
		updateExpBar ();

	}
	
	// Update is called once per frame
	new void Update () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Flip(horizontal, vertical);
        Run(horizontal, vertical);
       
        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
        
	}
	new void Attack(){
		base.Attack ();
		if (!movementDisabled) {
			foreach (Enemy e in Targets) {
				if (e.Hit (attackBase*attackLevel)) {
					xp+= 5;
					updateExpBar ();
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Dentro del rango");
		Enemy e = other.GetComponent<Enemy> ();
		if(e != null)
 			Targets.Add (e);
	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Fuera del rango");
		Enemy e = other.GetComponent<Enemy> ();
		if(e != null)
			Targets.Remove (e);
	}

	override protected void updateBar(){
		healthBar.sizeDelta = new Vector2 (healthBarWidth * life / (maxLifeBase * maxLifeLevel), healthBar.sizeDelta.y);
	}

	void updateExpBar(){
		if (xp == xpNecessary) {
			level++;
			xp = 0;
			xpNecessary = xpNecessary + 5;
			levelText.text = level.ToString();
		}
		expBar.sizeDelta = new Vector2 (expBarWidth * xp / xpNecessary, expBar.sizeDelta.y);
	}

    public void LevelUp(int id) {
        Debug.Log("LeveledUP " + id);
        switch (id) {
            case 1:
                LevelUpSpeed();
                break;
            case 2:
                LevelUpAttack();
                break;
            case 3:
                LevelUpAttackSpeed();
                break;
            case 4:
                LevelUpLife();
                break;
        }
        life = maxLifeBase * LifeLevel;
        updateBar();
        acp.SetFloat("life", life);
    }

    private void LevelUpLife() {
        maxLifeLevel++;
    }

    private void LevelUpAttackSpeed() {
        AttackSpeedLevel++;
    }

    private void LevelUpAttack() {
        attackLevel++;
    }

    private void LevelUpSpeed() {
        RunSpeedLevel++;
        
    }
}
