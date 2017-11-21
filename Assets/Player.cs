using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human {
	List<Enemy> Targets = new List<Enemy>();
	// Use this for initialization
	void Start () {
        base.Start();   
		Debug.Log (transform.localScale.x);
        life = 100;
		acp.SetFloat ("attackSpeed", 1);

	}
	
	// Update is called once per frame
	void Update () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Flip(horizontal, vertical);
        Run(horizontal, vertical);
       
        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
        
	}
	void Attack(){
		base.Attack ();
		foreach(Enemy e in Targets) {
			e.Hit (0.1f);
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Dentro del rango");
		Targets.Add (other.GetComponent<Enemy> ());
	}
	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Fuera del rango");
		Targets.Remove (other.GetComponent<Enemy> ());
	}
}
