using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDatabase : MonoBehaviour {
    
	// 게임 처음 시작하는 사람들은 Data를 모두 초기값으로 설정해준다.
	void Awake () {
        int count = PlayerPrefs.GetInt("StartCount");
        int level = 1;
        int maxSouls = 100;
        float regainSouls = 0.8f;
        float monsAttack = 5;
        float monsShield = 5;
        int totalGolds = 0;
        int requireGolds = 3;
        int totalHero = 0;
        int totalVillage = 0;
        int gemCount = 0;

        if (count == 0)
        {
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetInt("MaxSouls", maxSouls);
            PlayerPrefs.SetFloat("RegainSouls", regainSouls);
            PlayerPrefs.SetFloat("MonsAttack", monsAttack);
            PlayerPrefs.SetFloat("MonsShield", monsShield);
            PlayerPrefs.SetInt("TotalGolds", totalGolds);
            PlayerPrefs.SetInt("RequireGolds", requireGolds);
            PlayerPrefs.SetInt("TotalHero ", totalHero);
            PlayerPrefs.SetInt("TotalVillage ", totalVillage);
            PlayerPrefs.SetInt("Gem", gemCount);
        }

        //level = PlayerPrefs.GetInt("Level");
        //maxSouls = PlayerPrefs.GetInt("MaxSouls");
        //regainSouls = PlayerPrefs.GetFloat("RegainSouls");
        //monsAttack = PlayerPrefs.GetFloat("MonsAttack");
        //monsShield = PlayerPrefs.GetFloat("MonsShield");
        //totalGolds = PlayerPrefs.GetInt("TotalGolds");
        //requireGolds = PlayerPrefs.GetInt("RequireGolds");
        //totalHero = PlayerPrefs.GetInt("TotalHero");
        //totalVillage = PlayerPrefs.GetInt("TotalVillage");
    }

    void Start()
    {
        SceneManager sceneManager;
    }

    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }


}
