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
            return;
        }
        
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject); // Keeps the same game manager when you load another scene
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

    //* Logic
    public int coins;
    public int experience;

    //* Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
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

    public int GetXToLevel(int level)
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

        if(!PlayerPrefs.HasKey("SaveState"))
        return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //* Change player skin
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        //* Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
