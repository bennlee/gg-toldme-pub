using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMoving : MonoBehaviour {

    GameObject hero = null;
    float y_rotation;
    
	public void Look(GameObject c_hero)
    {
        hero = c_hero;
    }

    private void Update()
    {
        if(hero != null)
        {
            transform.LookAt(hero.transform);
        }
    }
}
