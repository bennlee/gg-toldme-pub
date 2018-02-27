using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
	public class ArrowSkillAttack : WeaponController
	{

		void Update(){
			transform.Translate (new Vector3(10,0,0) * Time.deltaTime);
		}

		GameObject passMonster;

		void OnTriggerEnter (Collider other)
		{
			if (other.tag == "Monster" && passMonster != other.gameObject) {
                if( other.GetComponent<TVNTCharacterController>().lives >0)
                {
                    passMonster = other.gameObject;
                    Debug.Log("bow skill attack!!!!");
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                }
                else
                {
                    //other.GetComponent<MonsterAIController>().CharacterDead();
                    other.gameObject.SetActive(false);
                }
			}
		}
	}
}

