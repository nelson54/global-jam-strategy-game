using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(EnemyPathNode) )]
public class Spawner : MonoBehaviour {

	public List<Spawn> spawns;
	public float spawnInterval = 15;
    public bool SpawnWavesIsRunning;

    private EnemyPathNode startNode;

    void Start () {
        startNode = GetComponent<EnemyPathNode>();

		if (spawns == null)
            spawns = new List<Spawn> ();

        SpawnWavesIsRunning = false;
	}

    public void StartSpawningIfNecessary()
    {
        if(!SpawnWavesIsRunning)
        {
            StartCoroutine(SpawnWaves());
            SpawnWavesIsRunning = true;
        }
    }

	IEnumerator SpawnWaves() {
		for(var i = 0; i < spawns.Count; i++) {
			var spawn = spawns[i];

			yield return StartCoroutine( spawn.Run(startNode) );

			if (i < spawns.Count - 1) 
				yield return new WaitForSeconds (spawnInterval);
		}
        SpawnWavesIsRunning = false;
	}
}
