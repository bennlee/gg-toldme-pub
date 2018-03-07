using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVNT
{
    public class SwordSkillAttack : WeaponController
    {


        void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.name);
            if (other.tag == "Monster")
            {
                //Debug.Log("sword attack!!");

                if (other.GetComponent<TVNTCharacterController>().lives > 0)
                {
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
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