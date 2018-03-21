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
    int scene_iterator;

    public Animator animator;

    float temp = 0;

    SceneManager SceneManager;

    void Awake()
    {
        dialogueFlow = GameObject.Find("DialogueFlow");
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene1Dialogue;
    }

    void Start()
    {
        ScreenSetting();
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
        if (currentDialogue.Length == dialogue_iterator+1)
        {
            dialogue_iterator = 0;
            NextScene();
        }
        else
        {
            dialogue_iterator++;
            ScreenSetting();
        }
    }

    void NextScene()
    {
        //Debug.Log("next scene");
        scene_iterator++;

        switch (scene_iterator)
        {
            case 1:
                StartCoroutine("ScenarioEvent1");
                break;
            case 2:
                StartCoroutine("ScenarioEvent2");
                break;
            case 3:
                //튜토리얼 봤는지 안봤는지 확인.
                int temp = PlayerPrefs.GetInt("StartCount");
                temp++;
                PlayerPrefs.SetInt("StartCount", temp);

                //worldMap으로 이동
                SceneManager.LoadScene(2);
                break;
        }
    }

    IEnumerator ScenarioEvent1()
    {
        //번쩍번쩍 효과
        background.GetComponent<Image>().color = new Color(0,0,0);
        yield return new WaitForSeconds(.2f);
        background.GetComponent<Image>().color = new Color(255, 255, 255);
        yield return new WaitForSeconds(.2f);
        background.GetComponent<Image>().color = new Color(0, 0, 0);
        yield return new WaitForSeconds(.2f);
        background.GetComponent<Image>().color = new Color(255, 255, 255);
        yield return new WaitForSeconds(.2f);
        temp = 255;

        //어두워졌다가
        while (temp > 0)
        {
            background.GetComponent<Image>().color = new Color(temp / 255f, temp / 255f, temp / 255f);
            temp -= 1;
            yield return 1.55f;
        }

        //배경 교체 후 밝아진다
        background.sprite = background2_Image;
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene2Dialogue;
        while (temp < 255f)
        {
            background.GetComponent<Image>().color = new Color(temp / 255f, temp / 255f, temp / 255f);
            temp += 1;
            yield return 1.5f;
        }
        
        ScreenSetting();
    }

    IEnumerator ScenarioEvent2()
    {
        //어두워졌다가
        while (temp > 0)
        {
            background.GetComponent<Image>().color = new Color(temp / 255f, temp / 255f, temp / 255f);
            temp -= 1;
            yield return 1.05f;
        }

        //배경 교체 후 밝아진다
        background.sprite = background3_Image;
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene3Dialogue;
        while (temp < 255f)
        {
            background.GetComponent<Image>().color = new Color(temp / 255f, temp / 255f, temp / 255f);
            temp += 1;
            yield return 1.0f;
        }

        ScreenSetting();
    }

}
