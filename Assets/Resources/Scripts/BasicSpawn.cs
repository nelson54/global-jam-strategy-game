using System.Collections;
using System;
using UnityEngine;

public class BasicSpawn : Spawn
{
	public GameObject enemyPrefab;
	public int size;
	public float interval;
    public float hpMultiplyer = 1;
    public float moneyMultiplyer = 1;

	public IEnumerator Run(EnemyPathNode start) {
		for (var i = 0; i < size; i++) {
			var instance = GameObject.Instantiate (enemyPrefab);
			instance.transform.position = start.transform.position;
            instance.GetComponent<Enemy>().Health *= hpMultiplyer;
			var follow = instance.GetComponent<FollowPathEnemy> ();
			follow.nextNode = start;
				
			if (i < size - 1) {
				yield return new WaitForSeconds (interval);
			}
		}
	}
}
