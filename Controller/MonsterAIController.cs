using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Coordinate
{
    public int x;
    public int y;
}


namespace TVNT
{
    public class MonsterAIController : TVNTCharacterController
    {
        public enum TargetType
        {
            Type1, //Targets the player position
            Type2, //Target a few blocks ahead of the player position
            Type3 //If the player is further than a certain distance than targets the player else targets a random tile
        };
        //public GameObject monsterController;
        public int type = 1;
        public TargetType targetType = TargetType.Type1;
        public float movementDelay = 0.8f;
        private float currentMovementDelay;
        //[HideInInspector]
        public Transform target;
        //[HideInInspector]
        public SpawnPoint mySpawnPoint;
        private Vector3 targetPosition;

        bool isHero = false;
        public bool allowMove = false;
        bool isTurn = true;
        //bool isThreaten = false;

        //public Transform currentTarget;
        //public Vector3 currentTargetPosition;

        public int level;

        public bool isTargetHero = false;
        public GameObject targetedHero;

        //몬스터가 처음 스폰되는 위치 (hero가 추적범위에서 몇초이상 벗어난 경우 이 시작위치로 되돌아간다.)
        public Vector3 homePosition;
        public Transform homeTarget;

        //경계범위안에서 히어로가 빠져나갔는지를 판단하기위한 시간단위
        //히어로가 들어오면 이 alertTime을 감소시키고(혹은 minimap 버그와 동일한 오류가 발생할경우 일정 숫자(0)로 바꾸어주고),
        //히어로가 범위안에서 빠져나가면 천천히 증가되어 기존 설정단위 이상이된다면 진정(relax)상태(추적중단, 인식해제)
        //히어로가 '은신'스킬을 쓸경우 이 alertTime 감소를 시키지 않거나 적게 시키고, 발각되면 급격히 감소시키는 방법도 고려
        //히어로가 '섬광탄'등을 사용할 경우 이 alertTime을 급격하게 늘려 일정시간 인식하지 않게 하는방법도 고려(이경우에는 기준치 이상으로 올라갈 경우의 예외처리 해줘야함)
        public const float alertTime = 13.5f;
        public const float peacefulTime = 15.0f;
        public float threatenTime;

        //경계범위
        //public float alertDiff = .0f;

        //히어로와의 거리
        public float heroDiff = 0.0f;

        //몬스터마다 공격종류가 다를테니까 hero의 Sword 처럼 여러가지 저장해두고 쓴다.
        public string attackType;

        //경계상승용 히어로와의 거리
        public float betweenDiff = 0.0f;

        public float currentAttackDurationTime;

        public Transform now;

        public void SetLevel()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.up * 100, out hit)==true)
            {
                if (hit.transform.tag == "Map")
                {
                    level = hit.transform.gameObject.GetComponent<MinimapCheck>().level;
                }
            }
        }
        public void TargetHero(GameObject nowTargetHero, float heroDiff)
        {
            if (!isTargetHero && allowMove && (heroDiff<30.0f))
            {
                isTargetHero = true;
                targetedHero = nowTargetHero;
            }
        }
        
        //바깥에 if(istargetHero)
        public void ThreatHero()
        {
            target = targetedHero.transform;
            //Debug.Log("Target Hero!");
            heroDiff = Vector3.Distance(transform.position, targetedHero.transform.position);

            if (heroDiff < 5.0f && !isHero)
            {
                isHero = true;
                //Debug.Log("Hero fight!");
            }
            if (heroDiff >= 8.0f && isHero)
            {
                isHero = false;
            }
        }

        public IEnumerator Fight()
        {
            while (true)
            {
                //currentAttackDurationTime = gameObject.GetComponent<WeaponController>().currentDurationOfAttackType;
                currentAttackDurationTime = 1.0f;
                if (isHero)
                {
                    if (isTargetHero)
                    {
                        LookAtEnemy();
                        //now.GetComponent<WeaponController>().target = targetedHero;
                        if (targetedHero.GetComponent<TVNTCharacterController>().lives > 0)
                        {
//                            targetedHero.GetComponent<MonsterAIController>().LifeLost(targetedHero.GetComponent<TVNTCharacterController>().lives);
                            //gameObject.GetComponent<WeaponController>().SetAttackType();
                            yield return new WaitForSeconds(currentAttackDurationTime);
                        }
                        else
                        {
                            targetedHero.GetComponent<MonsterAIController>().CharacterDead();
                            target = homeTarget;
                            targetPosition = homePosition;

                            isTargetHero = false;
                            isHero = false;
                            //isTurn = true;
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        public void LookAtEnemy()
        {
            transform.LookAt(target);
        }

        public void Threaten(float monsterDiff)
        {
            //isThreaten = true;
            betweenDiff = monsterDiff;
        }
        public void IncreaseThreatenTime()
        {
            if (threatenTime > 0.0f)
            {
                //threatenTime -= (1.0f/((betweenDiff-3.8f)*(betweenDiff-3.8f))* Time.deltaTime);
                threatenTime -= Mathf.Pow((0.7f),(betweenDiff-12.0f))*Time.deltaTime;
            }
        }

        private void Awake()
        {
            //initiate default value of CharacterController
            base.movementStyle = MovementStyle.CONTINUOS_WALK;
            base.barrierOffset = new Vector3(0, 0.25f, 0);
            base.jumpAnimationName = "Slime_Walk"; ;
            base.groundOffset = new Vector3(0, 0.125f, 0);
            base.idleAnimationName = "Slime_Idle";
            base.walkAnimationName = "Slime_Walk";
            base.moveTime = 0.5f;
            base.lives = 20;

        }

        protected override void Start()
        {
            //monsterController = GameObject.FindGameObjectWithTag("MonsterController");
            homeTarget = this.transform;
            //homePosition = this.transform.position;
            betweenDiff = float.MaxValue;
            threatenTime = peacefulTime;
            target = homeTarget;
            targetPosition = homePosition;
            //isTargetHero = false;
            SetLevel();
            //now.GetComponent<WeaponController>();
            currentMovementDelay = movementDelay;

            StartCoroutine("Fight");
            base.Start();
        }



        void Update()
        {
            Vector3 start = transform.localPosition;
            Vector3 end = start + new Vector3(horizontal * PatternSettings.tiledSize, 0f, vertical * PatternSettings.tiledSize);
            IncreaseThreatenTime();

            if (isTargetHero)
            {
                ThreatHero();
            }
            if (threatenTime < alertTime && !allowMove)
            {
                allowMove = true;
            }
            if (threatenTime >= alertTime && allowMove)
            {
                allowMove = false;
                isTargetHero = false;
            }
            if (threatenTime < peacefulTime)
            {
                threatenTime += 0.4f * Time.deltaTime;
            }

            if (activate && Time.timeScale > 0 && !isHero && allowMove)
            {
                if (idle && skipTurn == false)
                {
                    if (onSlidingGround || currentMovementDelay >= movementDelay)
                    {
                        if (onSlidingGround == false)
                        {
                            GetTargetTile();
                            GetMovementDirection();
                        }
                        if (horizontal != 0)
                        {
                            vertical = 0;
                        }
                        if (horizontal == 0 && vertical == 0)
                        {
                            PlayIdleAnimation();
                            transform.position = transform.parent.position + new Vector3(0, PatternSettings.playerYOffset, 0);
                            currentMovementDelay = 0f;
                        }
                        else if (horizontal != 0 || vertical != 0)
                        {
                            AttemptMove();
                            currentMovementDelay = 0f;
                        }
                    }
                    else
                    {
                        currentMovementDelay += Time.deltaTime;
                        PlayIdleAnimation();
                    }
                }
                skipTurn = false;
            }

            else if (Time.timeScale > 0)
            {
                PlayIdleAnimation();
            }
        }

        private void GetTargetTile()
        {
            switch (targetType)
            {
                case TargetType.Type1:
                    GetType1TargetTile();
                    break;
                case TargetType.Type2:
                    GetType2TargetTile();
                    break;
                case TargetType.Type3:
                    GetType3TargetTile();
                    break;
            }
        }

        private void GetType1TargetTile()
        {
            targetPosition = target.position;
        }

        private void GetType2TargetTile()
        {
            targetPosition = target.position + target.forward * (4 * PatternSettings.tiledSize);
        }

        private void GetType3TargetTile()
        {
            Vector3 distanceToPlayer = target.position - transform.position;
            if (distanceToPlayer.x * distanceToPlayer.x <= (8 * PatternSettings.tiledSize) * (8 * PatternSettings.tiledSize) ||
                distanceToPlayer.z * distanceToPlayer.z <= (8 * PatternSettings.tiledSize) * (8 * PatternSettings.tiledSize))
            {
                Vector2 randomDisplacement = Random.insideUnitCircle * (8 * PatternSettings.tiledSize);
                targetPosition = target.position + new Vector3(randomDisplacement.x, 0, randomDisplacement.y);
            }
            else
            {
                GetType1TargetTile();
            }

        }

        private void GetMovementDirection()
        {
            bool foundTargetSpot = false;
            float distanceToTarget = float.MaxValue;
            Vector3 start = transform.parent.position + new Vector3(0, PatternSettings.playerYOffset, 0);
            Vector3 end = start;
            int tempHorizontal = 0;
            int tempVertical = 0;

            for (int x = -1; x <= 1; x += 1)
            {
                for (int z = -1; z <= 1; z += 1)
                {
                    if (x * x != z * z)
                    {
                        if ((horizontal == 0 || horizontal != -x) && (vertical == 0 || vertical != -z))
                        { //To prevent the AI from turning back on itself
                            end = start + new Vector3(x * PatternSettings.tiledSize, 0f, z * PatternSettings.tiledSize);
                            if (Physics.Linecast(start + barrierOffset, end + barrierOffset, barrierLayerMask) == false)
                            {
                                if (HasGround(end))
                                {
                                    float currentDistanceToTarget = (target.position - end).sqrMagnitude;
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

        //public void Dead()
        //{
        //    gameObject.SetActive(false);
        //}
        //void OnDestroy()
        //{
        //    if (mySpawnPoint)
        //    {
        //        mySpawnPoint.activeSpawnedEnemies.Remove(transform);
        //    }
        //}
        //protected ->public
        public override void LifeLost(int currentLives)
        {
            base.LifeLost(currentLives);
//            Debug.Log(this.name + " : " + this.lives + "Lives left");
        }


        //protected -> public
        public override void CharacterDead()
        {
            base.CharacterDead();
        }
    }
}
