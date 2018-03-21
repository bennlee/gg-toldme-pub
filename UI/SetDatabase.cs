using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDatabase : MonoBehaviour {

    int count;

    // 게임 처음 시작하는 사람들은 Data를 모두 초기값으로 설정해준다.
    void Awake () {

        //Screen.SetResolution(1080, 1920, true);
        Screen.SetResolution((Screen.height * 9) / 16, Screen.height, true);
        count = PlayerPrefs.GetInt("GameCount");
        
        //PlayerPrefs.SetInt("GameCount", count);
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
        PlayerPrefs.DeleteAll();
        Debug.Log(count);
        SceneManager sceneManager;
        if (count == 0) { SetData(); }
    }

    public void OnClick()
    { 
            //튜토리얼 로드
            count++;
            PlayerPrefs.SetInt("GameCount", count);
            SceneManager.LoadScene(1);
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
        int tutorialCount = 0;

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

        //게임 시작 횟수
        PlayerPrefs.SetInt("GameCount", gameCount);

        //월드맵 튜토리얼
        PlayerPrefs.SetInt("WorldMapCount", worldMapCount);

        //게임 튜토리얼
        PlayerPrefs.SetInt("TutorialCount", tutorialCount);
    }
}
