using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class GroundFire : MonoBehaviour
    {

        public GameObject groundFireEffect;
        GameObject effect;
        float endTime;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                Debug.Log("Player.");
                //플레이어에게 데미지
                collision.transform.GetComponent<TVNTCharacterController>().lives -= 40;
                GameObject.Destroy(gameObject);

            }
            //서로 다른 공에 맞았거나, 타일맵에 맞았을 경우 없어지게
            if(collision.transform.tag == "FireSphere" || collision.transform.tag == "TileMap")
            {
                Debug.Log(collision.transform.name);
                GameObject.Destroy(gameObject);
            }
            else
            {
                effect = Instantiate(groundFireEffect, transform.position, transform.rotation);
                endTime = UnityEngine.Random.Range(4.0f, 6.5f);
                GameObject.Destroy(effect, endTime);
                GameObject.Destroy(gameObject, 1.0f);
            }
        }
    }
}

