using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieDummy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DieMonster());
	}
	
    IEnumerator DieMonster()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
