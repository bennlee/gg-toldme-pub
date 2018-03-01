using UnityEngine;
using System.Collections;

namespace TVNT {
	public class ButtonTile : LevelTiles, ISwitchable {

		public Material onMaterial;
		public Material offMaterial;
		public Transform[] switchConnectedTiles;
		private Transform button = null;
		private bool switchedOn = false;
		private GroundCollider myGroundCollider;

		protected override void Start () {
			base.Start ();

			AssignParts ();
		}

		void OnDisable() {
			if (button) {
				button.GetComponent<MeshRenderer> ().material = offMaterial;
				switchedOn = false;
			}
		}

		public void SwitchOn() {
			if (switchedOn == false && button) {
				button.GetComponent<MeshRenderer> ().material = onMaterial;
				switchedOn = true;
				if (switchConnectedTiles.Length > 0) {
					for (int i = 0; i < switchConnectedTiles.Length; i++) {
						switchConnectedTiles [i].GetComponent<ISwitchable>().SwitchOn ();
					}
				}
			}
		}

		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "Button") {
					button = prefab.GetChild (i).transform;
					break;
				}
			}
			myGroundCollider = button.GetComponent<GroundCollider> ();
		}
	}
}
