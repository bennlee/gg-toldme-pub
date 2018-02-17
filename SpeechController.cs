using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            EASTEREGGS                      //이스터에그

        };
        public SpeechType currentSpeechType;


        public List<string> DBHeroOutOfDungeon = new List<string>();
        public List<string> DBHeroEntranceDungeon = new List<string>();
        public List<string> DBHeroFight = new List<string>();
        public List<string> DBHeroDead = new List<string>();
        public List<string> DBHeroTouch = new List<string>();
        public List<string> DBHeroDecideRoute = new List<string>();
        public List<string> DBHeroInFrontOfDoor = new List<string>();
        public List<string> DBHeroMeetsFather = new List<string>();
        public List<string> DBHeroChat = new List<string>();
        public List<string> DBHeroEncounterEnemy = new List<string>();
        public List<string> DBFamily = new List<string>();
        public List<string> DBEasterEggs = new List<string>();

        public GameObject textMesh;

        
        //public GameObject parentHero;

        public string currentText;

        public float speechBubbleDestroyTime = 2.0f;
        //default position & rotation
        //안써도 될듯(오히려 쓰면 월드좌표계로 이상하게됨)
        //public Vector3 popUpPosition = new Vector3(0, 6, 0);
        public Quaternion popUpRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        
        

        public void Speech()
        {
            switch (currentSpeechType)
            {
                case SpeechType.HEROOUTOFDUNGEON:
                    HeroOutOfDungeon();
                    break;
                case SpeechType.HEROENTRANCEDUNGEON:
                    HeroEntranceDungeon();
                    break;
                case SpeechType.HEROFIGHT:
                    HeroFight();
                    break;
                case SpeechType.HERODEAD:
                    HeroDead();
                    break;
                case SpeechType.HEROTOUCH:
                    HeroTouch();
                    break;
                case SpeechType.HERODECIDEROUTE:
                    HeroDecideRoute();
                    break;
                case SpeechType.HEROINFRONTOFDOOR:
                    HeroInFrontOfDoor();
                    break;
                case SpeechType.HEROMEETSFATHER:
                    HeroMeetsFather();
                    break;
                case SpeechType.HEROCHAT:
                    HeroChat();
                    break;
                case SpeechType.HEROENCOUNTERENEMY:
                    HeroEncounterEnemy();
                    break;
                case SpeechType.FAMILY:
                    Family();
                    break;
                case SpeechType.EASTEREGGS:
                    EasterEggs();
                    break;
            }
        }

        public void SetSpeechType()
        {

        }


       

        //public void GetParentHero()
        //{
        //    //parentHero = this.gameObject.GetComponentInParent<HeroController>().
        //    parentHero = this.gameObject;
        //}



        void Awake()
        {
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

            //임시로 일단 용사 혼잣말 만
            currentSpeechType = SpeechType.HEROCHAT;
        }

        // Use this for initialization
        void Start()
        {
            InvokeRepeating("Speech",0.0f,5.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }




        //********************************************************************************
        //--------------------------------SPEECH METHOD-----------------------------------
        //********************************************************************************
        public void HeroOutOfDungeon()
        {
            GameObject currentTextMesh;
            currentText = DBHeroOutOfDungeon[Random.Range(0, DBHeroOutOfDungeon.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
            Destroy(currentTextMesh, speechBubbleDestroyTime);
        }
        public void HeroEntranceDungeon()
        {
            GameObject currentTextMesh;
            currentText = DBHeroEntranceDungeon[Random.Range(0, DBHeroEntranceDungeon.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
            Destroy(currentTextMesh, speechBubbleDestroyTime);
        }
        public void HeroFight()
        {
            GameObject currentTextMesh;
            currentText = DBHeroFight[Random.Range(0, DBHeroFight.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
            Destroy(currentTextMesh, speechBubbleDestroyTime);
        }
        public void HeroDead()
        {
            GameObject currentTextMesh;
            currentText = DBHeroDead[Random.Range(0, DBHeroDead.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
            Destroy(currentTextMesh, speechBubbleDestroyTime);
        }
        public void HeroTouch()
        {
            GameObject currentTextMesh;
            currentText = DBHeroTouch[Random.Range(0, DBHeroTouch.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
            Destroy(currentTextMesh, speechBubbleDestroyTime);
        }
        public void HeroDecideRoute()
        {
            GameObject currentTextMesh;
            currentText = DBHeroDecideRoute[Random.Range(0, DBHeroDecideRoute.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
        }
        public void HeroInFrontOfDoor()
        {
            GameObject currentTextMesh;
            currentText = DBHeroInFrontOfDoor[Random.Range(0, DBHeroInFrontOfDoor.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
        }
        public void HeroMeetsFather()
        {
            GameObject currentTextMesh;
            currentText = DBHeroMeetsFather[Random.Range(0, DBHeroMeetsFather.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
        }
        public void HeroChat()
        {
            GameObject currentTextMesh;
            currentText = DBHeroChat[Random.Range(0, DBHeroChat.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
            Destroy(currentTextMesh, speechBubbleDestroyTime);
        }
        public void HeroEncounterEnemy()
        {
            GameObject currentTextMesh;
            currentText = DBHeroEncounterEnemy[Random.Range(0, DBHeroEncounterEnemy.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
        }
        public void Family()
        {
            GameObject currentTextMesh;
            currentText = DBFamily[Random.Range(0, DBFamily.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
        }
        public void EasterEggs()
        {
            GameObject currentTextMesh;
            currentText = DBEasterEggs[Random.Range(0, DBEasterEggs.Count)];
            currentTextMesh = Instantiate(textMesh, gameObject.transform.position, gameObject.transform.rotation * popUpRotation, gameObject.transform) as GameObject;
            currentTextMesh.GetComponent<TextMesh>().text = currentText;
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
    }
}