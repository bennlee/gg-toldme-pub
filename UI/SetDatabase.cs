using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDatabase : MonoBehaviour {

    int count;

    // 게임 처음 시작하는 사람들은 Data를 모두 초기값으로 설정해준다.
    void Awake () {

        count = PlayerPrefs.GetInt("StartCount");
        PlayerPrefs.SetInt("StartCount", count);
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
        if (count == 0) { SetData(); }
    }

    public void OnClick()
    {
        if(count > 0)
        {
            //미니맵 로드
            SceneManager.LoadScene(2);
        }
        else
        {
            //튜토리얼 로드
            SceneManager.LoadScene(1);
        }
    }
    
    void SetData() {
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

        int worldMapCount = 0;
        int gameCount = 0;
        int gameTutorial = 0;

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

            PlayerPrefs.SetInt("WorldMapCount", worldMapCount);
            PlayerPrefs.SetInt("GameCount", gameCount);
        PlayerPrefs.SetInt("GameCount", gameTutorial);
    }
}
