using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RulerAttack : MonoBehaviour
{
    public Collider2D rulerCollider;
    public float damage = 3;
    public Vector3 faceRight = new Vector3(1, -0.9f, 0);
    public Vector3 faceLeft = new Vector3(-1, -0.9f, 0);
    public float knockbackForce = 15f;
    private void Start()
    {
        if(rulerCollider == null)
        {
            Debug.LogWarning("not set");
        }
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageale damagealeOject = collider.GetComponent<IDamageale>();
        if(damagealeOject != null )
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2) (collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;
            damagealeOject.OnHit(damage, knockback);

        }


    }
}
