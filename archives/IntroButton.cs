using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class IntroButton : MonoBehaviour
    {
        GameObject unit;



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(transform.GetChild(0).GetChild(0).childCount != 0)
            {
                unit = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

                if(unit.tag == "Player")
                {
                    if(unit.GetComponent<HeroController>().isActivate == false)
                    {
                        unit.GetComponent<HeroController>().isActivate = true;
                    }
                    else
                    {
                        unit.GetComponent<HeroController>().HeroCloneDead();
                    }
                }

            }
        }
    }
}