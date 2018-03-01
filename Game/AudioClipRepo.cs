using UnityEngine;
using System.Collections;

public class AudioClipRepo : MonoBehaviour {

	public static AudioClipRepo instance = null;

	public AudioClip spikeUp;
	public AudioClip spikeDown;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
}
