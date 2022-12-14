using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float distance = 0.5f;
    public float[] fireballSpeed = {2.5f, -2.5f};
    public Transform[] fireballs;

    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(fireballSpeed[i] * Time.time) * distance, Mathf.Sin(fireballSpeed[i] * Time.time) * distance, 0);
        }
    }
}
