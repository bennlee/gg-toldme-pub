using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class MagicSkillAttack : WeaponController
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster")
            {
                if (other.GetComponent<TVNTCharacterController>().lives > 0)
                {
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