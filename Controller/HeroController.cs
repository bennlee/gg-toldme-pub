using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class HeroController : TVNTCharacterController
    {
        public enum TargetType
        {
            //MAP, MONSTER, BOX
            Type1,
            Type2,
            Type3
        };
        //**********************************************************************************
        //----------------------------- SpeechController -----------------------------------
        //**********************************************************************************
        public enum Situation
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
        }
        public Situation currentSituation;

        public void SetSituation(Situation now)
        {
            currentSituation = now;
        }
        //------------------------End of for SpeechController -------------------------------


        //**********************************************************************************
        //----------------------------------- Fight ----------------------------------------
        //**********************************************************************************

        public float skill1Mana = 20;
        public float skill2Mana = 50;

        GameObject heroSpawnController;

        //현재타겟
		public Collider nowTarget;

        //공격 모션이 끝났는지를 체크하는 변수
        public float currentAttackDurationTime;

        //몬스터랑 싸울것인지 지나칠것인지 판단할 변수(몬스터로 길막당했을때는 대비해서 공격당할시 false)
        public bool passMonster;

        //가까운 몬스터를 타겟으로 정했는지 판단할 변수
        public bool decideTarget;

        //타겟을 인식하고 이동하기 시작하는 거리
        public float recognitionReach = 10.0f;

        //타겟이 공격범위 안에 도달했다고 인식할 거리
        public float reach = 5.0f;

        public float nowTargetDiff = float.MaxValue;
		public Vector3 nowTargetPosition;

        bool isMonster = false;
        
        public enum WeaponType
        {
            SWORD,
            BOW,
            WAND
        }
        public enum AttackType
        {
            NormalAttack,
            Skill1,
            Skill2
        }

        public WeaponType currentWeaponType;
        public AttackType currentAttackType;
        public int attackTypeSetting;
        //init Start
        public void SetWeaponType()
        {
            if(gameObject.GetComponent<WeaponController>().currentWeaponType == WeaponController.WeaponType.SWORD)
            {
                currentWeaponType = WeaponType.SWORD;
            }
            else if(gameObject.GetComponent<WeaponController>().currentWeaponType == WeaponController.WeaponType.BOW)
            {
                currentWeaponType = WeaponType.BOW;
            }
            else if(gameObject.GetComponent<WeaponController>().currentWeaponType == WeaponController.WeaponType.WAND)
            {
                currentWeaponType = WeaponType.WAND;
            }
        }

        public void DecideAttackType()
        {
            if (mana > skill2Mana)
            {
				attackTypeSetting = Random.Range(0, 3);
            }
            else if (mana > skill1Mana)
			{
				attackTypeSetting = Random.Range(0, 2);
            }
            else
            {
                attackTypeSetting = 0;
            }
            SetReach();
        }

        public void SetReach()
        {
            if (currentWeaponType == WeaponType.SWORD)
            {
                switch (attackTypeSetting)
                {
                    case 0:
                        recognitionReach = 10.0f;
                        reach = 5.0f;
                        break;
                    case 1:
                        recognitionReach = 15.0f;
                        reach = 10.0f;
                        break;
                    case 2:
                        recognitionReach = 20.0f;
                        reach = 15.0f;
                        break;
                }
            }
            else if(currentWeaponType == WeaponType.BOW)
            {
                switch (attackTypeSetting)
                {
                    case 0:
                        recognitionReach = 10.0f;
                        reach = 5.0f;
                        break;
                    case 1:
                        recognitionReach = 15.0f;
                        reach = 10.0f;
                        break;
                    case 2:
                        recognitionReach = 20.0f;
                        reach = 15.0f;
                        break;
                }
            }
            else if (currentWeaponType == WeaponType.WAND)
            {
                switch (attackTypeSetting)
                {
                    case 0:
                        recognitionReach = 15.0f;
                        reach = 10.0f;
                        break;
                    case 1:
                        recognitionReach = 15.0f;
                        reach = 10.0f;
                        break;
                    case 2:
                        recognitionReach = 30.0f;
                        reach = 20.0f;
                        break;
                }
            }
        }


        public void ThreatMonster()
        {
            //Debug.Log(gameObject.name + "ThreatMonster");
            //최대거리 = 227.0f, 레이어 14 = Monster
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 227f, 1<<14);
            int i = 0;
            //heroSpawnController.GetComponent<HeroSpawnController>().threatControlTrigger = false;
            float monsterDiff = 0;
            //Debug.Log(hitColliders.Length);
            while (i < hitColliders.Length)
            {
                //같은 레벨에 있는 몬스터에게만 영향 및 타겟팅
                if (hitColliders[i].gameObject.tag == "Monster" && hitColliders[i].gameObject.GetComponent<MonsterAIController>().level == route[iterator])
                {
                    monsterDiff = Vector3.Distance(transform.position, hitColliders[i].gameObject.transform.position);
                    //몬스터 경계수치 거리에 반비례하게 상승
                    //프레임마다 실행해야함
                    hitColliders[i].gameObject.GetComponent<MonsterAIController>().Threaten(monsterDiff);
                    //Debug.Log(gameObject.name+"WWWW");
                    hitColliders[i].gameObject.GetComponent<MonsterAIController>().TargetHero(gameObject, monsterDiff);
                    //hitColliders[i].gameObject.GetComponent<MonsterAIController>().IncreaseThreatenTime();
                    
                    if (monsterDiff < recognitionReach && !decideTarget)
                    {
                        //타겟몬스터를 설정했음을 알려준다.
                        decideTarget = true;
                        nowTarget = hitColliders[i];
						nowTargetPosition = nowTarget.gameObject.transform.position;
                        SetTargetToMonster(nowTarget);
                    }
                    if (decideTarget)
                    {
                        nowTargetDiff = Vector3.Distance(transform.position, nowTargetPosition);
                    }
                    if (nowTargetDiff <= reach && !isMonster && decideTarget)
                    {
                        isMonster = true;
                    }
                    if (nowTargetDiff > reach && isMonster && decideTarget)
                    {
                        isMonster = false;
                    }
                }
                i++;
            }
            //heroSpawnController.GetComponent<HeroSpawnController>().threatControlTrigger = true;
        }

		public Collider dummy_fuck;

        //in start()
        public IEnumerator Fight()
        {

            while (true)
            {
                DecideAttackType();
                //currentAttackDurationTime = gameObject.GetComponent<WeaponController>().currentDurationOfAttackType;
                currentAttackDurationTime = 1.0f;
                if (isMonster)
                {
                    LookAtEnemy();
                    gameObject.GetComponent<WeaponController>().target = nowTarget.gameObject;
                    if (nowTarget.gameObject.activeSelf)
                    {
                        nowTarget.GetComponent<MonsterAIController>().LifeLost(nowTarget.GetComponent<TVNTCharacterController>().lives);
                        myAnimator.SetTrigger("Sword");
                        gameObject.GetComponent<WeaponController>().SetAttackType(attackTypeSetting);
                        UseMana(attackTypeSetting);
                        yield return new WaitForSeconds(currentAttackDurationTime);
                    }
                    else
                    {
						Debug.Log ("monster dead");
                        //nowTarget.GetComponent<MonsterAIController>().CharacterDead();
                        target = currentTarget;
                        targetPosition = currentTargetPosition;
                        decideTarget = false;
                        isMonster = false;
                        nowTargetDiff = float.MaxValue;
                        //nowTarget = null;
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }


        public void SetTargetToMonster(Collider nowTarget)
        {
            currentTarget = target.transform;
            currentTargetPosition = targetPosition;
            target = nowTarget.transform;
            targetPosition = target.position;
        }
        public void LookAtEnemy()
        {
            transform.LookAt(target);
//			Debug.Log (target.position);
        }

        //마나감소
        public void UseMana(int skill)
        {
            switch (skill)
            {
                case 1:
                    mana -= skill1Mana;
                    break;
                case 2:
                    mana -= skill2Mana;
                    break;
            }
        }
        //----------------------------- End of Fight ---------------------------------------

        //init Start
        public int generation;

        public float maxMana;
        public float mana;

        public float movementDelay = 0.8f;
        private float currentMovementDelay;

        public Transform target;
        public Vector3 targetPosition;

        public Transform currentTarget;
        public Vector3 currentTargetPosition;

        public int[] route = new int[50];
        int iterator = 0;

        List<GameObject> level1Ground = new List<GameObject>();
        List<GameObject> level2Ground = new List<GameObject>();
        List<GameObject> level3Ground = new List<GameObject>();
        GameObject level4;

        private bool invincible = false;

        public void Awake()
        {
            //무기 타입에 따라 처음에 리치 결정
            //SetReach();
        }
        //시작할때는 일단 가까운 버튼으로 향하자.
        protected override void Start()
        {
            heroSpawnController = GameObject.FindGameObjectWithTag("HeroSpawnController");
            SetWeaponType();
            generation = HeroSpawnController.respawnCount;
            currentMovementDelay = movementDelay;
            level1Ground.AddRange(GameObject.FindGameObjectsWithTag("Level1"));
            level2Ground.AddRange(GameObject.FindGameObjectsWithTag("Level2"));
            level3Ground.AddRange(GameObject.FindGameObjectsWithTag("Level3"));
            level4 = GameObject.FindGameObjectWithTag("Level4");

            findLevel1Room();
            iterator--;

            StartCoroutine("Fight");
            base.Start();
        }



        void Update()
        {
            ParameterRecovery();
            ThreatMonster();
            Vector3 start = transform.localPosition;
            Vector3 end = start + new Vector3(horizontal * PatternSettings.tiledSize, 0f, vertical * PatternSettings.tiledSize);

            //활성화되어있고, 게임을 시작한지 0초가 지났고, 타겟이 설정되어 있다면
            if (activate && Time.timeScale > 0 && !isMonster)
            {
                //가만히 있는 상태와 skipTurn이 false라면
                if (idle && skipTurn == false)
                {
                    //땅으로 미끄러지고 있거나 현재 이동 딜레이가 이동딜레이보다 크거나 같다면
                    if (onSlidingGround || currentMovementDelay >= movementDelay)
                    {
                        //땅으로 미끄러지고 있는 것이 거짓이라면
                        if (onSlidingGround == false)
                        {

                            if (Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f
                                && Mathf.Abs(transform.position.z - targetPosition.z) < 0.1f)
                            {
                                //Debug.Log ("on target.");
                                if (route[iterator] == 1)
                                {
                                    findLevel1Room();
                                }
                                else if (route[iterator] == 2)
                                {
                                    findLevel2Room();
                                }
                                else if (route[iterator] == 3)
                                {
                                    findLevel3Room();
                                }
                                else if (route[iterator] == 4)
                                {
                                    findLevel4Room();
                                }

                                //Debug.Log(iterator + ", " + route[iterator]);
                            }


                            GetMovementDirection(); //타겟을 향해 움직인다
                        }
                        //수평이 0이 아니면 수직은 0이다 (?)
                        if (horizontal != 0)
                        {
                            vertical = 0;
                        }
                        //수평과 수직이 모두 0이면
                        if (horizontal == 0 && vertical == 0)
                        {
                            PlayIdleAnimation(); //대기 상태 애니메이션 재생
                                                 //포지션은 플레이어의 y오프셋에 따라 움직인다
                            transform.position = transform.parent.position + new Vector3(0, PatternSettings.playerYOffset, 0);
                            currentMovementDelay = 0f;
                            //수평이 0이 아니거나 수직이 0이 아니라면
                        }
                        else if (horizontal != 0 || vertical != 0)
                        {
                            AttemptMove(); //움직임 시도
                            currentMovementDelay = 0f;

                        }
                        //수직이 0이 아닐 경우
                    }
                    else
                    {
                        currentMovementDelay += Time.deltaTime;
                        PlayIdleAnimation(); //대기 애니메이션 재생
                    }
                }
                skipTurn = false;
                //활성화되있지 않으면 대기 애니메이션 재생
            }
            else if (Time.timeScale > 0)
            {
                PlayIdleAnimation();
            }
        }

        //이동 방향을 설정한다
        private void GetMovementDirection()
        {
            //Debug.Log ("Movement function launch");

            bool foundTargetSpot = false;
            float distanceToTarget = float.MaxValue;
            //start와 end는 플레이어와의 y오프셋 값
            Vector3 start = transform.parent.position + new Vector3(0, PatternSettings.playerYOffset, 0);
            Vector3 end = start;
            int tempHorizontal = 0;
            int tempVertical = 0;

            RaycastHit hitMonsterInfo;

            //x : -1, 0, 1
            for (int x = -1; x <= 1; x += 1)
            {
                //z : -1, 0, 1
                for (int z = -1; z <= 1; z += 1)
                {
                    //만약 x제곱과 z제곱이 같지 않다면
                    if (x * x != z * z)
                    {
                        //To prevent the AI from turning back on itself
                        if ((horizontal == 0 || horizontal != -x) && (vertical == 0 || vertical != -z))
                        {
                            end = start + new Vector3(x * PatternSettings.tiledSize, 0f, z * PatternSettings.tiledSize);
                            //linecast를 start지점부터 end지점까지 배리어마스크를 이용해 쏘았을 때 리턴값이 없다면
                            if (Physics.Linecast(start + barrierOffset, end + barrierOffset, out hitMonsterInfo, barrierLayerMask) == false)
                            {
                                //Debug.DrawLine(start + barrierOffset, end + barrierOffset);
                                //end가 땅에 속하는 지점이라면
                                if (HasGround(end))
                                {
                                    //타겟까지의 현재 거리를 구하고 수직, 수평거리를 설정해준다.
                                    float currentDistanceToTarget = (targetPosition - end).sqrMagnitude;
                                    if (currentDistanceToTarget < distanceToTarget)
                                    {
                                        foundTargetSpot = true;
                                        distanceToTarget = currentDistanceToTarget;
                                        tempHorizontal = x;
                                        tempVertical = z;
                                        if (flipHorizontalInput)
                                        {
                                            tempHorizontal *= -1;
                                        }
                                        if (flipVerticalInput)
                                        {
                                            tempVertical *= -1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (foundTargetSpot == false)
            {
                horizontal = 0;
                vertical = 0;
                return;
            }

            horizontal = tempHorizontal;
            vertical = tempVertical;
        }

        //땅 위에 있는지 없는지를 판별
        private bool HasGround(Vector3 end)
        {

            boxCollider.enabled = false;
            RaycastHit hitInfo;
            if (Physics.Linecast(end + groundOffset, end + new Vector3(0, -1f, 0) + groundOffset, out hitInfo, groundLayerMask))
            {
                GroundCollider targetGroundCollider = hitInfo.transform.GetComponent<GroundCollider>();
                if (targetGroundCollider && targetGroundCollider.occupied == false && targetGroundCollider.tag != "MovingPlatform")
                {
                    return true;
                }
            }
            return false;
        }


        public void findLevel1Room()
        {
            //Debug.Log("level1");
            int max = level1Ground.Count;
            int rand = UnityEngine.Random.Range(0, max);
            GameObject temp = level1Ground[rand];
            float diff = Vector3.Distance(temp.transform.position, transform.position);

            while (diff > 25.0f || diff < 10.0f)
            {
                rand = UnityEngine.Random.Range(0, max);
                temp = level1Ground[rand];
                diff = Vector3.Distance(temp.transform.position, transform.position);
            }
            target = temp.transform;
            targetPosition = target.position;
            iterator++;
        }

        public void findLevel2Room()
        {
            //Debug.Log("level2");
            int count = 0;
            int max = level2Ground.Count;
            int rand = UnityEngine.Random.Range(0, max);
            GameObject temp = level2Ground[rand];
            float diff = Vector3.Distance(temp.transform.position, transform.position);

            while (diff > 25.0f || diff < 10.0f)
            {
                //모 서 리
                if (count > 40)
                {
                    Debug.Log("edge!");
                    iterator--;
                    findLevel1Room();
                    return;
                }
                rand = UnityEngine.Random.Range(0, max);
                temp = level2Ground[rand];
                diff = Vector3.Distance(temp.transform.position, transform.position);
                count++;
            }
            target = temp.transform;
            targetPosition = target.position;
            iterator++;
        }

        public void findLevel3Room()
        {
            int count = 0;
            int max = level3Ground.Count;
            int rand = UnityEngine.Random.Range(0, max);
            GameObject temp = level3Ground[rand];
            float diff = Vector3.Distance(temp.transform.position, transform.position);

            while (diff > 25.0f || diff < 10.0f)
            {
                //모 서 리
                if (count > 20)
                {
                    Debug.Log("edge!");
                    iterator--;
                    findLevel2Room();
                    return;
                }
                rand = UnityEngine.Random.Range(0, max);
                temp = level3Ground[rand];
                diff = Vector3.Distance(temp.transform.position, transform.position);
                count++;
            }
            target = temp.transform;
            targetPosition = target.position;
            iterator++;
        }

        public void findLevel4Room()
        {
            target = level4.transform;
            targetPosition = target.position;
            int count = 0;
            float diff = Vector3.Distance(targetPosition, transform.position);
            while (diff > 25.0f || diff < 10.0f)
            {
                if (count > 10)
                {
                    Debug.Log("edge!");
                    iterator--;
                    findLevel3Room();
                    return;
                }
                count++;
            }
            target = level4.transform;
            targetPosition = target.position;
        }

        public void SetRoute(int[] mRoute)
        {
            //Debug.Log(mRoute.Length);
            for (int i = 0; i < mRoute.Length; i++)
            {
                this.route[i] = mRoute[i];
            }
        }
        
        //기본 패러미터 설정
        public void SetParameter(float respawnCount)
        {
            maxMana = generation * 20.0f + 1000.0f;
			mana = 1000f;
        }

        //기본 패러미터 회복
        public void ParameterRecovery()
        {
            if (mana <= maxMana)
            {
                mana += generation * 0.5f;
            }
        }


        public void HeroCloneDead()
        {
            heroSpawnController.GetComponent<HeroSpawnController>().HeroDead(this.gameObject.name);
        }

        //protected ->public
        public override void LifeLost(int currentLives)
        {
            base.LifeLost(currentLives);
            //Debug.Log(this.name + " : " + this.lives + "Lives left");
        }


        //protected -> public
        public override void CharacterDead()
        {
            base.CharacterDead();
            HeroCloneDead();
        }



    }
}