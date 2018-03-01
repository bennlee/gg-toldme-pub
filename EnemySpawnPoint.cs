using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TVNT {
	public class EnemySpawnPoint : SpawnPoint {

		private Transform spawnPointTop = null;
		private BoxCollider rodsBoxCollider = null;
		public float moveUpAmount = 4;

		private Transform spawnPointLights = null;
		public Material spawnLight;
		public Material clearLight;

		private Transform spawnLift = null;
		private GroundCollider spawnLiftGroundCollider = null;

		public Transform[] spawnEnemyTypes;
		public int numberOfEnemiesToSpawn = 2;
		public float timeBetweenSpawns = 2f;

		private float currentTimeToSpawn;
		private bool spawning = false;
		private bool spawnTopUp = false;
		private Transform activeCharacter;

		protected override void Start () {
			base.Start ();
			currentTimeToSpawn = timeBetweenSpawns;
			activeCharacter = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		void Update() {
			if (activeSpawnedEnemies.Count < numberOfEnemiesToSpawn && spawning == false && activeCharacter) {
				if (currentTimeToSpawn <= 0) {
					SpawnEnemy ();
				} else {
					if (currentTimeToSpawn < timeBetweenSpawns * 0.5f) {
						spawnPointLights.GetComponent<MeshRenderer> ().material = spawnLight;
					}
					currentTimeToSpawn -= Time.deltaTime;
				}
			}

			if (spawnTopUp && spawnLiftGroundCollider.occupied == false) {
				spawnTopUp = false;
				//rodsBoxCollider.enabled = true;
				ResetSpawner ();
			}
		}

		private void SpawnEnemy() {
			spawning = true;
			Transform currentEnemy = (Transform)Instantiate (spawnEnemyTypes [Random.Range (0, spawnEnemyTypes.Length)]);
			currentEnemy.position = spawnLift.position+new Vector3(0,2f,0);
			currentEnemy.GetComponent<TVNTAIController> ().mySpawnPoint = this;
			//currentEnemy.parent = spawnLift;
			activeSpawnedEnemies.Add (currentEnemy);
			StartCoroutine (Spawn ());
		}

		private IEnumerator Spawn() {
			Vector3 start = spawnPointTop.localPosition;
			Vector3 end = start + new Vector3 (0, moveUpAmount, 0);
			float openTime = 1f;
			for (float t=0; t < 1; t += Time.deltaTime/openTime) {
				spawnPointTop.localPosition = Vector3.Lerp (spawnPointTop.localPosition, end, t);
				yield return null;
			}
			spawnPointTop.localPosition = end;
			spawnTopUp = true;
			//rodsBoxCollider.enabled = false;
			activeSpawnedEnemies [activeSpawnedEnemies.Count-1].GetComponent<TVNTAIController> ().activate = true;
		}

		private void ResetSpawner() {
			StartCoroutine (ResetSpawn ());
		}

		private IEnumerator ResetSpawn() {
			Vector3 start = spawnPointTop.localPosition;
			Vector3 end = start - new Vector3 (0, moveUpAmount, 0);
			float openTime = 0.3f;
			for (float t=0; t < 1; t += Time.deltaTime/openTime) {
				spawnPointTop.localPosition = Vector3.Lerp (spawnPointTop.localPosition, end, t);
				yield return null;
			}
			spawnPointTop.localPosition = end;
			spawning = false;
			spawnPointLights.GetComponent<MeshRenderer> ().material = clearLight;
			currentTimeToSpawn = timeBetweenSpawns;
		}
		
		public override void Initialize() {
			base.Initialize ();
			AssignParts ();
		}

		private void AssignParts() {
			Transform prefab = transform.GetChild (0);
			for (int i = 0; i < prefab.childCount; i++) {
				if (prefab.GetChild (i).name == "ES_Top") {
					spawnPointTop = prefab.GetChild (i);
					spawnPointTop.localPosition = new Vector3 (0, -4, 0);
					break;
				}
			}
			for (int i = 0; i < spawnPointTop.childCount; i++) {
				if (spawnPointTop.GetChild (i).name == "ES_Lights") {
					spawnPointLights = spawnPointTop.GetChild (i);
				}
				if (spawnPointTop.GetChild (i).name == "ES_Lift") {
					spawnLift = spawnPointTop.GetChild (i);
					spawnLiftGroundCollider = spawnLift.GetComponent<GroundCollider> ();
				} 
				if (spawnPointTop.GetChild (i).name == "ES_Rods") {
					rodsBoxCollider = spawnPointTop.GetChild (i).GetComponent<BoxCollider> ();
				} 
			}
		}
	}
}
