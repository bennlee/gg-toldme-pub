using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public Sprite background2_Image;
    public Sprite background3_Image;

    public Image background;
    public Image characterImage;
    public Text nameText;
    public Text dialogueText;
    public GameObject dialoguePanel;
    Button nextButton;

    GameObject dialogueFlow;
    Dialogue[] currentDialogue;

    int dialogue_iterator = 0;
    int scene_iterator;
    private int nowStory = 1;

    public Animator animator;

    float temp = 0;

    public GameObject soundManager;
    SceneManager SceneManager;

    void Awake()
    {
        animator = dialoguePanel.GetComponent<Animator>();
        animator.SetBool("isOpen", true);
        dialogueFlow = GameObject.Find("DialogueFlow");
        nextButton = dialoguePanel.GetComponent<Button>();
        currentDialogue = dialogueFlow.GetComponent<DialogueFlow>().Scene1Dialogue;
    }

    void Start()
    {
        ScreenSetting();

        nowStory = 1;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        soundManager.GetComponent<SoundManager>().PlayBGM(soundManager.GetComponent<SoundManager>().story1Bgm);
        soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story1DoorOpen);
    }

    void ScreenSetting()
    {
        nextButton.interactable = false;
        characterImage.sprite = currentDialogue[dialogue_iterator].characterSpriteImage;
        nameText.text = currentDialogue[dialogue_iterator].name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue[dialogue_iterator].speech));
        nextButton.interactable = true;
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

    public void NextSpeech()
    {
        if (currentDialogue.Length == dialogue_iterator + 1)
        {
            nowStory++;
            dialogue_iterator = 0;
            NextScene();

            if (nowStory == 2)
            {
                soundManager.GetComponent<SoundManager>().PlayBGM(soundManager.GetComponent<SoundManager>().story2Bgm);
            }
            else if (nowStory == 3)
            {
                soundManager.GetComponent<SoundManager>().PlayBGM(soundManager.GetComponent<SoundManager>().story3Bgm);
            }
        }
        else
        {
            dialogue_iterator++;

            if (dialogue_iterator == 2 && nowStory == 1)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story1DoorClose);
            }
            if (dialogue_iterator == 3 && nowStory == 1)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story1DoorOpen);
            }
            if (dialogue_iterator == 6 && nowStory == 1)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story1DoorClose);
            }
            if (dialogue_iterator == 3 && nowStory == 2)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story2Crowd);
            }
            if (dialogue_iterator == 0 && nowStory == 3)
            {
                //soundManager.GetComponent<SoundManager>().efxSource.Stop();
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story1DoorOpen);
            }
            if (dialogue_iterator == 3 && nowStory == 3)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story3Laugh);
            }
            if (dialogue_iterator == 8 && nowStory == 3)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().story3Laugh);
            }
            if (dialogue_iterator == 10 && nowStory == 3)
            {
                soundManager.GetComponent<SoundManager>().PlaySingle(soundManager.GetComponent<SoundManager>().minionVoice7);
            }
            ScreenSetting();
        }
    }

    void NextScene()
    {
        nextButton.interactable = false;
        animator.SetBool("isOpen", false);
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
                //튜토리얼을 수행했다는 것을 알려주는 키값
                PlayerPrefs.SetInt("StartTutorial", 0);
                PlayerPrefs.Save();
                SceneManager.LoadScene(2);
                break;
        }
    }

    IEnumerator ScenarioEvent1()
    {
        //번쩍번쩍 효과
        background.GetComponent<Image>().color = new Color(0, 0, 0);
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

        animator.SetBool("isOpen", true);
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

        animator.SetBool("isOpen", true);
        ScreenSetting();
    }

    public void SkipButtonOnClick()
    {
        PlayerPrefs.SetInt("StartTutorial", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }

}