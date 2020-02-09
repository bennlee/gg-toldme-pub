using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
	public class ArrowSkillAttack : WeaponController
	{
        void Start()
        {
            damage = GetComponentInParent<WeaponController>().damage;
        }
        void Update(){
			transform.Translate (new Vector3(10,0,0) * Time.deltaTime);
		}

		GameObject passMonster;

		void OnTriggerEnter (Collider other)
		{
			if (other.tag == "Monster" && passMonster != other.gameObject)
            {
                Debug.Log("arrow skill1 attack : " + damage);
                if ( other.GetComponent<TVNTCharacterController>().lives >= 5)
                {
                    passMonster = other.gameObject;
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                }
                else if (other.GetComponent<TVNTCharacterController>().lives < 5 && other.GetComponent<TVNTCharacterController>().lives > 2)
                {
                    passMonster = other.gameObject;
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                    other.GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERDEAD);
                }
                else
                {
                    other.GetComponent<MonsterAIController>().CharacterDead();
                    //other.gameObject.SetActive(false);
                }
			}
		}
	}
}

