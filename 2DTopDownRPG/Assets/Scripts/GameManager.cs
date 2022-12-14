using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            return;
        }
        
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //* Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //* References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public GameObject deathMenu;

    //* Logic
    public int coins;
    public int experience;

    //* Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //* Death Menu
    public void ShowDeathMenu()
    {
        deathMenu.SetActive(true);
    }

    //* Respawn
    public void RespawnButton()
    {
        SceneManager.LoadScene("MainScene");
        deathMenu.SetActive(false);
        player.Respawn();
    }

    //* Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // Is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        
        return false;
    }

    //* Hitpoint bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //* Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //* If max level
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel(); // Get the current level before getting the experience
        experience += xp;

        if (currentLevel < GetCurrentLevel()) // If did level up, call the function OnLevelUp
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
        //TODO throw some particles like runescape and a text saying you leveled up
    }

    //* On Scene Loaded
    public void OnSceneLoaded(Scene saving, LoadSceneMode mode)
    {
        //* Teleport to the checkpoint
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Save state
    /*
    INT preferedSkin
    INT coins
    INT experience
    INT weaponLevel
    */
    public void SaveState()
    {
        string saving = "";

        saving += "0" + "|";
        saving += coins.ToString() + "|";
        saving += experience.ToString() + "|";
        saving += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", saving);
    }

    public void LoadState(Scene saving, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //* Change player skin
        coins = int.Parse(data[1]);

        //* Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //* Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
