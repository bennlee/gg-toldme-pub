using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen3Fade : MonoBehaviour {

	public void Fade()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;

        CanvasManager.instance.screen4Active();
    }

}
