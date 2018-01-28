using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager> {

    public List<GameObject> basicEnemyPrefabs;
    public List<Spawner> spawners;
    public float waveInterval = 30;

	public float intensityInterval = 10;
	public float waveIntervalDelta = 5;
	public float minWaveInterval = 1.5f;

    private List<Wave> waves = new List<Wave>();

    public void Start()
    {
        for(var i = 0; i < 10; i++)
        {
            waves.Add(MakeWave());
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

    protected Wave MakeWave()
    {
        var wave = new Wave();

        for (int i = 0; i < 5; i++)
        {
            var spawner = GetRandomSpawner();

            var spawn = new BasicSpawn();
            //Instantiate a random enemy to be in the wave spawn
            int ChooseRandomEnemyToSpawn = Random.Range(0, basicEnemyPrefabs.Count);
            GameObject SpawnRandomEnemy = basicEnemyPrefabs[ChooseRandomEnemyToSpawn];
            spawn.enemyPrefab = SpawnRandomEnemy;
            spawn.size = 6;
            spawn.interval = .333f;

            var waveSpawn = new Wave.WaveSpawn
            {
                spawn = spawn,
                spawner = spawner
            };

            wave.waveSpawns.Add(waveSpawn);
        }

        return wave;
    }

    protected Spawner GetRandomSpawner()
    {
        int ChooseRandomSpawner = Random.Range(0, spawners.Count);
        return spawners[ChooseRandomSpawner];
    }
}
