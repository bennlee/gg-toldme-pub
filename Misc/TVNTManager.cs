using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class MyVector3Event : UnityEvent<Vector3> {}

[System.Serializable]
public class MyIntEvent : UnityEvent<int> {}

namespace TVNT {
	public class TVNTManager : MonoBehaviour {

		public static TVNTManager instance = null;
		public bool isPersistant = true;

		//level end functions -- called from the end level tile
		public UnityEvent nextLevelEvents;

		//Pot smashing functions -- called when a pot is smashed
		public MyVector3Event potSmashEvents;

		//Coin pickup functions -- called when a coin is picked up
		public MyVector3Event coinPickupEvents;

		//Player life lost functions -- called when the player loses a life
		public MyIntEvent lifeLostEvents;

		//Player death functions -- called when the player dies
		public UnityEvent playerDeadEvents;

        public UnityEvent cameraTreadmillEvents;

		void Awake() {
			if (instance == null) {
				instance = this;
			} else if (instance != this) {
				Destroy (gameObject);
			}
			if (isPersistant) {
				DontDestroyOnLoad (gameObject);
			}
		}

		public void EndLevel() {
			Debug.Log ("Go to next level");
			if (nextLevelEvents != null) {
				nextLevelEvents.Invoke ();
			}
		}

		public void SmashPot(Vector3 potLocation) {
			Debug.Log ("Pot smashed");
			if (potSmashEvents != null) {
				potSmashEvents.Invoke (potLocation);
			}
		}

		public void PickupCoin(Vector3 coinLocation) {
			Debug.Log ("Coin picked up");
			if (coinPickupEvents != null) {
				coinPickupEvents.Invoke (coinLocation);
			}
		}

		public void PlayerLifeLost(int currentLives) {
			Debug.Log ("Player loses a life and now has " + currentLives + " lives");
			if (lifeLostEvents != null) {
				lifeLostEvents.Invoke (currentLives);
			}
		}

		public void PlayerDead() {
			Debug.Log ("Player Dead");
			if (playerDeadEvents != null) {
				playerDeadEvents.Invoke ();
			}
		}

        public void CameraTreadmill()
        {
            Debug.Log("Game treadmill activated");
            if (cameraTreadmillEvents != null)
            {
                cameraTreadmillEvents.Invoke();
            }
        }
	}
}
