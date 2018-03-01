using UnityEngine;
using System.Collections;

namespace TVNT {
	public class SlidingGroundStraight : SlidingGround {

		public enum Direction
		{
			TOP_BOTTOM,
			BOTTOM_TOP,
			LEFT_RIGHT,
			RIGHT_LEFT
		}

		public Direction currentDirection = Direction.BOTTOM_TOP;

		public override void InspectorUpdate() {
			switch (currentDirection) {
			case Direction.TOP_BOTTOM:
				transform.localRotation = Quaternion.Euler (0, 180, 0);
				horizontal = 0;
				vertical = -1;
				break;
			case Direction.BOTTOM_TOP:
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				horizontal = 0;
				vertical = 1;
				break;
			case Direction.LEFT_RIGHT:
				transform.localRotation = Quaternion.Euler (0, 90, 0);
				horizontal = 1;
				vertical = 0;
				break;
			case Direction.RIGHT_LEFT:
				transform.localRotation = Quaternion.Euler (0, -90, 0);
				horizontal = -1;
				vertical = 0;
				break;
			}
			base.InspectorUpdate ();
		}
	}
}
