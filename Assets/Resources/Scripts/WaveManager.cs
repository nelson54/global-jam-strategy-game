using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : Singleton<WaveManager> {

    public List<GameObject> basicEnemyPrefabs;
	public GameObject bossPrefab;
	public List<int> defaultSpawnSizes;
    public List<Spawner> spawners;
    public float waveInterval = 30;

	public float intensityInterval = 10;
	public float waveIntervalDelta = 5;
	public float minWaveInterval = 1.5f;

    private List<Wave> waves = new List<Wave>();

    public void Start()
    {
		float intensity = 1;
		float intensityDelta = 1 / 5f;

        for(int i = 0; i < 60; i++)
        {
			var wave = new Wave();

			if ((i + 1) % 5 == 0) {
				var spawner = GetRandomSpawner ();

				var spawn = new BossSpawn ();
				spawn.enemyPrefab = bossPrefab;
				spawn.hp = 200 * 2 * intensity;

				var waveSpawn = new Wave.WaveSpawn {
					spawn = spawn,
					spawner = spawner
				};

				wave.waveSpawns.Add(waveSpawn);
			} 
			else {
				int numSpawns = (int)Math.Min (5, intensity * 2); //this can be changed
				for (int j = 0; j < numSpawns; j++) {
					var spawner = GetRandomSpawner ();

					var spawn = new BasicSpawn ();

					int index = UnityEngine.Random.Range (0, basicEnemyPrefabs.Count);

					spawn.enemyPrefab = basicEnemyPrefabs [index];
					spawn.size = (int)Mathf.Floor (intensity * defaultSpawnSizes [index]);
					spawn.hpMultiplyer = Mathf.Min(1, intensity / 2);
					spawn.interval = .333f;

					var waveSpawn = new Wave.WaveSpawn {
						spawn = spawn,
						spawner = spawner
					};

					wave.waveSpawns.Add(waveSpawn);
				}
			}

            waves.Add(wave);
			intensity += intensityDelta;
        }

		StartCoroutine (IncreaseIntensity ());
        StartCoroutine(SpawnWaves());
    }

	protected IEnumerator IncreaseIntensity() {
		while (true) {
			yield return new WaitForSeconds (intensityInterval);
			waveInterval = Mathf.Max (minWaveInterval, waveInterval - waveIntervalDelta);
		}
	}

    protected IEnumerator SpawnWaves()
    {
		yield return new WaitForSeconds(waveInterval);

        //TODO add a new wave to the back of the list after we run the first one... goes forever
        for(var i = 0; i < waves.Count; i++)
        {
            var wave = waves[i];
            wave.Run();

            yield return new WaitForSeconds(waveInterval);
        }
    }

    protected Spawner GetRandomSpawner()
    {
		int ChooseRandomSpawner = UnityEngine.Random.Range(0, spawners.Count);
        return spawners[ChooseRandomSpawner];
    }
}
