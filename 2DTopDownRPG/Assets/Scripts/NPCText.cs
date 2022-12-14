using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;

    private float cooldown = 4.0f;
    private float lastShout;

    protected override void OnCollide(Collider2D other)
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position, Vector3.zero, cooldown);
        }
    }
}
