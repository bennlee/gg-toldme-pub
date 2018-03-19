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
        public GameObject heroSpawnController;

        private void Start()
        {
            heroSpawnController = GameObject.FindGameObjectWithTag("HeroSpawnController");
        }
        public IEnumerator TeleportMonster(GameObject clone)
        {
            if (heroSpawnController.GetComponent<HeroSpawnController>().breakTime > 4.0f)
            {
                yield return new WaitForSeconds(3.0f);
                clone.GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERCHAT);
                clone.transform.Find("Speech").gameObject.SetActive(false);
                clone.SetActive(false);
            }
            else
            {
                clone.GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERCHAT);
                clone.tag = "Monster";
            }
        }
        public void CreateMonster(GameObject monster, Vector3 position, Quaternion rotation)
        {
            GameObject clone;
            clone = Instantiate(monster, position, rotation);
            clone.name = clone.name + deployCount;
            deployCount++;
            monsterList.Add(clone);
            clone.SetActive(true);
            clone.tag = "MonsterDeactive";
            Debug.Log("Deactive tag");

            //Debug.Log(monsterList[recycleCount].transform.name);
            clone.transform.Find("Speech").gameObject.SetActive(true);
            clone.GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERDEPLOY);
            
        }

        public void ActivateMonster(GameObject monster, int recycleCount, Vector3 position, Quaternion rotation)
        {
            monsterList[recycleCount].transform.position = position;
            monsterList[recycleCount].transform.rotation = rotation;
            monsterList[recycleCount].name = monster.name + deployCount;
            deployCount++;
            monsterList[recycleCount].SetActive(true);
            monsterList[recycleCount].tag = "MonsterDeactive";
            monsterList[recycleCount].transform.Find("Speech").gameObject.SetActive(true);
           
            monsterList[recycleCount].GetComponent<MonsterAIController>().SetSituation(MonsterAIController.Situation.MONSTERDEPLOY);
            
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
                if (!monsterList[i].activeSelf
                    && monsterList[i].GetComponent<MonsterAIController>().type == monster.GetComponent<MonsterAIController>().type
                    && monsterList[i].tag == "Monster")
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