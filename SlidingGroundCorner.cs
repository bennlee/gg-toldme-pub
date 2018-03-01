using UnityEngine;
using System.Collections;

namespace TVNT {
	public class SlidingGroundCorner : SlidingGround {

		public enum Direction
		{
			TOP_LEFT,
			TOP_RIGHT,
			BOTTOM_LEFT,
			BOTTOM_RIGHT,
			LEFT_TOP,
			LEFT_BOTTOM,
			RIGHT_TOP,
			RIGHT_BOTTOM
		}

		public Direction currentDirection = Direction.TOP_LEFT;

		public override void InspectorUpdate() {
			switch (currentDirection) {
			case Direction.TOP_LEFT:
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				transform.localScale = Vector3.one;
				horizontal = -1;
				vertical = 0;
				break;
			case Direction.TOP_RIGHT:
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				transform.localScale = new Vector3(-1,1,1);
				horizontal = 1;
				vertical = 0;
				break;
			case Direction.BOTTOM_LEFT:
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				transform.localScale = new Vector3(1,1,-1);
				horizontal = -1;
				vertical = 0;
				break;
			case Direction.BOTTOM_RIGHT:
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				transform.localScale = new Vector3(-1,1,-1);
				horizontal = 1;
				vertical = 0;
				break;
			case Direction.LEFT_TOP:
				transform.localRotation = Quaternion.Euler (0, -90, 0);
				transform.localScale = new Vector3(-1,1,1);
				horizontal = 0;
				vertical = 1;
				break;
			case Direction.LEFT_BOTTOM:
				transform.localRotation = Quaternion.Euler (0, 90, 0);
				transform.localScale = new Vector3(-1,1,-1);
				horizontal = 0;
				vertical = -1;
				break;
			case Direction.RIGHT_TOP:
				transform.localRotation = Quaternion.Euler (0, 90, 0);
				transform.localScale = new Vector3(1,1,1);
				horizontal = 0;
				vertical = 1;
				break;
			case Direction.RIGHT_BOTTOM:
				transform.localRotation = Quaternion.Euler (0, -90, 0);
				transform.localScale = new Vector3(1,1,-1);
				horizontal = 0;
				vertical = -1;
				break;
			}
			base.InspectorUpdate ();
		}
			
	}
}

