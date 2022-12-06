using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chest is using the code from collidable, that is using the code from monobehavior
// This is called Inheritance
public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinsAmount;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coinsAmount;
            GameManager.instance.ShowText("+" + coinsAmount + " coins", 25, Color.yellow, transform.position, Vector3.up * 30, 1.5f);
        }
    }
}
