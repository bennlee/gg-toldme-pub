using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSkill : MonoBehaviour {

    Vector3 vector;
    Animator dragonAnimator;
    public GameObject fireEffect;
    //public GameObject fireEffect2;
    public GameObject sphere;

	// Use this for initialization
	void Awake () {
        dragonAnimator = GetComponent<Animator>();
        dragonAnimator.SetTrigger("Fly Fire Breath Attack");
        GameObject.Destroy(gameObject, 1.89f);
    }

    void Start()
    {
        InvokeRepeating("ThrowObject", 0, 0.1f);
        //Debug.Log(transform.position);
        if (transform.position.z < 0)
        {
            vector = new Vector3(64, 4, 69);
        }
        else
        {
            vector = new Vector3(68, 4, -68);
        }
    }
    
    void Update ()
    {
        transform.Translate(vector * Time.deltaTime, Space.World);

        //fire dragon throw effect
        Instantiate(fireEffect, transform.position, transform.rotation);
    }

    void ThrowObject()
    {
        //fire ground object
        Instantiate(sphere, transform.position, transform.rotation);
    }
}
