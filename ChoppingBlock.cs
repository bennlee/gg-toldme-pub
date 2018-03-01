using UnityEngine;
using System.Collections;

namespace TVNT {
	[RequireComponent(typeof(WeaponStand))]
	public class ChoppingBlock : LevelTiles {

		public bool axeDown = false;

		private WeaponStand weaponStand;
		private Transform axeHandle = null;
		private bool changeAxeState = true;
		private float timeInPosition = 0;
		[HideInInspector]
		public AudioSource myAudio = null;

		protected override void Start () {
			base.Start ();
			AssignParts ();
			if (axeDown) {
				weaponStand.currentTimeInPosition = weaponStand.phaseTime * 0.5f;
				timeInPosition = weaponStand.currentTimeInPosition;
			}
		}

		void Update () {
			if (weaponStand.inPosition) {
				timeInPosition += Time.deltaTime;
				if (timeInPosition >= weaponStand.phaseTime * 0.33f && axeDown == false && changeAxeState) {
					axeDown = true;
					StartCoroutine (ChangeAxeState (new Vector3 (0, 90, 0), weaponStand.rotationTime * 0.5f));
					if (myAudio) {
						myAudio.Play ();
					}
				} else if (timeInPosition >= weaponStand.phaseTime - (weaponStand.phaseTime * 0.33f) && axeDown && changeAxeState) {
					axeDown = false;
					StartCoroutine (ChangeAxeState (new Vector3 (0, -90, 0), weaponStand.rotationTime * 0.5f));
					changeAxeState = false;
				}
			} else if (timeInPosition > 0) {
				timeInPosition = 0;
				changeAxeState = true;
			}
		}

		private IEnumerator ChangeAxeState(Vector3 rotateByAngle, float inTime) {
			Quaternion fromAngle = axeHandle.localRotation;
			Quaternion toAngle = Quaternion.Euler (axeHandle.localEulerAngles + rotateByAngle);
			for (float t = 0f; t < 1; t += Time.deltaTime / inTime) {
				axeHandle.localRotation = Quaternion.Lerp (fromAngle, toAngle, t);
				yield return null;
			}
			axeHandle.localRotation = toAngle;
		}

		private void UpdateAxeState() {
			if (axeHandle) {
				if (axeDown) {
					axeHandle.localRotation = Quaternion.Euler (0, 90, 0);
				} else {
					axeHandle.localRotation = Quaternion.Euler (0, 0, 0);
				}
			}
		}

		public override void InspectorUpdate() {
			UpdateAxeState ();
			if (weaponStand) {
				weaponStand.UpdateStandDirection ();
			}
		}

		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			weaponStand = GetComponent<WeaponStand> ();
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "AxeStand") {
					weaponStand.stand = prefab.GetChild (i).transform;
				} else if (prefab.GetChild (i).name == "AudioSource") {
					myAudio = prefab.GetChild (i).GetComponent<AudioSource> ();
				}
			}
			for (int i = 0; i < weaponStand.stand.childCount; i++) {
				if (weaponStand.stand.GetChild (i).name == "AxeHandle") {
					axeHandle = weaponStand.stand.GetChild (i).transform;
					break;
				}
			}
			InspectorUpdate ();
		}
	}
}