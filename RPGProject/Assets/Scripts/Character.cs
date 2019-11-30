using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField]
    private float health = 100;
    public float Health {
        get {
            return health;
        } set {
            if (value > maxHealth) health = maxHealth;
            else if (value < 0) health = 0;
            else health = value;
        }
    }
    protected float maxHealth;

    [SerializeField]
    private float mana = 100;
    public float Mana {
        get {
            return mana;
        }
        set {
            if (value > maxMana) mana = maxMana;
            else if (value < 0) mana = 0;
            else mana = value;
        }
    }
    protected float maxMana;

    [SerializeField]
    private float speed = 2.0f;
    public float Speed {
        get {
            return speed;
        } set {
            if (value < 1) speed = 1;
            else speed = value;
        }
    }

    [SerializeField]
    private float attackSpeed = 1.0f;
    public float AttackSpeed {
        get {
            return attackSpeed;
        } set {
            if (value < 0) attackSpeed = 0.01f; //you gotta have some attack speed my guy
            else speed = value;
        }
    }

    protected Vector2 direction;
    protected Rigidbody2D myRigidbody;

    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        maxHealth = Health;
        maxMana = Mana;

        direction = Vector2.zero;
        myRigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Animate();
    }

    private void FixedUpdate() {
        Move();
    }

    protected void Move() {
        myRigidbody.velocity = direction.normalized * speed;
        setFacing();
    }

    private void setFacing() {
        if (direction.x < 0) animator.SetInteger("facing", -1);
        else if (direction.x > 0) animator.SetInteger("facing", 1);
    }

    protected virtual void Animate() {
        animator.SetFloat("x", direction.x);
        
    }

    protected void ActivateLayer(string layerName) {
        for(int i=0; i<animator.layerCount; i++) {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }
}
