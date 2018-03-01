using UnityEngine;
using System.Collections;

namespace TVNT {
	public class Door : LevelTiles, ISwitchable {
		public float doorUpY = 2f;
		public float doorDownY = 0f;
		private Transform door = null;
		private bool doorOpened = false;

		protected override void Start () {
			base.Start ();
		}

		void OnDisable() {
			if (door) {
				doorOpened = false;
				door.localPosition = new Vector3 (0, doorUpY, 0);
			}
		}

		public void SwitchOn() {
			if (doorOpened == false) {
				Vector3 end = new Vector3 (0, doorDownY, 0);
				doorOpened = true;
				StartCoroutine (ChangeDoorState (end));
			}
		}

		private IEnumerator ChangeDoorState(Vector3 end) {
			float sqrRemainingDistance = (end - door.localPosition).sqrMagnitude;
			while (sqrRemainingDistance > 0.01f) {
				door.localPosition = Vector3.Lerp (door.localPosition, end, Time.deltaTime * 10f);
				sqrRemainingDistance = (end - door.localPosition).sqrMagnitude;
				yield return null;
			}
			door.localPosition = end;
		}
		
		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "Door") {
					door = prefab.GetChild (i).transform;
					break;
				}
			}
		}
	}
}