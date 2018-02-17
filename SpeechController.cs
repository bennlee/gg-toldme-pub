using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVNT
{
    public class SpeechController : MonoBehaviour
    {
        public enum SpeechType
        {
            CHAT,
            ENCOUNTERENEMY,
            DECIDEROUTE,
            REACHANCHORPOINT
        };

        public List<string> chat = new List<string>();
        public List<string> encounterEnemy = new List<string>();
        public List<string> decideRoute = new List<string>();
        public List<string> reachAnchorPoint = new List<string>();

        public GameObject textMesh;

        public string currentText;

        //default position & rotation
        public Vector3 popUpPosition = new Vector3(0, 6, 0);
        public Quaternion popUpRotation = new Quaternion(90, 0, 0);


        public void Speech(SpeechType type)
        {
            switch (type)
            {
                case SpeechType.CHAT:
                    Chat();
                    break;
                case SpeechType.ENCOUNTERENEMY:
                    EncounterEnemy();
                    break;
                case SpeechType.DECIDEROUTE:
                    DecideRoute();
                    break;
                case SpeechType.REACHANCHORPOINT:
                    ReachAnchorPoint();
                    break;
            }
        }

        public void Chat()
        {
            GameObject currentTextMesh;
            currentText = chat[Random.Range(0, chat.Count)];
            currentTextMesh = Instantiate()
         }
        

        public void EncounterEnemy()
        {

        }

        public void DecideRoute()
        {

        }

        public void ReachAnchorPoint()
        {

        }





        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}