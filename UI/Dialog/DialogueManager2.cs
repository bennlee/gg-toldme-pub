using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager2 : MonoBehaviour
{

    public Image characterImage;
    public Text nameText;
    public Text dialogueText;
    GameObject dialogueFlow;
    Dialogue[] currentDialogue;

    public GameObject dialoguePanel;
    public GameObject LevelLoader;

    int dialogue_iterator = 0;
    public GameObject skipButton;

    float temp = 0;

    public GameObject soundManager;
    SceneManager SceneManager;

    void Awake()
    {
        dialogueFlow = GameObject.Find("DialogueFlow");
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow2>().Scene1Dialogue;
    }

    void Start()
    {
        ScreenSetting();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        soundManager.GetComponent<SoundManager>().PlayBGM(soundManager.GetComponent<SoundManager>().worldmapBgm);
        soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().worldmapPaper);
    }

    void ScreenSetting()
    {
        characterImage.sprite = currentDialogue[dialogue_iterator].characterSpriteImage;
        nameText.text = currentDialogue[dialogue_iterator].name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue[dialogue_iterator].speech));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }

    void FixedUpdate()
    {
        //Debug.Log("conversation");
    }

    public void NextSpeech()
    {
        //Debug.Log("next speech");
        if (currentDialogue.Length == dialogue_iterator + 1)
        {
            dialogue_iterator = 0;
            LevelLoader.GetComponent<LevelLoader>().LoadLevel(5);
        }
        else
        {
            dialogue_iterator++;
            ScreenSetting();
        }
    }

    public void Skip()
    {
        LevelLoader.GetComponent<LevelLoader>().LoadLevel(5);
    }

}