using UnityEngine;
using System.Collections;

namespace TVNT {
	public class Pot : LevelTiles, ISmashable {

		public Transform destructionPE;

		protected override void Start() {
			base.Start ();
		}

		public void Smash(float delay) {
			Invoke ("DestroyMe", delay);
		}

		private void DestroyMe() {
			Transform _destructionPE = (Transform)Instantiate (destructionPE);
			_destructionPE.position = new Vector3 (transform.position.x, 4f, transform.position.z);
			float timeDelay = _destructionPE.GetComponent<ParticleSystem> ().startLifetime;
			_destructionPE.GetComponent<TimedDestroy> ().delay = timeDelay;
			TVNTObjectPool.instance.ReleaseObject(myPrefab);
			myPrefab = null;
			//Camera.main.SendMessage ("SmashedPot", transform.position+new Vector3(0,4,0),SendMessageOptions.DontRequireReceiver);
			TVNTManager.instance.SmashPot(transform.position+new Vector3(0,4,0));
		}

		public override void Initialize() {
			base.Initialize ();
		}
	}
}
