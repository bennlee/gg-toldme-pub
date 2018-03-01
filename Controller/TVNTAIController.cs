using UnityEngine;
using System.Collections;

namespace TVNT {
	public class TVNTAIController : TVNTCharacterController {

		public enum TargetType
		{
			Type1, //Targets the player position
			Type2, //Target a few blocks ahead of the player position
			Type3 //If the player is further than a certain distance than targets the player else targets a random tile
		};
		public TargetType targetType = TargetType.Type1;
		public float movementDelay = 0.8f;
		private float currentMovementDelay;
		[HideInInspector]
		public Transform target;
		[HideInInspector]
		public SpawnPoint mySpawnPoint;
		private Vector3 targetPosition;

		protected override void Start () {
			currentMovementDelay = movementDelay;
			target = GameObject.FindGameObjectWithTag ("Player").transform;

			base.Start ();
		}
		
		void Update () {
			if (activate && Time.timeScale > 0 && target) {
				if (idle && skipTurn == false) {
					if (onSlidingGround || currentMovementDelay >= movementDelay) {
						if (onSlidingGround == false) {
							GetTargetTile ();
							GetMovementDirection ();
						}
						if (horizontal != 0) {
							vertical = 0;
						}
						if (horizontal == 0 && vertical == 0) {
							PlayIdleAnimation ();
							transform.position = transform.parent.position + new Vector3 (0, PatternSettings.playerYOffset, 0);
							currentMovementDelay = 0f;
						} else if (horizontal != 0 || vertical != 0) {
							AttemptMove ();
							currentMovementDelay = 0f;
						}
					} else {
						currentMovementDelay += Time.deltaTime;
						PlayIdleAnimation ();
					}
				}
				skipTurn = false;
			} else if (Time.timeScale > 0) {
				PlayIdleAnimation ();
			}
		}

		private void GetTargetTile() {
			switch (targetType) {
			case TargetType.Type1:
				GetType1TargetTile ();
				break;
			case TargetType.Type2:
				GetType2TargetTile ();
				break;
			case TargetType.Type3:
				GetType3TargetTile ();
				break;
			}
		}

		private void GetType1TargetTile() {
			targetPosition = target.position;
		}

		private void GetType2TargetTile() {
			targetPosition = target.position + target.forward * (4 * PatternSettings.tiledSize);
		}

		private void GetType3TargetTile() {
			Vector3 distanceToPlayer = target.position - transform.position;
			if (distanceToPlayer.x * distanceToPlayer.x <= (8 * PatternSettings.tiledSize) * (8 * PatternSettings.tiledSize) ||
				distanceToPlayer.z * distanceToPlayer.z <= (8 * PatternSettings.tiledSize) * (8 * PatternSettings.tiledSize)) {
				Vector2 randomDisplacement = Random.insideUnitCircle * (8 * PatternSettings.tiledSize);
				targetPosition = target.position + new Vector3 (randomDisplacement.x, 0, randomDisplacement.y);
			} else {
				GetType1TargetTile ();
			}

		}

		private void GetMovementDirection() {
			bool foundTargetSpot = false;
			float distanceToTarget = float.MaxValue;
			Vector3 start = transform.parent.position + new Vector3 (0, PatternSettings.playerYOffset, 0);
			Vector3 end = start;
			int tempHorizontal = 0;
			int tempVertical = 0;

			for (int x = -1; x <= 1; x += 1) {
				for (int z = -1; z <= 1; z += 1) {
					if (x * x != z * z) {
						if ((horizontal == 0 || horizontal != -x) && (vertical == 0 || vertical != -z)) { //To prevent the AI from turning back on itself
							end = start + new Vector3 (x * PatternSettings.tiledSize, 0f, z * PatternSettings.tiledSize);
							if (Physics.Linecast (start + barrierOffset, end + barrierOffset, barrierLayerMask) == false) {
								if (HasGround (end)) {
									float currentDistanceToTarget = (target.position - end).sqrMagnitude;
									if (currentDistanceToTarget < distanceToTarget) {
										foundTargetSpot = true;
										distanceToTarget = currentDistanceToTarget;
										tempHorizontal = x;
										tempVertical = z;
										if (flipHorizontalInput) {
											tempHorizontal *= -1;
										}
										if (flipVerticalInput) {
											tempVertical *= -1;
										}
									}
								}
							}
						}
					}
				}
			}

			if (foundTargetSpot == false) {
				horizontal = 0;
				vertical = 0;
				return;
			}

			horizontal = tempHorizontal;
			vertical = tempVertical;
		}

		private bool HasGround(Vector3 end) {
			boxCollider.enabled = false;
			RaycastHit hitInfo;
			if (Physics.Linecast (end + groundOffset, end + new Vector3(0,-1f,0) + groundOffset, out hitInfo, groundLayerMask)) {
				GroundCollider targetGroundCollider = hitInfo.transform.GetComponent<GroundCollider> ();
				if (targetGroundCollider && targetGroundCollider.occupied == false && targetGroundCollider.tag != "MovingPlatform") {
					return true;
				}
			}
			return false;
		}

		void OnDestroy() {
			if (mySpawnPoint) {
				mySpawnPoint.activeSpawnedEnemies.Remove (transform);
			}
		}

		protected override void LifeLost (int currentLives) {
			base.LifeLost (currentLives);
		}

		protected override void CharacterDead () {
			base.CharacterDead ();
		}
	}
}
