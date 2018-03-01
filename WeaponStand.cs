using UnityEngine;
using System.Collections;

namespace TVNT {
	public class WeaponStand : MonoBehaviour {

		public enum Direction {
			FRONT, 
			RIGHT,
			BACK,
			LEFT
		};

		public float phaseTime = 2f;
		public Direction[] standCycle;
		public bool rotateClockwise = true;
		public bool rotateInSingleDirection = true;

		[HideInInspector]
		public Transform stand = null;
		[HideInInspector]
		public float rotationTime;
		[HideInInspector]
		public bool inPosition = true;
		[HideInInspector]
		public float currentTimeInPosition = 0f;

		private int prevStandCycleIndex = 0;
		private int standCycleIndex = 0;
		private float rotateByYAngle = 0;

		void Awake() {
			rotationTime = (0.3f * phaseTime) * 0.5f;
		}

		void Update () {
			if (inPosition) {
				currentTimeInPosition += Time.deltaTime;
			}
			if (currentTimeInPosition >= phaseTime) {
				if (standCycle.Length > 1) {
					RotateStand ();
					currentTimeInPosition = 0;
				} else if (inPosition) {
					inPosition = false;
				} else {
					inPosition = true;
					currentTimeInPosition = 0;
				}
			}
		}

		private int standStateChanges = 0;
		private void RotateStand() {
			prevStandCycleIndex = standCycleIndex;
			standStateChanges++;

			if (standStateChanges >= standCycle.Length) {
				standStateChanges = 1;
				if (rotateInSingleDirection == false) {
					if (rotateClockwise) {
						rotateClockwise = false;
					} else {
						rotateClockwise = true;
					}
				}
			}

			if (rotateClockwise) {
				standCycleIndex++;
				if (standCycleIndex >= standCycle.Length) {
					standCycleIndex = 0;
				}
			} else {
				standCycleIndex--;
				if (standCycleIndex < 0) {
					standCycleIndex = standCycle.Length - 1;
				}
			}

			SetRotateByAngle ();

			inPosition = false;
			StartCoroutine (ChangeStandState (rotateByYAngle, rotationTime * Mathf.Abs (rotateByYAngle / 90f)));
		}
		
		private void SetRotateByAngle() {
			int current = (int)standCycle [prevStandCycleIndex];
			int target = (int)standCycle [standCycleIndex];

			rotateByYAngle = 0;
			if (rotateClockwise) {
				if (target < current) {
					target += 4;
				}
				rotateByYAngle = Mathf.Abs(current - target)*90f;
			} else {
				if (target > current) {
					target = 4 - target;
					rotateByYAngle = Mathf.Abs ((int)current + (int)target) * -90f;
				} else {
					rotateByYAngle = Mathf.Abs ((int)current - (int)target) * -90f;
				}

			}
		}
		
		private IEnumerator ChangeStandState(float rotateByAngle, float inTime) {
			Quaternion fromAngle = stand.localRotation;
			Quaternion toAngle = Quaternion.Euler (stand.localEulerAngles + new Vector3 (0, rotateByAngle, 0));
			for (float t = 0f; t < 1; t += Time.deltaTime / inTime) {
				stand.localRotation = Quaternion.Euler (fromAngle.eulerAngles + new Vector3 (0, rotateByAngle * t, 0));
				yield return null;
			}
			stand.localRotation = toAngle;
			inPosition = true;
		}
		
		public void UpdateStandDirection() {
			if (stand) {
				if (standCycle.Length > 0) {
					float standYRotation = 0f;
					switch (standCycle [0]) {
					case Direction.FRONT:
						standYRotation = 0f;
						break;
					case Direction.BACK:
						standYRotation = 180f;
						break;
					case Direction.LEFT:
						standYRotation = -90f;
						break;
					case Direction.RIGHT:
						standYRotation = 90f;
						break;
					}
					if (Mathf.Approximately (standYRotation, stand.localRotation.eulerAngles.y) == false) {
						stand.localRotation = Quaternion.Euler (-90f, standYRotation, 0);
					}
				}
			}
		}

	}
}
