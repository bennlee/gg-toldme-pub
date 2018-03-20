using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVNT
{
    public class SwordSkillAttack : WeaponController
    {
        void Start()
        {
            damage = GetComponentInParent<WeaponController>().damage;
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster")
            {
                if (other.GetComponent<TVNTCharacterController>().lives >= 5)
                {
                    Debug.Log("sword skill attack : " + damage + "\n" + other.name + " lives : " + other.GetComponent<TVNTCharacterController>().lives);
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                }
                if (other.GetComponent<TVNTCharacterController>().lives < 5 && other.GetComponent<TVNTCharacterController>().lives > 2)
                {
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                    other.GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERDEAD);
                }
                else
                {
                    other.GetComponent<MonsterAIController>().CharacterDead();
                }
                GameObject.Destroy(gameObject);
            }
        }
    }

}