using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject container;
    
    private float speed = 0.5f;
    private float fadeSpeed = 0.5f;
    private float offScreen = 750;
    private float onScreen = 300;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void MoveMenuUp()
    {
        LeanTween.moveY(container, offScreen, speed); // Moves the container up
        LeanTween.alphaCanvas(canvasGroup, 0, fadeSpeed);
        canvasGroup.interactable = false;               
        canvasGroup.blocksRaycasts = false;
    }

    public void MoveMenuDown()
    {
        LeanTween.moveY(container, onScreen, speed);
        LeanTween.alphaCanvas(canvasGroup, 1, fadeSpeed);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}