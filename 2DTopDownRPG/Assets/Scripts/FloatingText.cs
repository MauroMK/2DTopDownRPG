using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gObject;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gObject.SetActive(true);
    }

    public void Hide()
    {
        active = false;
        gObject.SetActive(false);
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }

        if (Time.time - lastShown > duration)
        {
            Hide();
        }
        // Moves the text up a little bit
        gObject.transform.position += motion * Time.deltaTime;
    }
    
}
