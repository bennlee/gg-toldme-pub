using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
	public class ArrowSkillAttack : WeaponController
	{

		void Update(){
			transform.Translate (new Vector3(10,0,0) * Time.deltaTime);
		}

		void OnTriggerEnter (Collider other)
		{
			Debug.Log (other.name);
			if (other.tag == "Smashable") {
				Debug.Log ("bow attack!!!!");
				other.GetComponent<TVNTCharacterController> ().lives -= damage;
			}
		}
	}
}

