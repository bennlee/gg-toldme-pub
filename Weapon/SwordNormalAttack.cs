using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
	public class SwordNormalAttack : WeaponController
	{
		void Update(){
			//transform.Translate (new Vector3(10,0,0) * Time.deltaTime);
		}


		void OnTriggerEnter (Collider other)
		{
			if (other.tag == "Monster") {
                if (other.GetComponent<TVNTCharacterController>().lives > 0)
                {
                    Debug.Log("sword normal attack");
                    //particle effect

                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                }
                else
                {
                    other.gameObject.SetActive(false);
                }
			}
		}
	}
}