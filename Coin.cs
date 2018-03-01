using UnityEngine;
using System.Collections;

namespace TVNT {
	public class Coin:MonoBehaviour {
		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player") {
				//Camera.main.SendMessage ("PickedUpCoin", transform.position, SendMessageOptions.DontRequireReceiver);
				TVNTManager.instance.PickupCoin(transform.position);
				Transform myPrefab = transform.parent.parent.GetComponent<LevelTiles> ().myPrefab;
				transform.parent.parent.GetComponent<LevelTiles> ().myPrefab = null;
				TVNTObjectPool.instance.ReleaseObject (myPrefab);
			}
		}
	}
}
