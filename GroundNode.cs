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
        bool isMouseOver = false;

        void Start()
        {
            MonsterIcon = GameObject.FindGameObjectWithTag("MonsterIcon");
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
                    MonsterIcon.GetComponent<MonsterSelect>().groundNode = gameObject;
                    hoverPrefab = MonsterIcon.GetComponent<MonsterSelect>().hoverPrefab;
                    hoverPrefab.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
                    hoverPrefab.SetActive(true);
                }
            }
        }
    }
}