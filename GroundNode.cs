using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TVNT
{
    public class GroundNode : MonoBehaviour
    {

        public GameObject prefab;
        GameObject hoverPrefab;

        GameObject MonsterIcon;
        //GameObject MonsterIcon_2;
        //GameObject MonsterIcon_3;
        //GameObject MonsterIcon_4;
        bool isMouseOver = false;

        // Use this for initialization
        void Start()
        {
            MonsterIcon = GameObject.FindGameObjectWithTag("MonsterIcon");
            //MonsterIcon_2 = GameObject.FindGameObjectWithTag("MonsterIcon_2");
            //MonsterIcon_3 = GameObject.FindGameObjectWithTag("MonsterIcon_3");
            //MonsterIcon_4 = GameObject.FindGameObjectWithTag("MonsterIcon_4");

            //prefab = GameObject.FindGameObjectWithTag("Slime");
            //prefab.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //if (!isMouseOver)
            //{
            //    OnMouseNotOver();
            //}
            //isMouseOver = false;
        }

        //public Vector3 GetDeployPosition()
        //{
        //    return;
        //}

        void SelectMonster(GameObject selectedMonster)
        {
            MonsterIcon = selectedMonster;
        }
        void OnMouseEnter()
        {
            if (MonsterIcon.GetComponent<MonsterSelect>().isMonsterSelected)
            {
                Debug.Log(gameObject.transform.parent.parent.name + "MouseGO");
                hoverPrefab = MonsterIcon.GetComponent<MonsterSelect>().hoverPrefab;
                hoverPrefab.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
                hoverPrefab.SetActive(true);
            }
        }

        //void OnMouseOver()
        //{
        //    isMouseOver = true;
        //}
        ////void OnMouseExit()
        ////{
        ////    if (MonsterIcon.GetComponent<MonsterSelect>().isMonsterSelected)
        ////    {
        ////        Debug.Log(gameObject.transform.parent.parent.name + "MouseExit");
        ////        if (hoverPrefab.activeSelf)
        ////        {
        ////            hoverPrefab.SetActive(false);
        ////        }
        ////    }
        ////}
        //void OnMouseNotOver()
        //{
        //    if (MonsterIcon.GetComponent<MonsterSelect>().isMonsterSelected)
        //    {
        //        if (hoverPrefab.activeSelf)
        //        {
        //            hoverPrefab.SetActive(false);
        //        }
        //    }
        //}
    }
}