using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text
    public Text levelText, hitpoinText, coinsText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            // If gone too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            // If gone too far away
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update the character information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }

        // Meta
        hitpoinText.text = GameManager.instance.player.hitpoint.ToString();
        coinsText.text = GameManager.instance.coins.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // XP bar

        int currLevel = GameManager.instance.GetCurrentLevel();

        if (currLevel == GameManager.instance.xpTable.Count) // If max level
        {
            xpText.text = GameManager.instance.experience.ToString() + "total xp"; // Display total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int previousLevelXp = GameManager.instance.GetXToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXToLevel(currLevel);

            int difference = currLevelXp - previousLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - previousLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)difference;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + difference;
        }
    }
}
