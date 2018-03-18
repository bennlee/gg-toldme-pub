using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen1Fade : MonoBehaviour {

    int startCount;

    void Awake()
    {
        startCount = PlayerPrefs.GetInt("StartCount");
    }

    public void Fade(){
        if(startCount >= 1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(DoFade());
        }
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;

        CanvasManager.instance.screen2Active();
    }
}