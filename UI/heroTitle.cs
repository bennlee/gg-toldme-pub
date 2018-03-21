using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroTitle : MonoBehaviour {

    public Text hero_Title;
    public GameObject devilSpeech;
    string currentTitle;

    //몇 세대 주어인지 확인하기 위한 주어 리스트
    List<string> subjectGeneration = new List<string>();

    //생성 타이틀
    List<string> Modifier = new List<string>();
    List<string> Subject = new List<string>();
    List<string> Verb = new List<string>();

    //사망 타이틀
    List<string> Dead_Modifier = new List<string>();
    List<string> Dead_Verb = new List<string>();

    public void LoadTitle()
    {
        Debug.Log("Load");
        //////Modifier (7)
        Modifier.Add("슈퍼");
        Modifier.Add("철부지");
        Modifier.Add("말괄량이");
        Modifier.Add("대머리");
        Modifier.Add("백수");
        Modifier.Add("스토커");
        Modifier.Add("시공의");

        //////Subject (11)
        Subject.Add("아재");
        Subject.Add("손자");
        Subject.Add("아들");
        Subject.Add("딸");
        Subject.Add("공주님");
        Subject.Add("왕자님");
        Subject.Add("꽃할배");
        Subject.Add("영감");
        Subject.Add("이모");
        Subject.Add("노처녀");
        Subject.Add("숫총각");

        //////Verb (6)
        Verb.Add("입장");
        Verb.Add("출동");
        Verb.Add("등장");
        Verb.Add("방문");
        Verb.Add("도착");
        Verb.Add("개입");

        //Dead_Modifier(8)
        Dead_Modifier.Add("장렬히");
        Dead_Modifier.Add("빠르게");
        Dead_Modifier.Add("마침내");
        Dead_Modifier.Add("비참히");
        Dead_Modifier.Add("아쉽게");
        Dead_Modifier.Add("드디어");
        Dead_Modifier.Add("결국");
        Dead_Modifier.Add("기어이");

        //Dead_Verb(10)
        Dead_Verb.Add("전사");
        Dead_Verb.Add("퇴근");
        Dead_Verb.Add("사망");
        Dead_Verb.Add("소멸");
        Dead_Verb.Add("승천");
        Dead_Verb.Add("죽음");
        Dead_Verb.Add("패배");
        Dead_Verb.Add("좌절");
        Dead_Verb.Add("생포");
        Dead_Verb.Add("잡힘");
    }

    public string MakeTitle()
    {

        int i = Random.Range(0, 7);
        int j = Random.Range(0, 11);
        int k = Random.Range(0, 6);
        
        currentTitle = Modifier[i] + " " + Subject[j] + "이(가) " + Verb[k] +"!";

        subjectGeneration.Add(Subject[j]);

        string temp = "";
        switch (j)
        {
            case 0: temp = "아재여..."; break;
            case 1: temp = "어린이는 출입 금지란다!"; break;
            case 2: temp = "어디 도전해 봐라!"; break;
            case 3: temp = "여기가 어딘줄 알고!"; break;
            case 4: temp = "공주는 조심히 다뤄라 얘들아"; break;
            case 5: temp = "어이쿠! 왕자님이네!"; break;
            case 6: temp = "할배여..."; break;
            case 7: temp = "영감님 허리 조심하슈"; break;
            case 8: temp = "이모 여기 맛집 아니야!"; break;
            case 9: temp = "선 보러 왔나?"; break;
            case 10: temp = "허우대만 멀쩡한 녀석이군"; break;
        }
        devilSpeech.GetComponent<TextMesh>().text = temp;

        return currentTitle;
    }

    public string DeadTitle(int gen)
    {
        int i = Random.Range(0, 8);
        int j = Random.Range(0, 10);

        string deadTitle = gen + "세대 " + Subject[gen] + "이(가) " + Dead_Modifier[i] + " " + Dead_Verb[j] + "!";
        return deadTitle;
    }

}


