using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{

    public float health = 10f;

    public float hurtDistance = 50f;

    private SpriteRenderer sr;
    private Rigidbody2D myRigidbody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AI();
        if (health <= 0) Destroy(gameObject, 0.1f);
        
    }

    void AI()
    {
        //bounce nigga
    }

    public void TakeDamage(float damage, float dir)
    {
        health -= damage;
        if (dir < 0) myRigidbody.AddForce(new Vector2((Vector2.left * hurtDistance).x, 0));
        else myRigidbody.AddForce(new Vector2((Vector2.right * hurtDistance).x, 0));
        StartCoroutine(Hurt());
        
    }

    private IEnumerator Hurt()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        sr.color = Color.white;
    }
}
