using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
	public class ArrowNormalAttack : WeaponController
	{
		void Start ()
		{
			gameObject.transform.Translate (Vector3.forward * 100);
		}

		private void OnTriggerEnter (Collider other)
		{
			if (other.tag == "Monster") {

				if (other.GetComponent<TVNTCharacterController> ().lives > 0) {
					Debug.Log ("bow attack!!!!");
					//attack
					other.GetComponent<TVNTCharacterController> ().lives -= damage;
					other.GetComponent<MonsterAIController> ().threatenTime = 0;
					GameObject.Destroy (gameObject);
				} else {
					other.GetComponent<MonsterAIController> ().CharacterDead ();
				}
			}
		}
	}
}