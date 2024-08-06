using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageale 
{
    public float Health { get; set; }
    public void OnHit (float damage, Vector2 knockback);
    public void OnHit(float damage);
    public bool Targetable { get; set; }
    public bool Invincible {  get; set; }
    public IEnumerator OnObjectDestroyed();
}
