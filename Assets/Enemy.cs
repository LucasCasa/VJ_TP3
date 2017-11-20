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
        life = 10;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 1) {
            Attack();
        } else {
            Vector2 direction = -(transform.position - player.transform.position) / distance;
            Flip(direction.x, direction.y);
            Run(direction.x, direction.y);
        }
    }
}
