using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TVNT
{
    public class SpeechController : MonoBehaviour
    {
        public enum SpeechType
        {
            HEROOUTOFDUNGEON,               //용사 던전 밖
            HEROENTRANCEDUNGEON,            //용사 입장
            HEROFIGHT,                      //용사 싸움
            HERODEAD,                       //용사 죽음
            HEROTOUCH,                      //용사 터치
            HERODECIDEROUTE,                //용사 길 탐색
            HEROINFRONTOFDOOR,              //용사 문 앞
            HEROMEETSFATHER,                //용사 조상 만남
            HEROCHAT,                       //용사 혼잣말
            HEROENCOUNTERENEMY,             //용사 적 조우
            FAMILY,                         //용사의 가족
            EASTEREGGS,                     //이스터에그
            MONSTERCHAT,                    //몬스터 혼잣말
            MONSTERENCOUNTERHERO,           //몬스터 용사 만났을 때
            MONSTERDEPLOY,                  //몬스터 배치될 때
            MONSTERDEAD                     //몬스터 죽을 때
        };

        List<string> DBHeroOutOfDungeon = new List<string>();
        List<string> DBHeroEntranceDungeon = new List<string>();
        List<string> DBHeroFight = new List<string>();
        List<string> DBHeroDead = new List<string>();
        List<string> DBHeroTouch = new List<string>();
        List<string> DBHeroDecideRoute = new List<string>();
        List<string> DBHeroInFrontOfDoor = new List<string>();
        List<string> DBHeroMeetsFather = new List<string>();
        List<string> DBHeroChat = new List<string>();
        List<string> DBHeroEncounterEnemy = new List<string>();
        List<string> DBFamily = new List<string>();
        List<string> DBEasterEggs = new List<string>();
        List<string> DBMonsterChat = new List<string>();
        List<string> DBMonsterEncounterHero = new List<string>();
        List<string> DBMonsterDeploy = new List<string>();
        List<string> DBMonsterDead = new List<string>();


        string currentText;
        float speechBubbleDestroyTime = 2.0f;
        public SpeechType currentSpeechType;
        GameObject mainCamera;
        GameObject hero;

        GameObject parent;

        public SpeechType ChangeSituationToSpeechType(HeroController.Situation now)
        {
            switch (now)
            {
                case HeroController.Situation.HEROOUTOFDUNGEON:
                    return SpeechType.HEROOUTOFDUNGEON;
                case HeroController.Situation.HEROENTRANCEDUNGEON:
                    return SpeechType.HEROENTRANCEDUNGEON;
                case HeroController.Situation.HEROFIGHT:
                    return SpeechType.HEROFIGHT;
                case HeroController.Situation.HERODEAD:
                    return SpeechType.HERODEAD;
                case HeroController.Situation.HEROTOUCH:
                    return SpeechType.HEROTOUCH;
                case HeroController.Situation.HERODECIDEROUTE:
                    return SpeechType.HERODECIDEROUTE;
                case HeroController.Situation.HEROINFRONTOFDOOR:
                    return SpeechType.HEROINFRONTOFDOOR;
                case HeroController.Situation.HEROMEETSFATHER:
                    return SpeechType.HEROMEETSFATHER;
                case HeroController.Situation.HEROCHAT:
                    return SpeechType.HEROCHAT;
                case HeroController.Situation.HEROENCOUNTERENEMY:
                    return SpeechType.HEROENCOUNTERENEMY;
                case HeroController.Situation.FAMILY:
                    return SpeechType.FAMILY;
                case HeroController.Situation.EASTEREGGS:
                    return SpeechType.EASTEREGGS;
                case HeroController.Situation.MONSTERCHAT:
                    return SpeechType.MONSTERCHAT;
                case HeroController.Situation.MONSTERENCOUNTERHERO:
                    return SpeechType.MONSTERENCOUNTERHERO;
                case HeroController.Situation.MONSTERDEPLOY:
                    return SpeechType.MONSTERDEPLOY;
                case HeroController.Situation.MONSTERDEAD:
                    return SpeechType.MONSTERDEAD;

            }
            return 0;
        }

        public SpeechType ChangeSituationToSpeechType(MonsterAIController.Situation now)
        {
            switch (now)
            {
                case MonsterAIController.Situation.HEROOUTOFDUNGEON:
                    return SpeechType.HEROOUTOFDUNGEON;
                case MonsterAIController.Situation.HEROENTRANCEDUNGEON:
                    return SpeechType.HEROENTRANCEDUNGEON;
                case MonsterAIController.Situation.HEROFIGHT:
                    return SpeechType.HEROFIGHT;
                case MonsterAIController.Situation.HERODEAD:
                    return SpeechType.HERODEAD;
                case MonsterAIController.Situation.HEROTOUCH:
                    return SpeechType.HEROTOUCH;
                case MonsterAIController.Situation.HERODECIDEROUTE:
                    return SpeechType.HERODECIDEROUTE;
                case MonsterAIController.Situation.HEROINFRONTOFDOOR:
                    return SpeechType.HEROINFRONTOFDOOR;
                case MonsterAIController.Situation.HEROMEETSFATHER:
                    return SpeechType.HEROMEETSFATHER;
                case MonsterAIController.Situation.HEROCHAT:
                    return SpeechType.HEROCHAT;
                case MonsterAIController.Situation.HEROENCOUNTERENEMY:
                    return SpeechType.HEROENCOUNTERENEMY;
                case MonsterAIController.Situation.FAMILY:
                    return SpeechType.FAMILY;
                case MonsterAIController.Situation.EASTEREGGS:
                    return SpeechType.EASTEREGGS;
                case MonsterAIController.Situation.MONSTERCHAT:
                    return SpeechType.MONSTERCHAT;
                case MonsterAIController.Situation.MONSTERENCOUNTERHERO:
                    return SpeechType.MONSTERENCOUNTERHERO;
                case MonsterAIController.Situation.MONSTERDEPLOY:
                    return SpeechType.MONSTERDEPLOY;
                case MonsterAIController.Situation.MONSTERDEAD:
                    return SpeechType.MONSTERDEAD;
            }
            return 0;
        }
        //********************************************************************************
        //--------------------------------SPEECH METHOD-----------------------------------
        //********************************************************************************
        IEnumerator Speech()
        {
            


            while (true)
            {
                if(currentSpeechType != SpeechType.FAMILY)
                {
                    if (currentSpeechType != SpeechType.HEROMEETSFATHER)
                    {
                        if (parent.tag == "Player")
                        {
                            currentSpeechType = ChangeSituationToSpeechType(hero.GetComponent<HeroController>().currentSituation);
                        }
                        else
                        {
                            currentSpeechType = ChangeSituationToSpeechType(hero.GetComponent<MonsterAIController>().currentSituation);
                        }
                    }
                }

                
                
                yield return new WaitForEndOfFrame();
                switch (currentSpeechType)
                {
                    case SpeechType.HEROOUTOFDUNGEON:
                        currentText = DBHeroOutOfDungeon[Random.Range(0, DBHeroOutOfDungeon.Count)];
                        break;
                    case SpeechType.HEROENTRANCEDUNGEON:
                        currentText = DBHeroEntranceDungeon[Random.Range(0, DBHeroEntranceDungeon.Count)];
                        break;
                    case SpeechType.HEROFIGHT:
                        currentText = DBHeroFight[Random.Range(0, DBHeroFight.Count)];
                        break;
                    case SpeechType.HERODEAD:
                        currentText = DBHeroDead[Random.Range(0, DBHeroDead.Count)];
                        break;
                    case SpeechType.HEROTOUCH:
                        currentText = DBHeroTouch[Random.Range(0, DBHeroTouch.Count)];
                        break;
                    case SpeechType.HERODECIDEROUTE:
                        currentText = DBHeroDecideRoute[Random.Range(0, DBHeroDecideRoute.Count)];
                        break;
                    case SpeechType.HEROINFRONTOFDOOR:
                        currentText = DBHeroInFrontOfDoor[Random.Range(0, DBHeroInFrontOfDoor.Count)];
                        break;
                    case SpeechType.HEROMEETSFATHER:
                        currentText = DBHeroMeetsFather[Random.Range(0, DBHeroMeetsFather.Count)];
                        break;
                    case SpeechType.HEROCHAT:
                        currentText = DBHeroChat[Random.Range(0, DBHeroChat.Count)];
                        break;
                    case SpeechType.HEROENCOUNTERENEMY:
                        currentText = DBHeroEncounterEnemy[Random.Range(0, DBHeroEncounterEnemy.Count)];
                        break;
                    case SpeechType.FAMILY:
                        currentText = DBFamily[Random.Range(0, DBFamily.Count)];
                        break;
                    case SpeechType.EASTEREGGS:
                        currentText = DBEasterEggs[Random.Range(0, DBEasterEggs.Count)];
                        break;
                    case SpeechType.MONSTERCHAT:
                        currentText = DBMonsterChat[Random.Range(0, DBMonsterChat.Count)];
                        break;
                    case SpeechType.MONSTERENCOUNTERHERO:
                        currentText = DBMonsterEncounterHero[Random.Range(0, DBMonsterEncounterHero.Count)];
                        break;
                    case SpeechType.MONSTERDEPLOY:
                        currentText = DBMonsterDeploy[Random.Range(0, DBMonsterDeploy.Count)];
                        break;
                    case SpeechType.MONSTERDEAD:
                        currentText = DBMonsterDead[Random.Range(0, DBMonsterDead.Count)];
                        break;
                }
                transform.GetComponent<TextMesh>().text = currentText;
                yield return new WaitForSeconds(2.5f);
                transform.GetComponent<TextMesh>().text = "";
                yield return new WaitForSeconds(1.0f);
            }
        }
        void Awake()
        {
            mainCamera = GameObject.Find("MainCamera");
            SetHeroOutOfDungeon();
            SetHeroEntranceDungeon();
            SetHeroFight();
            SetHeroDead();
            SetHeroTouch();
            SetHeroDecideRoute();
            SetHeroInFrontOfDoor();
            SetHeroMeetsFather();
            SetHeroChat();
            SetHeroEncounterEnemy();
            SetFamily();
            SetEasterEggs();
            SetMonsterChat();
            SetMonsterEncounterHero();
            SetMonsterDeploy();
            SetMonsterDead();
        }

        // Use this for initialization
        void Start()
        {
            parent = transform.parent.gameObject;
            mainCamera = GameObject.Find("MainCamera");
            hero = gameObject.transform.parent.gameObject;
            StartCoroutine("Speech");
        }

        void OnEnable()
        {
            //StartCoroutine("Speech");
        }
        // Update is called once per frame
        void Update()
        {
            if (mainCamera.transform.position.y <= 70)
            {
                transform.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
            transform.eulerAngles = new Vector3(60, 0, 0);
        }

        //********************************************************************************
        //-----------------------------------CHAT DB--------------------------------------
        //********************************************************************************
        void SetHeroOutOfDungeon()
        {
            DBHeroOutOfDungeon.Add("다녀오겟소!");
            DBHeroOutOfDungeon.Add("아들아 집 잘 지켜라!");
            DBHeroOutOfDungeon.Add("술잔이 식기 전에 돌아오겠소");
        }
        void SetHeroEntranceDungeon()
        {
            DBHeroEntranceDungeon.Add("악마야 내가 간다!");
            DBHeroEntranceDungeon.Add("내 검을 받아라!");
            DBHeroEntranceDungeon.Add("조상의 복수를 하러 왔다!");
            DBHeroEntranceDungeon.Add("증조할아버지의 사명은 내가 이어받는다!");
        }
        void SetHeroFight()
        {
            DBHeroFight.Add("이얍!");
            DBHeroFight.Add("하하하!");
            DBHeroFight.Add("무난하군!");
            DBHeroFight.Add("제법 강한데..");
            DBHeroFight.Add("원! 투! 강냉이!");
            DBHeroFight.Add("남자는 선빵이지!");
            DBHeroFight.Add("앙 기모리~");
            DBHeroFight.Add("데마시아!!!!!");
        }
        void SetHeroDead()
        {
            DBHeroDead.Add("분하다..");
            DBHeroDead.Add("내 후손이 복수하러 올 것이다.");
            DBHeroDead.Add("실화냐?");
        }
        void SetHeroTouch()
        {
            DBHeroTouch.Add("아야!");
            DBHeroTouch.Add("누가 자꾸 툭툭 건드리는데..");
            DBHeroTouch.Add("나는 이곳을 정복하고 말거야");
            DBHeroTouch.Add("나를 무시하지 마라!");
        }
        void SetHeroDecideRoute()
        {
            DBHeroDecideRoute.Add("여기가 아닌가..?");
            DBHeroDecideRoute.Add("이 길이 아닌가..?");
            DBHeroDecideRoute.Add("증조할아버지가 이쪽 길로는 가지 말라고 하셨어");
        }
        void SetHeroInFrontOfDoor()
        {
            DBHeroInFrontOfDoor.Add("이쪽이군.");
            DBHeroInFrontOfDoor.Add("용사! 가고싶은대로 간다!");
            DBHeroInFrontOfDoor.Add("왔던 곳은 아니겠지");
            DBHeroInFrontOfDoor.Add("이쪽임이 틀림없다");
            DBHeroInFrontOfDoor.Add("가즈아~");
            DBHeroInFrontOfDoor.Add("큿..! 결계인가?!");
        }
        void SetHeroMeetsFather()
        {
            DBHeroMeetsFather.Add("오랜만입니다.");
            DBHeroMeetsFather.Add("제가 그 후손입니다.");
            DBHeroMeetsFather.Add("세배 받으시지요");
            DBHeroMeetsFather.Add("오랜만이구나!");
            DBHeroMeetsFather.Add("오오 네가 민수렸다!");
            DBHeroMeetsFather.Add("왜이리 늦었느냐?");
        }
        void SetHeroChat()
        {
            DBHeroChat.Add("치킨먹고싶다");
            DBHeroChat.Add("왜이리 복잡해 여기");
            DBHeroChat.Add("벌써부터 배고프다");
            DBHeroChat.Add("개발자 수준 나오죠?");
            DBHeroChat.Add("망겜 왜하냐 ㅋㅋ");
            DBHeroChat.Add("그만 들여다봐 변태들아");
            DBHeroChat.Add("호옹이?");
            DBHeroChat.Add("사실 나가고싶다");
            DBHeroChat.Add("여기가 아닌가..?");
            DBHeroChat.Add("이 길이 아닌가..?");
            DBHeroChat.Add("증조할아버지가 이쪽 길로는 가지 말라고 하셨어");
        }
        void SetHeroEncounterEnemy()
        {
            DBHeroEncounterEnemy.Add("네놈이냐?");
            DBHeroEncounterEnemy.Add("드디어 복수를 할 수 있겠군!");
            DBHeroEncounterEnemy.Add("간다아아아!");
            DBHeroEncounterEnemy.Add("강해보이는데... 도망칠까?");
            DBHeroEncounterEnemy.Add("민식이냐??");
            DBHeroEncounterEnemy.Add("조무래기로군");
            DBHeroEncounterEnemy.Add("(삐빗)호오! 전투력이 332. 이런 놈도 있었나?");
            DBHeroEncounterEnemy.Add("이..이녀석!(퍼엉!) 내 전투력 스카우터!!");
        }
        void SetFamily()
        {
            DBFamily.Add("잘 다녀와요!");
            DBFamily.Add("곧 뒤따라 가겠습니다!");
            DBFamily.Add("할아버지를 구해다오!");
            DBFamily.Add("용돈주고가!");
        }
        void SetEasterEggs()
        {
            DBEasterEggs.Add("화톳불을 찾아서..");
            DBEasterEggs.Add("헤세드로 간다");
        }
        void SetMonsterChat()
        {
            DBMonsterChat.Add("마왕 꼰대놈 같으니라고");
            DBMonsterChat.Add("형 이건아니야..");
            DBMonsterChat.Add("야 잘좀 해봐라");
            DBMonsterChat.Add("이게다 마왕때문입니다.");
            DBMonsterChat.Add("일하기 귀찮네");
        }
        void SetMonsterEncounterHero()
        {
            DBMonsterEncounterHero.Add("아버지 이쪽으로 가셨는데?");
            DBMonsterEncounterHero.Add("응~ 이쪽 아니야~");
        }
        void SetMonsterDeploy()
        {
            DBMonsterDeploy.Add("형 이쪽에 놓지마");
            DBMonsterDeploy.Add("아야! 살살 놔");
            DBMonsterDeploy.Add("여기는 아무도 안올거같은데?");
            DBMonsterDeploy.Add("다시한번 생각해봐~");
        }
        void SetMonsterDead()
        {
            DBMonsterDead.Add("퇴근 개꿀띠~");
            DBMonsterDead.Add("칼퇴해야겠다");
            DBMonsterDead.Add("승천각 나오냐..");
        }
    }
}