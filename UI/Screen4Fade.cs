using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen4Fade : MonoBehaviour {

    int startCount;

    public void Fade()
    {
        StartCoroutine(DoFade());
    }

    void Awake()
    {
        startCount = PlayerPrefs.GetInt("StartCount");
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

        //씬 전환 코드 넣을 것.
        startCount++;
        PlayerPrefs.SetInt("StartCount", startCount);
        SceneManager.LoadScene(1);

        yield return null;

    }
}
