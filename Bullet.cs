using UnityEngine;
using System.Collections;

namespace TVNT {
	public class Bullet : MonoBehaviour {

		public float moveSpeed = 4f;
		public float maxDistanceInTiles = 2f;
		private float currentMoveDistance = 0;

		void OnEnable() {
			currentMoveDistance = 0;
		}

		void Update () {
			if (currentMoveDistance < maxDistanceInTiles*PatternSettings.tiledSize) {
				currentMoveDistance += Time.deltaTime * moveSpeed;
				transform.localPosition += transform.forward * Time.deltaTime * moveSpeed;
			} else {
				transform.parent = null;
				currentMoveDistance = 0;
				TVNTObjectPool.instance.ReleaseObject (transform);
			}
		}

		void OnTriggerEnter(Collider other) {
			if (other.tag != "Shooter") {
				transform.parent = null;
				currentMoveDistance = 0;
				TVNTObjectPool.instance.ReleaseObject (transform);
			}
		}
	}
}
