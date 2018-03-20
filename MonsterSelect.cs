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

        void Start()
        {
            monsterController = GameObject.FindGameObjectWithTag("MonsterController");
            hoverPrefab = Instantiate(prefab);
            hoverPrefab.tag = "MonsterDeactivate";
            GameObject.Destroy(hoverPrefab.GetComponent<MonsterAIController>());
            GameObject.Destroy(hoverPrefab.GetComponent<WeaponController>());
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
            // Debug.Log("Beginning drag");
            isMonsterSelected = true;
            //hoverPrefab = Instantiate(prefab);
            //hoverPrefab.tag = "MonsterDeactivate";
            //hoverPrefab.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (hoverPrefab.activeSelf)
            {
                monsterController.GetComponent<MonsterController>().DeployMonster(prefab, hoverPrefab.transform.position, Quaternion.identity);
            }
            //Destroy(hoverPrefab);
            //groundNode.GetComponent<GroundNode>().hoverPrefab.SetActive(false);
            //if (groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().parentGroundCollider)
            //{
            //    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().parentGroundCollider.occupied = false;
            //    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().parentGroundCollider = null;
            //}
            //if (groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().targetGroundCollider)
            //{
            //    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().targetGroundCollider.occupied = false;
            //    groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<TVNTCharacterController>().targetGroundCollider = null;
            //}
            //groundNode.GetComponent<GroundNode>().hoverPrefab.transform.parent = null;
            groundNode.GetComponent<GroundNode>().hoverPrefab.SetActive(false);
            //groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<MonsterAIController>().StopCoroutine(groundNode.GetComponent<GroundNode>().hoverPrefab.GetComponent<MonsterAIController>().Fight());
            isMonsterSelected = false;
        }
    }
}