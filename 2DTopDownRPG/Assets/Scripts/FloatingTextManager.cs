using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        
    }

    private FloatingText GetFloatingText()
    {
        /* 
        for (int i = 0; i < floatingTexts.Count; i++)
        {
            if(!floatingTexts[i].active)
        } 
        same thing 
        */

        // Function that returns a floating text
        FloatingText txt = floatingTexts.Find(t => !t.active);

        // ta procurando o texto, se ja estiver um, só mostra ele, se não tiver, cria um novo
        if (txt == null)
        {
            txt = new FloatingText();
            txt.gObject = Instantiate(textPrefab);
            txt.gObject.transform.SetParent(textContainer.transform);
            txt.txt = txt.gObject.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;

    }
}
