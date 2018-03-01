using UnityEngine;
using System.Collections;

namespace TVNT {
	public class SlidingGround : LevelTiles, IDriveableTile {

		public static float SLIDING_TILE_MOVETIME = 0.35f;

		[HideInInspector]
		public int horizontal = 0;
		[HideInInspector]
		public int vertical = 0;

		protected override void Start () {
			base.Start ();
			InspectorUpdate ();
		}

		public int GetHorizontal() {
			return horizontal;
		}

		public int GetVertical() {
			return vertical;
		}

		public float GetMoveSpeed() {
			return SLIDING_TILE_MOVETIME;
		}

		public override void InspectorUpdate() {
			base.InspectorUpdate ();
		}

		public override void Initialize() {
			base.Initialize ();
		}
	}
}
