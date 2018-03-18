using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class GolemSpinAttack : WeaponController
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<TVNTCharacterController>().lives > 0)
                {
                    Debug.Log("golem shock attack.");
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    GameObject.Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<HeroController>().CharacterDead();
                    //other.gameObject.SetActive(false);
                }
            }
            GameObject.Destroy(gameObject);
        }
    }
}
