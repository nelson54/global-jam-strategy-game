using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Wave {

    public struct WaveSpawn
    {
        public Spawn spawn;
        public Spawner spawner;
    }

    public List<WaveSpawn> waveSpawns = new List<WaveSpawn>();

    public void Run()
    {
        foreach(var waveSpawn in waveSpawns)
        {
            var spawner = waveSpawn.spawner;
            var spawn = waveSpawn.spawn;

            spawner.spawns.Add(spawn);
            spawner.StartSpawningIfNecessary();
        }
    }
}