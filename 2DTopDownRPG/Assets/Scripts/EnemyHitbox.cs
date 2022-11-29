using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float knockback = 2;

    protected override void OnCollide(Collider2D other)
    {
        if (other.tag == "Fighter" && other.name == "Player")
        {
            // Create a new damage object, then we'll send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                knockback = knockback
            };

            other.SendMessage("ReceiveDamage", dmg);
        }
    }
}
