using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {

	public float delay;

	// Use this for initialization
	void Start () {
		Invoke ("Destruct", delay);
	}
	
	private void Destruct() {
		GameObject.Destroy (gameObject);
	}
}
