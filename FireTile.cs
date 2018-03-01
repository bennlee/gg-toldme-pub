using UnityEngine;
using System.Collections;

namespace TVNT {
	public class FireTile : LevelTiles {

		public float phaseTime = 2f;
		public bool fireOn = false;

		private Transform fire = null;

		protected override void Start () {
			base.Start ();
			AssignParts ();
			InvokeRepeating ("SetFireState", phaseTime, phaseTime);
		}

		private void SetFireState() {
			if (fireOn) {
				fire.gameObject.SetActive (false);
				fireOn = false;
			} else {
				fire.gameObject.SetActive (true);
				fireOn = true;
			}
		}

		public override void InspectorUpdate() {
			if (fire) {
				if (fireOn) {
					fire.gameObject.SetActive (true);
				} else {
					fire.gameObject.SetActive (false);
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
				if (prefab.GetChild (i).name == "Fire") {
					fire = prefab.GetChild (i).transform;
					break;
				}
			}
			InspectorUpdate ();
		}
	}
}