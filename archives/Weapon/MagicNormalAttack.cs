using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class MagicNormalAttack : WeaponController
    {
        void Start()
        {
            damage = GetComponentInParent<WeaponController>().damage;
        }
        void Update()
        {
            transform.Translate(new Vector3(10, 2, 0) * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster")
            {
                Debug.Log("magic normal attack : " + damage);
                //Debug.Log("magic attack!!");
                if (other.GetComponent<TVNTCharacterController>().lives >= 5)
                {
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                }
                else if (other.GetComponent<TVNTCharacterController>().lives < 5 && other.GetComponent<TVNTCharacterController>().lives > 2)
                {
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<MonsterAIController>().threatenTime = 0;
                    other.GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERDEAD);
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