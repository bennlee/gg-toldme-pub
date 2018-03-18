using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class SkeletonKnightNormalAttack : WeaponController
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<TVNTCharacterController>().lives > 0)
                {
                    //Debug.Log("Normal");
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    //GameObject.Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<HeroController>().CharacterDead();
                    //other.gameObject.SetActive(false);
                }
            }
            //GameObject.Destroy(gameObject);
        }
    }
}
