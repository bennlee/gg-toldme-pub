using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {
	public GameObject nextLevelTargetObject;
	public string nextLevelFunctionName;

	public void NextLevel() {
		if (nextLevelTargetObject) {
			nextLevelTargetObject.SendMessage (nextLevelFunctionName);
		}
	}
}
