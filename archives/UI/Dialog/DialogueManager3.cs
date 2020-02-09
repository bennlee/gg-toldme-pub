using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager3 : MonoBehaviour
{
    //public Sprite background2_Image;
    //public Sprite background3_Image;
    //public Image background;

    public Image characterImage;
    public Text nameText;
    public Text dialogueText;

    GameObject dialogueFlow;
    Dialogue[] currentDialogue;

    int scene_iterator;

    public Animator animator;
    public GameObject heroSpawnController;
    public GameObject monsterController;
    public GameObject dialogPanel;
    public GameObject dialogPanelButton;
    public GameObject tutorialCurser;
    public Text tutorial;
    public GameObject tutorialBox;

    int dialogue_iterator;
    float temp = 0;

    SceneManager SceneManager;
    public GameObject soundManager;

    void Awake()
    {
        //Invoke("DisableSpawn", 0.9f);
        dialogPanelButton.SetActive(true);
        dialogueFlow = GameObject.Find("DialogueFlow");
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow3>().Scene1Dialogue;
        animator.SetBool("isOpen", true);

        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        if (soundManager != null)
        {
            soundManager.GetComponent<SoundManager>().PlayBGM(soundManager.GetComponent<SoundManager>().tutorialBgm);
            soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story3Laugh);
        }

        dialogue_iterator = 0;
        ScreenSetting();
    }

    void Start()
    {
        PlayerPrefs.SetInt("GameTutorial", 0);   
    }

    void DisableSpawn()
    {
        heroSpawnController.SetActive(false);
    }

    void ScreenSetting()
    {
        dialogPanelButton.GetComponent<Button>().interactable = false;

        characterImage.sprite = currentDialogue[dialogue_iterator].characterSpriteImage;
        nameText.text = currentDialogue[dialogue_iterator].name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue[dialogue_iterator].speech));

        dialogPanelButton.GetComponent<Button>().interactable = true;
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
            //dialogPanel.SetActive(false);
            //heroSpawnController.SetActive(true);
            NextScene();
        }
        else
        {
            dialogue_iterator++;
            if (dialogue_iterator == 2 && scene_iterator == 1)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().minionVoice7);
            }
            if (dialogue_iterator == 6 && scene_iterator == 1)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().minionVoice7);
            }
            ScreenSetting();
        }
    }

    void NextScene()
    {
        //Debug.Log("next scene");
        scene_iterator++;

        dialogPanelButton.GetComponent<Button>().interactable = false;

        switch (scene_iterator)
        {
            case 1:
                StopAllCoroutines();
                StartCoroutine("ScenarioEvent1");
                break;
            case 2:
                StopAllCoroutines();
                StartCoroutine("ScenarioEvent2");
                break;
            case 3:
                StopAllCoroutines();
                StartCoroutine("ScenarioEvent4");
                break;
        }
    }

    IEnumerator ScenarioEvent1()
    {
        animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isOpen", true);
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow3>().Scene2Dialogue;
        ScreenSetting();
    }

    IEnumerator ScenarioEvent2()
    {
        animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isOpen", true);
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow3>().Scene3Dialogue;
        ScreenSetting();
    }

    IEnumerator ScenarioEvent3()
    {
        animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isOpen", true);
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow3>().Scene3Dialogue;
    }

    IEnumerator ScenarioEvent4()
    {
        dialogPanel.SetActive(false);
        heroSpawnController.SetActive(true);
        tutorialCurser.SetActive(true);
        tutorialBox.SetActive(true);

        tutorial.text = "몬스터를 끌어서 맵에 소환하세요.";
        yield return new WaitForSeconds(3.5f);
        tutorialCurser.SetActive(false);

        tutorial.text = "스와이프로 화면을 확대하고\n이동할 수 있습니다.";
        yield return new WaitForSeconds(3.0f);

        tutorial.text = "용사들은 동서남북의\n네 방향에서 들어옵니다.";
        yield return new WaitForSeconds(3.0f);
        tutorial.text = "용사들이 마왕이 있는 방에\n들어가면 패배입니다.";
        yield return new WaitForSeconds(3.0f);
        tutorial.text = "낮에는 스킬을 이용하고,\n밤에는 몬스터를 소환하세요.";
        yield return new WaitForSeconds(3.0f);
        tutorial.text = "밤에 몬스터를 소환하면\n아침에 출근합니다.";
        yield return new WaitForSeconds(3.0f);
        tutorial.text = "최후의 용사가 수명이 다하거나\n 죽으면 게임 승리입니다.";
        yield return new WaitForSeconds(3.0f);
        tutorial.text = "몬스터를 소환할 수 있는 시간은\n게임 시작 5초 전 까지입니다.";
        yield return new WaitForSeconds(3.0f);
        tutorial.text = "몬스터를 적재적소에 소환해\n용사들을 막으세요!";
        yield return new WaitForSeconds(2.0f);
        tutorial.text = "준비되셨나요?\n자, 이제 시작합니다!!";
        yield return new WaitForSeconds(2.0f);


        SceneManager.LoadScene(4);
    }

    public void Skip()
    {
        SceneManager.LoadScene(4);
    }

}