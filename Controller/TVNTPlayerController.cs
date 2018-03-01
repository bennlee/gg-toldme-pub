using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace TVNT {
	public class TVNTPlayerController : TVNTCharacterController {
		//Variables to handle touch input
		private Vector3 verticalVector;
		private Vector3 horizontalVector;
		private Vector2 touchOrigin = -Vector2.one;
		public bool tapToMove = true;
		public float dragThreshold = 2.5f; // compare drag distance to this percent of screen height before registering a drag if taptomove is enabled
		private bool touchConsumed = false;
		private float currentTouchDragTime = 0;
		public float touchDragTime = 0.25f; // the time to prolong the input from a drag to help with imprecise taps (i.e:tapping when the player has not landed)
		private float dragDistance;
		private List<Vector2> touchPositions = new List<Vector2>();
		private bool setIdlePosition = true;

		protected override void Start () {
			//Variables used for touch input
			verticalVector = Camera.main.WorldToScreenPoint (Vector3.zero) - Camera.main.WorldToScreenPoint (new Vector3 (0, 0, -1));
			verticalVector.Normalize ();
			horizontalVector = Camera.main.WorldToScreenPoint (Vector3.zero) - Camera.main.WorldToScreenPoint (new Vector3 (1, 0, 0));
			horizontalVector.Normalize ();

			dragDistance = Screen.height * (dragThreshold / 100f);

			base.Start ();
		}
			
		void Update () {
			
			if (Time.timeScale > 0 && activate) {
				if (parentGroundCollider && parentGroundCollider.tag == "Goal") {
					GameObject tvntManager = GameObject.Find ("TVNTManager");
					if (TVNTManager.instance) {
						TVNTManager.instance.EndLevel ();
					} else {
						Debug.LogWarning ("You need the TVNTManager object in your scene to trigger the next level function!");
					}
					activate = false;
					return;
				}

				int tempHorizontal = 0;
				int tempVertical = 0;
#if UNITY_EDITOR || UNITY_WEBPLAYER
				tempHorizontal = (int)Input.GetAxisRaw ("Horizontal");
				tempVertical = (int)Input.GetAxisRaw ("Vertical");
				if (flipHorizontalInput) {
					tempHorizontal *= -1;
				}
				if (flipVerticalInput) {
					tempVertical *= -1;
				}
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
				if (Input.touchCount > 0) {
					touchConsumed = false;
					currentTouchDragTime = touchDragTime;
					Touch touch = Input.touches [0];
					if (touch.phase == TouchPhase.Began) {
						touchPositions.Clear ();
						touchPositions.Add (touch.position);
					} else if (touch.phase == TouchPhase.Moved) {
						touchPositions.Add (touch.position);
					} else if (touch.phase == TouchPhase.Ended) {
						Vector2 fp = touchPositions [0];
						Vector2 lp = touchPositions [touchPositions.Count - 1];
						Vector2 touchDiff = lp - fp;
						float touchDiffMag = touchDiff.magnitude;

						float x = Vector3.Dot (touchDiff.normalized, horizontalVector);
						float y = Vector3.Dot (touchDiff.normalized, verticalVector);

						if (touchDiffMag > dragDistance || tapToMove==false) {

							if (Mathf.Abs (x) > Mathf.Abs (y)) {
								tempHorizontal = x > 0 ? -1 : 1;
								tempVertical = 0;
							} else {
								tempVertical = y > 0 ? 1 : -1;
								tempHorizontal = 0;
							}
						} else if (tapToMove) {
							if (flipVerticalInput) {
								tempVertical = 1;
							} else {
								tempVertical = -1;
							}		
							tempHorizontal = 0;
						}

						if (flipHorizontalInput) {
							tempHorizontal *= -1;
						}
						if (flipVerticalInput) {
							tempVertical *= -1;
						}
					}
				} else {
					if (touchConsumed == false) {
						tempHorizontal = horizontal;
						tempVertical = vertical;
						if (currentTouchDragTime > 0) {
							currentTouchDragTime -= Time.deltaTime;
						} else {
							touchConsumed = true;
						}
					}
				}
#else
				tempHorizontal = (int)Input.GetAxisRaw ("Horizontal");
				tempVertical = (int)Input.GetAxisRaw ("Vertical");
				if (flipHorizontalInput) {
				tempHorizontal *= -1;
				}
				if (flipVerticalInput) {
				tempVertical *= -1;
				}
#endif

				if (skipTurn == false) {
					if (movementStyle == MovementStyle.CONTINUOS_JUMP || movementStyle == MovementStyle.CONTINUOS_WALK) {
						if (tempHorizontal != 0) {
							horizontal = tempHorizontal;
							vertical = 0;
						}
						if (tempVertical != 0) {
							vertical = tempVertical;
							horizontal = 0;
						}
					} else {
						horizontal = tempHorizontal;
						vertical = tempVertical;
					}
				} else {
					skipTurn = false;
				}

				if (idle) {
					if (horizontal != 0) {
						vertical = 0;
					}
					if (horizontal == 0 && vertical == 0 && setIdlePosition) {
						transform.position = transform.parent.position + new Vector3 (0, PatternSettings.playerYOffset, 0);
						setIdlePosition = false;
					} else if (horizontal != 0 || vertical != 0) {
						AttemptMove ();
						touchConsumed = true;
						setIdlePosition = true;
					}
				}
			}
		}

		public override void LifeLost (int currentLives) {
			TVNTManager.instance.PlayerLifeLost (currentLives);
			base.LifeLost (currentLives);
		}

		public override void CharacterDead () {
			TVNTManager.instance.PlayerDead ();
			base.CharacterDead ();
		}
	}
}