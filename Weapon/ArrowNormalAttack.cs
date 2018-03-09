using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class ArrowNormalAttack : WeaponController
    {
        Transform thisTarget;
        public void Awake()
        {
            //target = base.target.transform;
            //Debug.Log(thisTarget.name);
            transform.LookAt(thisTarget);
        }
        void Update()
        {
<<<<<<< HEAD
            transform.LookAt(thisTarget);
            transform.Translate(new Vector3(5, 5, 5) * Time.deltaTime);
=======
            transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime);
>>>>>>> bcc55a4e0f2efcabdefcdf12f69f772788a8e197
            //빗나갔을 경우 2초 후 삭제
            GameObject.Destroy(gameObject, 2.0f);
        }

        public void SetTarget(GameObject target)
        {
<<<<<<< HEAD
            thisTarget = target.transform;
=======
            if (other.tag == "Monster")
            {
                //Debug.Log("arrow attack!!");
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
                GameObject.Destroy(gameObject);
            }
>>>>>>> bcc55a4e0f2efcabdefcdf12f69f772788a8e197
        }
        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "Monster")
        //    {
        //        Debug.Log("arrow attack!!");
        //        if (other.GetComponent<TVNTCharacterController>().lives > 0)
        //        {
        //            other.GetComponent<TVNTCharacterController>().lives -= damage;
        //            other.GetComponent<MonsterAIController>().threatenTime = 0;
        //        }
        //        else
        //        {
        //            //other.GetComponent<MonsterAIController>().CharacterDead();
        //            other.gameObject.SetActive(false);
        //        }
        //        GameObject.Destroy(gameObject);
        //    }
        //}
    }
}
