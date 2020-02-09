using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class GolemShockWaveAttack : WeaponController
    {
        void Start()
        {
            damage = (int)(GetComponentInParent<WeaponController>().damage * 1.2f);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("golem shock wave attack : " + damage);
                if (other.GetComponent<TVNTCharacterController>().lives >= 5)
                {
                    Debug.Log("golem shock attack.");
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    GameObject.Destroy(gameObject);
                }
                else if (other.GetComponent<TVNTCharacterController>().lives < 5 && other.GetComponent<TVNTCharacterController>().lives > 2)
                {
                    Debug.Log("golem shock attack.");
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    other.GetComponent<HeroController>().SetSituation(HeroController.Situation.HERODEAD);
                    GameObject.Destroy(gameObject);
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
