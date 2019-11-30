using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("UI Settings")]
    public Stat healthbar;
    public Stat manabar;

    [Header("Attack Settings")]
    public Transform attackPos;
    public Vector3 attackPosOffset;
    public float attackRange;
    public float attackDamage;

    [Header("Animation Settings")]
    public float animatorRunSpeed = 1.5f;
    public float attackSwingTime = 0.7f;
    //Speed to set the animator scaled by the player's attack speed
    private float animatorAttackSwingSpeed;
    //Time to wait until attack is finished scaled by the inverse of the player's attack speed
    private float animatorAttackSwingTime;



    private float walkSpeed;

    public bool IsWalking {
        get {
            return (direction.x != 0 || direction.y != 0);
        }
    }
    public bool IsRunning {
        get {
            return (Speed > walkSpeed);
        }
    }

    private bool isAttacking = false;

    

    protected override void Start() {
        base.Start();
        walkSpeed = Speed;
        healthbar.Initialize(maxHealth, Health);
        manabar.Initialize(maxMana, Mana);

        ActivateLayer("IdleLayer");
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        healthbar.CurrentValue = Health;
        manabar.CurrentValue = Mana;

        base.Update();
    }

    private void GetInput() {

        direction = Vector2.zero;

        //DEBUGGING
        if (Input.GetKeyDown(KeyCode.I)) Mana-=10;
        if (Input.GetKeyDown(KeyCode.O)) Mana+=10;

        //Can't recieve input while attacking
        if (!isAttacking)
        {
            //MOVEMENT
            if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
            if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
            if (Input.GetKey(KeyCode.D)) direction += Vector2.right;
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down;

            if (Input.GetKey(KeyCode.LeftShift)) Speed = walkSpeed * 2.0f; else Speed = walkSpeed;

            //ATTACKING
            if (Input.GetMouseButtonDown(0)) StartCoroutine(Attack());
        }

    }

    private IEnumerator Attack() {
        SetAttack(true);

        animatorAttackSwingSpeed = attackSwingTime * AttackSpeed;
        animatorAttackSwingTime = attackSwingTime * (1 / AttackSpeed);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position + attackPosOffset, attackRange);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<GreenSlime>().TakeDamage(attackDamage, direction.x);

        }

        yield return new WaitForSeconds(animatorAttackSwingTime); //Hardcoded cast time for debugging

        SetAttack(false);

    }

    private void SetAttack(bool state) {
        animator.SetBool("attack", state);
        isAttacking = state;
    }

    protected override void Animate() {
        //Reset animator speed before each frame
        animator.speed = 1.0f;
        if (isAttacking) {
            animator.speed = animatorAttackSwingSpeed;
            ActivateLayer("AttackSwingLayer");
        } else if (IsWalking) {

            if (IsRunning) animator.speed = animatorRunSpeed;

            ActivateLayer("WalkLayer");
            base.Animate();

        } else ActivateLayer("IdleLayer");

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position + attackPosOffset, attackRange);
    }
}
