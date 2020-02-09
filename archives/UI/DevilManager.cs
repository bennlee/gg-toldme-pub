using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DevilManager : MonoBehaviour {

    //instance put to inspector
    //public Text t_userName;
    public Text t_Level;
    public Text t_maxSouls;
    public Text t_regainSouls;
    public Text t_monsAttack;
    public Text t_monsShield;
    //public Text t_skillPoints;
    public GameObject levelupBtn;
    public Text t_totalGolds;
    public Text t_requireGolds;
    public Text t_totalHero;
    //public Text t_totalVillage;
    
    //local DB instance
    int level;
    int maxSouls;
    float regainSouls;
    float monsAttack;
    float monsShield;
    int totalGolds;
    int requireGolds;
    int totalHero;
    int totalVillage;
    int worldMapCount;
    int gameCount;
    int gameTutorial;

    SceneManager SceneManager;
    
	// Use this for initialization
	void OnEnable ()
    {
        LoadData();
        TextSetting();
    }

    private void Start()
    {
        LevelUpUI();
    }

    void LevelUpUI()
    {
        if (totalGolds < requireGolds)
        {
            //t_requireGolds.color = Color.red;
            levelupBtn.GetComponent<Image>().color = new Color(250.0f / 255.0f, 171.0f / 255.0f, 8.0f / 255.0f, 50.0f / 255.0f);
        }
        else
        {
            //t_requireGolds.color = Color.white;
            levelupBtn.GetComponent<Image>().color = new Color(250.0f / 255.0f, 171.0f / 255.0f, 8.0f / 255.0f, 200.0f / 255.0f);
        }
    }

    //button functions
    public void BackBtn()
    {
        Debug.Log("Back to world map...");
        SceneManager.LoadScene(2);
    }

    void TextSetting()
    {
        regainSouls = Mathf.Round(regainSouls * 100) * 0.01f;
        monsAttack = Mathf.Round(monsAttack * 100) * 0.01f;
        monsShield = Mathf.Round(monsShield * 100) * 0.01f;
        //t_userName.text = PlayerPrefs.GetString("UserName");
        t_Level.text = ("Lv." + level);
        t_maxSouls.text = ("최대 소울 : " + maxSouls);
        t_regainSouls.text = ("소울 재생 : " + regainSouls +"/초");
        t_monsAttack.text = ("몬스터 충성도 : " + monsAttack);
        //t_monsShield.text = ("몬스터 충성도 : " + monsShield);
        t_totalGolds.text = ("" + totalGolds);
        t_requireGolds.text = ("" + requireGolds);
        t_totalHero.text = ("지금까지 잡은 용사 : " + totalHero + "명");
        //t_totalVillage.text = ("지금까지 정복한 마을 : " + totalVillage + "개");
        LevelUpUI();
    }

    public void LevelUpBtn()
    {
        if(totalGolds >= requireGolds)
        {
            Debug.Log("level up");

            totalGolds -= requireGolds;
            level++;
            maxSouls = 100 + level * 2;
            regainSouls = 0.8f + level * 0.17f ;
            monsAttack = 5 + level * 1.33f;
            monsShield = 5 + level * 1.23f;
            requireGolds += (int)(level*4.3f);

            TextSetting();
            SaveData();
        }
    }

    //load from loacl DB
    void LoadData()
    {
        //Debug.Log("DB Load...");
        level = PlayerPrefs.GetInt("Level");
        maxSouls = PlayerPrefs.GetInt("MaxSouls");
        regainSouls = PlayerPrefs.GetFloat("RegainSouls");
        monsAttack = PlayerPrefs.GetFloat("MonsAttack");
        monsShield = PlayerPrefs.GetFloat("MonsShield");
        totalGolds = PlayerPrefs.GetInt("TotalGolds");
        requireGolds = PlayerPrefs.GetInt("RequireGolds");
        totalHero = PlayerPrefs.GetInt("TotalHeroCount");

        worldMapCount = PlayerPrefs.GetInt("WorldMapCount");
        int gameCount = PlayerPrefs.GetInt("GameCount");
        int gameTutorial = PlayerPrefs.GetInt("GameCount");
    }

    //save to local DB
    void SaveData()
    {
        //Debug.Log("DB save...");
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("MaxSouls", maxSouls);
        PlayerPrefs.SetFloat("RegainSouls", regainSouls);
        PlayerPrefs.SetFloat("MonsAttack", monsAttack);
        PlayerPrefs.SetFloat("MonsShield", monsShield);
        PlayerPrefs.SetInt("TotalGolds", totalGolds);
        PlayerPrefs.SetInt("RequireGolds", requireGolds);
        PlayerPrefs.SetInt("TotalHeroCount", totalHero);

        PlayerPrefs.SetInt("WorldMapCount", worldMapCount);
        PlayerPrefs.SetInt("GameCount", gameCount);
        PlayerPrefs.SetInt("GameCount", gameTutorial);
    }

    public void ResetData()
    {
        level = 1;
        maxSouls = 100;
        regainSouls = 0.8f;
        monsAttack = 5;
        monsShield = 5;
        totalGolds = 10000;
        requireGolds = 3;
        totalHero = 0;

        SaveData();
        TextSetting();
    }

}
