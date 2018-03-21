using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDie : MonoBehaviour {

    // Use this for initialization
    void Start() {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        for (float i = 1; i >= 0;)
        {
            i -= 0.65f * Time.deltaTime;
            Color color = new Vector4(1, 1, 1, i);
            transform.GetComponent<Renderer>().material.color = color;
            yield return new WaitForEndOfFrame();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
