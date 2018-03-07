using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TVNT
{
    public class MonsterController : MonoBehaviour
    {
        public List<GameObject> monsterList = new List<GameObject>();

        public int deployCount = 1;
        public int recycleCount = 0;
        public bool isRecyclable = false;

        public void CreateMonster(GameObject monster, Vector3 position, Quaternion rotation)
        {
            GameObject clone;
            clone = Instantiate(monster, position, rotation);
            clone.name = clone.name + deployCount;
            deployCount++;
            monsterList.Add(clone);
            clone.SetActive(true);
        }

        public void ActivateMonster(GameObject monster, int recycleCount, Vector3 position, Quaternion rotation)
        {
            monsterList[recycleCount].transform.position = position;
            monsterList[recycleCount].transform.rotation = rotation;
            monsterList[recycleCount].name = monster.name + deployCount;
            deployCount++;
            monsterList[recycleCount].SetActive(true);
        }

        //public void DeactivateMonster(GameObject monster)
        //{
        //    monster.SetActive(false);
        //}

        public void DeployMonster(GameObject monster, Vector3 position, Quaternion rotation)
        {
            int i = 0;
            while (i < monsterList.Count)
            {
                if (!monsterList[i].activeSelf && monsterList[i].GetComponent<MonsterAIController>().type ==
                    monster.GetComponent<MonsterAIController>().type)
                {
                    recycleCount = i;
                    isRecyclable = true;
                    break;
                }
                i++;
            }

            if (isRecyclable)
            {
                ActivateMonster(monster, recycleCount, position, rotation);
            }
            else
            {
                CreateMonster(monster, position, rotation);
            }
            isRecyclable = false;
        }
    }
}