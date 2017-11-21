using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Human {
    public GameObject player;
	// Use this for initialization
	void Start () {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        life = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			Destroy (gameObject);
		}
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 1) {
            Attack();
		} else if(distance < 10){
            Vector2 direction = -(transform.position - player.transform.position) / distance;
            Flip(direction.x, direction.y);
            Run(direction.x, direction.y);
        }
    }

	public void Hit(float attackDamage){
		Debug.Log ("Enemy Hitted");
		life-= attackDamage;
		acp.SetFloat ("life", life);
		updateBar ();
	}

	void updateBar(){
		maskTransform.localScale = new Vector3(life,maskTransform.localScale.y,1);
	}
}
