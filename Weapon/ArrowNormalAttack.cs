using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//namespace TVNT
//{
//    public class ArrowNormalAttack : WeaponController
//    {
//        float angle;
//        Rigidbody rid;
//        void Start()
//        {
//            //rid = GetComponent<Rigidbody>();
//            //angle = Mathf.Atan2(rid.velocity.y, rid.velocity.x);
//            //gameObject.transform.localEulerAngles = new Vector3(0, 0, (angle * 180) / Mathf.PI);
//            //transform.LookAt(;
//            GetComponent<Rigidbody>().AddForce(transform.forward * 150f);

//        }
//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.tag == "Monster")
//            {
//                //attack
//                other.GetComponent<TVNTCharacterController>().lives -= damage;
//            }
//        }
//    }
//}
namespace TVNT
{
    public class ArrowNormalAttack : WeaponController
    {
        //        float angle;
        //        Rigidbody rid;
        bool isPass = true;
        void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isPass)
            {
                if (other.tag == "Monster")
                {
                    Debug.Log("bow attack!!!!");
                    //attack
                    other.GetComponent<TVNTCharacterController>().lives -= damage;
                    GameObject.Destroy(gameObject);
                    isPass = false;
                }
            }
        }
    }
}