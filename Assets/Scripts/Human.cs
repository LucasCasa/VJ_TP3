using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Human : MonoBehaviour {
    public Animator acp;
    public SpriteRenderer sr;
    protected bool moving = false;
    public bool movementDisabled = false;

    public bool dead = false;

    protected float maxLifeBase = 1;
    public int maxLifeLevel = 1;
    public float life;

    protected float attackBase;
    public int attackLevel = 1;

    protected float runSpeedBase = 1;
    public int runSpeedLevel = 1;
    protected float actualSpeed;

    protected float shieldBase;
    public int shieldLevel = 1;

    protected float attackSpeedBase = 1;
    public int attackSpeedLevel = 1;
    protected float actualAttackSpeed;

    protected AudioSource audioSource;
    public AudioClip missSound;
    public AudioClip hitSound;

	public int kills;
	public int hitsTaken;

    protected void Start () {
        acp.GetBehaviour<PlayerAnimationController>().h = this;
        actualSpeed = Mathf.Sqrt(runSpeedLevel) * runSpeedBase;
        actualAttackSpeed = Mathf.Sqrt(attackSpeedLevel) * attackSpeedBase;
        acp.SetFloat("runSpeed", actualSpeed);
        audioSource = GetComponent<AudioSource>();
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
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, new Vector2(horizontal, 0), 0.2f, 512);
            if (hit.collider != null) {
                horizontal = 0;
            }
            hit = Physics2D.CircleCast(transform.position, 0.3f, new Vector2(0, vertical), 0.2f, 512);
            if (hit.collider != null) {
                vertical = 0;
            }
            transform.position+= new Vector3(Speed(horizontal), Speed(vertical));
        }
    }
    protected void Attack() {
        if (!movementDisabled) {
            acp.SetTrigger("attack");
        }
    }

	public bool Hit(float attackDamage){
		Debug.Log ("Enemy Hitted " + attackDamage);
		Debug.Log ("Enemy Life: " + life);
		life-= attackDamage;
		Debug.Log ("Enemy Life: " + life);
		acp.SetFloat ("life", life);
		updateBar ();
		hitsTaken++;
		return life <= 0.01;
	}

	abstract protected void updateBar ();

    public int RunSpeedLevel {
        get { return runSpeedLevel; }
        set {
            runSpeedLevel = value;
            actualSpeed = Mathf.Sqrt(runSpeedLevel) * runSpeedBase;
            acp.SetFloat("runSpeed", actualSpeed);
        }
    }

    public int AttackLevel {
        get { return attackLevel; }
        set { attackLevel = value; }
    }

    public int AttackSpeedLevel {
        get { return attackSpeedLevel; }
        set {
            attackSpeedLevel = value;
            actualAttackSpeed = Mathf.Sqrt(attackSpeedLevel) * attackSpeedBase;
            acp.SetFloat("attackSpeed", actualAttackSpeed);
        }
    }

    public int LifeLevel {
        get { return maxLifeLevel; }
        set { 
			maxLifeLevel = value;
			life = maxLifeBase * maxLifeLevel;
			acp.SetFloat ("life", life);
		}
    }

    public int getStat(int id) {
        switch (id) {
            case 1:
                return RunSpeedLevel;
            case 2:
                return AttackLevel;
            case 3:
                return AttackSpeedLevel;
            case 4:
                return LifeLevel;
            default:
                return -1;
        }
    }

    protected float Speed() {
        return actualSpeed;
    }

    protected float Speed(float magnitude) {
        return Time.deltaTime * actualSpeed * magnitude * 5;
    }
}
