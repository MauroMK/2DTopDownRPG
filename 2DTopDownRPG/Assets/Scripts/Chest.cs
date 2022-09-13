using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chest is using the code from collidable, that is using the code from monobehavior
// This is called Inheritance
public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinsAmount = 5;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant " + coinsAmount + " coins.");
        }
    }
}
