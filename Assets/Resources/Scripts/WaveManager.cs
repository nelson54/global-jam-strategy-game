using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	protected Wave MakeWave(float intensity)
    {
        var wave = new Wave();

		int numSpawns = Math.Min (5, intensity * 2); //this can be changed
        for (int i = 0; i < numSpawns; i++)
        {
            var spawner = GetRandomSpawner();

			Wave.WaveSpawn waveSpawn = null;
			if (i % 10 == 0) {
				var spawn = new BossSpawn ();
				spawn.enemyPrefab = bossPrefab;
				spawn.hp = 200 * intensityInterval;

				waveSpawn = new Wave.WaveSpawn
				{
					spawn = spawn,
					spawner = spawner
				};
			} else {
				var spawn = new BasicSpawn();

				int index = Random.Range(0, basicEnemyPrefabs.Count);
		
				spawn.enemyPrefab = basicEnemyPrefabs[index];
				spawn.size = (int)Mathf.Floor(waveInterval * defaultSpawnSizes[index]);
				spawn.interval = .333f;

				waveSpawn = new Wave.WaveSpawn
				{
					spawn = spawn,
					spawner = spawner
				};
			}
           
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
