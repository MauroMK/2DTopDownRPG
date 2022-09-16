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
        DontDestroyOnLoad(gameObject);
    }

    // Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    // public weapon weapon...

    // Logic
    public int coins;
    public int experience;

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
        saving += "0";

        PlayerPrefs.SetString("SaveState", saving);
    }

    public void LoadState(Scene saving, LoadSceneMode mode)
    {

        if(!PlayerPrefs.HasKey("SaveState"))
        return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player skin
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change the weapon level
    }
}
