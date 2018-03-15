using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVNT
{
    public class SwordSkillAttack : WeaponController
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster")
            {
                if (other.GetComponent<TVNTCharacterController>().lives > 0)
                {
                    //Debug.Log("Skill");
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                    GameObject.Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<MonsterAIController>().CharacterDead();
                    //other.gameObject.SetActive(false);
                }
                GameObject.Destroy(gameObject);
            }
        }
    }

}