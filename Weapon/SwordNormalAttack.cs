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
				Debug.Log ("sword attack!!!!");
				other.GetComponent<TVNTCharacterController> ().lives -= damage;
				GameObject.Destroy (gameObject);
			}
		}
	}
}