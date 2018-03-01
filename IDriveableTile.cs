using UnityEngine;
using System.Collections;

namespace TVNT {
	public interface IDriveableTile {
		int GetHorizontal();
		int GetVertical();
		float GetMoveSpeed();
	}
}
