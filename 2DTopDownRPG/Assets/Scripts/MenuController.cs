using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject backgroundButton;
    
    private float speed = 0.5f;
    private float offScreen = 750;
    private float onScreen = 300;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        backgroundButton.SetActive(false);
    }

    public void MoveMenuUp()
    {
        LeanTween.moveY(container, offScreen, speed);
        this.gameObject.LeanAlpha(0, speed);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void MoveMenuDown()
    {
        LeanTween.moveY(container, onScreen, speed);
        this.gameObject.LeanAlpha(1, speed);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        backgroundButton.SetActive(true);
    }
}
