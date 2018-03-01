using UnityEngine;
using System.Collections;

namespace TVNT {
	public class MovingPlatform : LevelTiles {

		public enum Direction {
			Horizontal,
			Vertical
		};

		public Direction movingDirection = Direction.Horizontal;
		public float moveSpeed = 4f;
		public int positiveMoves = 0;
		public int negativeMoves = 0;
		public bool movePositive = true;
		public float platformChangeDelay = 0.3f;
		private Transform platform = null;
		private Transform rail = null;
		private Vector3 target;

		[HideInInspector]
		public AudioSource myAudio = null;

		protected override void Start () {
			base.Start ();
			AssignParts ();
			float targetX = platform.localPosition.x;
			if (movePositive) {
				targetX += positiveMoves * PatternSettings.tiledSize;
				movePositive = false;
			} else {
				targetX -= negativeMoves * PatternSettings.tiledSize;
				movePositive = true;
			}
			target = new Vector3 (targetX, platform.localPosition.y, platform.localPosition.z);
			StartCoroutine (MoveToTarget (target));
		}

		private IEnumerator MoveToTarget(Vector3 target) {
			float sqrRemainingDistance = (platform.localPosition - target).sqrMagnitude;
			while (sqrRemainingDistance > float.Epsilon) {
				platform.localPosition = Vector3.MoveTowards (platform.localPosition, target, moveSpeed * Time.deltaTime);
				sqrRemainingDistance = (platform.localPosition - target).sqrMagnitude;
				yield return null;
			}
			platform.localPosition = target;
			if (myAudio) {
				myAudio.Play ();
			}
			Invoke ("ChangePlatformDirection", platformChangeDelay);
		}

		private void ChangePlatformDirection() {
			float targetX = platform.localPosition.x;
			if (movePositive) {
				targetX += (positiveMoves + negativeMoves) * PatternSettings.tiledSize;
				movePositive = false;
			} else {
				targetX -= (positiveMoves + negativeMoves) * PatternSettings.tiledSize;
				movePositive = true;
			}
			target = new Vector3 (targetX, platform.localPosition.y, platform.localPosition.z);
			StartCoroutine (MoveToTarget (target));
		}

		private void SetPlatformDirection() {
			if (movingDirection == Direction.Horizontal) {
				transform.localRotation = Quaternion.Euler (0, 0, 0);
			} else {
				transform.localRotation = Quaternion.Euler (0, 90, 0);
			}
		}

		private void SetRailLength() {
			if (rail) {
				rail.localScale = new Vector3 (((positiveMoves + negativeMoves) + 1) * 100, rail.localScale.y, rail.localScale.z);
				if (negativeMoves > positiveMoves) {
					rail.localPosition = new Vector3 (-(negativeMoves - positiveMoves) * PatternSettings.tiledSize * 0.5f, 0, 0);
				} else {
					rail.localPosition = new Vector3 ((positiveMoves - negativeMoves) * PatternSettings.tiledSize * 0.5f, 0, 0);
				}
			}
		}

		public override void InspectorUpdate() {
			SetPlatformDirection ();
			SetRailLength ();
			base.InspectorUpdate ();
		}

		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "Base") {
					platform = prefab.GetChild (i).transform;
					platform.localPosition = Vector3.zero;
				} else if (prefab.GetChild (i).name == "Rail") {
					rail = prefab.GetChild (i).transform;
				} else if (prefab.GetChild (i).name == "AudioSource") {
					myAudio = prefab.GetChild (i).GetComponent<AudioSource> ();
				}
			}
			InspectorUpdate ();
		}

		private void OnDrawGizmosSelected() {
			if (rail) {
				Gizmos.color = Color.green;
				if (movingDirection == Direction.Horizontal) {
					Gizmos.DrawWireCube (rail.position, new Vector3 ((rail.localScale.x / 100f) * PatternSettings.tiledSize, PatternSettings.tiledSize, PatternSettings.tiledSize));
				} else {
					Gizmos.DrawWireCube (rail.position, new Vector3 (PatternSettings.tiledSize, PatternSettings.tiledSize, (rail.localScale.x / 100f) * PatternSettings.tiledSize));
				}
			}
		}

	}
}
