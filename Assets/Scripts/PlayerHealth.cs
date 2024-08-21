using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageale
{
    public float Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool Targetable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool Invincible { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float health = 3f;
    public bool targetable = true;
    public void OnHit(float damage, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }


    IEnumerator IDamageale.OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }
}
