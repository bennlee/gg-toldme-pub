using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen4Fade : MonoBehaviour {

    public void Fade()
    {
        StartCoroutine(DoFade());
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

        //씬 전환 코드 넣을 것.
    }
}
