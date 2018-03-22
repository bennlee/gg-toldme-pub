using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDatabase : MonoBehaviour
{

    void Awake()
    {
        Screen.SetResolution((Screen.height * 9) / 16, Screen.height, true);
    }

    void Start()
    {
        //디버그가 필요할 때 실행. 모든 키값 삭제.
        //PlayerPrefs.DeleteAll();
        SceneManager sceneManager;
    }

    public void OnClick()
    {
        Debug.Log("delete start");
        //SetData()를 한 적이 있다면
        if (PlayerPrefs.HasKey("StartTutorial"))
        {
            //맵으로 이동
            SceneManager.LoadScene(2);
        }
        else
        {
            //데이터 생성 및 저장
            SetData();
            PlayerPrefs.Save();

            //시나리오로 이동
            SceneManager.LoadScene(1);
        }
    }
    
    void SetData()
    {
        //인게임 초깃값
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("MaxSouls", 100);
        PlayerPrefs.SetFloat("RegainSouls", 0.8f);
        PlayerPrefs.SetFloat("MonsterStat", -100);
        PlayerPrefs.SetInt("TotalGolds", 0);
        PlayerPrefs.SetInt("RequireGolds", 3);
        PlayerPrefs.SetInt("TotalHero", 0);
        PlayerPrefs.SetInt("Gem", 0);
        //시작 듀토리얼 수행 유무
        PlayerPrefs.SetInt("StartTutorial", 0);
    }
}