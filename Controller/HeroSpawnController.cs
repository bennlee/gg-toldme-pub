using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class HeroSpawnController : MonoBehaviour
    {

        public List<GameObject> heroList = new List<GameObject>();

        public GameObject hero;
        public Vector3[] SpawnPosition = new Vector3[4];

        public GameObject[] level_1;
        public GameObject[] level_2;
        public GameObject[] level_3;
        public GameObject boss;

        int threatCount = 0;

        bool x = false;
        bool y = false;
        bool z = false;

        //public GameObject[] route = new GameObject[51];
        public int[] route = new int[51];

        public static int respawnCount = 0;

        public bool threatControlTrigger;

        //Level 1 << x << Level 2
        //쉬운난이도를 원한다면 이부분을 상승
        public int l1L2 = 25;

        //Level 2 << y << Level 3
        //쉬운난이도를 원한다면 이부분을 상승
        public int l2L3 = 40;

        //Level 3 << z << boss
        //쉬운난이도를 원한다면 이부분을 상승
        public int l3L4 = 50;

        public int l4L5 = 51;

        public void SetSpawnPosition()
        {
            SpawnPosition[0] = new Vector3(68, 2, 0);
            SpawnPosition[1] = new Vector3(-76, 2, 0);
            SpawnPosition[2] = new Vector3(-4, 2, -72);
            SpawnPosition[3] = new Vector3(-4, 2, 72);

        }
        // 먼저한번 실행(난이도설정)
        public void SetDifficulty(int firstLevel, int secondLevel, int thirdLevel)
        {
            l1L2 = firstLevel;
            l2L3 = secondLevel;
            l3L4 = thirdLevel;

        }


        ////start에 넣어주기
        //public void InputRooms()
        //{
        //    level_1 = GameObject.FindGameObjectsWithTag("Level1");
        //    level_2 = GameObject.FindGameObjectsWithTag("Level2");
        //    level_3 = GameObject.FindGameObjectsWithTag("Level3");
        //    boss = GameObject.FindGameObjectWithTag("Boss");
        //}

        //start에 넣어주기
        //용사 지능(난이도) 점진적 상승
        public void UpgradeInt()
        {
            //bool x = false;
            //bool y = false;
            //bool z = false;

            //각층의 최소값에 도달했으면 실행중지
            //while (x && y && z)
            if (!(x && y && z))
            {
                //Level 1 이 최소 5번은 돌게
                if (l1L2 > 4)
                {
                    int j = UnityEngine.Random.Range(0, 100 - respawnCount);

                    //40%(50번에 최대 -20)
                    if (j <= 4 * (100 - respawnCount) / 10)
                    {
                        l1L2--;
                        if (l1L2 == 5) { x = true; }
                    }
                }

                //Level 2 가 최소 5번은 돌게
                if (l2L3 - 1 > l1L2 && l2L3 > 9)
                {
                    int k = UnityEngine.Random.Range(0, 100 - respawnCount);

                    //60%(50번에 최대 -30)
                    if (k <= 6 * (100 - respawnCount) / 10)
                    {
                        l2L3--;
                        if (l2L3 == 10) { y = true; }
                    }
                }

                //Level 3 가 최소 5번은 돌게
                // 15의 값을 내리면 더욱더 다이나믹
                if (l3L4 - 1 > l2L3 && l3L4 > 15)
                {
                    int l = UnityEngine.Random.Range(0, 100 - respawnCount);

                    //70%(50번에 최대 -35)
                    if (l <= 7 * (100 - respawnCount) / 10)
                    {
                        l3L4--;
                        if (l3L4 == 15) { z = true; }
                    }
                }

                //캐릭터 스폰 하나당 한번만 돌게
                //break;
            }
        }


        //정해진 난이도대로 route 배열에 입력
        public void CreateRoute()
        {
            for (int i = 0; i < l1L2; i++)
            {
                route[i] = 1;
            }
            for (int i = l1L2; i < l2L3; i++)
            {
                route[i] = 2;
            }
            for (int i = l2L3; i < l3L4; i++)
            {
                route[i] = 3;
            }
            for (int i = l3L4; i < l4L5; i++)
            {
                route[i] = 4;
            }
        }

        // 한번에 5세대이상 못생김 (현실반영)
        // 죽으면 생성(투입)
        public void HeroRespawn()
        {

            //int clonedCount = 1;
            if (respawnCount < 100)
            {
                GameObject clonedHero;
                Vector3 currentSpawnPosition = SpawnPosition[Random.Range(0, SpawnPosition.Length)];
                UpgradeInt();
                CreateRoute();
                clonedHero = Instantiate(hero, currentSpawnPosition, hero.transform.rotation) as GameObject;
                respawnCount++;
                clonedHero.name = "Hero " + (respawnCount);
                heroList.Add(clonedHero);
            }
        }

        void awake()
        {
            //threatControlTrigger = true;
        }
        // Use this for initialization
        void Start()
        {
            SetDifficulty(25, 40, 50);
            SetSpawnPosition();

            //InvokeRepeating("GenHero", 0.0f, 5.0f);
            //Repeat();
            threatCount = 0;
            threatControlTrigger = true;
            StartCoroutine("SpawnControl");
            
        }

        

        void Update()
        {
            //if (heroList.Count >= 5)
            //{
            //  CancelInvoke("GenHero");
            //}


            CheckIsHeroListFull();
            ThreatMonster();
        }

        public void ThreatMonster()
        {
            if (threatControlTrigger)
            {
                if (threatCount > heroList.Count)
                {
                    threatCount = 0;
                }
                if (threatCount == 1)
                {
                    heroList[threatCount-1].GetComponent<HeroController>().ThreatMonster();
                    threatCount++;
                }
                else if (threatCount == 2)
                {
                    heroList[threatCount-1].GetComponent<HeroController>().ThreatMonster();
                    threatCount++;
                }
                else if (threatCount == 3)
                {
                    heroList[threatCount-1].GetComponent<HeroController>().ThreatMonster();
                    threatCount++;
                }
                else if (threatCount == 4)
                {
                    heroList[threatCount-1].GetComponent<HeroController>().ThreatMonster();
                    threatCount++;
                }
                else if (threatCount == 5)
                {
                    heroList[threatCount-1].GetComponent<HeroController>().ThreatMonster();
                    threatCount++;
                }
                else if (threatCount == 0)
                {
                    threatCount++;
                }
            }
        }
        public void CheckIsHeroListFull()
        {
            if (heroList.Count >= 5)
            {
                heroListFull = true;
            }
            else
            {
                heroListFull = false;
            }
        }


        IEnumerator SpawnControl()
        {
            while (true)
            {
                if (!heroListFull)
                {
                    GenHero();
                    yield return new WaitForSeconds(5);
                }
                else
                {
                    yield return new WaitForSeconds(5);
                }

            }
        }


        public bool heroListFull = false;
       
        public int awakeCount = 0;

        public void GenHero()
        { 
            //if (heroList.Count >= 5)
            //{


            HeroRespawn();

            //awakeCount = respawnCount - 1;

            awakeCount = heroList.Count - 1;

            heroList[awakeCount].SetActive(true);
            heroList[awakeCount].SendMessage("SetRoute", route);
            heroList[awakeCount].SendMessage("SetParameter", respawnCount);

            Debug.Log("Hero Respawn!");


                //awakeCount++;
            //}

        }


        //heroname = respawnCount - 1
        public void HeroDead(string heroname)
        {
            for (int i = 0; i < heroList.Count; i++)
            {
                if (heroname == heroList[i].name)
                {
                    heroList.RemoveAt(i);
                    //awakeCount--;
                }
            }

        }
    }
}