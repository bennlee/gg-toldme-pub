using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public Sprite background2_Image;
    public Sprite background3_Image;

    public Image background;
    public Image characterImage;
    public Text nameText;
    public Text dialogueText;
    
    GameObject dialogueFlow;
    Dialogue[] currentDialogue;

    int dialogue_iterator = 0;
    int scene_iterator = 0;

    public Animator animator;

    SceneManager SceneManager;

    void Awake()
    {
        dialogueFlow = GameObject.Find("DialogueFlow");
    }

    void Start()
    {
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene1Dialogue;
    }

    void FixedUpdate()
    {
        //Debug.Log("conversation");
        characterImage.sprite = currentDialogue[dialogue_iterator].characterSpriteImage;
        nameText.text = currentDialogue[dialogue_iterator].name;
        dialogueText.text = currentDialogue[dialogue_iterator].speech;
    }
    
    public void NextSpeech()
    {
        Debug.Log("next speech");
        if (currentDialogue.Length == dialogue_iterator+1)
        {
            dialogue_iterator = 0;
            NextScene();
        }
        else
        {
            dialogue_iterator++;
        }
    }

    void NextScene()
    {
        Debug.Log("next scene");
        scene_iterator++;

        switch (scene_iterator)
        {
            case 1:
                background.sprite = background2_Image;
                currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene2Dialogue;
                break;
            case 2:
                background.sprite = background3_Image;
                currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene3Dialogue;
                break;
            case 3:
                //worldMap으로 이동
                SceneManager.LoadScene(2);
                break;
        }

    }

}
