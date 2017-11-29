using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Human {
    public GameObject player;
	bool attackMode = false;

	public Transform healthBar;
	protected float healthBarWidth;
	// Use this for initialization
	new void Start () {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        life = 1;
		maxLifeBase = 1;
        attackBase = 0.1f;
		healthBarWidth = healthBar.localScale.x;
        audioSource.clip = hitSound;

    }
	
	// Update is called once per frame
	new void Update () {
		if (dead) {
			Destroy (gameObject);
		}
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (attackMode) {
            Attack();
		} else if(distance < 10){
            Vector2 direction = -(transform.position - player.transform.position) / distance;
            Flip(direction.x, direction.y);
            Run(direction.x, direction.y);
        }
    }

	override protected void updateBar(){
		healthBar.localScale = new Vector2 (healthBarWidth * life / (maxLifeBase * maxLifeLevel), healthBar.localScale.y);
	}

	new void Attack(){
		if (!movementDisabled) {
			Debug.Log ("EnemyAttacking");
			base.Attack ();
			player.GetComponent<Player> ().Hit (attackBase * attackLevel);
            audioSource.Play();
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<Player>() != null)
			attackMode = true;
	}

	void OnTriggerExit2D(Collider2D other){
		attackMode = false;
	}

    public void SetLevel(int level) {
        LifeLevel = level;
        AttackLevel = level;
        RunSpeedLevel = level;
        AttackSpeedLevel = level;
    }
}
