using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	public Image FadeImage;
	public float fadeSpeed = 1.5f;
	[HideInInspector]
	public bool sceneStarted = false;
	private string loadLevelName = "";

	void Start () {
		StartCoroutine (Fade(Color.clear));
	}
	
	private IEnumerator Fade(Color targetColor) {
		Color originalColor = FadeImage.color;
		for (float t = 0; t < 1; t += Time.deltaTime / fadeSpeed) {
			FadeImage.color = Color.Lerp (originalColor, targetColor, t);
			yield return null;
		}
		if (sceneStarted == false) {
			FadeImage.gameObject.SetActive (false);
			sceneStarted = true;
		} else {
			Application.LoadLevel (loadLevelName);
		}
	}

	public void EndScene(string sceneName) {
		FadeImage.gameObject.SetActive (true);
		loadLevelName = sceneName;
		StartCoroutine (Fade (Color.black));
	}
}
