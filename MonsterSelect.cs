using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace TVNT
{
    public class MonsterSelect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GameObject monsterController;
        int mapLayer;
        public GameObject prefab;
        public GameObject hoverPrefab;
        public bool isMonsterSelected;
        public GameObject groundNode;
        GameObject SpawnController;

        void Start()
        {
            SpawnController = GameObject.FindGameObjectWithTag("HeroSpawnController");
            monsterController = GameObject.FindGameObjectWithTag("MonsterController");
            hoverPrefab = Instantiate(prefab);
            hoverPrefab.tag = "MonsterDeactive";
            hoverPrefab.SetActive(false);
            mapLayer = LayerMask.GetMask("Map");
        }

        void AdjustPrefabAlpha()
        {
            MeshRenderer[] meshRenderers = hoverPrefab.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                Material mat = meshRenderers[i].material;
                meshRenderers[i].material.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);
            }
        }

        //public static void ChangeLayersRecursively(this Transform trans, string name)
        //{
        //    trans.gameObject.layer = LayerMask.NameToLayer(name);
        //    foreach(Transform child in trans)
        //    {
        //        ChangeLayersRecursively(child, name);
        //    }
        //}

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (SpawnController.GetComponent<HeroSpawnController>().currentSouls >= prefab.GetComponent<MonsterAIController>().requireSoul)
            {
                isMonsterSelected = true;
            }
            // Debug.Log("Beginning drag");
            //hoverPrefab = Instantiate(prefab);
            //hoverPrefab.tag = "MonsterDeactivate";
            //hoverPrefab.SetActive(false);


        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isMonsterSelected) {
                if (hoverPrefab.activeSelf)
                {
                    monsterController.GetComponent<MonsterController>().DeployMonster(prefab, hoverPrefab.transform.position, Quaternion.identity);
                    SpawnController.GetComponent<HeroSpawnController>().currentSouls -= prefab.GetComponent<MonsterAIController>().requireSoul;
                    Debug.Log("soul decrease.");
                }
                //Destroy(hoverPrefab);
                //groundNode.GetComponent<GroundNode>().hoverPrefab.SetActive(false);
                if (groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().parentGroundCollider)
                {
                    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().parentGroundCollider.occupied = false;
                    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().parentGroundCollider = null;
                }
                if (groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().targetGroundCollider)
                {
                    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().targetGroundCollider.occupied = false;
                    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().targetGroundCollider = null;
                }
                groundNode.GetComponent<GroundNode>().hoverPrefab.transform.parent = null;
                groundNode.GetComponent<GroundNode>().hoverPrefab.SetActive(false);
                groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<MonsterAIController>().StopCoroutine(groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<MonsterAIController>().Fight());
                isMonsterSelected = false;
            }
            isMonsterSelected = false;
        }     
    }
}