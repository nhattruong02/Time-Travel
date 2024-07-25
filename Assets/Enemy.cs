using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 100f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    private bool isMoving = true;
    private Animator animator;
    DamagebleCharacter damagebleCharacter;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damagebleCharacter = GetComponent<DamagebleCharacter>();
        animator = GetComponent<Animator>();

    }
    private void Update()
    {

        if (damagebleCharacter.Targetable && detectionZone.detectedOjs.Count > 0)
        {
            animator.SetBool("isMoving", isMoving);

            Vector2 direction = (detectionZone.detectedOjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageale damageale  = collider.GetComponent<IDamageale>();
        if (damageale != null)
        {
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageale.OnHit(damage, knockback);
        }
    }
}
