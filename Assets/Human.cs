using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {
    public Animator acp;
    public SpriteRenderer sr;
    protected bool moving = false;
    public bool movementDisabled = false;
    protected float life;
    // Use this for initialization
    protected void Start () {
        acp.GetBehaviour<PlayerAnimationController>().h = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void Flip(float horizontal, float vertical) {
        if (horizontal < 0 && !movementDisabled) {
                sr.flipX = true;

        } else if (horizontal > 0 && !movementDisabled) {
                sr.flipX = false;
        }
    }

    protected void Run(float horizontal, float vertical) {
        if ((vertical != 0 || horizontal != 0) && !moving && !movementDisabled) {
            moving = true;
            acp.SetTrigger("walk");
            Debug.Log("Started Walking");
        }
        if (vertical == 0 && horizontal == 0 && moving) {
            acp.SetTrigger("stop");
            moving = false;
            Debug.Log("Stopped Walking");
        }

        if (!movementDisabled) {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, new Vector2(horizontal, 0), 0.1f, 512);
            if (hit.collider != null) {
                horizontal = 0;
            }
            hit = Physics2D.CircleCast(transform.position, 0.5f, new Vector2(0, vertical), 0.1f, 512);
            if (hit.collider != null) {
                vertical = 0;
            }
            transform.position = new Vector2(transform.position.x + horizontal / 10, transform.position.y + vertical / 10);
        }
    }
    protected void Attack() {
        if (!movementDisabled) {
            acp.SetTrigger("attack");
        }
    }
}
