using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TVNT
{
    public class GroundNode : MonoBehaviour
    {

        public GameObject prefab;
        public GameObject hoverPrefab;
        GameObject MonsterIcon;
        GameObject MonsterIcon2;
        GameObject MonsterIcon3;
        GameObject nowMonsterIcon;
        bool isMouseOver = false;

        void Start()
        {
            MonsterIcon = GameObject.FindGameObjectWithTag("MonsterIcon");
            MonsterIcon2 = GameObject.FindGameObjectWithTag("MonsterIcon2");
            MonsterIcon3 = GameObject.FindGameObjectWithTag("MonsterIcon3");
        }
        void SelectMonster(GameObject selectedMonster)
        {
            MonsterIcon = selectedMonster;
        }
        void OnMouseEnter()
        {
            if (MonsterIcon.GetComponent<MonsterSelect>().isMonsterSelected)
            {
                if (gameObject.transform.childCount == 0)
                {
                    nowMonsterIcon = MonsterIcon;
                    nowMonsterIcon.GetComponent<MonsterSelect>().groundNode = gameObject;
                    hoverPrefab = nowMonsterIcon.GetComponent<MonsterSelect>().hoverPrefab;
                    hoverPrefab.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
                    hoverPrefab.SetActive(true);
                }
            }
            else if (MonsterIcon2.GetComponent<MonsterSelect>().isMonsterSelected)
            {
                if (gameObject.transform.childCount == 0)
                {
                    nowMonsterIcon = MonsterIcon2;
                    nowMonsterIcon.GetComponent<MonsterSelect>().groundNode = gameObject;
                    hoverPrefab = nowMonsterIcon.GetComponent<MonsterSelect>().hoverPrefab;
                    hoverPrefab.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
                    hoverPrefab.SetActive(true);
                }
            }
            else if( MonsterIcon3.GetComponent<MonsterSelect>().isMonsterSelected)
            {
                if (gameObject.transform.childCount == 0)
                {
                    nowMonsterIcon = MonsterIcon3;
                    nowMonsterIcon.GetComponent<MonsterSelect>().groundNode = gameObject;
                    hoverPrefab = nowMonsterIcon.GetComponent<MonsterSelect>().hoverPrefab;
                    hoverPrefab.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
                    hoverPrefab.SetActive(true);
                }
            }
        }

    }
}