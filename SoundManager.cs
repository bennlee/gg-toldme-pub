using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager Instance = null;

    public float lowPitchRange = .95F;
    public float highPitchRange = 1.05f;
    //UI클릭소리
    public AudioClip UIClick;

    //엔딩 크레딧 브금
    public AudioClip endingBgm;
    
    //타이틀 브금
    public AudioClip titleBgm;

    //타이틀 문 소리
    public AudioClip titleDoorOpen;

    //삽화1 브금
    public AudioClip story1Bgm;

    //삽화1 문여는 소리
    public AudioClip story1DoorOpen;

    //삽화1 똑똑똑
    public AudioClip story1Knock;

    //삽화1 쾅(문세게 닫는소리)
    public AudioClip story1DoorClose;

    //삽화2 브금
    public AudioClip story2Bgm;

    //삽화2 와아아 소리
    public AudioClip story2Crowd;

    //삽화3 브금
    public AudioClip story3Bgm;

    //삽화3 낄낄낄소리
    public AudioClip story3Laugh;

    //월드맵 지도펼치는 소리
    public AudioClip worldmapPaper;

    //월드맵 브금
    public AudioClip worldmapBgm;

    //튜토리얼 브금
    public AudioClip tutorialBgm;

    //인게임 낮 브금
    public AudioClip dayBgm1;
    public AudioClip dayBgm2;

    //인게임 밤
    public AudioClip nightBgm;

    //전사음성
    public AudioClip hero1Voice;
    //전사 공격소리 1,2
    public AudioClip hero1Attack1;
    public AudioClip hero1Attack2;

    //궁수음성
    public AudioClip hero2Voice;
    //궁수 공격소리 1,2
    public AudioClip hero2Attack1;
    public AudioClip hero2Attack2;

    //법사음성
    public AudioClip hero3Voice;
    //법사 공격소리 1,2
    public AudioClip hero3Attack1;
    public AudioClip hero3Attack2;

    //용 스킬소리 1,2,3
    public AudioClip skillDragon1;
    public AudioClip skillDragon2;
    public AudioClip skillDragon3;

    //레이저 스킬소리 1,2
    public AudioClip skillLaser1;
    public AudioClip skillLaser2;

    //스켈레톤 소리
    public AudioClip monster1Voice;

    //고스트 소리
    public AudioClip monster2Voice;

    //골렘 소리
    public AudioClip monster3Voice;

    //미니언즈 소리
    public AudioClip minionVoice1;
    public AudioClip minionVoice2;
    public AudioClip minionVoice3;
    public AudioClip minionVoice4;
    public AudioClip minionVoice5;
    public AudioClip minionVoice6;
    public AudioClip minionVoice7;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
