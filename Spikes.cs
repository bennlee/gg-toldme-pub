using UnityEngine;
using System.Collections;

namespace TVNT {
	public class Spikes : LevelTiles {

		public float phaseTime = 2f;
		public float spikeUpY = 2f;
		public float spikeDownY = 0f;
		public float spikeMoveUpSpeed = 15f;
		public float spikeMoveDownSpeed = 10f;

		public bool spikeUp = false;

		private Transform spike = null;

		[HideInInspector]
		public AudioSource myAudio = null;

		protected override void Start () {
			base.Start ();
			AssignParts ();
			InvokeRepeating ("SetSpikeState", phaseTime, phaseTime);
		}

		private void SetSpikeState() {
			if (spikeUp) {
				Vector3 target = new Vector3 (0, spikeDownY, 0);
				StartCoroutine(ChangeSpikeState(target, spikeMoveDownSpeed));
				spikeUp = false;
				if (myAudio && AudioClipRepo.instance && AudioClipRepo.instance.spikeDown) {
					myAudio.clip = AudioClipRepo.instance.spikeDown;
					myAudio.Play ();
				}
			} else {
				Vector3 target = new Vector3 (0, spikeUpY, 0);
				StartCoroutine(ChangeSpikeState(target, spikeMoveUpSpeed));
				spikeUp = true;
				if (myAudio && AudioClipRepo.instance && AudioClipRepo.instance.spikeUp) {
					myAudio.clip = AudioClipRepo.instance.spikeUp;
					myAudio.Play ();
				}
			}
		}

		private IEnumerator ChangeSpikeState(Vector3 target, float spikeMoveSpeed) {
			float sqrRemainingDistance = (spike.localPosition - target).sqrMagnitude;
			while (sqrRemainingDistance > float.Epsilon) {
				spike.localPosition = Vector3.MoveTowards (spike.localPosition, target, spikeMoveSpeed * Time.deltaTime);
				sqrRemainingDistance = (spike.localPosition - target).sqrMagnitude;
				yield return null;
			}
		}
		
		public override void InspectorUpdate() {
			if (spike) {
				if (spikeUp) {
					spike.localPosition = new Vector3 (0, spikeUpY, 0);
				} else {
					spike.localPosition = new Vector3 (0, spikeDownY, 0);
				}
			}
			base.InspectorUpdate ();
		}

		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "Spikes") {
					spike = prefab.GetChild (i).transform;
				} else if (prefab.GetChild (i).name == "AudioSource") {
					myAudio = prefab.GetChild (i).GetComponent<AudioSource> ();
				}
			}
			InspectorUpdate ();
		}
	}
}
