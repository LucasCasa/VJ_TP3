using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Animator acp;
    public SpriteRenderer sr;
    bool moving = false;
	public bool movementDisabled = false;
	// Use this for initialization
	void Start () {
		Debug.Log (transform.localScale.x);
	}
	
	// Update is called once per frame
	void Update () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if((vertical != 0 || horizontal != 0) && !moving && !movementDisabled) {
            moving = true;
            acp.SetTrigger("playerWalking");
			Debug.Log ("Started Walking");
        }
        if(vertical == 0 && horizontal == 0 && moving) {
			acp.SetTrigger("playerStopped");
            moving = false;
			Debug.Log("Stopped Walking");
        }
		if(horizontal < 0 && !movementDisabled) {
            if(!sr.flipX)
				//transform.position += new Vector3(sr.size.x*transform.localScale.x / 2, 0, 0);
            sr.flipX = true;
            
		}else if(horizontal > 0 && !movementDisabled) {
            if(sr.flipX)
				//transform.position -= new Vector3(sr.size.x*sr.transform.localScale.x / 2, 0, 0);
            sr.flipX = false;
        }
        if (!movementDisabled) {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position,0.5f, new Vector2(horizontal, 0),0.1f,512);
            if(hit.collider != null) {
                horizontal = 0;
                Debug.Log(hit.distance);
            }
            hit = Physics2D.CircleCast(transform.position,0.5f, new Vector2(0,vertical), 0.1f, 512);
            if (hit.collider != null) {
                vertical = 0;
                Debug.Log(hit.distance);
            }
            transform.position = new Vector2(transform.position.x + horizontal / 10, transform.position.y + vertical / 10);
        }
        	


        if (Input.GetKeyDown(KeyCode.Space)) {
            acp.SetTrigger("playerAttack");
			movementDisabled = true;
        }
        
	}
}
