using UnityEngine;
using System.Collections;

namespace TVNT {
	[RequireComponent(typeof(WeaponStand))]
	public class Shooter : LevelTiles {

		public Transform bullet;
		public float bulletDistance = 2f;
		public float bulletMinSpeed = 4f;
		//public float bulletMaxSpeed = 8f;
		public bool halfPhase = false;
		[HideInInspector]
		public bool fireBullet = true;

		private WeaponStand weaponStand;
		private Transform barrel;

		protected override void Start () {
			base.Start ();
			AssignParts ();
			if (halfPhase) {
				weaponStand.currentTimeInPosition = weaponStand.phaseTime * 0.5f;
				fireBullet = false;
			}
		}
		
		void Update () {
			if (weaponStand.inPosition) {
				if (weaponStand.currentTimeInPosition >= weaponStand.phaseTime * 0.5f && fireBullet) {
					Fire ();
				}
			} else {
				fireBullet = true;
			}
		}

		private void Fire() {
			Transform currentBullet = null;
			currentBullet = TVNTObjectPool.instance.SpawnObject (bullet);
			currentBullet.position = barrel.position;
			currentBullet.rotation = Quaternion.Euler (new Vector3 (0, barrel.eulerAngles.y + 90, 0));
			currentBullet.gameObject.SetActive (true);
			Bullet currentBulletScript = currentBullet.GetComponent<Bullet> ();
			currentBulletScript.maxDistanceInTiles = bulletDistance;
			//currentBulletScript.moveSpeed = Random.Range (bulletMinSpeed, bulletMaxSpeed);
			currentBulletScript.moveSpeed = bulletMinSpeed;
			currentBulletScript.transform.parent = transform;
			fireBullet = false;
		}

		public override void InspectorUpdate() {
			if (weaponStand) {
				weaponStand.UpdateStandDirection ();
			}
			base.InspectorUpdate ();
		}

		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			weaponStand = GetComponent<WeaponStand> ();
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "Shooter_Stand") {
					weaponStand.stand = prefab.GetChild (i).transform;
					break;
				}
			}
			for (int i = 0; i < weaponStand.stand.childCount; i++) {
				if (weaponStand.stand.GetChild (i).name == "Shooter_Barrel") {
					barrel = weaponStand.stand.GetChild (i).transform;
					break;
				}
			}
			InspectorUpdate ();
		}
	}
}
