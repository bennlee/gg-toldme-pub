using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT{
public class AttackController : MonoBehaviour {

	GameObject hero;
	// Use this for initialization
	void Start () {
hero = GameObject.Find("Knight");
	}

	// Update is called once per frame
	void Update () {

	}


	void Shoot(){

		hero.transform.GetComponent<HeroController>().Shoot();
		
	}

	void EndOfFight()
	{
		hero.transform.GetComponent<HeroController>().EndOfFight();
	}
}
}
