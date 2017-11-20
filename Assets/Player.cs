using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human {
    
	// Use this for initialization
	void Start () {
        base.Start();   
		Debug.Log (transform.localScale.x);
        life = 100;

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
}
