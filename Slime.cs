using UnityEngine;
using System.Collections;

namespace TVNT {
	public class Slime : MonoBehaviour {

		private TVNTAIController characterController;
		public float attackTime = 1f;
		public int attackTileRange = 2;
		public int bulletRange = 3;
		public float bulletSpeed = 10f;
		public Transform barrel;
		public Transform bullet;

		private bool playerInRange = false;
		private float attackRange;
		private float timeToFire;
		private bool characterJumping = false;

		void Start () {
			characterController = GetComponent<TVNTAIController> ();
			attackRange = attackTileRange * PatternSettings.tiledSize;
			timeToFire = attackTime;
			characterJumping = (characterController.movementStyle == TVNTCharacterController.MovementStyle.CONTINUOS_JUMP || characterController.movementStyle == TVNTCharacterController.MovementStyle.DISCRETE_JUMP) ? true : false;
		}

		private float distanceToPlayer = float.MaxValue;

		void Update () {
			timeToFire -= Time.deltaTime;
			if (characterController.target && (characterController.idle||characterJumping==false)) {
				Vector3 diff = transform.position - characterController.target.position;
				distanceToPlayer = diff.magnitude;
				if (Mathf.Approximately (Vector3.Dot (transform.forward, diff * (1 / distanceToPlayer)), -1)) {
					if (distanceToPlayer <= attackRange) {
						playerInRange = true;
					} else {
						playerInRange = false;
					}
				} else {
					playerInRange = false;
				}
				Fire ();
			}
		}

		private void Fire() {
			if (playerInRange && timeToFire <= 0) {
				//check if there is a barrier in the way
				characterController.boxCollider.enabled = false;
				if (Physics.Linecast (transform.position + characterController.barrierOffset, (transform.position + (transform.forward*distanceToPlayer)) + characterController.barrierOffset, characterController.barrierLayerMask) == false) {
					Transform currentBullet = TVNTObjectPool.instance.SpawnObject (bullet);
					currentBullet.position = barrel.position;
					currentBullet.gameObject.SetActive (true);
					Bullet currentBulletScript = currentBullet.GetComponent<Bullet> ();
					currentBulletScript.maxDistanceInTiles = bulletRange;
					currentBulletScript.moveSpeed = bulletSpeed;
					currentBullet.localRotation = Quaternion.LookRotation (barrel.forward, barrel.up);
					timeToFire = attackTime;
				}
				characterController.boxCollider.enabled = true;
			}
		}
	}
}
