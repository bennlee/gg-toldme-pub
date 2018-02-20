using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TVNT
{
    public class ArrowNormalAttack : WeaponController
    {
        void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster")
            {
                //attack
                other.GetComponent<TVNTCharacterController>().lives -= damage;
            }
        }
    }
}