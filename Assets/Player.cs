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
	// Use this for initialization
	new void Start () {
        base.Start();   
		healthBarWidth = healthBar.sizeDelta.x;
		expBarWidth = expBar.sizeDelta.x;
        life = 10;
		maxLife = 10;
		acp.SetFloat ("attackSpeed", 1);
		acp.SetFloat ("life", 10);
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
				if (e.Hit (0.1f)) {
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
		healthBar.sizeDelta = new Vector2 (healthBarWidth * life / maxLife, healthBar.sizeDelta.y);
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
}
