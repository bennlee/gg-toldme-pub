using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public ScreenFader screenFader;
	public string levelToLoad = ""; //the name of the scence to load once the splash screen time is done
	public float splashScreenTime = 2f; //in seconds

	// Use this for initialization
	void Start () {
		Invoke ("SplashScreenDone", splashScreenTime);
	}
	
	private void SplashScreenDone() {
		screenFader.EndScene (levelToLoad);
	}
}
