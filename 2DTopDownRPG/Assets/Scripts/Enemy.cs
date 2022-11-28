using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    #region Experience
    public int xpValue = 1;
    #endregion

    #region Logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    #endregion

    #region Hitbox
    private BoxCollider2D hitbox;
    private List<Collider2D> hits = new List<Collider2D>(10);
    #endregion

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
            }

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    
                }
            }
        }


        
    }
}
