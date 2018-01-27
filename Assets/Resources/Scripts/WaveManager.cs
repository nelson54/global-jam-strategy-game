using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager> {

	public GameObject basicEnemyPrefab;
	public List<EnemyPathNode> startNodes;
	public List<Wave> waves;
	public float waveInterval = 15;

	void Start () {
		if (waves == null)
			waves = new List<Wave> ();

		CreateSomeWavesBaby ();
		StartCoroutine (SpawnWaves ());
	}

	//TODO this is a temporary solution
	void CreateSomeWavesBaby() {
		for (int i = 0; i < 10; i++) {
			var wave = new BasicWave ();
			wave.enemyPrefab = basicEnemyPrefab;
			wave.size = 6;
			wave.interval = .333f;

			waves.Add (wave);
		}
	}

	IEnumerator SpawnWaves() {
		for(var i = 0; i < waves.Count; i++) {
			var wave = waves [i];
			var startNode = GetRandomStartNode (); //Wave should define a path name...

			yield return StartCoroutine( wave.Spawn(startNode) );

			if (i < waves.Count - 1) 
				yield return new WaitForSeconds (waveInterval);
		}
	}

	protected EnemyPathNode GetRandomStartNode() {
		int index = Random.Range (0, startNodes.Count - 1);
		return startNodes [index];
	}
}
